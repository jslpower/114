using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.sso
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string domain = "";
            for (int i = 1; i < Request.Url.Host.Split('.').Length; i++)
            {
                domain += Request.Url.Host.Split('.')[i] + ".";
            }
            if (domain.EndsWith("."))
                domain = domain.Substring(0, domain.Length - 1);
            HttpCookie cookie = Request.Cookies["ssotest"];
            cookie.Value = "logout";
            cookie.Expires = DateTime.Today.AddDays(-1);
            cookie.Domain = domain;
            Response.AppendCookie(cookie);
            Response.Write("<iframe src=\"http://www.lifepop.com/sso/sso.ashx?logout=\" style=\"display:none\"></iframe>");
            Response.Write("<iframe src=\"http://www.1111.com/sso/sso.ashx?logout=\" style=\"display:none\"></iframe>");
            Response.Write("<iframe src=\"http://www.ty.com/sso/sso.ashx?logout=\" style=\"display:none\"></iframe>");
            Session.Remove("UID");
        }
    }
}
