using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Cache.Facade;
using EyouSoft.CacheTag;
using EyouSoft.SSOComponent;
using EyouSoft.SSOComponent.Entity;

namespace EyouSoft.SSOComponent.Remote
{
    /// <summary>
    /// 用户登陆处理(远程访问方式)
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    public class UserLogin : IUserLoginService
    {
        /// <summary>
        /// 远程处理对象，继承接口EyouSoft.SSOComponent.IUserLoginService
        /// </summary>
        EyouSoft.SSOComponent.SSORemote.SSOFlow RemoteLogin = null;
        /// <summary>
        /// 本地处理对象，继承接口EyouSoft.SSOComponent.IUserLogin
        /// </summary>
        EyouSoft.SSOComponent.UserLogin LocalLogin = null;

        public UserLogin()
        {
            RemoteLogin = new EyouSoft.SSOComponent.SSORemote.SSOFlow();
            LocalLogin = new EyouSoft.SSOComponent.UserLogin();
        }
        #region IUserLogin 成员
        /// <summary>
        /// 根据用户名、凭据值获取用户信息
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <returns>SSO返回信息</returns>
        public SSOResponse GetTicketUser(string UserName, string LoginTicket)
        {
            SSOResponse response = new SSOResponse();
            UserInfo User = new UserInfo();
            EyouSoft.SSOComponent.SSORemote.SSOResponse remoteResponse = RemoteLogin.GetTicketUser(UserName, LoginTicket);
            if (remoteResponse != null && !string.IsNullOrEmpty(remoteResponse.SSOScript))
            {
                response.UserInfo = EyouSoft.Common.SerializationHelper.InvertJSON<UserInfo>(remoteResponse.SSOScript);
                response.IsValid = remoteResponse.IsValid;
                if (response.UserInfo != null)
                {
                    //response.UserInfo = this.ConvertToUser(remoteResponse.UserInfo);
                    //SetCache();
                    LocalLogin.UpdateUserInfo(response.UserInfo);
                }
            }
            return response;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="RedirectUrl">跳转地址</param>
        /// <returns>SSO返回信息</returns>
        public SSOResponse UserLoginAct(string UserName, string PWD, string LoginTicket, string RedirectUrl)
        {            
            SSOResponse response = new SSOResponse();            
            EyouSoft.SSOComponent.SSORemote.SSOResponse remoteResponse = RemoteLogin.UserLoginAct(UserName, PWD, LoginTicket, RedirectUrl);
            if (remoteResponse!=null && !string.IsNullOrEmpty(remoteResponse.SSOScript))
            {
                response.UserInfo = EyouSoft.Common.SerializationHelper.InvertJSON<UserInfo>(remoteResponse.SSOScript);
                response.IsValid = remoteResponse.IsValid;
                if (response.UserInfo != null && response.UserInfo.IsEnable)
                {
                    //response.UserInfo = this.ConvertToUser(remoteResponse.UserInfo);
                    //SetCache();
                    LocalLogin.UpdateUserInfo(response.UserInfo);
                }
            }
            return response;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="PwdType">登录密码类型</param>
        /// <returns>SSO返回信息</returns>
        public SSOResponse UserLoginPassword(string UserName, string PWD, string LoginTicket, PasswordType PwdType)
        {
            SSOResponse response = new SSOResponse();
            SSORemote.PasswordType _pwdtype = (EyouSoft.SSOComponent.SSORemote.PasswordType)Convert.ToInt32(PwdType);
            EyouSoft.SSOComponent.SSORemote.SSOResponse remoteResponse = RemoteLogin.UserLoginPassword(UserName, PWD, LoginTicket, _pwdtype);
            if (remoteResponse != null && !string.IsNullOrEmpty(remoteResponse.SSOScript))
            {
                response.IsValid = remoteResponse.IsValid;
                response.UserInfo = EyouSoft.Common.SerializationHelper.InvertJSON<UserInfo>(remoteResponse.SSOScript);
                if (response.UserInfo != null && response.UserInfo.IsEnable)
                {
                    //response.UserInfo = this.ConvertToUser(remoteResponse.UserInfo);
                    //SetCache();
                    LocalLogin.UpdateUserInfo(response.UserInfo);
                }
            }
            return response;
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <param name="RedirectUrl">跳转地址</param>a
        /// <returns>SSO返回信息</returns>
        public SSOResponse UserLogout(string UID, string RedirectUrl)
        {
            //RemoveCache();
            LocalLogin.UserLogout(UID); 
            SSOResponse response = new SSOResponse();
            EyouSoft.SSOComponent.SSORemote.SSOResponse remoteResponse = RemoteLogin.UserLogout(UID, RedirectUrl);
            response.IsValid = remoteResponse.IsValid;
            return null;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>用户信息</returns>
        public SSOResponse GetUserInfo(string UID)
        {
            //GetCache(); 
            SSOResponse response = new SSOResponse();
            UserInfo User = LocalLogin.GetUserInfo(UID);           
            if (User == null)
            {
                EyouSoft.SSOComponent.SSORemote.SSOResponse remoteresponse = RemoteLogin.GetUserInfo(UID);

                if (remoteresponse != null && !string.IsNullOrEmpty(remoteresponse.SSOScript))
                {
                    User = EyouSoft.Common.SerializationHelper.InvertJSON<UserInfo>(remoteresponse.SSOScript);
                    if(User!=null)
                        LocalLogin.UpdateUserInfo(User);
                }
            }
            response.UserInfo = User;
            return response;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sso">sso信息</param>
        public void UpdateUserInfo(SSOResponse sso)
        {
            //SetCache();
            if (sso == null)
                return;
            LocalLogin.UpdateUserInfo(sso.UserInfo);

            EyouSoft.SSOComponent.SSORemote.SSOResponse remoteresponse = new EyouSoft.SSOComponent.SSORemote.SSOResponse();
            remoteresponse.SSOScript = EyouSoft.Common.SerializationHelper.ConvertJSON<UserInfo>(sso.UserInfo);
            RemoteLogin.UpdateUserInfo(remoteresponse);
        }
        /*
        private EyouSoft.SSOComponent.SSORemote.UserInfo ConvertToSSOUser(UserInfo User)
        {
            EyouSoft.SSOComponent.SSORemote.UserInfo RemoteUser = new EyouSoft.SSOComponent.SSORemote.UserInfo();
            RemoteUser.ID = User.ID;
            RemoteUser.AreaId = User.AreaId;
            RemoteUser.CityId = User.CityId;
            RemoteUser.CompanyID = User.CompanyID;
            RemoteUser.DepartId = User.DepartId;
            RemoteUser.DepartName = User.DepartName;
            RemoteUser.IsAdmin = User.IsAdmin;
            RemoteUser.IsEnable = User.IsEnable;
            RemoteUser.LoginTicket = User.LoginTicket;
            RemoteUser.OpUserId = User.OpUserId;
            RemoteUser.PermissionList = User.PermissionList;
            RemoteUser.ProvinceId = User.ProvinceId;
            RemoteUser.UserName = User.UserName;
            foreach (EyouSoft.Model.CompanyStructure.CompanyType companyType in User.CompanyRole.RoleItems)
            {
                RemoteUser.CompanyRole.SetRole((EyouSoft.SSOComponent.SSORemote.CompanyType)Convert.ToInt32(companyType));
            }
            RemoteUser.ContactInfo.ContactName = User.ContactInfo.ContactName;
            RemoteUser.ContactInfo.ContactSex = (EyouSoft.SSOComponent.SSORemote.Sex)Convert.ToInt32(User.ContactInfo.ContactSex);
            RemoteUser.ContactInfo.Email = User.ContactInfo.Email;
            RemoteUser.ContactInfo.Fax = User.ContactInfo.Fax;
            RemoteUser.ContactInfo.Mobile = User.ContactInfo.Mobile;
            RemoteUser.ContactInfo.MQ = User.ContactInfo.MQ;
            RemoteUser.ContactInfo.MSN = User.ContactInfo.MSN;
            RemoteUser.ContactInfo.QQ = User.ContactInfo.QQ;
            RemoteUser.ContactInfo.Tel = User.ContactInfo.Tel;
            return RemoteUser;
        }
        private EyouSoft.SSOComponent.Entity.UserInfo ConvertToUser(EyouSoft.SSOComponent.SSORemote.UserInfo RemoteUser)
        {
            UserInfo User = new UserInfo();
            User.ID = RemoteUser.ID;
            User.AreaId = RemoteUser.AreaId;
            User.CityId = RemoteUser.CityId;
            User.CompanyID = RemoteUser.CompanyID;
            User.DepartId = RemoteUser.DepartId;
            User.DepartName = RemoteUser.DepartName;
            User.IsAdmin = RemoteUser.IsAdmin;
            User.IsEnable = RemoteUser.IsEnable;
            User.LoginTicket = RemoteUser.LoginTicket;
            User.OpUserId = RemoteUser.OpUserId;
            User.PermissionList = RemoteUser.PermissionList;
            User.ProvinceId = RemoteUser.ProvinceId;
            User.UserName = RemoteUser.UserName;
            foreach (EyouSoft.SSOComponent.SSORemote.CompanyType companyType in RemoteUser.CompanyRole.RoleItems)
            {
                User.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)Convert.ToInt32(companyType));
            }

            User.ContactInfo.ContactName = RemoteUser.ContactInfo.ContactName;
            User.ContactInfo.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)Convert.ToInt32(RemoteUser.ContactInfo.ContactSex);
            User.ContactInfo.Email = RemoteUser.ContactInfo.Email;
            User.ContactInfo.Fax = RemoteUser.ContactInfo.Fax;
            User.ContactInfo.Mobile = RemoteUser.ContactInfo.Mobile;
            User.ContactInfo.MQ = RemoteUser.ContactInfo.MQ;
            User.ContactInfo.MSN = RemoteUser.ContactInfo.MSN;
            User.ContactInfo.QQ = RemoteUser.ContactInfo.QQ;
            User.ContactInfo.Tel = RemoteUser.ContactInfo.Tel;
            return User;
        }*/
        #endregion
    }
}

