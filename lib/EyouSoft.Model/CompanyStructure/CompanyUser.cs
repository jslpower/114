 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 联系人信息实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-27
    [Serializable]
    public class ContactPersonInfo
    {
        private Sex _contactsex = Sex.未知;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系人性别(默认为未知)
        /// </summary>
        public Sex ContactSex { get { return this._contactsex; } set { this._contactsex = value; } }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 联系人传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系人QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 联系人MQ
        /// </summary>
        public string MQ { get; set; }
        /// <summary>
        /// 联系人MSN
        /// </summary>
        public string MSN { get; set; }
    }

    /// <summary>
    /// 用户帐号实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-27
    [Serializable]
    public class UserAccount
    {
        private PassWord _passwordinfo = new PassWord();
        /// <summary>
        /// 用户编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码(在应用层设置时,只需设置其NoEncryptPassword属性)
        /// </summary>
        public PassWord PassWordInfo { get { return this._passwordinfo; } set { this._passwordinfo = value; } }
    }

    /// <summary>
    /// 用户基本信息实体类
    /// </summary>
    /// 创建人：张志瑜 2010-06-25
    [Serializable]
    public class CompanyUserBase : UserAccount
    {
        private ContactPersonInfo _contactinfo = new ContactPersonInfo();
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 是否管理员[该字段不允许修改]
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 联系信息
        /// </summary>
        public ContactPersonInfo ContactInfo { get { return this._contactinfo; } set { this._contactinfo = value; } }

        /// <summary>
        /// 公司职位 
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// MQ昵称
        /// </summary>
        public string MqNickName { get; set; }
    }

    /// <summary>
    /// 用户明细信息实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-27
    [Serializable]
    public class CompanyUser : CompanyUserBase
    {
        #region 属性   
        /// <summary>
        /// 其它平台用户编号
        /// </summary>
        public int OpUserId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }        
        /// <summary>
        /// 权限组(角色)编号
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 是否启用 true:启用  false:停用
        /// </summary>
        public bool IsEnable { get; set; }        
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 所属线路区域
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.AreaBase> Area { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime JoinTime { get; set; }

        /// <summary>
        /// 同业114平台登录次数
        /// </summary>
        public int LoginCount { get; set; }

        #endregion
    }

    /// <summary>
    /// 获得公司和用户明细信息实体类
    /// </summary>
    /// 创建人：张志瑜 2010-08-13
    [Serializable]
    public class CompanyAndUserInfo
    {
        /// <summary>
        /// 用户明细信息
        /// </summary>
        public CompanyUser User { get; set; }
        /// <summary>
        /// 公司明细信息
        /// </summary>
        public CompanyDetailInfo Company { get; set; }
    }

    /// <summary>
    /// 密码实体
    /// </summary>
    /// 创建人：张志瑜 2010-05-27
    [Serializable]
    public class PassWord
    {
        /// <summary>
        /// SHA加密密码
        /// </summary>
        private string _shapassword = "";
        /// <summary>
        /// MD5加密密码
        /// </summary>
        private string _md5password = "";

        /// <summary>
        /// 获取或设置未加密密码(只需要设置未加密密码即可)
        /// </summary>
        public string NoEncryptPassword { get; set; }
        /// <summary>
        /// 获取SHA加密密码(只需要设置未加密密码即可)
        /// </summary>
        public string SHAPassword { get { return this._shapassword; } }
        /// <summary>
        /// 获取MD5加密密码(只需要设置未加密密码即可)
        /// </summary>
        public string MD5Password { get { return this._md5password; } }

        /// <summary>
        /// 构造方法
        /// </summary>
        public PassWord() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="noencryptpassword">未加密密码</param>
        public PassWord(string noencryptpassword) 
        {
            this.NoEncryptPassword = noencryptpassword;
        }

        /// <summary>
        /// 设置所有密码(该方法只需在业务逻辑层使用)
        /// </summary>
        /// <param name="noencryptpassword">未加密密码</param>
        /// <param name="shapassword">SHA加密密码</param>
        /// <param name="md5password">MD5加密密码</param>
        public void SetEncryptPassWord(string noencryptpassword, string shapassword, string md5password)
        {
            this.NoEncryptPassword = noencryptpassword;
            this._shapassword = shapassword;
            this._md5password = md5password;
        }
    }

    /// <summary>
    /// 性别枚举型
    /// </summary>
    /// 创建人：张志瑜 2010-05-27
    [Serializable]
    public enum Sex
    { 
        /// <summary>
        /// 男
        /// </summary>
        男 = 0,
        /// <summary>
        /// 女
        /// </summary>
        女 = 1,
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 2
    }

    /// <summary>
    /// 公司子账户基本查询条件参数实体
    /// </summary>
    public class QueryParamsUser 
    {
        /// <summary>
        /// 用户名,默认为空[若=空,则不作为查询条件]
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系人,默认为空[若=空,则不作为查询条件]
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 是否显示总账号，默认为不显示
        /// </summary>
        public bool IsShowAdmin { get; set; }
    }

    #region 用户登录日志实体类
    /// <summary>
    /// 用户登录日志实体类
    /// </summary>
    public class LogUserLogin
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogUserLogin() { }

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string EventID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 事件地区
        /// </summary>
        public string EventArea
        {
            get;
            set;
        }
        /// <summary>
        /// 日志标题
        /// </summary>
        public string EventTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string EventMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 日志类型号
        /// </summary>
        public int EventCode
        {
            get;
            set;
        }
        /// <summary>
        /// 日志发生地址
        /// </summary>
        public string EventUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 日志发生IP
        /// </summary>
        public string EventIP
        {
            get;
            set;
        }
        /// <summary>
        /// 日志发生时间
        /// </summary>
        public DateTime EventTime
        {
            get;
            set;
        }
        #endregion

    }
    #endregion

    #region 运营后台用户列表用户信息业务实体
    /// <summary>
    /// 运营后台用户列表用户信息业务实体
    /// </summary>
    public class MLBYNUserInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBYNUserInfo() { }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Gender { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// MQ
        /// </summary>
        public string MQ { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime? RecentlyLoginTime { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimes { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public enum OrderingType
        {
            /// <summary>
            /// 默认，按注册时间降序
            /// </summary>
            Default,
            /// <summary>
            /// 按注册时间降序
            /// </summary>
            RegTimeDesc,
            /// <summary>
            /// 按注册时间升序
            /// </summary>
            RegTimeAsc,
            /// <summary>
            /// 按登录时间降序
            /// </summary>
            LoginTimeDesc,
            /// <summary>
            /// 按登录时间升序
            /// </summary>
            LoginTimeAsc
        }
    }

    /// <summary>
    /// 运营后台用户列表用户查询信息业务实体
    /// </summary>
    public class MLBYNUserSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBYNUserSearchInfo() { }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int? DistrictId { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public MLBYNUserInfo.OrderingType? OrderingType { get; set; }

    }
    #endregion
}
