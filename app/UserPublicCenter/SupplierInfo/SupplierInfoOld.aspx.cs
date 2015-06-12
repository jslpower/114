using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 供求信息
    /// </summary>
    /// 周文超 2010-07-30
    public partial class SupplierInfo : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 登录页面url
        /// </summary>
        private string strLoginUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsLogin)
            {
                panAddExchange.Visible = true;
            }
            #region 动态添加Title以及keywords，description
            Page.Title = string.Format(PageTitle.Supplier_Title, CityModel.CityName);
            AddMetaTag("description", string.Format(PageTitle.Supplier_Des, CityModel.CityName));
            AddMetaTag("keywords",PageTitle.Supplier_Keywords);
            #endregion
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 1;
            strLoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Domain.UserPublicCenter + Request.RawUrl, string.Empty);
            //ibtnSaveExchange.ImageUrl = ;

            if (!IsPostBack)
            {
                InitPageData();
                InitUserInfo();
            }
        }

        #region 初始化函数

        /// <summary>
        /// 计算促销广告的文字的背景颜色
        /// </summary>
        /// <param name="ItemIndex">索引值</param>
        /// <returns></returns>
        protected string GetHtmlAFontColor(int ItemIndex)
        {
            if (ItemIndex == 0)
                return string.Empty;
            string strStyle = string.Empty;
            if (ItemIndex < 4 && ItemIndex >= 0)
            {
                switch (ItemIndex)
                {
                    case 1:
                        strStyle = "style=\"color:Black\"";
                        break;
                    case 2:
                        strStyle = "style=\"color:Blue\"";
                        break;
                    case 3:
                        strStyle = "style=\"color:Green\"";
                        break;
                }
            }
            else if ((ItemIndex - 12) < 4 && (ItemIndex - 12) >= 0)
            {
                switch ((ItemIndex - 12))
                {
                    case 1:
                        strStyle = "style=\"color:Black\"";
                        break;
                    case 2:
                        strStyle = "style=\"color:Blue\"";
                        break;
                    case 3:
                        strStyle = "style=\"color:Green\"";
                        break;
                }
            }
            else if ((ItemIndex - 8) < 4 && (ItemIndex - 8) >= 0)
            {
                switch ((ItemIndex - 8))
                {
                    case 1:
                        strStyle = "style=\"color:Black\"";
                        break;
                    case 2:
                        strStyle = "style=\"color:Blue\"";
                        break;
                    case 3:
                        strStyle = "style=\"color:Green\"";
                        break;
                }
            }
            else if ((ItemIndex - 4) < 4 && (ItemIndex - 4) >= 0)
            {
                switch ((ItemIndex - 4))
                {
                    case 1:
                        strStyle = "style=\"color:Black\"";
                        break;
                    case 2:
                        strStyle = "style=\"color:Blue\"";
                        break;
                    case 3:
                        strStyle = "style=\"color:Green\"";
                        break;
                }
            }
            return strStyle;
        }

        /// <summary>
        /// 初始化页面数据(主初始化函数)
        /// </summary>
        private void InitPageData()
        {
            InitPromotionsAdv();

            #region 同业之星访谈

            CommonTopicControl1.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.同业之星访谈;
            CommonTopicControl1.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;
            CommonTopicControl1.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.同业之星;
            CommonTopicControl1.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
            CommonTopicControl1.CurrCityId = CityId;

            #endregion

            #region 同业学堂

            CommonTopicControl2.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.同业学堂;
            CommonTopicControl2.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
            #endregion

            #region 最新行业资讯

            CommonTopicControl3.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.资讯;
            CommonTopicControl3.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
            CommonTopicControl3.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
            CommonTopicControl3.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.行业动态;
            #endregion

            #region 景区

            CommonTopicControl4.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.景区新闻;

            #endregion

            #region 旅行社

            CommonTopicControl4.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.旅行社;

            #endregion

            #region 酒店

            CommonTopicControl4.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.酒店新闻;

            #endregion

            InitExchangeTag();
            InitTopExchange();
        }

        #region 绑定供求信息频道推荐广告
        /// <summary>
        /// 绑定供求信息频道推荐广告
        /// </summary>
        private void InitTopExchange()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> ListAdv = null;
            ListAdv = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道推荐广告);
            rptTopExchange.DataSource = ListAdv;
            rptTopExchange.DataBind();
        }
        #endregion 

        /// <summary>
        /// 置顶的供求信息行绑定事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptTopExchange_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                System.Text.StringBuilder strTmp = null;
                Literal ltrImg = (Literal)e.Item.FindControl("ltrImg");
                Literal ltrMQ = (Literal)e.Item.FindControl("ltrMQ");
                EyouSoft.Model.AdvStructure.AdvInfo model = (EyouSoft.Model.AdvStructure.AdvInfo)e.Item.DataItem;
                if (ltrMQ != null && model != null)
                {
                    ltrMQ.Text = Utils.GetMQ(model.ContactMQ);
                }
                if (ltrImg != null && model != null)
                {
                    ltrImg.Text = string.Format("<a {2}><img src=\"{0}{1}\" width=\"112\" height=\"73\" /></a>", Domain.FileSystem, model.ImgPath, string.IsNullOrEmpty(model.RedirectURL) ? "href=\"javascript:void(0);\" " : string.Format(" target=\"_blank\" href=\"{0}\" ", model.RedirectURL));
                }
            }
        }

        /// <summary>
        /// 初始化广告
        /// </summary>
        private void InitPromotionsAdv()
        {
            //初始化促销广告
            IList<EyouSoft.Model.AdvStructure.AdvInfo> ListAdv = null;
            ListAdv = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道促销广告);
            rptPromotionsAdv.DataSource = ListAdv;
            rptPromotionsAdv.DataBind();

            //初始化旗帜广告
            ListAdv = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道旗帜广告);
            rptBannerAdv.DataSource = ListAdv;
            rptBannerAdv.DataBind();

            if (ListAdv != null) ListAdv.Clear();
            ListAdv = null;
        }

        /// <summary>
        /// 初始化供求信息的发布框
        /// </summary>
        private void InitExchangeTag()
        {
            //发布联系信息初始化
            if (IsLogin)
            {
                txtName.Value = SiteUserInfo.ContactInfo.ContactName;
                txtMQ.Value = SiteUserInfo.ContactInfo.MQ;
                txtTel.Value = string.IsNullOrEmpty(SiteUserInfo.ContactInfo.Tel) ? SiteUserInfo.ContactInfo.Mobile : SiteUserInfo.ContactInfo.Tel;
            }

            IList<ListItem> tmpList = null;

            //初始化标签
            tmpList = new List<ListItem>();
            tmpList = EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag));
            rptExchangeTag.DataSource = tmpList;
            rptExchangeTag.DataBind();

            ddlTag.DataSource = tmpList;
            ddlTag.DataTextField = "text";
            ddlTag.DataValueField = "value";
            ddlTag.DataBind();
            ddlTag.Items.Insert(0, new ListItem("--标签--", "-1"));

            if (tmpList != null) tmpList.Clear();
            tmpList = null;

            //初始化类别
            tmpList = new List<ListItem>();
            tmpList = EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.ExchangeType));
            rptExchangeType.DataSource = tmpList;
            rptExchangeType.DataBind();

            ddlType.DataSource = tmpList;
            ddlType.DataTextField = "text";
            ddlType.DataValueField = "value";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("--类别--", "-1"));

            if (tmpList != null) tmpList.Clear();
            tmpList = null;

            //初始化发布到的省份
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList();
            rptProvince.DataSource = list;
            rptProvince.DataBind();

            ddlProvince.DataSource = list;
            ddlProvince.DataTextField = "ProvinceName";
            ddlProvince.DataValueField = "ProvinceId";
            ddlProvince.DataBind();
            ddlProvince.Items.Insert(0, new ListItem("--省份--", "0"));

            if (list != null) list.Clear();
            list = null;
        }

       


        #region 初始化用户信息

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUserInfo()
        {
            //发布联系信息初始化
            if (IsLogin)
            {
                ltrCompanyName.ToolTip = SiteUserInfo.CompanyName;
                ltrCompanyName.Text = Utils.GetText2(SiteUserInfo.CompanyName, 10, false);
                EyouSoft.Model.CompanyStructure.CompanyAttachInfo model = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (model != null)
                {
                    string CompanyLogo = ImageServerUrl + "/Images/UserPublicCenter/defaultlogo.jpg";
                    if(model.CompanyLogo.ImagePath.Length>0)
                    {
                        CompanyLogo=Domain.FileSystem+model.CompanyLogo.ImagePath;
                    }
                    ltrCompanyLog.Text = string.Format("<img src=\"{0}\" width=\"142\" height=\"80\" />", CompanyLogo);
                }
                model = null;
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (CompanyModel != null)
                {
                    aSupplyInfo1.HRef = Domain.UserBackCenter+"/supplyinformation/allsupplymanage.aspx";
                    aSupplyInfo2.HRef = Domain.UserBackCenter+ "/supplyinformation/allsupplymanage.aspx";
                    aSupplyInfo3.HRef = Domain.UserBackCenter + "/supplyinformation/hassupplyfavorites.aspx";
                    aEditUser.HRef = Domain.UserBackCenter + "/SupplyManage/FreeShop.aspx";
                    if (CompanyModel.BusinessProperties != EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社)
                    {
                        aEditUser.HRef = Domain.UserBackCenter + "/SupplyManage/MyOwenerShop.aspx";
                        EyouSoft.Model.CompanyStructure.CompanyService ServiceModel = CompanyModel.StateMore.CompanyService; ;
                        bool IsOpenHighShop = ServiceModel.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
                        if (IsOpenHighShop)
                        {
                            aEditUser.HRef = Domain.UserBackCenter + "/SupplyManage/Default.aspx";
                        }
                        ServiceModel = null;
                    }
                    EyouSoft.Model.CompanyStructure.CompanyType? cType= Utils.GetCompanyType(SiteUserInfo.CompanyID);
                    if(cType.HasValue)
                    {
                        aShop.HRef = Utils.GetCompanyDomain(SiteUserInfo.CompanyID,cType.Value);
                    }
                    else
                    {
                        aShop.Visible=false;
                    }
                }
                CompanyModel = null;
            }
        }

        #endregion

        #endregion

        #region 发布供求信息保存按钮事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 验证数据
            string ErrStr = string.Empty;
            string[] Tags = Utils.GetFormValues("radioTag");
            string[] Types = Utils.GetFormValues("radioType");
            string AttatchPath = Utils.GetFormValue("ctl00$ctl00$Main$SupplierMain$SingleFileUpload1$hidFileName");
            if (Tags.Length == 0)
            {
                ErrStr += "请选择标签\\n";
            }
            if (Types.Length == 0)
            {
                ErrStr += "请选择类别\\n";
            }
            if (txtInfo.Value.Trim().Length == 0)
            {
                ErrStr += "请输入供求信息内容\\n";
            }
            if (txtMQ.Value.Trim().Length == 0)
            {
                ErrStr += "请输入MQ号码";
            }
            if (!string.IsNullOrEmpty(ErrStr))
            {
                MessageBox.Show(this.Page, ErrStr);
                return;
            }
            #endregion
            EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            model.AttatchPath = AttatchPath;
            model.CityId = SiteUserInfo.CityId;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.ContactName = Utils.InputText(txtName.Value);
            model.ContactTel = Utils.InputText(txtTel.Value);
            model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(Tags[0]);
            model.ExchangeText = Utils.InputText(txtInfo.Value);
            model.ExchangeTitle = Utils.GetText(model.ExchangeText, 26);
            model.ID = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = Utils.InputText(txtMQ.Value);
            model.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            model.ProvinceId = SiteUserInfo.ProvinceId;
            model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(Types[0]);
            model.IsCheck = IsCompanyCheck;//供求审核状态默认等于当前用户所在公司的审核状态
            IList<int> ProvinceIds = null;
            string[] strProvinceIds = Utils.GetFormValues("ckbProvince");
            if (strProvinceIds != null && strProvinceIds.Length > 0)
            {
                ProvinceIds = new List<int>();
                for (int i = 0; i < strProvinceIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strProvinceIds[i]) && StringValidate.IsInteger(strProvinceIds[i]))
                        ProvinceIds.Add(int.Parse(strProvinceIds[i]));
                }
            }
            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().Add(model, ProvinceIds == null ? null : ProvinceIds.ToArray());
            model = null;
            txtInfo.Value = "";
            if (Result)
            {
                Utils.ShowAndRedirect("供求发布成功！", Request.RawUrl);
            }
            else
            {
                Utils.ShowAndRedirect("供求发布失败！", Request.RawUrl);
            }

        }
        #endregion
    }
}
