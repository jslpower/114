using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.SSOComponent;
using EyouSoft.SSOComponent.Entity;

namespace EyouSoft.Service.SSO
{
    /// <summary>
    /// 用户登陆验证中心
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。

    // [System.Web.Script.Services.ScriptService]
    public class SSOFlow : System.Web.Services.WebService, IUserLoginService
    {
        SSOComponent.UserLogin CurrentUser = null;
        public SSOFlow()
        {
            CurrentUser = new SSOComponent.UserLogin();
        }

        #region IUserLogin 成员
        /// <summary>
        /// 根据用户名、凭据值获取用户信息
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <returns>SSO返回信息</returns>
        [WebMethod]
        public SSOResponse GetTicketUser(string UserName, string LoginTicket)
        {
            SSOResponse response1 = new SSOResponse();
            UserInfo User = CurrentUser.UserLoginAct(UserName, "", LoginTicket);
            SSOResponse response = new SSOResponse();
            if (User != null)
            {
                response.IsValid = true;
                response.SSOScript = EyouSoft.Common.SerializationHelper.ConvertJSON<UserInfo>(User);
                response.UserInfo = User;
            }
            return response;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="RedirectUrl">跳转地址</param>
        /// <returns>SSO返回信息</returns>
        [WebMethod]
        public SSOResponse UserLoginAct(string UserName, string PWD, string LoginTicket, string RedirectUrl)
        {
            UserInfo User = CurrentUser.UserLoginAct(UserName, PWD, LoginTicket);
            SSOResponse response = new SSOResponse();
            if (User != null)
            {
                response.IsValid = true;
                response.UserInfo = User;
            }
            return response;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="PwdType">密码类型</param>
        /// <returns>SSO返回信息</returns>
        [WebMethod]
        public SSOResponse UserLoginPassword(string UserName, string PWD, string LoginTicket, PasswordType PwdType)
        {
            UserInfo User = CurrentUser.UserLoginAct(UserName, PWD, LoginTicket, PwdType);
            SSOResponse response = new SSOResponse();
            if (User != null)
            {
                response.IsValid = true;
                response.UserInfo = User;
            }
            return response;
        }
        /// <summary>
        /// 用户退出

        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <param name="RedirectUrl">跳转地址</param>
        /// <returns>SSO返回信息</returns>
        [WebMethod]
        public SSOResponse UserLogout(string UID, string RedirectUrl)
        {
            SSOResponse response = new SSOResponse();
            CurrentUser.UserLogout(UID);
            return response;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>用户信息</returns>
        [WebMethod]
        public SSOResponse GetUserInfo(string UID)
        {
            SSOResponse response = new SSOResponse();
            response.SSOScript = EyouSoft.Common.SerializationHelper.ConvertJSON<UserInfo>(CurrentUser.GetUserInfo(UID));
            return response;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sso">sso信息</param>
        [WebMethod]
        public void UpdateUserInfo(SSOResponse sso)
        {
            if (!string.IsNullOrEmpty(sso.SSOScript))
            {
                UserInfo User = EyouSoft.Common.SerializationHelper.InvertJSON<UserInfo>(sso.SSOScript);
                CurrentUser.UpdateUserInfo(User);
            }
        }

        #endregion
    }
}
