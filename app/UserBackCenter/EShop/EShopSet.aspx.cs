using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
using System.Collections.Generic;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店后台设置
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// 
    public partial class EShopSet : BasePage
    {
        private string CompanyID = string.Empty;  //公司Id
        protected string strTripGuide1 = string.Empty, strTripGuide2 = string.Empty;   //目的地指南（风土人情/温馨提醒）
        protected string CompanyBrand = string.Empty;    //公司品牌
        protected string BannerImage = string.Empty;       //高级网店头部图片
        protected string CompanyCardPath = string.Empty;  //企业卡片    
        protected string Mqpath = string.Empty;           //mq
        protected string initFlashJs = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("EShopPage.aspx", "登录");
            }
            CompanyID = this.SiteUserInfo.CompanyID;

            if (!Page.IsPostBack)
            {
                InitCompanyInfo();   // 公司信息                
                GetShopAdvInfo();    // 轮换广告
                InitShopNewsInfo();  // 最新动态
                InitTripGuidInfo();  // 目的地指南
                InitResourceInfo();  // 旅游资源推荐
                InitSysAreaList(); //线路列表
                InitLinkFriendList();   //友情链接
                SetShopTemplate();   //设置模板
            }
        }
        #region 模板设置
        private void SetShopTemplate()
        {
            //  根据用户选择网店模板，初始化,默认选择style1.css
            int Template = 1;
            int queryStringTemplateId=Utils.GetInt(Request.QueryString["style"]);

            if (queryStringTemplateId > 0)
            {
                Template = queryStringTemplateId;
            }
            else
            {
                EyouSoft.Model.ShopStructure.HighShopCompanyInfo info = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID);
                if (info != null)
                {
                    Template = info.TemplateId;
                }
            }

            if (Template < 1 || Template > 4)
            {
                Template = 1;
            }

            string styleName = string.Format("style{0}", Template);
            this.AddStylesheetInclude(CssManage.GetCssFilePath(styleName));
        }
        #endregion

        #region 轮换广告
        private void GetShopAdvInfo()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> advList = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5, CompanyID);
            int i = 1;
            string format = ";imag[{0}]=\"{1}\";link[{2}]=\"{3}\";text[{4}]=\"{5}\";";
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            string imagePath = null;
            foreach (EyouSoft.Model.ShopStructure.HighShopAdv adv in advList)
            {
                imagePath = string.IsNullOrEmpty(adv.ImagePath) == true ? Utils.NoRotaImage : Domain.FileSystem + adv.ImagePath;
                strb.AppendFormat(format, i, imagePath, i, adv.LinkAddress != string.Empty ? adv.LinkAddress : "#", i, "");
            }
            initFlashJs = strb.ToString();
            advList = null;

        }
        #endregion

        #region 最新旅游动态
        private void InitShopNewsInfo()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopNews bll = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance();
            IList<EyouSoft.Model.ShopStructure.HighShopNews> list = bll.GetTopNumberList(6, CompanyID);
            if (list != null && list.Count > 0)
            {
                this.creptNews.DataSource = list;
                this.creptNews.DataBind();
            }
            else
                MessageBox.ResponseScript(this.Page, "$(\"#div_NewsNoMessage\").show();");
        }
        #endregion

        //TypeID 1.风土人情介绍 2.温馨提醒 3.综合介绍
        #region 目的地指南
        private void InitTripGuidInfo()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopTripGuide bll = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance();

            string tmpText = "<tr><td><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"maintop5\" style=\"margin-bottom: 5px;\"><tr><td><a href=\"javascript:void(0)\"><img src=\"{0}\" width=\"97\" height=\"73\" border=\"0\" /></a></td><td style=\"padding-left: 3px;\" align=\"left\"><a href=\"javascript:void(0)\" class=\"huizi\">{1}</a></td></tr></table></td></tr>";

            #region 风土人情
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> TripGuideList = bll.GetWebList(3, CompanyID, 1, "");
            if (TripGuideList != null && TripGuideList.Count > 0)
            {
                for (int i = 0; i < TripGuideList.Count; i++)
                {
                    if (i == 0)
                    {
                        strTripGuide1 += string.Format(tmpText, Utils.GetLineShopImgPath(TripGuideList[i].ImagePath, 5), Utils.GetText(Utils.InputText(TripGuideList[i].ContentText), 50));
                    }
                    else
                    {
                        strTripGuide1 += string.Format("<tr><td>•&nbsp;&nbsp;<a href=\"javascript:void(0)\" class=\"huizi\">{0}</a></td></tr>", Utils.GetText(TripGuideList[i].Title, 25));
                    }
                }
            }
            else
            {
                strTripGuide1 = "<tr><td><div style=\"text-align:center;margin-top:50px; margin-bottom:50px;\">暂无风土人情信息！</div></td></tr>";
            }
            TripGuideList = null;
            #endregion

            #region 温馨提醒
            TripGuideList = bll.GetWebList(3, CompanyID, 2, "");
            if (TripGuideList != null && TripGuideList.Count > 0)
            {
                for (int i = 0; i < TripGuideList.Count; i++)
                {
                    if (i == 0)
                    {
                        strTripGuide2 += string.Format(tmpText, Utils.GetLineShopImgPath(TripGuideList[i].ImagePath, 5), Utils.GetText(Utils.InputText(TripGuideList[i].ContentText), 50));
                    }
                    else
                    {
                        strTripGuide2 += string.Format("<tr><td>•&nbsp;&nbsp;<a href=\"javascript:void(0)\" class=\"huizi\">{0}</a></td></tr>", Utils.GetText(TripGuideList[i].Title, 25));
                    }
                }
            }
            else
                strTripGuide2 = "<tr><td><div style=\"text-align:center;  margin-top:50px; margin-bottom:50px;\">暂无温馨提醒信息！</div></td></tr>";

            TripGuideList = null;
            #endregion

            #region 综合介绍
            TripGuideList = bll.GetWebList(3, CompanyID, 3, "");
            if (TripGuideList != null && TripGuideList.Count > 0)
            {
                this.crptTripGuide3.DataSource = TripGuideList;
                this.crptTripGuide3.DataBind();
            }
            else
                div_GuideNoMessage.Visible = true;
            #endregion

            TripGuideList = null;
            bll = null;
        }
        #endregion

        #region 旅游资源推荐
        private void InitResourceInfo()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopResource> list = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().GetIndexList(5, CompanyID);
            if (list != null && list.Count > 0)
            {
                this.crptReso.DataSource = list;
                this.crptReso.DataBind();
            }
            else
                MessageBox.ResponseScript(this.Page, "$(\"#tr_ResoNoMessage\").show();");

            list = null;
        }
        #endregion



        #region 获取公司信息和版权
        private void InitCompanyInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyID); //公司详细信息
            if (compDetail != null)
            {
                CompanyBrand = compDetail.CompanyBrand;             
                ltr_Address.Text = "地址：" + compDetail.CompanyAddress;
                ltr_LinkPerson.Text = "联系人：" + compDetail.ContactInfo.ContactName;
                ltr_Fax.Text = "传真：" + compDetail.ContactInfo.Fax;
                ltr_Mobile.Text = "手机：" + compDetail.ContactInfo.Mobile;
                ltr_Phone.Text = "电话：" + compDetail.ContactInfo.Tel;

                if (!string.IsNullOrEmpty(compDetail.ContactInfo.MQ))
                {
                    Mqpath = string.Format("<img src=\"{0}/Images/seniorshop/mqonline.gif\" width=\"70\" height=\"18\" />", ImageManage.GetImagerServerUrl(1));
                }//compDetail.AttachInfo.CompanyShopBanner.ImagePath  图片路径
                BannerImage = EyouSoft.Common.Utils.GetEShopImgOrFalash(compDetail.AttachInfo.CompanyShopBanner.ImagePath, "javascript:void(0)");  //高级网店头部图片
                CompanyCardPath = Utils.GetLineShopImgPath(compDetail.AttachInfo.CompanyCard.ImagePath, 1); //卡片
            }
            compDetail = null;
            //版权显示
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo info = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(CompanyID);
            if (info != null)
            {
                ltrCopyRight.Text = info.ShopCopyRight;
            }
            info = null;
        }
        #endregion

        #region 线路信息
        private void InitSysAreaList()
        {
            IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetShopList(7, 0, SiteUserInfo.CompanyID);
            if (RouteList != null && RouteList.Count > 0)
            {
                this.crptLinelist.DataSource = RouteList;
                this.crptLinelist.DataBind();
            }
            else
            {
                MessageBox.ResponseScript(this.Page, "$(\"#div_NoDataMessage\").show();");
            }
        }

        protected string TeamPlanDesAndPrices(string routeID, string RouteSource, string teamPlabDes, string Prices)
        {
            string TeamPlanDesAndPricesHtml = string.Empty;
            if (RouteSource == "地接社添加")
            {
                TeamPlanDesAndPricesHtml = "<td align=\"center\">暂无</td><td align=\"center\" class=\"td_noDefault\">电询</td>";
            }
            else
            {
                IList<EyouSoft.Model.NewTourStructure.MPowderList> PowerList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(routeID);
                if (PowerList != null && PowerList.Count > 0)
                {
                    TeamPlanDesAndPricesHtml = "<td align=\"center\">" + teamPlabDes + "</td><td align=\"center\" class=\"td_noDefault\">￥" + Prices + "起</td>";
                }
                else
                {
                    TeamPlanDesAndPricesHtml = "<td align=\"center\">暂无</td><td align=\"center\" class=\"td_noDefault\">电询</td>";
                }
            }
            return TeamPlanDesAndPricesHtml;
        }


        /// <summary>
        /// 获的团队推广状态
        /// </summary>
        /// <param name="TourSpreadState">团队推广状态</param>
        /// <returns></returns>
        protected string ShowTourStateInfo(string TourSpreadState)
        {
            string strTemp = "";
            switch (TourSpreadState)
            {
                //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩
                case "1": strTemp = ""; break;
                case "2": strTemp = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_30.gif\" width=\"25\" height=\"15\" alt=\"推荐\"/>"; break;
                case "3": strTemp = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_35.gif\" width=\"25\" height=\"15\" alt=\"特价\"/>"; break;
                case "4": strTemp = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_39.gif\" width=\"25\" height=\"15\" alt=\"豪华\"/>"; break;
                case "5": strTemp = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_42.gif\" width=\"25\" height=\"15\" alt=\"热门\"/>"; break;
                case "6": strTemp = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_44.gif\" width=\"25\" height=\"15\" alt=\"新品\"/>"; break;
                case "7": strTemp = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_48.gif\" width=\"25\" height=\"15\" alt=\"经典\"/>"; break;
                case "8": strTemp = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_46.gif\" width=\"25\" height=\"15\" alt=\"纯玩\"/>"; break;
                default: break;
            }             
            return strTemp;
        }
        #endregion

        /// <summary>
        /// 推广说明/团队状态
        /// </summary>
        /// <param name="TourId">团队ID</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="TourState">团队状态</param>
        /// <param name="TourSpreadStateName">推广名称</param>
        /// <returns></returns>
        protected string GetRouteName(string TourId, string RouteName, string TourState, string TourSpreadStateName)
        {
            StringBuilder strRouteInfo = new StringBuilder();
            string strTourMarkerNote = TourSpreadStateName;
            //团队推广说明
            if (TourState == "手动客满" || TourState == "自动客满")
            {
                strTourMarkerNote = "<span class=\"keman\">客满</span>";
            }
            if (TourState == "手动停收" || TourState == "自动停收")
            {
                strTourMarkerNote = "<span class=\"tings\">停收</span>";
            }
            strRouteInfo.AppendFormat("{0}<a href=\"{2}\" target=\"_blank\" >{1}</a>", strTourMarkerNote, RouteName, EyouSoft.Common.Utils.GetTeamInformationPagePath(TourId));
            return strRouteInfo.ToString();
        }


        #region 友情链接
        private void InitLinkFriendList()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> list = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(CompanyID);
            if (list != null)
            {
                rptLinkFriend.DataSource = list;
                rptLinkFriend.DataBind();
            }
            list = null;
        }
        #endregion

        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}/{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
        }

        //去除html标签
        protected string StripHT(string strHtml)
        {
            if (strHtml != null)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("<.+?>|&nbsp;", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                string strOutput = regex.Replace(strHtml, "");
                strHtml = strOutput;
            }
            return strHtml;
        }
    }
}