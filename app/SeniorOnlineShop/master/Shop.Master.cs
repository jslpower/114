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
using EyouSoft.Common;
using EyouSoft.Common.Control;
using EyouSoft.Common.URLREWRITE;
using System.Text;
using System.Collections.Generic;

namespace SeniorOnlineShop.master
{
    public partial class Shop : System.Web.UI.MasterPage
    {
        protected string ImageServerPath = "";

        private int _headMenuIndex = 0;
        protected string strAllHotCity = "";
        protected string weatherHtml;
        protected string noticeHtml;//公告
        protected bool IsSiteExtend;//是否二类分站
        /// <summary>
        /// 目录菜单索引
        /// 1：首页  2：线路 3：机票 4：酒店  5：景区 6：供求信息 7:旅游资讯
        /// </summary>
        public int HeadMenuIndex
        {
            get
            {
                if (_headMenuIndex != 0)
                {
                    return _headMenuIndex;
                }
                else
                {
                    return 1;
                }
            }
            set { _headMenuIndex = value; }
        }
        protected int CityId = 0;
        protected string Parms = string.Empty;
        protected string strReturnUrl = string.Empty;
        protected string DefaultUrl = Domain.UserPublicCenter + "/Default.aspx";  //点击同业图标链接地址
        protected string UnionLogo = string.Empty;
        protected string LogoPath = string.Empty;
        protected string ImagePath = string.Empty; //宣传图片路径
        protected string MqPath = string.Empty;   //公司mq路径
        private string _CompanyId = null;
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = null;
        /// <summary>
        /// 获取当前网店指向的公司ID
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
            _CompanyId = Utils.InputText(Request.QueryString["cid"]);
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo _detailCompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(_CompanyId);
            //if (_detailCompanyInfo == null)
            //{
            //    Utils.ShowError("普通网店不存在!", "Shop");
            //    return;
            //}
            compDetail = _detailCompanyInfo;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUnionLogo();
                InitCompanyInfo();
                FrontPage basepage = this.Page as FrontPage;
                basepage.AddJavaScriptInclude(JsManage.GetJsFilePath("jquery"), false, false);
                basepage.AddStylesheetInclude(CssManage.GetCssFilePath("gouwu"));
                basepage.AddStylesheetInclude(CssManage.GetCssFilePath("body"));
                GetCity();
                CityId = (this.Page as EyouSoft.Common.Control.FrontPage).CityId;
                IsSiteExtend = EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().IsExtendSite(CityId);
                BindNotice();//公告
                BindNum();//采购商、供应商、供需数量
                GetUnionLogo();//联盟logo
                this.GetHotCitys();//热门城市

                switch (HeadMenuIndex)
                {
                    case 1:
                        menu1.Attributes["class"] = "select";
                        break;
                    case 2:
                        menu2.Attributes["class"] = "select";
                        break;
                    case 3:
                        menu3.Attributes["class"] = "select";
                        break;
                    case 4:
                        menu4.Attributes["class"] = "select";
                        break;
                    case 5:
                        menu5.Attributes["class"] = "select";
                        break;
                    case 6:
                        menu6.Attributes["class"] = "select";
                        break;
                    case 7:
                        menu7.Attributes["class"] = "select";
                        break;
                    default:
                        menu1.Attributes["class"] = "select";
                        break;
                }

