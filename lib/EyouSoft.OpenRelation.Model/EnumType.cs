using System;

namespace EyouSoft.OpenRelation.Model
{
    #region 系统类型
    /// <summary>
    /// 系统类型
    /// </summary>
    public enum SystemType
    {
        /// <summary>
        /// 易游通
        /// </summary>
        YYT=1,
        /// <summary>
        /// 同业通
        /// </summary>
        TYT,
        /// <summary>
        /// 平台
        /// </summary>
        Platform
    }
    #endregion

    #region 系统公司类型
    /// <summary>
    /// 系统公司类型，用来确定各个系统专线、组团、供应商公司是否存在于一个表内，若存在于一个表内用ZX
    /// </summary>
    public enum SystemCompanyType
    {
        /// <summary>
        /// 专线
        /// </summary>
        ZX = 1,
        /// <summary>
        /// 组团
        /// </summary>
        ZT,
        /// <summary>
        /// 供应商
        /// </summary>
        GY
    }
    #endregion

    #region 指令类型
    /// <summary>
    /// 指令类型
    /// </summary>
    public enum InstructionType
    {
        /// <summary>
        /// 空白指令
        /// </summary>
        None = 0,
        /// <summary>
        /// 创建公司信息指令，中间处理程序会根据是否存在公司相应关系决定是否创建公司
        /// </summary>
        CreateCompany,
        /// <summary>
        /// 创建用户信息指令
        /// </summary>
        CreateUser,
        /// <summary>
        /// 更新公司信息指令
        /// </summary>
        UpdateCompany,
        /// <summary>
        /// 更新用户信息指令
        /// </summary>
        UpdateUser,
        /// <summary>
        /// 删除公司信息指令
        /// </summary>
        DeleteCompany,
        /// <summary>
        /// 删除用户信息指令
        /// </summary>
        DeleteUser,
        /// <summary>
        /// 用户是否存在指令
        /// </summary>
        ExistsUser,
        /// <summary>
        /// 获取用户系统权限
        /// </summary>
        UserPermission
    }
    #endregion

    #region 性别
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 女
        /// </summary>
        L = 0,
        /// <summary>
        /// 男
        /// </summary>
        G
    }
    #endregion

    #region 用户是否存在验证方式
    /// <summary>
    /// 用户是否存在验证方式
    /// </summary>
    public enum ExistsUserType
    {
        /// <summary>
        /// 用户名
        /// </summary>
        UserName = 1,
        /// <summary>
        /// 邮箱
        /// </summary>
        Email,
        /// <summary>
        /// 用户名及邮箱
        /// </summary>
        Both
    }
    #endregion

    #region 平台公司类型
    /// <summary>
    /// 平台公司类型
    /// </summary>
    public enum PlatformCompanyType
    {
        /// <summary>
        /// 专线
        /// </summary>
        ZX = 1,
        /// <summary>
        /// 组团
        /// </summary>
        ZT
    }
    #endregion
}
