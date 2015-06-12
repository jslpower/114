using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;

namespace FileSystem
{
    public class Global : EyouSoft.WEB.EyouSoftApplication
    {
        void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpCookie cookie = request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookie_name);
                request.Cookies.Add(cookie);
            }
            cookie.Value = cookie_value;
            request.Cookies.Set(cookie);
        }

        protected new void Application_BeginRequest(object sender, EventArgs e)
        {
            base.Application_BeginRequest(sender, e);
            /* Fix for the Flash Player Cookie bug in Non-IE browsers.
		     * Since Flash Player always sends the IE cookies even in FireFox
		     * we have to bypass the cookies by sending the values as part of the POST or GET
		     * and overwrite the cookies with the passed in values.
		     * 
		     * The theory is that at this point (BeginRequest) the cookies have not been read by
		     * the Session and Authentication logic and if we update the cookies here we'll get our
		     * Session and Authentication restored correctly
		     */

            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SESSIONID";
                string sso_param_name = EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO;
                string sso_cookie_name = EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO;
                string u_param_name = EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName;
                string u_cookie_name = EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName;
                string p_param_name = EyouSoft.Security.Membership.UserProvider.LoginCookie_Password;
                string p_cookie_name = EyouSoft.Security.Membership.UserProvider.LoginCookie_Password;

                string mastersso_param_name = EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_SSO;
                string mastersso_cookie_name = EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_SSO;
                string masteru_parama_name = EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_UserName;
                string masteru_cookie_name = EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_UserName;

                HttpRequest request = HttpContext.Current.Request;

                //ASPSESSIONID.
                if (request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, request.Form[session_param_name]);
                }
                else if (request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, request.QueryString[session_param_name]);
                }

                //SSO Cookie
                if (request.Form[sso_param_name] != null)
                {
                    UpdateCookie(sso_cookie_name, request.Form[sso_param_name]);
                }
                else if (request.QueryString[sso_param_name] != null)
                {
                    UpdateCookie(sso_cookie_name, request.QueryString[sso_param_name]);
                }

                //U Cookie.
                if (request.Form[u_param_name] != null)
                {
                    UpdateCookie(u_cookie_name, request.Form[u_param_name]);
                }
                else if (request.QueryString[u_param_name] != null)
                {
                    UpdateCookie(u_cookie_name, request.QueryString[u_param_name]);
                }

                //password Cookie.
                if (request.Form[p_param_name] != null)
                {
                    UpdateCookie(p_cookie_name, request.Form[p_param_name]);
                }
                else if (request.QueryString[p_param_name] != null)
                {
                    UpdateCookie(p_cookie_name, request.QueryString[p_param_name]);
                }

                //MasterSSO Cookie.
                if (request.Form[mastersso_param_name] != null)
                {
                    UpdateCookie(mastersso_cookie_name, request.Form[mastersso_param_name]);
                }
                else if (request.QueryString[mastersso_param_name] != null)
                {
                    UpdateCookie(mastersso_cookie_name, request.QueryString[mastersso_param_name]);
                }

                //masterUser Cookie.
                if (request.Form[masteru_parama_name] != null)
                {
                    UpdateCookie(masteru_cookie_name, request.Form[masteru_parama_name]);
                }
                else if (request.QueryString[masteru_parama_name] != null)
                {
                    UpdateCookie(masteru_cookie_name, request.QueryString[masteru_parama_name]);
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Session");
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Forms Authentication");
            }
        }
    }
}