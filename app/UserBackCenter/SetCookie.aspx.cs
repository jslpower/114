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
using EyouSoft.Security.Membership;

namespace UserBackCenter
{
    public partial class SetCookie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = -1;

            string username = Utils.InputText(Request.QueryString["username"]);
            string token = UserProvider.DecodeSSOToken(Utils.InputText(Request.QueryString["logincookie"]));

            int re = Utils.GetInt(Request.QueryString["re"]);//是否记住登陆
            bool isRe = re == 1 ? true : false;
            string p = UserProvider.DecodePassword(Utils.InputText(Request.QueryString["p"]));

            bool isValid = EyouSoft.Security.Membership.UserProvider.IsSSOTokenValid(token, username);

            bool isRemoteLogin = new UserProvider().RemoteUserLogin(username, string.Empty, token);
            if (isRemoteLogin)
            {
                isValid = true;
                if (!isRe)
                {
                    EyouSoft.Security.Membership.UserProvider.GenerateUserLoginCookies(token, username);
                }
                else
                {
                    EyouSoft.Security.Membership.UserProvider.GenerateUserLoginCookies(token, username, p);
                }
            }
            else
            {
                isValid = false;
            }

            Response.Clear();
            Response.AddHeader("P3P", "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
            Response.Write(isValid);
            Response.End();
        }
    }
}
