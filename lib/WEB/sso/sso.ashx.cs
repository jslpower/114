using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.sso
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    public class sso : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            System.Threading.Thread.Sleep(2000);
            string domain = "";
            for (int i = 1; i < context.Request.Url.Host.Split('.').Length; i++)
            {
                domain += context.Request.Url.Host.Split('.')[i] + ".";
            }
            if (domain.EndsWith("."))
                domain = domain.Substring(0, domain.Length - 1);
            context.Response.AddHeader("P3P", "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
            if (context.Request["login"] != null)
            {
                context.Response.Cookies["ssotest"].Value = "login";
                context.Response.Cookies["ssotest"].Expires = DateTime.Today.AddDays(1);
                context.Response.Cookies["ssotest"].Domain = domain;
            }
            else
            {
                HttpCookie cookie = context.Request.Cookies["ssotest"];
                cookie.Expires = DateTime.Today.AddDays(-1);
                cookie.Domain = domain;
                cookie.Value = "logout";
                context.Response.AppendCookie(cookie);
            }
            context.Response.Write("alert('" + context.Request.UrlReferrer.ToString() + "');");
            //context.Response.Write("ok");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
