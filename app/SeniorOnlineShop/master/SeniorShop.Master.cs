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
using EyouSoft.Model.ShopStructure;

namespace SeniorOnlineShop.master
{
    public partial class SeniorShop : System.Web.UI.MasterPage
    {
        #region Attributes
        /// <summary>
        /// 首页链接
        /// </summary>
        private string TABLINKS_SY = "/seniorshop";

        protected string ImageServerUrl;
        protected string CompanyBrandName;
        protected string XinYongZhiShu;
        protected string HuoYueZhiShu;
        protected string TuiJianZhiShu;
        protected string BannerImage;

        protected string MQLink = "";
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo _detailCompanyInfo = null;
        #endregion

        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo DetailCompanyInfo
        {
            get 
            {
                return _detailCompanyInfo;
            }
        }
        private EyouSoft.Model.ShopStructure.HighShopCompanyInfo _highCompanyInfo = null;
        private string _CompanyId = null;
        /// <summary>
        /// 获取当前高级网店指向的公司ID
        /// </summary>
        public string CompanyId
        {
            get
            {
                return _CompanyId;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            //初始化公司ID
            bool IsIndependentDomain = false;
            string currentUrlPath = HttpContext.Current.Request.ServerVariables["Http_Host"];
            //是否正在使用独立域名访问网店
            if (Domain.SeniorOnlineShop.IndexOf(currentUrlPath) == -1)
            {
                IsIndependentDomain = true;
            }
            //如果使用独立域名访问网店，则根据域名查询所属公司ID.
            if (IsIndependentDomain)
            {
                EyouSoft.Model.SystemStructure.SysCompanyDomain cDomianModel = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().
                    GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, currentUrlPath);
                if (cDomianModel != null)
                {
                    _CompanyId = cDomianModel.CompanyId;
                    this.TABLINKS_SY = "/";
                }                
            }
            //如果没有使用独立域名访问网店，则从QueryString集合中查找cid参数，获取公司ID
            if (string.IsNullOrEmpty(_CompanyId))
            {
                _CompanyId = Utils.GetQueryStringValue("cid");
            }
            //获取高级网店信息和公司信息
            _detailCompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(_CompanyId);
            if (_detailCompanyInfo == null)
            {
                Utils.ShowError("高级网店不存在!", "SeniorShop");
                return;
            }
            //判断是否开通高级网店                
            if (_detailCompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {
                bool isOpenShop = false;
                isOpenShop = Utils.IsOpenHighShop(_CompanyId);
                //EyouSoft.Model.CompanyStructure.CompanyState state = new EyouSoft.Model.CompanyStructure.CompanyState();
                //Utils.GetCompanyDomain(CompanyId, out state,out isOpenShop, EyouSoft.Model.CompanyStructure.CompanyType.专线);
                if (!isOpenShop)
                {
                    Utils.ShowError("该公司未开通高级网店!", "SeniorShop");
                    return;
                }
            }

            _highCompanyInfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(_CompanyId);

            if (_highCompanyInfo == null)
            {
                Utils.ShowError("高级网店不存在!", "SeniorShop");
                return;
            }
            Utils.EShopTemplateValidate(this.CompanyId, _highCompanyInfo.TemplateId);

            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //网店的点击量
            EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().SetShopClickNum(this.CompanyId);

            #region 根据用户选择网店模板，初始化,默认选择style1.css
            int styleIndex = 1;
            if (_highCompanyInfo != null)
            {
                styleIndex = _highCompanyInfo.TemplateId;
                if (_highCompanyInfo.TemplateId < 1 || _highCompanyInfo.TemplateId > 4)
                {
                    styleIndex = 1;
                }
            }
            string styleName = string.Format("style{0}", styleIndex);
            FrontPage basepage = this.Page as FrontPage;
            basepage.AddStylesheetInclude(CssManage.GetCssFilePath(styleName));
            #endregion

            //初始化ImageServerUrl
            ImageServerUrl = basepage.ImageServerUrl;

            //初始化页面标题，公司名称
            string companyName = _detailCompanyInfo.CompanyName;
            //this.Page.Title = string.Format("{0}_{1}",this.Page.Title,companyName);
            if (this.Page.Title.Equals(string.Empty) || this.Page.Title.Equals("首页"))
            {
                this.Page.Title = string.Format("{0}", companyName);
            }
            else
            {
                this.Page.Title = string.Format("{0}_{1}", this.Page.Title, companyName);
            }
            //EyouSoft.Model.SystemStructure.SysProvince provinceModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(_detailCompanyInfo.ProvinceId);
            //EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(_detailCompanyInfo.CityId);
            //this.Page.Title = string.Format("{1}", this.Page.Title, companyName);
            //HtmlMeta meta = new HtmlMeta();
            //meta.Name = "description";
            //meta.Content = string.Format(EyouSoft.Common.PageTitle.SeniorShop_Des, companyName, provinceModel.ProvinceName + cityModel.CityName, "3");
            //Page.Header.Controls.Add(meta);

            #region 页面顶部初始化
            //用户是否登录
            //noLogin.Visible = true;
            //isLogin.Visible = false;
            //顶部菜单链接初始化
            #endregion

            //高级网店头部图片javascript:void(0)
            BannerImage = Utils.GetEShopImgOrFalash(_detailCompanyInfo.AttachInfo.CompanyShopBanner.ImagePath, "javascript:void(0)");
                //Utils.GetLineShopImgPath( _detailCompanyInfo.AttachInfo.CompanyLogo.ImagePath,2);
            //imgLogo.Src = companyLogoSrc;
            //linkLogo.HRef = Utils.GenerateShopPageUrl("/seniorshop/Default.aspx", _CompanyId);


            //公司名称初始化
            //ltrCompanyName.Text = companyName;

            //营业执照号初始化
            //ltrNum.Text = _detailCompanyInfo.License;

            #region 初始化导航栏
            string navOnCss = "nav-on";
            string currentUrl = Request.Url.ToString();
            //根据当前访问的Url，高亮访问的url对应的导航链接
            if (currentUrl.IndexOf("seniorshop/default", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkHome.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/tourlist", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkTourList.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/TourDetail", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkTourList.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/ziyuan", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkZiYuan.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/mudidi", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkMudidi.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/newslist", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkNews.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/new", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkNews.Attributes["class"] = navOnCss;               
            }
            else if (currentUrl.IndexOf("seniorshop/noticelist", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkNotice.Attributes["class"] = navOnCss;
            }
            else if (currentUrl.IndexOf("seniorshop/aboutus", StringComparison.OrdinalIgnoreCase) != -1)
            {
                linkAboutUs.Attributes["class"] = navOnCss;
            }
            //初始化导航链接
            linkHome.HRef = Utils.GenerateShopPageUrl2(this.TABLINKS_SY, _CompanyId);
            linkTourList.HRef = Utils.GenerateShopPageUrl2("/TourList", _CompanyId); ;
            linkZiYuan.HRef = Utils.GenerateShopPageUrl2("/ziyuan", _CompanyId);
            linkMudidi.HRef = Utils.GenerateShopPageUrl2("/mudidis", _CompanyId);
            linkNews.HRef = Utils.GenerateShopPageUrl2("/newslist", _CompanyId);
            linkNotice.HRef = Utils.GenerateShopPageUrl2("/noticelist", _CompanyId);
            linkAboutUs.HRef = Utils.GenerateShopPageUrl2("/aboutus", _CompanyId);
            #endregion


            #region 公司档案初始化
            if (basepage.IsLogin)
            {
                phLogin.Visible = true;
                phNoLogin.Visible = false;

                ltrUserName.Text = basepage.SiteUserInfo.UserName;
                linkLogout.HRef = Utils.GetLogoutUrl(linkHome.HRef);
            }
            else
            {
                phLogin.Visible = false;
                phNoLogin.Visible = true;

                basepage.AddJavaScriptInclude(JsManage.GetJsFilePath("blogin"),false, false);
                this.Page.RegisterClientScriptBlock("blogininit", "<script>;blogin.ssologinurl='" + EyouSoft.Common.Domain.PassportCenter + "';</script>");
                btnLogin.Attributes["onclick"] = "$('#errSpan').html('');blogin(document.getElementById('aspnetForm'),'','" + linkHome.HRef + "',function(m){$('#errSpan').html(m);});return false;";
            }
            //注册链接初始化
            linkUserReg.HRef = Domain.UserPublicCenter + "/Register/CompanyUserRegister.aspx";

            //初始化公司品牌名称
            CompanyBrandName = _detailCompanyInfo.CompanyBrand;//"公司品牌名称";

            //出港城市初始化
            //ltrDepartureCity.Text = "出港城市";

            //联系人初始化
            ltrContactPeople.Text = _detailCompanyInfo.ContactInfo.ContactName;//"联系人";
            //MQ点击初始化
            MQLink =Utils.GetMQ(_detailCompanyInfo.ContactInfo.MQ);

            //手机初始化
            ltrMobile.Text = _detailCompanyInfo.ContactInfo.Mobile;

            //电话初始化
            ltrTelPhone.Text = _detailCompanyInfo.ContactInfo.Tel;

            //传真初始化
            ltrFax.Text = _detailCompanyInfo.ContactInfo.Fax;
            
            //地址初始化
            ltrAddress.Text = _detailCompanyInfo.CompanyAddress;

            //会员信用信息初始化
            //是否已发布服务承诺
            //ltrChengNuo.Text = @"<img src='"+ImageServerUrl+"/images/seniorshop/Commitico16.gif' /><span class='hui'>已发布承诺书，查看详细</span>";

            ////信用指数
            //XinYongZhiShu = "信用60分";
            ////活跃指数
            //HuoYueZhiShu = "活跃60fen";
            ////推荐指数
            //TuiJianZhiShu = "推荐60分";
            #endregion

            //企业名片初始化
            imgMingPian.Src =Utils.GetLineShopImgPath( _detailCompanyInfo.AttachInfo.CompanyCard.ImagePath,1);//string.Format("{0}/images/seniorshop/mingpian.gif",ImageServerUrl);

            //友情链接初始化
            //HighShopFriendLink..::.GetList 
            rptFriendLinks.DataSource = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(_CompanyId);
            rptFriendLinks.DataBind();

            

            //页脚初始化
            ltrCopyRight.Text = _highCompanyInfo!=null?_highCompanyInfo.ShopCopyRight:"";
        }
    }
}
