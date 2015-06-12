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
using EyouSoft.SSOComponent.Entity;
using EyouSoft.Security.Membership;
using EyouSoft.Common;

namespace SiteOperationsCenter
{
    public partial class SetCookie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = -1;

            string username = Utils.InputText(Request.QueryString["username"]);
            string token = Utils.InputText(Request.QueryString["logincookie"]);

            bool isValid = EyouSoft.Security.Membership.UserProvider.IsSSOTokenValid(token, username);
            //MasterUserInfo masterUser = new UserProvider().GetMaster();

            if (isValid)
            {
                EyouSoft.Security.Membership.UserProvider.GenerateYunYingUserLoginCookies(token, username);
            }

            Response.Clear();
            Response.Write(isValid);
            Response.End();
        }
    }
}
