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
using EyouSoft.SSOComponent.SSORemote;
using WebSampleHelper;
using Discuz.Toolkit;
using System.Collections.Specialized;

namespace PassportCenter
{
    public partial class crossdomain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string u = Utils.InputText(Request.QueryString["u"]);
            string p = Utils.InputText(Request.QueryString["p"]);
            string vc = Utils.InputText(Request.QueryString["vc"]);
            string callback = Utils.InputText(Request.QueryString["callback"]);
            int re = Utils.GetInt(Request.QueryString["re"]);//是否记住登陆
            bool isRe = re == 1 ? true : false;

            bool isUserValid = false;

            string token = EyouSoft.Security.Membership.UserProvider.GenerateSSOToken(u);
            string encodeedToken = UserProvider.EncodeSSOToken(token);
            EyouSoft.SSOComponent.Entity.UserInfo userInfo = null;

            isUserValid = new UserProvider().UserLogin(u, p, token, out userInfo);



            if (!isUserValid)
            {
                Response.Clear();
                Response.Write(";" + callback + "({m:'用户名或密码不正确'});");
                Response.End();
            }
            else
            {
                if (userInfo != null)
                {
                    if (userInfo.IsEnable == false)
                    {
                        Response.Clear();
                        Response.Write(";" + callback + "({m:'您的账户已停用或已过期，请联系管理员'});");
                        Response.End();
                    }
                    else
                    {
                        LoginCommunity(userInfo);
                    }
                }
            }

            System.Text.StringBuilder strb = new System.Text.StringBuilder();

            bool isHaveOtherHost = false;
            string currentHost = HttpContext.Current.Request.UrlReferrer.Authority;

            if (Domain.SeniorOnlineShop.IndexOf(currentHost) == -1
                && Domain.UserBackCenter.IndexOf(currentHost) == -1
                && Domain.UserPublicCenter.IndexOf(currentHost) == -1
                && Domain.FileSystem.IndexOf(currentHost) == -1
                && Domain.IMFrame.IndexOf(currentHost) == -1
                && (Domain.UserClub.IndexOf(currentHost) == -1))
            {
                isHaveOtherHost = true;
            }

            string html = string.Empty;
            u = HttpUtility.UrlEncode(u, System.Text.Encoding.UTF8);
            p = UserProvider.EncodePassword(p);
            if (!isHaveOtherHost)
            {

                string arr = "['{0}','{1}','{2}','{3}','{4}']";
                html = string.Format(arr, 
                    
                    Domain.UserClub + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u,
                    Domain.SeniorOnlineShop + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u+"&p="+p+"&re="+re,
                    Domain.UserBackCenter + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re,
                    Domain.UserPublicCenter + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re,
                    Domain.FileSystem + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re
                    );

            }
            else
            {
                string arr = "['{0}','{1}','{2}','{3}','{4}','{5}']";
                html = string.Format(arr,
                    Domain.UserClub + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u,
                    "http://" + currentHost + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u,
                    Domain.SeniorOnlineShop + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re,
                    Domain.UserBackCenter + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re,
                    Domain.UserPublicCenter + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re,
                    Domain.FileSystem + "/SetCookie.aspx?logincookie=" + encodeedToken + "&username=" + u + "&p=" + p + "&re=" + re
                    
                    );
            }
            //string html = "['']";
            Response.Clear();
            Response.Write(";" + callback + "({h:" + html + "});");
            Response.End();
        }

        private void LoginCommunity(EyouSoft.SSOComponent.Entity.UserInfo UserModel)
        {
            int num = 100;
            try
            {
                DiscuzSession session = DiscuzSessionHelper.GetSession();
                int userID = session.GetUserID(UserModel.UserName);
                string str = "tongye114.com";
                NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("DiscuzAPI");
                if (section != null)
                {
                    str = string.IsNullOrEmpty(section["CookieDomain"]) ? "tongye114.com" : section["CookieDomain"];
                    num = string.IsNullOrEmpty(section["CookieExpires"]) ? 100 : int.Parse(section["CookieExpires"]);
                }
                session.Login(userID, UserModel.PassWordInfo.NoEncryptPassword, false, num, str);
            }
            catch(Exception e) { }
        }
    }
}
