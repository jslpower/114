using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace SeniorOnlineShop.master
{
    /// <summary>
    /// 高级网店模板4母版页
    /// </summary>
    /// Author:汪奇志 2010-11-11
    public partial class T4 : System.Web.UI.MasterPage
    {
        #region TAB LINKS
        /// <summary>
        /// 首页
        /// </summary>
        private string TABLINKS_SY = "/default";
        /// <summary>
        /// 散拼计划
        /// </summary>
        private const string TABLINKS_SPJH = "/TourList2";
        /// <summary>
        /// 团队定制
        /// </summary>
        private const string TABLINKS_TDDZ = "/TeamCustomization";
        /// <summary>
        /// 出游指南
        /// </summary>
        private const string TABLINKS_CYZN = "/GuideBooks";
        /// <summary>
        /// 机票预定
        /// </summary>
        private const string TABLINKS_JPYD = "/default";
        /// <summary>
        /// 酒店预定
        /// </summary>
        private const string TABLINKS_JDYD = "/default";
        /// <summary>
        /// 关于我们
        /// </summary>
        private const string TABLINKS_GYWM = "/aboutus2";
        #endregion

        #region Attributes
        /// <summary>
        /// 公司编号
        /// </summary>
        private string _companyid;
        /// <summary>
        /// 公司信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo _companyinfo = null;
        /// <summary>
        /// 高级网店信息
        /// </summary>
        private EyouSoft.Model.ShopStructure.HighShopCompanyInfo _companyeshopinfo = null;

        /// <summary>
        /// 图片服务器
        /// </summary>
        protected string ImageServerUrl;
        /// <summary>
        /// 头部Bannner样式
        /// </summary>
        protected string STYHeadBanner { get; set; }

        /// <summary>
        /// 公司信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfo
        {
            get { return this._companyinfo; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get { return this._companyid; }
        }
        /// <summary>
        /// 高级网店信息
        /// </summary>
        public EyouSoft.Model.ShopStructure.HighShopCompanyInfo CompanyEShopInfo
        {
            get { return this._companyeshopinfo; }
        }
        /// <summary>
        /// 当前所在导航TAB
        /// </summary>
        public T4TAB CTAB { get; set; }
        #endregion

        #region Override OnInit
        /// <summary>
        /// Override OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //初始化公司ID
            string currentUrlPath = HttpContext.Current.Request.ServerVariables["Http_Host"];

            //如果使用独立域名访问网店，则根据域名查询所属公司ID.
            if (Domain.SeniorOnlineShop.IndexOf(currentUrlPath) == -1)
            {
                EyouSoft.Model.SystemStructure.SysCompanyDomain cDomianModel = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().
                    GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, currentUrlPath);
                if (cDomianModel != null)
                {
                    this._companyid = cDomianModel.CompanyId;
                    this.TABLINKS_SY = "/";
                }
            }

            //如果没有使用独立域名访问网店，则从QueryString集合中查找cid参数，获取公司ID
            if (string.IsNullOrEmpty(this._companyid))
            {
                this._companyid = Utils.GetQueryStringValue("cid");
            }

            //获取高级网店信息和公司信息
            this._companyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this._companyid);
            if (this._companyinfo == null)
            {
                Utils.ShowError("高级网店不存在!", "SeniorShop");
                return;
            }

            //判断是否开通高级网店                
            if (this._companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) && !Utils.IsOpenHighShop(this._companyid))
            {
                Utils.ShowError("该公司未开通高级网店!", "SeniorShop");
                return;
            }

            this._companyeshopinfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this._companyid);

            this.ImageServerUrl = ImageManage.GetImagerServerUrl(1);

            Utils.EShopTemplateValidate(this.CompanyId, _companyeshopinfo.TemplateId);

            base.OnInit(e);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //网店的点击量
            EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().SetShopClickNum(this._companyid);

            this.InitInfo();
            this.InitNews();
            this.InitLinks();

            this.Page.ClientScript.RegisterClientScriptInclude(Guid.NewGuid().ToString(), "/DatePicker/WdatePicker.js");
            //初始化页面标题，公司名称
            if (this.Page.Title.Equals(string.Empty) || this.Page.Title.Equals("首页"))
            {
                this.Page.Title = string.Format("{0}", _companyinfo.CompanyName);
            }
            else
            {
                this.Page.Title = string.Format("{0}_{1}", this.Page.Title, _companyinfo.CompanyName);
            }
        }

        #region private memebers
        /// <summary>
        /// 初始化网店头部 企业名片 登录块 版权
        /// </summary>
        private void InitInfo()
        {
            //Head
            if (this.CompanyInfo.AttachInfo.CompanyShopBanner.BannerType == EyouSoft.Model.CompanyStructure.ShopBannerType.Default)
            {
                this.phDefaultHead.Visible = true;
                this.imgHeadLogo.Src = Domain.FileSystem + this.CompanyInfo.AttachInfo.CompanyShopBanner.CompanyLogo;
                this.ltrHeadLogoName.Text = this.CompanyInfo.CompanyName;
                this.STYHeadBanner = "background:url('" + Domain.FileSystem + this.CompanyInfo.AttachInfo.CompanyShopBanner.BannerBackground + "');background-repeat:no-repeat;";
            }
            else
            {
                this.phPersonalizeHead.Visible = true;
                this.ltrPersonalizeHead.Text = Utils.GetEShopImgOrFalash(this.CompanyInfo.AttachInfo.CompanyShopBanner.ImagePath, "javascript:void(0)");
            }

            //当前的TAB
            switch (this.CTAB)
            {
                case T4TAB.出游指南: this.lnkCYZN.Attributes.Add("class", "nav-on"); break;
                case T4TAB.机票预定: this.lnkJPYD.Attributes.Add("class", "nav-on"); break;
                case T4TAB.酒店预定: this.lnkJDYD.Attributes.Add("class", "nav-on"); break;
                case T4TAB.散拼计划: this.lnkSPJH.Attributes.Add("class", "nav-on"); break;
                case T4TAB.团队定制: this.lnkTDDZ.Attributes.Add("class", "nav-on"); break;
                default: this.lnkSY.Attributes.Add("class", "nav-on"); break;
            }

            //TAB链接
            this.lnkSY.HRef = this.lnkSY1.HRef = Utils.GenerateShopPageUrl2(this.TABLINKS_SY, this.CompanyId);
            this.lnkSPJH.HRef = this.lnkSPJH1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_SPJH, this.CompanyId);
            this.lnkTDDZ.HRef = this.lnkTDDZ1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_TDDZ, this.CompanyId);
            this.lnkCYZN.HRef = this.lnkCYZN1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_CYZN, this.CompanyId);
            this.lnkJPYD.HRef = this.lnkJPYD1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JPYD, this.CompanyId);
            this.lnkJDYD.HRef = this.lnkJDYD1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JDYD, this.CompanyId);
            this.lnkGYWM1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_GYWM, this.CompanyId);

            //企业名片
            this.ltrCompanyBrandName.Text = this.CompanyInfo.CompanyBrand;
            this.ltrContactName.Text = this.CompanyInfo.ContactInfo.ContactName + "&nbsp;" + Utils.GetBigImgMQ(this.CompanyInfo.ContactInfo.MQ);
            this.ltrContactMobile.Text = this.CompanyInfo.ContactInfo.Mobile;
            this.ltrContactTelephone.Text = this.CompanyInfo.ContactInfo.Tel;
            this.ltrContactFax.Text = this.CompanyInfo.ContactInfo.Fax;
            this.ltrContactAddress.Text = this.CompanyInfo.CompanyAddress;
            this.imgCompanyCard.Src = Utils.GetLineShopImgPath(this.CompanyInfo.AttachInfo.CompanyCard.ImagePath, 1);

            FrontPage basepage = this.Page as FrontPage;

            //登录块
            if (basepage.IsLogin)
            {
                phLogin.Visible = true;
                phNoLogin.Visible = false;

                ltrUserName.Text = basepage.SiteUserInfo.UserName;
                linkLogout.HRef = Utils.GetLogoutUrl(Utils.GenerateShopPageUrl2("/default", this.CompanyId));
            }
            else
            {
                phLogin.Visible = false;
                phNoLogin.Visible = true;

                basepage.AddJavaScriptInclude(JsManage.GetJsFilePath("blogin"), false, false);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), ";blogin.ssologinurl='" + EyouSoft.Common.Domain.PassportCenter + "';", true);
                btnLogin.Attributes["onclick"] = "$('#errSpan').html('');blogin(document.getElementById('aspnetForm'),'','" + Utils.GenerateShopPageUrl2(TABLINKS_SY, this.CompanyId) + "',function(m){$('#errSpan').html(m);});return false;";
            }

            //版权
            this.ltrCopyRight.Text = this.CompanyEShopInfo.ShopCopyRight;
        }

        /// <summary>
        /// 初始化最新旅游动态
        /// </summary>
        private void InitNews()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopNews> news = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetTopNumberList(6, this.CompanyId);

            if (news != null && news.Count > 0)
            {
                this.rptNews.DataSource = news;
                this.rptNews.DataBind();
            }

            news = null;
        }

        /// <summary>
        /// 初始化友情链接
        /// </summary>
        private void InitLinks()
        {
            var links = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(this.CompanyId);

            if (links != null && links.Count > 0)
            {
                this.rptLinks.DataSource = links;
                this.rptLinks.DataBind();
            }

            links = null;
        }
        #endregion

    }

    #region 高级网店模板4导航TAB
    /// <summary>
    /// 高级网店模板4导航TAB
    /// </summary>
    public enum T4TAB
    {
        /// <summary>
        /// 首页
        /// </summary>
        首页 = 0,
        /// <summary>
        /// 散拼计划
        /// </summary>
        散拼计划,
        /// <summary>
        /// 团队定制
        /// </summary>
        团队定制,
        /// <summary>
        /// 出游指南
        /// </summary>
        出游指南,
        /// <summary>
        /// 机票预定
        /// </summary>
        机票预定,
        /// <summary>
        /// 酒店预定
        /// </summary>
        酒店预定
    }
    #endregion
}
