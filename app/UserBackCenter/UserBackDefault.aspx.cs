using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

namespace UserBackCenter
{
    /// <summary>
    /// 首页
    /// 罗丽娥   2010-07-27
    /// </summary>
    public partial class UserBackDefault : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断用户是否登录，如果没有登录跳转到登录页面，如果有登录，初始化用户对象UserInfoModel
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
            }
            else
            {
                if (!IsCompanyCheck)
                {
                    Utils.ShowAndRedirect("对不起，您的账号未审核!", Domain.UserPublicCenter + "/Default.aspx");
                    return;
                }
                if (SiteUserInfo != null)
                {
                    this.userbackdefault1.UserInfoModel = SiteUserInfo;
                }
            }
        }
    }
}
