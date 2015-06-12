using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using EyouSoft.Common;
using EyouSoft.SSOComponent.Entity;
using EyouSoft.DAL.CompanyStructure;
namespace EyouSoft.Common.Control
{
    /// <summary>
    /// 所有页面基类
    /// 功能：统一处理页面共性，处理用户身份，存储基本的用户信息，页面元信息的处理方法集合
    /// </summary>
    public class BasePage:System.Web.UI.Page
    {  
        private string imageServerUrl;

        public string ImageServerUrl
        {
            get { return imageServerUrl; }
        }

        private bool isLogin = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return isLogin;
            }
        }

        private UserInfo _userInfo = null;

        public UserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            imageServerUrl = ImageManage.GetImagerServerUrl(1);
            isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin();
            Model.CompanyStructure.CompanyUser user = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel("50caa9ca-f15c-440e-8d91-8d160de3c382");
            _userInfo = new UserInfo()
            {
                CityId = 1,
                CompanyID = "1e5edc20-4b83-4c16-8c52-008e0c09bd1e",
                UserName = user.UserName,
                ContactInfo = user.ContactInfo,
                DepartId = "1",
                ProvinceId = 1,
                DepartName = "432fds",
                IsAdmin = true,
                ID = "50caa9ca-f15c-440e-8d91-8d160de3c382",
                                
            };
            Model.CompanyStructure.CompanyRole r = new EyouSoft.Model.CompanyStructure.CompanyRole();
            r.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
            r.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.组团);
            _userInfo.CompanyRole = r;
            _userInfo.AreaId= new int[3]{1,2,3};
            //string[] arr = user.AreaList.Split(',');
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    _userInfo.AreaId[i] = Int32.Parse(arr[i]);
            //}
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            //AddMetaContentType();
            //AddMetaIE8Compatible();
            //AddMetaTag("description", "");
            ////根据站点
            //AddMetaTag("keywords", "");
            ////add Head Cache Contorl
            //AddSiteIcon();
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            //统一处理网站标题
            //将Web网站名字 加入到页面标题中
            Page.Title = Page.Title + "_同业114";
        }
        /// <summary>
        /// 添加Meta 标记到页面头部
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected virtual void AddMetaTag(string name, string value)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(value))
            {
                return;
            }

            HtmlMeta meta = new HtmlMeta();
            meta.Name = name;
            meta.Content = value;
            Page.Header.Controls.Add(meta);
        }

        /// <summary>
        /// 添加Content-Type Meta标记到页面头部
        /// </summary>
        protected virtual void AddMetaContentType()
        {
            HtmlMeta meta = new HtmlMeta();
            //meta.HttpEquiv = "content-type";
            //meta.Content = Response.ContentType + "; charset=" + Response.ContentEncoding.HeaderName;
            meta.Attributes["charset"] = Response.ContentEncoding.HeaderName;
            Page.Header.Controls.Add(meta);
        }
        /// <summary>
        /// 添加IE8兼容IE7 Meta Tag.
        /// </summary>
        protected virtual void AddMetaIE8Compatible()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "X-UA-Compatible";
            meta.Content = "IE=EmulateIE7";
            Page.Header.Controls.Add(meta);
        }
        /// <summary>
        /// 添加网站favicon
        /// </summary>
        protected virtual void AddSiteIcon()
        {
            AddGenericLink("image/x-icon", "icon", "/images/favicon.ico");
            AddGenericLink("image/x-icon", "shortcut icon", "/images/favicon.ico");
        }

        /// <summary>
        /// 添加link 标记到页面头部
        /// </summary>
        /// <summary>
        /// Adds the generic link to the header.
        /// </summary>
        protected virtual void AddGenericLink(string type, string relation, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = type;
            link.Attributes["rel"] = relation;
            link.Attributes["href"] = href;
            Page.Header.Controls.Add(link);
        }

        /// <summary>
        /// 添加Javascript外部文件到html页面
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="placeInBottom">是否添加在html代码底部</param>
        /// <param name="addDeferAttribute">是否添加defer属性</param>
        public virtual void AddJavaScriptInclude(string url, bool placeInBottom, bool addDeferAttribute)
        {
            if (placeInBottom)
            {
                string script = "<script type=\"text/javascript\"" + (addDeferAttribute ? " defer=\"defer\"" : string.Empty) + " src=\"" + url + "\"></script>";
                ClientScript.RegisterStartupScript(GetType(), url.GetHashCode().ToString(), script);
            }
            else
            {
                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes["type"] = "text/javascript";
                script.Attributes["src"] = url;
                if (addDeferAttribute)
                {
                    script.Attributes["defer"] = "defer";
                }

                Page.Header.Controls.Add(script);
            }
        }

        /// <summary>
        /// 添加css外部文件到html页面
        /// </summary>
        /// <param name="url">The relative URL.</param>
        public virtual void AddStylesheetInclude(string url)
        {
            //HtmlLink link = new HtmlLink();
            //link.Attributes["type"] = "text/css";
            //link.Attributes["href"] = url;
            //link.Attributes["rel"] = "stylesheet";
            //Page.Header.Controls.Add(link);
            AddGenericLink("text/css", "stylesheet", url);
        }
    }
}
