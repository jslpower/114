using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using EyouSoft.Common.URLREWRITE;

namespace SeniorOnlineShop.GeneralShop.GeneralShopControl
{
    /// <summary>
    /// 普通网店城市及菜单控件
    /// </summary>
    /// 周文超 2011-11-14
    public partial class CityAndMenu : System.Web.UI.UserControl
    {
        protected string ImageServerPath = "";
        protected string strReturnUrl = "";
        protected string UnionLogo = "";
        private int _headMenuIndex = 0;
        protected bool IsSiteExtend;//是否二类分站
        //旅游主题
        protected System.Text.StringBuilder sbThemelist = new StringBuilder();

        /// <summary>
        /// 目录菜单索引
        /// 1：首页  2：线路 3：机票 4：酒店  5：景区 6：供求信息 7:旅游资讯
        /// </summary>
        public int HeadMenuIndex
        {
            get
            {
                return _headMenuIndex;
            }
            set
            {
                _headMenuIndex = value;
            }
        }

        protected int CityId = 0;
        protected int PrivoinceId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                CityId = (this.Page as EyouSoft.Common.Control.FrontPage).CityId;
                if ((this.Page as EyouSoft.Common.Control.FrontPage).CityModel != null)
                {
                    PrivoinceId = (this.Page as EyouSoft.Common.Control.FrontPage).CityModel.ProvinceId;
                }

                IsSiteExtend = EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().IsExtendSite(CityId);
                GetUnionLogo();//联盟logo
                this.GetHotCitys();//热门城市
                this.GetThemeList();//旅游主题

                switch (HeadMenuIndex)
                {
                    case 1:
                        this.liMenu1.Attributes["class"] = "dqy";
                        break;
                    case 2:
                        this.liMenu2.Attributes["class"] = "dqy";
                        break;
                    case 3:
                        this.liMenu3.Attributes["class"] = "dqy";
                        break;
                    case 4:
                        this.liMenu4.Attributes["class"] = "dqy";
                        break;
                    case 5:
                        this.liMenu5.Attributes["class"] = "dqy";
                        break;
                    case 6:
                        this.liMenu6.Attributes["class"] = "dqy";
                        break;
                    case 7:
                        this.liMenu7.Attributes["class"] = "dqy";
                        break;
                    default:
                        this.liMenu1.Attributes["class"] = "dqy";
                        break;
                }

                menu1.HRef = SubStation.CityUrlRewrite(CityId);
                menu2.HRef = Tour.GetXianLuUrl(CityId);
                menu3.HRef = Plane.PlaneDefaultUrl(CityId);
                menu4.HRef = Hotel.GetHotelBannerUrl(CityId);
                menu5.HRef = ScenicSpot.ScenicDefalutUrl(CityId);
                menu6.HRef = EyouSoft.Common.URLREWRITE.SupplierInfo.InfoDefaultUrlWrite(CityId);
                menu7.HRef = Infomation.InfoDefaultUrlWrite();
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
        /// 获取旅游主题
        /// </summary>
        /// <returns></returns>
        protected string GetThemeList()
        {
            int maxIndex = 0;
            IList<EyouSoft.Model.SystemStructure.SysFieldBase> ThemeList = EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetSysFieldBaseList(EyouSoft.Model.SystemStructure.SysFieldType.线路主题);
            if (ThemeList != null && ThemeList.Count > 0)
            {
                maxIndex = ThemeList.Count <= 7 ? ThemeList.Count : 7;
                for (int i = 0; i < maxIndex; i++)
                {
                    sbThemelist.Append("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourThemeUrl(0, ThemeList[i].FieldId, CityId) + "\">" + ThemeList[i].FieldName + "</a>&nbsp;");
                }
            }
            return sbThemelist.ToString();
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
    }
}