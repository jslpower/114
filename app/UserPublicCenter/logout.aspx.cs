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

namespace UserPublicCenter
{
    /// <summary>
    /// 用户退出
    /// </summary>
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string backurl = Utils.InputText(Request.QueryString["backurl"]);

            LogoutCommunity();
            EyouSoft.Security.Membership.UserProvider.UserLogout();
        }

        /// <summary>
        /// 同步退出同业社区
        /// </summary>
        private void LogoutCommunity()
        {
            string CookieDomain = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("DiscuzAPI", "CookieDomain");
            if (string.IsNullOrEmpty(CookieDomain))
                CookieDomain = "tongye114.com";

            try
            {
                Discuz.Toolkit.DiscuzSession ds = WebSampleHelper.DiscuzSessionHelper.GetSession();
                if (ds != null)
                {
                    ds.Logout(CookieDomain);
                    ds.session_info = null;
                }
            }
            catch
            {
                return;
            }
        }
    }
}
