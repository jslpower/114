using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-05
    /// 描述：系统权限数据层接口
    /// </summary>
    public interface ISysPermission
    {
        /// <summary>
        /// 新增系统权限数据
        /// </summary>
        /// <param name="model">系统权限数据实体</param>
        /// <returns>返回受影响行数</returns>
        int AddSysPermission(Model.SystemStructure.SysPermission model);

        /// <summary>
        /// 修改系统权限数据
        /// </summary>
        /// <param name="model">系统权限数据实体</param>
        /// <returns>返回受影响行数</returns>
        int UpdateSysPermission(Model.SystemStructure.SysPermission model);

        /// <summary>
        /// 批量设置是否启用
        /// </summary>
        /// <param name="SysPermissionIds">系统权限ID集合</param>
        /// <param name="IsEnable">是否启用(1：启用；0：禁用)</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsEnable(string SysPermissionIds, bool IsEnable);

        /// <summary>
        /// 批量删除系统权限数据
        /// </summary>
        /// <param name="SysPermissionIds">系统权限ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSysPermission(string SysPermissionIds);

        /// <summary>
        /// 获取系统权限数据实体
        /// </summary>
        /// <param name="SysPermissionId">系统权限ID</param>
        /// <returns>返回系统权限数据实体</returns>
        Model.SystemStructure.SysPermission GetSysPermission(int SysPermissionId);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="CategoryId">权限大类编号(小于等于0不作条件)</param>
        /// <param name="ClassId">权限类别编号(小于等于0不作条件)</param>
        /// <returns>返回系统权限数据实体集合</returns>
        IList<Model.SystemStructure.SysPermission> GetSysPermissionList(int CategoryId, int ClassId);

        /// <summary>
        /// 获取用户后台所有的（已启用的）权限
        /// </summary>
        /// <param name="companyTypes">用户公司类型集合</param>
        /// <returns></returns>
        Model.SystemStructure.SysPermissionCategory GetAllPermissionByUser(Model.CompanyStructure.CompanyType[] companyTypes);
    }
}
