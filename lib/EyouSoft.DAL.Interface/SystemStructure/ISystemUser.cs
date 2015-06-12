using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 系统用户数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    public interface ISystemUser
    {
        /// <summary>
        /// 新增系统用户
        /// </summary>
        /// <param name="model">系统用户实体</param>
        /// <returns>返回新加用户的ID</returns>
        int AddSystemUser(EyouSoft.Model.SystemStructure.SystemUser model);        

        /// <summary>
        /// 修改系统用户
        /// </summary>
        /// <param name="model">系统用户实体</param>
        /// <returns>返回受影响行数</returns>
        int UpdateSystemUser(EyouSoft.Model.SystemStructure.SystemUser model);

        /// <summary>
        ///  根据用户ID删除用户
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSystemUser(IList<int> SystemUserIds);

        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0添加时间升序，其他添加时间降序</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <param name="UserName">用户名(模糊查询)</param>
        /// <param name="ContactName">联系人(模糊查询)</param>
        /// <returns>系统用户实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SystemUser> GetSystemUserList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, int AreaId, string UserName, string ContactName);
        
        /// <summary>
        /// 根据用户名和密码修改密码
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">原密码信息</param>
        /// <param name="NewsPassword">新密码信息</param>
        /// <returns>返回受影响行数</returns>
        int UpdateUserPassWord(string UserName, EyouSoft.Model.CompanyStructure.PassWord OldPassword, EyouSoft.Model.CompanyStructure.PassWord NewsPassword);

        /// <summary>
        /// 根据用户ID修改密码
        /// </summary>
        /// <param name="SystemUserId">用户id</param>
        /// <param name="password">密码信息</param>
        /// <returns>返回受影响行数</returns>
        int UpdateUserPassWord(int SystemUserId, EyouSoft.Model.CompanyStructure.PassWord password);

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="SystemUserId">用户ID</param>
        /// <returns>返回用户实体</returns>
        EyouSoft.Model.SystemStructure.SystemUser GetSystemUserModel(int SystemUserId);

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>存在返回true，不存在返回false</returns>
        bool ExistsByUserName(string UserName);

        /// <summary>
        /// 验证用户是否存在(存在返回该用户实体，不存在返回null)
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns>存在返回该用户实体，不存在返回null</returns>
        Model.SystemStructure.SystemUser ExistsByUserName(string UserName, string PassWord);

        /// <summary>
        /// 设置该用户是否停用/启用
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="IsDisable">停用(true)/启用(false)</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsDisable(int UserId, bool IsDisable);

        #region 系统管理员用户和线路区域关系函数

        /// <summary>
        /// 新增系统管理员用户和线路区域的关系
        /// </summary>
        /// <param name="UserId">管理员用户Id</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回受影响的行数</returns>
        int AddSysUserAreaControl(int UserId, IList<int> AreaId);

        /// <summary>
        /// 根据用户ID删除用户和线路区域的关系
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回受影响的行数</returns>
        int DeleteSysUserAreaControl(IList<int> SystemUserIds);

        #endregion

        #region 系统管理员用户和用户所能查看的易诺用户池客户类型 方法

        /// <summary>
        /// 新增用户所能查看的易诺用户池客户类型
        /// </summary>
        /// <param name="UserId">系统用户ID</param>
        /// <param name="CustomerTypeIds">易诺用户池客户类型ID集合</param>
        /// <returns>返回受影响的行数</returns>
        int AddSysUserCustomerType(int UserId, IList<int> CustomerTypeIds);

        /// <summary>
        /// 根据用户ID删除用户所能查看的易诺用户池客户类型
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回受影响的行数</returns>
        int DeleteSysUserCustomerType(IList<int> SystemUserIds);

        #endregion
    }
}
