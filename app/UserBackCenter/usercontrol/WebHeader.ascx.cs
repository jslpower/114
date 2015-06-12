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

namespace UserBackCenter.usercontrol
{
    public partial class WebHeader : System.Web.UI.UserControl
    {
        protected string LoginMsg = string.Empty;
        protected string userpubliccenter = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.Common.Control.BasePage page = (EyouSoft.Common.Control.BasePage)this.Page;
            EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
            bool IsLogin = page.IsLogin;
            userpubliccenter = Domain.UserPublicCenter;
            if (IsLogin)
            {
                UserInfoModel = page.SiteUserInfo;
                LoginMsg = "您好，" + UserInfoModel.UserName + "，欢迎来同业114! &nbsp;<a href=\"" + Utils.GetLogoutUrl(Domain.UserPublicCenter + "/Default.aspx") + "\">退出</a>";
            }
            else {
                LoginMsg = string.Format(
                    "<a href=\"{0}/Register/Login.aspx\">请登录</a>&nbsp;&nbsp;<a href=\"{1}/Register/CompanyUserRegister.aspx\">免费注册</a>",
                    Domain.UserPublicCenter,
                    Domain.UserPublicCenter);
            }
        }
    }
}