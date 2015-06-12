using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.EShop.SightShop
{
    /// <summary>
    /// 景区网店后台首页
    /// </summary>
    /// 罗丽娥   2010-12-09
    public partial class IframeSightShop : EyouSoft.Common.Control.BasePage
    {
        protected string[] arrPicpath = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/eshop/sightshop/sightshopdefault.aspx", "请登录");
            }

            if (!Page.IsPostBack)
            {
                InitCompanyInfo();
                InitTradeGuid();
                InitAdvInfo();
                InitGoogleMap();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"];
                if (flag.Equals("get", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(GetData());
                    Response.End();
                }
            }
        }

        #region 初始化公司信息
        private void InitCompanyInfo()
        {
            // 公司基本信息
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (model != null)
            {
                if (model.AttachInfo != null && model.AttachInfo.CompanyShopBanner != null && !String.IsNullOrEmpty(model.AttachInfo.CompanyShopBanner.ImagePath))
                {
                    this.imgBanner.Src = Domain.FileSystem + model.AttachInfo.CompanyShopBanner.ImagePath;
                }
                this.ltrContactName.Text = model.ContactInfo.ContactName;
                this.ltrTel.Text = model.ContactInfo.Tel;
                this.ltrFax.Text = model.ContactInfo.Fax;
                this.ltrWebSite.Text = model.WebSite;
                this.ltrAdress.Text = model.CompanyAddress;
                this.ltrMQ.Text = Utils.GetMQ(model.ContactInfo.MQ);
                this.ltrQQ.Text = Utils.GetQQ(model.ContactInfo.QQ);
            }
            model = null;

            EyouSoft.Model.CompanyStructure.CompanyAttachInfo attachmodel = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (attachmodel != null && attachmodel.CompanyLogo != null && !String.IsNullOrEmpty(attachmodel.CompanyLogo.ImagePath))
            {
                this.imgLogo.Src = Domain.FileSystem + attachmodel.CompanyLogo.ImagePath;
            }
            else
            {
                this.imgLogo.Src = Domain.FileSystem + "/images/shopnoimg/nologo.gif";
            }

            EyouSoft.Model.ShopStructure.HighShopCompanyInfo CompanyInfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (CompanyInfo != null && !String.IsNullOrEmpty(CompanyInfo.CompanyInfo))
            {
                // 关于我们
                this.ltrAboutUsTitle.Text = SiteUserInfo.CompanyName;
                this.ltrAboutUs.Text = Utils.GetText(Utils.InputText(CompanyInfo.CompanyInfo), 150, true) + "<span style=\"background: #FFF6C7; border: 1px solid #FF8624;color: #339933; float: center; text-align:left;height:15px; width:165px; cursor:pointer;\" onclick=\"return boxymethods('/eshop/setaboutus.aspx','景区介绍',730,470);\"><img src=\"" + ImageManage.GetImagerServerUrl(1) + "/Images/seniorshop/gan.gif\" /><strong>【添加景区介绍】</strong></span>";

                this.ltrCopyright.Text = CompanyInfo.ShopCopyRight;   // 版权
            }
            CompanyInfo = null;

            // 广告图片
            EyouSoft.Model.CompanyStructure.SupplierInfo smodel = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (smodel != null && smodel.ProductInfo != null && smodel.ProductInfo.Count > 0)
            {
                this.imgAdv.Src = Domain.FileSystem + smodel.ProductInfo[0].ImagePath.ToString();
            }
            else
            {
                this.imgAdv.Src = ImageManage.GetImagerServerUrl(1) + "/images/shopnoimg/norotapic.gif";
            }
            smodel = null;

            // 友情链接
            IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> FriendLinkList = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(SiteUserInfo.CompanyID);
            string tmp = "<b><font class=\"C_G\">友情链接：</font></b>";
            if (FriendLinkList != null && FriendLinkList.Count > 0)
            {
                foreach (EyouSoft.Model.ShopStructure.HighShopFriendLink linkModel in FriendLinkList)
                {
                    tmp += linkModel.LinkName + "|";
                }
                if (tmp.EndsWith("|"))
                {
                    tmp = tmp.Substring(0, tmp.Length - 1);
                }
            }
            this.ltrFriendLink.Text = tmp;
            FriendLinkList = null;
        }
        #endregion

        #region 初始化资讯信息
        private void InitTradeGuid()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = null;
            list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(5, SiteUserInfo.CompanyID, (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区动态, string.Empty);
            if (list != null && list.Count > 0)
            {
                this.rptTripGuideDT.DataSource = list;
                this.rptTripGuideDT.DataBind();
            }
            list = null;

            list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(5, SiteUserInfo.CompanyID, (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区线路, string.Empty);
            if (list != null && list.Count > 0)
            {
                this.rptTripGuideXL.DataSource = list;
                this.rptTripGuideXL.DataBind();
            }
            list = null;

            var qmodel = new EyouSoft.Model.ScenicStructure.MScenicImgSearch
            {
                ImgType =
                    new EyouSoft.Model.ScenicStructure.ScenicImgType?[]
                                         {
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.景区形象,
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.其他
                                         }
            };
            var listImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(8, SiteUserInfo.CompanyID,
                                                                                         qmodel);
            if (listImg != null && listImg.Count > 0)
            {
                this.rptTripGuideMT.DataSource = listImg;
                this.rptTripGuideMT.DataBind();
            }

            list = null;
        }
        #endregion

        #region 轮换广告
        private void InitAdvInfo()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> list = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5, SiteUserInfo.CompanyID);
            if (list != null && list.Count > 0)
            {
                this.focpic.Src = Utils.GetLineShopImgPath(list[0].ImagePath, 3);
                StringBuilder str = new StringBuilder();
                str.AppendFormat("<div id=\"Slide_Thumb\" style=\"float:left;\"><ul>");
                foreach (EyouSoft.Model.ShopStructure.HighShopAdv model in list)
                {
                    str.AppendFormat("<li style=\"float:left;\"><img src=\"{0}\"/></li>", Utils.GetLineShopImgPath(model.ImagePath, 5));
                }
                str.AppendFormat("<div class=\"clearboth\"></div></ul></div>");
                this.ltrAdvInfo.Text = str.ToString();
            }
            list = null;
        }
        #endregion

        #region 初始化地图
        /// <summary>
        /// 初始化Google Map
        /// </summary>
        private void InitGoogleMap()
        {
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo model = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (model != null)
            {
                this.GoogleMapControl1.Longitude = model.PositionInfo.Longitude;
                this.GoogleMapControl1.Latitude = model.PositionInfo.Latitude;
                this.GoogleMapControl1.ShowMapWidth = 244;
                this.GoogleMapControl1.ShowMapHeight = 226;
                this.GoogleMapControl1.IsBorder = false;
                this.GoogleMapControl1.ShowTitleText = SiteUserInfo.CompanyName;
                GoogleMapControl1.GoogleMapKey = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
            }
            model = null;
        }
        #endregion

        #region 获取景区资讯信息
        private string GetData()
        {
            string returnVal = string.Empty;
            int typeid = Utils.GetInt(Utils.GetQueryStringValue("typeid"), 0);

            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = null;

            if ((EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)typeid == EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿)
            {
                list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(4, SiteUserInfo.CompanyID, (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿, string.Empty);
                StringBuilder strHotel = new StringBuilder();
                strHotel.Append("<ol>");
                if (list != null && list.Count > 0)
                {
                    foreach (EyouSoft.Model.ShopStructure.HighShopTripGuide model in list)
                    {
                        strHotel.AppendFormat("<li style=\"float:left;padding:5px 10px 5px 10px;\"><img src=\"{0}\" style=\"width:135px; height:91px;\" /><br>{1}</li>", Domain.FileSystem + model.ImagePath, Utils.GetText(model.Title, 10, true));
                    }
                }
                else
                {
                    strHotel.Append("<li style=\"text-align:left;\">暂无相关内容</li>");
                }
                strHotel.Append("<div class=\"clearboth\"></div>");
                strHotel.Append("</ol>");
                strHotel.Append("<div class=\"clearboth\"></div>");
                returnVal = strHotel.ToString();
            }
            else
            {
                list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(10, SiteUserInfo.CompanyID, typeid, string.Empty);
                if (list != null && list.Count > 0)
                {
                    int i = 0;
                    StringBuilder strL = new StringBuilder();
                    StringBuilder strR = new StringBuilder();
                    strL.Append("<ul class=\"sidebar02_2Content_L\">");
                    strR.Append("<ul class=\"sidebar02_2Content_R\">");

                    foreach (EyouSoft.Model.ShopStructure.HighShopTripGuide model in list)
                    {
                        if (i < 5)
                        {
                            strL.AppendFormat("<li><p>{0}</p> <em>{1}</em></li>", Utils.GetText(model.Title, 15, true), model.IssueTime.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            strR.AppendFormat("<li><p>{0}</p> <em>{1}</em></li>", Utils.GetText(model.Title, 15, true), model.IssueTime.ToString("yyyy-MM-dd"));
                        }
                        i++;
                    }
                    strL.Append("</ul>");
                    strR.Append("</ul>");
                    returnVal = strL.ToString() + strR.ToString() + "<div class=\"clearboth\"></div>";
                }
                else
                {
                    returnVal = "<ul class=\"clearboth\"><li style=\"text-align:left;\" class=\"c999\">暂无相关内容</li></ul>";
                }
            }
            list = null;
            return returnVal;
        }
        #endregion
    }
}
