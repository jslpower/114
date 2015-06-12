using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.sso
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Request["txtUsername"]);
            Session.Add("UID", Request["txtUsername"]);
            //string domain = "";
            //for (int i = 1; i < Request.Url.Host.Split('.').Length; i++)
            //{
            //    domain += Request.Url.Host.Split('.')[i] + ".";
            //}
            //if (domain.EndsWith("."))
            //    domain = domain.Substring(0, domain.Length - 1);
            //Response.Cookies["ssotest"].Value = "login";
            //Response.Cookies["ssotest"].Expires = DateTime.Now.AddDays(1);
            //Response.Cookies["ssotest"].Domain = domain;
            //Response.Write("<iframe src=\"http://www.lifepop.com/sso/sso.ashx?login=\" style=\"display:none\"></iframe>");
            //Response.Write("<iframe src=\"http://www.1111.com/sso/sso.ashx?login=\" style=\"display:none\"></iframe>");
            //Response.Write("<iframe src=\"http://www.ty.com/sso/sso.ashx?login=\" style=\"display:none\"></iframe>");
        }
    }
}
