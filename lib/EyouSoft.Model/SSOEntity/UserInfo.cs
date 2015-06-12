using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.SSOComponent.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    [Serializable]
    public class UserInfo
    {
        EyouSoft.Model.CompanyStructure.CompanyRole _CompanyRole = new EyouSoft.Model.CompanyStructure.CompanyRole();
        EyouSoft.Model.CompanyStructure.ContactPersonInfo _ContactPersonInfo = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
        EyouSoft.Model.CompanyStructure.PassWord _passwordinfo = new EyouSoft.Model.CompanyStructure.PassWord();
        #region 属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 其它平台用户编号
        /// </summary>
        public int OpUserId { get; set; }
        /// <summary>
        /// 用户公司类型
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyRole CompanyRole { get { return _CompanyRole; } /*set { _CompanyRole = value; }*/  }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }    
        /// <summary>
        /// 联系信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactInfo { get { return _ContactPersonInfo; } /*set { _ContactPersonInfo = value; } */}
        /// <summary>
        /// 用户密码
        /// </summary>
        public EyouSoft.Model.CompanyStructure.PassWord PassWordInfo { get { return this._passwordinfo; } }
        /// <summary>
        /// 角色权限列表
        /// </summary>
        public int[] PermissionList { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 用户区域ID
        /// </summary>
        public int[] AreaId { get; set; }
        /// <summary>
        /// 登录凭据值
        /// </summary>
        public string LoginTicket { get; set; }
        #endregion
    }

    /// <summary>
    /// 登录实体类
    /// </summary>
    public class LocalUserInfo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UID
        {
            get;
            set;
        }
        /// <summary>
        /// 登录凭据值
        /// </summary>
        public string LoginTicket { get; set; }
        /// <summary>
        /// 原始登录凭据值
        /// </summary>
        public DecryptLoginTicket DecryptLoginTicket { get; set; }
    }

    /// <summary>
    /// 原始登录凭据
    /// </summary>
    public class DecryptLoginTicket
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 用户密码类型
    /// </summary>
    public enum PasswordType
    {
        /// <summary>
        /// SHA
        /// </summary>
        SHA,
        /// <summary>
        /// MD5
        /// </summary>
        MD5
    }
}
