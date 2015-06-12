using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.SSOComponent.Entity;

namespace EyouSoft.SSOComponent
{
    /// <summary>
    /// 用户登陆处理接口
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    public interface IUserLogin
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <returns>用户信息</returns>
        UserInfo UserLoginAct(string UserName, string PWD, string LoginTicket);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="PwdType">密码类型</param>
        /// <returns>用户信息</returns>
        UserInfo UserLoginAct(string UserName, string PWD, string LoginTicket, PasswordType PwdType);
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>是否成功</returns>
        bool UserLogout(string UID);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>用户信息</returns>
        UserInfo GetUserInfo(string UID);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="User">用户信息</param>
        void UpdateUserInfo(UserInfo User);
    }
    /// <summary>
    /// 用户登陆远程处理接口
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-6-1
    public interface IUserLoginService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="RedirectUrl">跳转地址</param>
        /// <returns>SSO返回信息</returns>
        SSOResponse UserLoginAct(string UserName, string PWD, string LoginTicket, string RedirectUrl);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="PwdType">密码类型</param>
        /// <returns></returns>
        SSOResponse UserLoginPassword(string UserName, string PWD, string LoginTicket, PasswordType PwdType);
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <param name="RedirectUrl">跳转地址</param>
        /// <returns>SSO返回信息</returns>
        SSOResponse UserLogout(string UID, string RedirectUrl);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>用户信息</returns>
        UserInfo GetUserInfo(string UID);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="User">用户信息</param>
        void UpdateUserInfo(UserInfo User);
    }

    /// <summary>
    /// 管理员登陆处理接口
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    public interface IMasterLogin
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <returns>管理员信息</returns>
        MasterUserInfo MasterLogin(string UserName, string PWD);
        /// <summary>
        /// 管理员退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <param name="PWD">用户密码</param>
        /// <returns></returns>
        void MasterLogout(string UID);
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="User">用户编号</param>
        void UpdateMasterInfo(MasterUserInfo User);
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>管理员信息</returns>
        MasterUserInfo GetMasterInfo(string UID);
    }
}