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
using EyouSoft.Common.Control;
using EyouSoft.Security.Membership;

namespace SiteOperationsCenter
{
    public partial class logout : YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string backurl = Utils.InputText(Request.QueryString["backurl"]);

            new EyouSoft.Security.Membership.UserProvider().MasterLogout(MasterUserInfo.UserName);

            UserProvider.RedirectLoginOpenTopPageYunYing();
        }
    }
}
