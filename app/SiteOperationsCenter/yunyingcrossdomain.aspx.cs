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
using EyouSoft.Security.Membership;
using EyouSoft.SSOComponent.Entity;
using EyouSoft.Common;

namespace PassportCenter
{
    public partial class yunyingcrossdomain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string u = Utils.InputText(Request.QueryString["u"]);
            string p = Utils.InputText(Request.QueryString["p"]);
            string vc = Utils.InputText(Request.QueryString["vc"]);
            string callback = Utils.InputText(Request.QueryString["callback"]);

            bool isUserValid = false;

            string token = EyouSoft.Security.Membership.UserProvider.GenerateSSOToken(u);

            MasterUserInfo masterUser = new UserProvider().MasterLogin(u, p);
            isUserValid = masterUser != null ? true : false;

            if (!isUserValid)
            {
                Response.Clear();
                Response.Write(";" + callback + "({m:'用户名或密码不正确'});");
                Response.End();
            }
            else
            {
                if (masterUser.IsDisable == true)
                {
                    Response.Clear();
                    Response.Write(";" + callback + "({m:'您的账户已停用或已过期，请联系管理员'});");
                    Response.End();
                }
            }

            token = HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8);


            EyouSoft.Security.Membership.UserProvider.GenerateYunYingUserLoginCookies(token, u);

            System.Text.StringBuilder strb = new System.Text.StringBuilder();

            //Domain.SiteOperationsCenter + "/SetCookie.aspx?logincookie=" + token + "&username=" + u,
            string arr = "['{0}']";
            string html = string.Format(arr, 
                Domain.FileSystem + "/SetCookie.aspx?type=1&logincookie=" + token + "&username=" + u);
            Response.Clear();
            Response.Write(";" + callback + "({h:" + html + "});");
            Response.End();
        }
    }
}
