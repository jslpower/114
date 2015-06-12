using System;

namespace EyouSoft.OpenRelation.Model
{
    #region 用户基本信息业务实体
    /// <summary>
    /// 用户基本信息业务实体
    /// </summary>
    /// Author:汪奇志 DateTime:2011-04-01
    public class MUserBaseInfo : MBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MUserBaseInfo() { }

        /// <summary>
        /// 系统用户编号
        /// </summary>
        public int SystemUserId { get; set; }        
        /// <summary>
        /// 平台用户编号
        /// </summary>
        public string PlatformUserId { get; set; }
    }
    #endregion

    #region 用户信息业务实体
    /// <summary>
    /// 用户信息业务实体
    /// </summary>
    /// Author:汪奇志 DateTime:2011-04-01
    public class MUserInfo:MUserBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MUserInfo() { }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// MSN
        /// </summary>
        public string MSN { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
    }
    #endregion

    #region 用户是否存在信息业务实体
    /// <summary>
    /// 用户是否存在信息业务实体
    /// </summary>
    public class MExistsUserInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户是否存在验证方式（无须赋值，由中间处理程序来决定）
        /// </summary>
        public ExistsUserType ExistsUserType { get; set; }
        /// <summary>
        /// 系统用户编号，修改时务必赋值
        /// </summary>
        public int SystemUserId { get; set; }
        /// <summary>
        /// 系统类型，修改时务必赋值
        /// </summary>
        public SystemType SystemType { get; set; }
    }
    #endregion
}
