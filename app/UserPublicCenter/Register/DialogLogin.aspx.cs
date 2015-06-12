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

namespace UserPublicCenter.Register
{
    public partial class DialogLogin : EyouSoft.Common.Control.FrontPage
    {
        protected string Message = " 你目前进行的操作，需要“登录”后才能继续……";
        protected string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["nlm"]))
            {
                Message = Request.QueryString["nlm"];
            }
            UserName = EyouSoft.Security.Membership.UserProvider.GetCookie_UserName();
        }
    }
}