                menu1.HRef = SubStation.CityUrlRewrite(CityId);
                menu2.HRef = Tour.GetXianLuUrl(CityId);
                //menu3.HRef = Plane.PlaneDefaultUrl(CityId);
                menu4.HRef = Hotel.GetHotelBannerUrl(CityId);
                menu5.HRef = ScenicSpot.ScenicDefalutUrl(CityId);
                menu6.HRef = EyouSoft.Common.URLREWRITE.SupplierInfo.InfoDefaultUrlWrite(CityId);
                menu7.HRef = Infomation.InfoDefaultUrlWrite();
            }
        }
        /// <summary>
        /// 绑定公告
        /// </summary>
        protected void BindNotice()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页广告公告);
            StringBuilder strBuilder = new StringBuilder();
            if (advList != null && advList.Count > 0)
            {
                foreach (EyouSoft.Model.AdvStructure.AdvInfo advModel in advList)
                {
                    strBuilder.AppendFormat("<li><a href=\"{3}/PlaneInfo/NewsDetailInfo.aspx?NewsID={0}&CityId={1}\">·{2}</a></li>", advModel.AdvId, CityId, Utils.GetText(advModel.Title, 15, true), Domain.UserPublicCenter);
                }
            }
            noticeHtml = strBuilder.ToString();
        }
        public string CityName
        {
            get
            {

                return (this.Page as EyouSoft.Common.Control.FrontPage).CityModel.HeaderLetter;
            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        protected void BindNum()
        {

            EyouSoft.Model.SystemStructure.SummaryCount SummaryModel = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            if (SummaryModel != null)
            {
                //采购商数量
                litBuy.Text = (SummaryModel.Buyer + SummaryModel.BuyerVirtual).ToString();
                //供需数量
                litNum.Text = (SummaryModel.SupplyInfos + SummaryModel.SupplyInfosVirtual).ToString();
                //供应商数量
                litSup.Text = (SummaryModel.Suppliers + SummaryModel.SuppliersVirtual).ToString();
            }
        }

        /// <summary>
        /// 获的热门城市
        /// </summary>
        protected void GetHotCitys()
        {
            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            CityId = page.CityId;
            if (HeadMenuIndex != 7)//当前不是旅游资讯
            {
                strReturnUrl = "Url=" + Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"]);
                if (page.CityModel != null)
                {
                    if (HeadMenuIndex == 6)
                    {
                        this.labCityName.Text = "全国";
                    }
                    else
                    {
                        this.labCityName.Text = page.CityModel.CityName;
                    }
                }
                else
                {
                    Utils.ShowError("页面不存在!", "City");
                    return;
                }
            }
            else
            {
                this.labCityName.Text = "全国";
            }

        }

        /// <summary>
        /// 获的联盟LOGO
        /// </summary>
        protected void GetUnionLogo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (Model != null)
            {
                UnionLogo = EyouSoft.Common.Domain.FileSystem + Model.UnionLog;
            }
            else
            {
                UnionLogo = EyouSoft.Common.Domain.ServerComponents + "/images/UserPublicCenter/indexlogo.gif";
            }
            Model = null;

        }


        /// <summary>
        /// 构建要在当前页面上跳转时,将当前页面本身含有的url参数带上,以免丢失原本的url参数
        /// </summary>
        /// <param name="noUrlParNameArr">不要自动带入的url的参数名称</param>
        /// 若无URL参数 则返回空字符串
        public static string BuildUrlQueryString(params string[] noUrlParNameArr)
        {
            string urlQueryString = "";
            System.Collections.Specialized.NameValueCollection query = HttpContext.Current.Request.QueryString;
            if (query == null)
                return urlQueryString;

            //排除url中不要的参数
            foreach (string key in query.AllKeys)
            {
                bool not = false;
                if (noUrlParNameArr != null && noUrlParNameArr.Length > 0)
                {
                    foreach (string noUrlName in noUrlParNameArr)
                    {
                        if (noUrlName != null && key != null && noUrlName.ToLower() == key.ToLower())
                        {
                            not = true;
                        }
                    }
                }

                if (!not)
                {
                    urlQueryString += string.Format("{0}={1}&", key, HttpContext.Current.Server.UrlEncode(query[key]));
                }
            }

            urlQueryString = urlQueryString.TrimEnd("&".ToCharArray());

            return urlQueryString;

        }
        /// <summary>
        /// 获的当前销售城市
        /// </summary>
        protected void GetCity()
        {
            FrontPage page = this.Page as FrontPage;
            CityId = page.CityId;
            strReturnUrl = Domain.UserPublicCenter + "/ToCutCity.aspx?" + Domain.UserPublicCenter + "/Default.aspx" + Request.ServerVariables["SCRIPT_NAME"];
            if (CityId > 0)
            {
                DefaultUrl += "?CityId=" + CityId;
                Parms = "?CityId=" + CityId;
            }

            EyouSoft.Model.SystemStructure.SysCity Model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            if (Model != null)
            {
                labCityName.Text = Model.CityName;
            }
            else
            {
                labCityName.Text = "全国";
            }
            Model = null;
        }

        #region 公司信息
        private void InitCompanyInfo()
        {
            compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId); //公司详细信息
            if (compDetail != null)
            {
                ltr_BrandName.Text = compDetail.CompanyBrand;
                ltr_CompanyName.Text = compDetail.CompanyName;
                ltr_Address.Text = compDetail.CompanyAddress;
                ltr_LinkPerson.Text = compDetail.ContactInfo.ContactName;
                ltr_Faxs.Text = compDetail.ContactInfo.Fax;
                ltr_Mobel.Text = compDetail.ContactInfo.Mobile;
                ltr_Phone.Text = compDetail.ContactInfo.Tel;
                if (compDetail.SaleCity != null && compDetail.SaleCity.Count > 0)
                {
                    string SaleCity = string.Empty;
                    for (int i = 0; i < compDetail.SaleCity.Count; i++)
                    {
                        SaleCity += compDetail.SaleCity[i].CityName + " ";
                    }
                    ltr_SaleArea.Text = SaleCity;
                }
                //公司类型
                EyouSoft.Model.CompanyStructure.CompanyRole roleModel = compDetail.CompanyRole;
                if (roleModel != null)
                {
                    StringBuilder strRoleName = new StringBuilder();
                    if (roleModel.RoleItems.Length > 0)
                    {
                        for (int i = 0; i < roleModel.RoleItems.Length; i++)
                        {
                            strRoleName.Append(roleModel.RoleItems[i].ToString() + " ");
                        }
                    }
                    ltr_CompanyType.Text = strRoleName.ToString();
                }
                //mq地址
                if (!string.IsNullOrEmpty(compDetail.ContactInfo.MQ))
                {
                    MqPath = Utils.GetBigImgMQ(compDetail.ContactInfo.MQ);
                }
                ImagePath = Utils.GetLineShopImgPath(compDetail.AttachInfo.CompanyImg.ImagePath, 6);
                //Logo 地址
                LogoPath = string.Format("<img src=\"{0}\" width=\"140\" height=\"62\" style=\"border:1px solid #E9E9E9;\"/>", Utils.GetLineShopImgPath(compDetail.AttachInfo.CompanyLogo.ImagePath, 2)); //LogoPath
            }
        }
        /// <summary>
        /// 获取当前公司的信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfo
        {
            get { return compDetail; }
        }
        #endregion
    }
}
