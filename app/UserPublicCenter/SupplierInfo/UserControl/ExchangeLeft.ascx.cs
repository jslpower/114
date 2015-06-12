using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace UserPublicCenter.SupplierInfo.UserControl
{
    /// <summary>
    /// 供求信息详细和列表左边控件
    /// </summary>
    /// 周文超 2010-08-02
    public partial class ExchangeLeft : EyouSoft.Common.Control.BaseUserControl
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private EyouSoft.SSOComponent.Entity.UserInfo SiteUserInfo = new EyouSoft.SSOComponent.Entity.UserInfo();

        /// <summary>
        /// 当前销售城市ID
        /// </summary>
        public int CurrCityId
        {
            get;
            set;
        }

        /// <summary>
        /// 静态图片路径
        /// </summary>
        public string ImageServerPath
        {
            get;
            set;
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
                InitSearchControl();
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitData()
        {
            #region 图片新闻

            CommonTopicControl1.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.资讯;
            CommonTopicControl1.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;

            #endregion

            #region 最多查看资讯绑定

            rptMostArticle.DataSource = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetTopListByReadCount(8, null, null);
            rptMostArticle.DataBind();

            #endregion

            IList<EyouSoft.Model.AdvStructure.AdvInfo> List = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CurrCityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道供求信息文章页图片广告1);
            if (List != null && List.Count > 0)
            {
                ltrAdv.Text = string.Format("<a target=\"_blank\" href=\"{0}\" title=\"{1}\"><img alt=\"{2}\" src=\"{3}\" width=\"250\" height=\"170\" /></a>", List[0].RedirectURL, List[0].Title, List[0].Title, Domain.FileSystem + List[0].ImgPath);
                List.Clear();
            }
            List = null;

            if (IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.IsUserLogin(out SiteUserInfo);
                panIsLogin.Visible = true;
                ltrContactName.ToolTip = SiteUserInfo.ContactInfo.ContactName;
                ltrContactName.Text = Utils.GetText2(SiteUserInfo.ContactInfo.ContactName, 5,false);
                ltrCompanyname.ToolTip = SiteUserInfo.CompanyName;
                ltrCompanyname.Text = Utils.GetText2(SiteUserInfo.CompanyName, 10, false);
                EyouSoft.Model.CompanyStructure.CompanyAttachInfo model = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (model != null)
                {
                    string CompanyLogo = ImageManage.GetImagerServerUrl(1) + "/Images/UserPublicCenter/defaultlogo.jpg";
                    if (model.CompanyLogo.ImagePath.Length > 0)
                    {
                        CompanyLogo = Domain.FileSystem + model.CompanyLogo.ImagePath;
                    }
                    ltrLog.Text = string.Format("<img src=\"{0}\" width=\"90\" height=\"56\" />", CompanyLogo);
                }
                model = null;
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (CompanyModel != null)
                {
                    aSupplyInfo1.HRef = Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx";
                    aSupplyInfo2.HRef = Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx";
                    aSupplyInfo3.HRef = Domain.UserBackCenter + "/supplyinformation/hassupplyfavorites.aspx";
                    //if (CompanyModel.BusinessProperties != EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社)
                    //{
                    //    EyouSoft.Model.CompanyStructure.CompanyService ServiceModel = CompanyModel.StateMore.CompanyService; ;
                    //    bool IsOpenHighShop = ServiceModel.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
                    //    if (!IsOpenHighShop)
                    //    {
                    //        aSupplyInfo1.HRef = "javascript:;";
                    //        aSupplyInfo1.Attributes.Add("onclick", "alert('您还没有开通高级网店，无法使用此功能！');return false;");
                    //        aSupplyInfo2.HRef = "javascript:;";
                    //        aSupplyInfo2.Attributes.Add("onclick", "alert('您还没有开通高级网店，无法使用此功能！');return false;");
                    //        aSupplyInfo3.HRef = "javascript:;";
                    //        aSupplyInfo3.Attributes.Add("onclick", "alert('您还没有开通高级网店，无法使用此功能！');return false;");
                    //    }
                    //    ServiceModel = null;
                    //}
                    EyouSoft.Model.CompanyStructure.CompanyType? cType=Utils.GetCompanyType(SiteUserInfo.CompanyID);
                    if (cType.HasValue)
                    {
                        aShop.HRef = Utils.GetCompanyDomain(SiteUserInfo.CompanyID, cType.Value);
                    }
                    else
                    {
                        aShop.HRef = "javascript:void(0);";
                        aShop.Target = "";
                    }
                }
                CompanyModel = null;
            }
            else
            {
                panLogin.Visible = true;
                panUrl.Visible = true;
            }
        }

        #region 生成信息检索块

        /// <summary>
        /// 生成信息检索块
        /// </summary>
        private void InitSearchControl()
        {
            #region 初始化时间块

            StringBuilder StrControl = new StringBuilder();
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.SearchDateType)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.SearchDateType), str);
                StrControl.AppendFormat("<a name=\"atime\" href=\"javascript:;\" style=\"{3}padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", strValue, 0, str, strValue == 0 ? "background:#CCCCCC;" : "");
            }

            ltrTime.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;
            
            #endregion

            #region 初始化标签块

            StrControl = new StringBuilder();
            StrControl.AppendFormat("<a name=\"atag\" href=\"javascript:;\" style=\"background:#CCCCCC;padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", "-1", 1, "全部");
            int nCount = 2;
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), str);
                StrControl.AppendFormat("<a name=\"atag\" href=\"javascript:;\" style=\"padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", strValue, 0, str);
                if (nCount % 3 == 0)
                    StrControl.Append("<br />");

                nCount++;
            }
            ltrTag.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;

            #endregion

            #region 初始化类别块
            StrControl = new StringBuilder();
            StrControl.AppendFormat("<a name=\"atype\" href=\"javascript:;\" style=\"background:#CCCCCC;padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", "-1", 1, "全部");
            nCount = 2;
            foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeType)))
            {
                int strValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), str);
                StrControl.AppendFormat("<a name=\"atype\" href=\"javascript:;\" style=\"padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", strValue, 0, str);
                if (nCount % 3 == 0)
                    StrControl.Append("<br />");

                nCount++;
            }
            ltrType.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;

            #endregion

            #region 初始化省份块

            StrControl = new StringBuilder();
            StrControl.AppendFormat("<a name=\"apro\" href=\"javascript:;\" style=\"background:#CCCCCC;padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", 0, 1, "全部");
            nCount = 2;
            foreach (EyouSoft.Model.SystemStructure.SysProvince model in EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList())
            {
                StrControl.AppendFormat("<a name=\"apro\" href=\"javascript:;\" style=\"padding:3px;\" value=\"{0}\" state=\"{1}\"><nobr>{2}</nobr></a>|", model.ProvinceId, 0, model.ProvinceName);
                if (nCount % 5 == 0)
                    StrControl.Append("<br />");

                nCount++;
            }
            ltrProvince.Text = StrControl.ToString().TrimEnd('|');
            StrControl = null;

            #endregion
        }
        #endregion
    }
}