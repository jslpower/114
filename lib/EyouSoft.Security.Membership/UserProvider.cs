using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.EncryptUtility;
using EyouSoft.SSOComponent;
using EyouSoft.SSOComponent.Entity;
using System.Web;

/*
 * 用户信息相关操作
 * 蒋胜蓝  2010-6-1
 * 
 */

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 凭据类型
    /// </summary>
    enum TicketType
    {
        /// <summary>
        /// 用户
        /// </summary>
        UserCookieName,
        /// <summary>
        /// 管理员
        /// </summary>
        MasterCookieName
    }

    /// <summary>
    /// 用户账号操作
    /// 1.系统登陆、退出
    /// 2.权限验证
    /// 3.用户更新
    /// </summary>
    public class UserProvider
    {
        /// <summary>
        /// 加密KEY
        /// </summary>
        private const string _Key = "92$#@!#@5tr%u8wsf]543$,23{e7w%$#";
        /// <summary>
        /// 解密IV
        /// </summary>
        private const string _IV = "!54~1)e74&m3+-q#";
        /// <summary>
        /// 加解密对象
        /// </summary>
        private HashCrypto crypto = null;
        /// <summary>
        /// COOKIE有效时间
        /// </summary>
        const int CookieExpireTime = 12;
        /// <summary>
        /// 凭据有效时间
        /// </summary>
        const int TicketExpireTime = 12;
        /// <summary>
        /// 用户COOKIE名称
        /// </summary>
        const string UserCookieName = "pfUser";
        /// <summary>
        /// 管理员COOKIE名称
        /// </summary>
        const string MasterCookieName = "pfmUser";

        public string RedirectUrl = "";

        public UserProvider()
        {
            crypto = new HashCrypto();
            crypto.Key = _Key;
            crypto.IV = _IV;
        }

        #region 平台用户
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">密码</param>
        public void UserLogin(string UserName,string PWD)
        {
            EyouSoft.SSOComponent.Entity.DecryptLoginTicket LoginTicket = new EyouSoft.SSOComponent.Entity.DecryptLoginTicket();
            LoginTicket.ExpireTime = DateTime.Now.AddHours(TicketExpireTime);
            LoginTicket.UserName = UserName;
            string strLoginTicket = CreateLoginTicket(LoginTicket);
            EyouSoft.SSOComponent.Entity.SSOResponse response = new EyouSoft.SSOComponent.Remote.UserLogin().UserLoginAct(UserName, PWD, strLoginTicket, RedirectUrl);
            if (response != null)
            {
                if (!response.IsValid)
                    return;                
                EyouSoft.SSOComponent.Entity.UserInfo User = response.UserInfo;
                HttpCookie hc = new HttpCookie(UserCookieName);
                hc.Values.Add("UID", crypto.DESEncrypt(User.ID.ToString()));
                hc.Values.Add("LoginTicket", strLoginTicket);
                hc.Domain = "asdf";
                hc.Expires = DateTime.Now.AddHours(CookieExpireTime);
                HttpContext.Current.Response.Cookies.Add(hc);

            }
        }
        /// <summary>
        /// API用户登陆
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">密码</param>
        /// <param name="PwdType">密码类型</param>
        public void APIUserLogin(string UserName, string PWD, PasswordType PwdType)
        {
            EyouSoft.SSOComponent.Entity.DecryptLoginTicket LoginTicket = new EyouSoft.SSOComponent.Entity.DecryptLoginTicket();
            LoginTicket.ExpireTime = DateTime.Now.AddHours(TicketExpireTime);
            LoginTicket.UserName = UserName;
            string strLoginTicket = CreateLoginTicket(LoginTicket);
            EyouSoft.SSOComponent.Entity.SSOResponse response = new EyouSoft.SSOComponent.Remote.UserLogin().UserLoginPassword(UserName, PWD, strLoginTicket, PwdType);
            if (response != null)
            {
                if (!response.IsValid)
                    return;
                EyouSoft.SSOComponent.Entity.UserInfo User = response.UserInfo;
                HttpCookie hc = new HttpCookie(UserCookieName);
                hc.Values.Add("UID", crypto.DESEncrypt(User.ID.ToString()));
                hc.Values.Add("LoginTicket", strLoginTicket);
                hc.Domain = "asdf";
                HttpContext.Current.Response.Cookies.Add(hc);
            }
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        public void UserLogout()
        {
            EyouSoft.SSOComponent.Entity.UserInfo User = GetUser();
            EyouSoft.SSOComponent.Entity.SSOResponse response = new EyouSoft.SSOComponent.Remote.UserLogin().UserLogout(User.ID, RedirectUrl);            
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public EyouSoft.SSOComponent.Entity.UserInfo GetUser()
        {
            EyouSoft.SSOComponent.Entity.LocalUserInfo LocalUser = GetLoginTicket(TicketType.UserCookieName);

            if (LocalUser == null) { return null; }

            if (LocalUser.DecryptLoginTicket.ExpireTime > DateTime.Now)
                return null;
            EyouSoft.SSOComponent.Entity.UserInfo User = new EyouSoft.SSOComponent.Remote.UserLogin().GetUserInfo(LocalUser.UID);
            if (ValidateUser(User))
            {
                return User;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        public void UpdateUser(EyouSoft.Model.CompanyStructure.CompanyUser CompanyUser)
        {
            EyouSoft.SSOComponent.Entity.UserInfo User = GetUser();
            User.ID = CompanyUser.ID;
            //new EyouSoft.SSOComponent.Remote.UserLogin().UpdateUserInfo(User);
        }
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="User">用户信息</param>
        /// <returns></returns>
        private bool ValidateUser(EyouSoft.SSOComponent.Entity.UserInfo User)
        {
            try
            {
                if (User == null)
                    return false;
                EyouSoft.SSOComponent.Entity.LocalUserInfo LocalUser = GetLoginTicket(TicketType.UserCookieName);
                if (LocalUser.LoginTicket == User.LoginTicket)
                {

                    if (LocalUser.DecryptLoginTicket.ExpireTime > DateTime.Now ||
                        User.UserName.ToString() != LocalUser.DecryptLoginTicket.UserName)
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 验证用户权限
        /// </summary>
        /// <param name="PermissionID">权限号</param>
        /// <returns></returns>
        public bool CheckGrant(int PermissionID)
        {
            EyouSoft.SSOComponent.Entity.UserInfo User = GetUser();
            return User.PermissionList.Contains(PermissionID);
        }
        /// <summary>
        /// 创建登录凭据
        /// </summary>
        /// <param name="LoginTicket">原始登录凭据</param>
        /// <returns></returns>
        private string CreateLoginTicket(EyouSoft.SSOComponent.Entity.DecryptLoginTicket LoginTicket)
        {
            return crypto.DESEncrypt(LoginTicket.UserName + "|" + LoginTicket.ExpireTime);
        }
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        private EyouSoft.SSOComponent.Entity.LocalUserInfo GetLoginTicket(TicketType ticket)
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies[ticket.ToString()];

            if (hc == null) { return null; }

            EyouSoft.SSOComponent.Entity.LocalUserInfo LocalUser = new EyouSoft.SSOComponent.Entity.LocalUserInfo();
            if (!string.IsNullOrEmpty(hc["UID"]))
                LocalUser.UID = crypto.DeDESEncrypt(hc["UID"].ToString());
            if (!string.IsNullOrEmpty(hc["LoginTicket"]))
            {
                LocalUser.LoginTicket = crypto.DeDESEncrypt(hc["LoginTicket"].ToString());
                LocalUser.DecryptLoginTicket = new EyouSoft.SSOComponent.Entity.DecryptLoginTicket();
                LocalUser.DecryptLoginTicket.UserName = LocalUser.LoginTicket.Split('|')[0];
                LocalUser.DecryptLoginTicket.ExpireTime = DateTime.Parse(LocalUser.LoginTicket.Split('|')[1]);
            }
            return LocalUser;
        }
        #endregion

        #region 系统管理员


        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <returns>管理员信息</returns>
        public MasterUserInfo MasterLogin(string UserName, string PWD)
        {
            EyouSoft.SSOComponent.Entity.DecryptLoginTicket LoginTicket = new EyouSoft.SSOComponent.Entity.DecryptLoginTicket();
            LoginTicket.ExpireTime = DateTime.Now.AddHours(TicketExpireTime);
            LoginTicket.UserName = UserName;
            string strLoginTicket = CreateLoginTicket(LoginTicket);
            MasterUserInfo MasterUser = new MaterLogin().MasterLogin(UserName, PWD);
            if (MasterUser == null)
                return null;
            HttpCookie hc = new HttpCookie(MasterCookieName);
            hc.Values.Add("UID", crypto.DESEncrypt(MasterUser.ID.ToString()));
            hc.Values.Add("LoginTicket", strLoginTicket);
            hc.Domain = "asdf";
            hc.Expires = DateTime.Now.AddHours(CookieExpireTime);
            HttpContext.Current.Response.Cookies.Add(hc);
            return MasterUser;
        }
        /// <summary>
        /// 管理员退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <param name="PWD">用户密码</param>
        /// <returns></returns>
        public void MasterLogout(string UID)
        {
            new MaterLogin().MasterLogout(UID);
        }
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="User">管理员信息</param>
        /// <returns></returns>
        private bool ValidateUser(EyouSoft.SSOComponent.Entity.MasterUserInfo User)
        {
            try
            {
                if (User == null)
                    return false;
                EyouSoft.SSOComponent.Entity.LocalUserInfo LocalUser = GetLoginTicket(TicketType.UserCookieName);
                if (LocalUser.LoginTicket == User.LoginTicket)
                {

                    if (LocalUser.DecryptLoginTicket.ExpireTime > DateTime.Now ||
                        User.UserName.ToString() != LocalUser.DecryptLoginTicket.UserName)
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 验证管理员权限
        /// </summary>
        /// <param name="PermissionID">权限号</param>
        /// <returns></returns>
        public bool CheckMasterGrant(int PermissionID)
        {
            EyouSoft.SSOComponent.Entity.MasterUserInfo User = GetMaster();
            return User.PermissionList.Contains(PermissionID);
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="User">用户编号</param>
        public void UpdateMasterInfo(MasterUserInfo User)
        {
            LocalUserInfo LocalUser = GetLoginTicket(TicketType.MasterCookieName);
            new MaterLogin().UpdateMasterInfo(User);
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>管理员信息</returns>
        public MasterUserInfo GetMaster()
        {
            LocalUserInfo LocalUser = GetLoginTicket(TicketType.MasterCookieName);

            if (LocalUser == null) { return null; }

            MasterUserInfo MasterUser = new MaterLogin().GetMasterInfo(LocalUser.UID);
            if (ValidateUser(MasterUser))
            {
                return MasterUser;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
