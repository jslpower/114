using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 系统用户业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-26
    public interface ISystemUser
    {
        /// <summary>
        /// 新增系统用户(管理员区域ID集合为null或者count小于等于0不添加)
        /// </summary>
        /// <param name="model">系统用户实体</param>
        /// <returns>-2:新增管理员区域失败;-1:新增用户失败;0:用户实体为空;1:Success</returns>
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
        /// <param name="AreaId">管理员区域ID</param>
        /// <param name="UserName">用户名(模糊查询)</param>
        /// <param name="ContactName">联系人(模糊查询)</param>
        /// <returns>系统用户实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SystemUser> GetSystemUserList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, int AreaId, string UserName, string ContactName);

        /// <summary>
        /// 根据用户名和密码修改密码
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="OldPassWord">原来的密码</param>
        /// <param name="NewPassWord">新密码</param>
        /// <returns>0:Error;1:Success</returns>
        int UpdateUserPassWord(string UserName, string OldPassWord, string NewPassWord);

        /// <summary>
        /// 根据用户ID修改密码
        /// </summary>
        /// <param name="SystemUserId">用户id</param>
        /// <param name="PassWord">密码</param>
        /// <returns>0:Error;1:Success</returns>
        int UpdateUserPassWord(int SystemUserId, string PassWord);

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
    }
}
