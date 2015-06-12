using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.Register
{
    /// <summary>
    /// 平台登陆页
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class Login : EyouSoft.Common.Control.FrontPage
    {
        protected string  Message = " 你目前进行的操作，需要“登录”后才能继续……";
        protected string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["nlm"]))
            {
                Message = Request.QueryString["nlm"];
            }
            UserName= EyouSoft.Security.Membership.UserProvider.GetCookie_UserName();
        }
    }
}
