using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserBackCenter.Tickets
{
    public partial class Default : EyouSoft.Common.Control.BackPage
    {
        protected string LoginUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(EyouSoft.Common.Domain.UserBackCenter + "/Tickets/Default.aspx", "您需要登录后才能查询有关机票信息!");
        }
    }
}
