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
namespace EyouSoft.Common.Control
{
    /// <summary>
    /// 所有页面基类
    /// 功能：统一处理页面共性，处理用户身份，存储基本的用户信息，页面元信息的处理方法集合
    /// </summary>
    /// 周文超 2011-11-07   将景区改为旅行社用户，即  是否旅行社用户 = true
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

        private bool _IsCompanyCheck = false;
        /// <summary>
        /// 公司是否通过审核
        /// </summary>
        public bool IsCompanyCheck
        {
            get
            {
                return _IsCompanyCheck;
            }
        }
        private bool _IsAirTicketSupplyUser = false;
        /// <summary>
        /// 当前用户是否是机票供应商用户
        /// </summary>
        public bool IsAirTicketSupplyUser
        {
            get
            {
                return _IsAirTicketSupplyUser;
            }
        }
        private bool _IsTravelUser = false;
        /// <summary>
        /// 当前用户是否是旅行社用户
        /// </summary>
        public bool IsTravelUser
        {
            get
            {
                return _IsTravelUser;
            }
        }

        private bool _IsTemporaryUser = false;
        /// <summary>
        /// 当前用户是否随便逛逛用户
        /// </summary>
        public bool IsTemporaryUser
        {
            get 
            {
                return _IsTemporaryUser;
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

        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="permissionId">权限ID</param>
        /// <returns></returns>
        public bool CheckGrant(params TravelPermission[] permissionIds)
        {
            if (this.SiteUserInfo != null)
            {
                bool isCheck = true;
                for (int i = 0; i < permissionIds.Length; i++)
                {
                    if (SiteUserInfo.PermissionList.Contains((int)permissionIds[i]) == false)
                    {
                        isCheck = false;
                        break;
                    }
                }
                return isCheck;
            }
            else
            {
                return false;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            imageServerUrl = ImageManage.GetImagerServerUrl(1);
            UserInfo userInfo = null;
            isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out userInfo);
 
            if (userInfo != null)
            {
                _userInfo = userInfo;
                //判断当前用户所在的公司是否通过审核
                _IsCompanyCheck = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(_userInfo.CompanyID).IsCheck;
                //如果用户属于【车队，购物店，酒店，旅游用品店】，则_IsSupplyUser=true,_IsAirTicketSupplyUser=false
                //如果用户属于【机票供应商】,则_IsSupplyUser=false,_IsAirTicketSupplyUser=true
                //如果用户属于【旅行社，景区】，则_IsAirTicketSupplyUser=false
                if (userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队)
                    || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.购物店)
                    || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店)
                    || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
                {

                    _IsAirTicketSupplyUser = false;
                    _IsTravelUser = false;
                }
                else if (userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商))
                {
                    _IsAirTicketSupplyUser = true;
                    _IsTravelUser = false;
                }
                else if (userInfo.CompanyRole.Length > 0 && userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛))  //身份角色长度为0，表示随便逛逛
                {
                    //zwc 20101122 增加随便逛逛
                    _IsTemporaryUser = true;
                }
                else
                {
                    _IsAirTicketSupplyUser = false;
                    _IsTravelUser = true;
                }
            }
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            if (Page.Header != null)
            {
                Page.Header.Controls.Add(meta);
            }
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
            if (Page.Header != null)
            {
                Page.Header.Controls.Add(meta);
            }
        }
        /// <summary>
        /// 添加IE8兼容IE7 Meta Tag.
        /// </summary>
        protected virtual void AddMetaIE8Compatible()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "X-UA-Compatible";
            meta.Content = "IE=EmulateIE7";
            if (Page.Header != null)
            {
                Page.Header.Controls.Add(meta);
            }
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
            if (Page.Header != null)
            {
                Page.Header.Controls.Add(link);
            }
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
            AddGenericLink("text/css", "stylesheet", url);
        }

        /// <summary>
        /// 获取企业信息修改Url
        /// </summary>
        /// <returns></returns>
        public string GetCompanySetUrl(string companyId)
        {
            //string shopUrl = "";
            //if (IsSupplyUser)//如果是供应商用户【酒店，景区，车队，购物店，旅游用品】
            //{
            //    //判断是否开通了高级网店
            //    if (Utils.IsOpenHighShop(companyId))//开通了高级网店
            //    {
            //        shopUrl = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/MyOwenerShop.aspx";
            //    }
            //    else//没有开通
            //    {
            //        shopUrl = EyouSoft.Common.Domain.UserBackCenter + "/supplymanage/freeshop.aspx";
            //    }
            //}
            return  EyouSoft.Common.Domain.UserBackCenter + "/SystemSet/CompanyInfoSet.aspx" ;
        }
    }
}
