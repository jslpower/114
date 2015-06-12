using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-06
    /// 描述：权限大类别业务层接口
    /// </summary>
    public interface ISysPermissionCategory
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">系统权限大类别实体类</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.SystemStructure.SysPermissionCategory model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">系统权限大类别实体类</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.SystemStructure.SysPermissionCategory model);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool DeleteIDs(int[] Ids);
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool DeleteByID(int Id);
        /// <summary>
        /// 批量设置启用状态
        /// </summary>
        /// <param name="Ids">权限编号(1,2,3,4)</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetEnableByIDs(int[] Ids, bool IsEnable);
        /// <summary>
        /// 单个设置启用状态
        /// </summary>
        /// <param name="ID">权限编号</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetEnableByID(int ID, bool IsEnable);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SysPermissionCategory GetModel(int ID);
        /// <summary>
        /// 分页获取权限大类别列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="PermissionType">权限类别数组（如组团，地接等等）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(int pageSize, int pageIndex, ref int recordCount, int[] PermissionType);
        /// <summary>
        /// 个人中心权限管理获取指定权限类别的权限详细列表
        /// </summary>
        /// <param name="PermissionType">公司类别数组（如组团，地接等等）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(int[] PermissionType);
        /// <summary>
        /// 获取指定权限类别的权限详细列表
        /// </summary>
        /// <param name="PermissionType">系统权限类别</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(EyouSoft.Model.SystemStructure.PermissionType PermissionType);
    }
}
