using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-06
    /// 描述：系统权限子类别业务层接口
    /// </summary>
    public interface ISysPermissionClass
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">系统权限子类别实体类</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.SystemStructure.SysPermissionClass model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">系统权限子类别实体类</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.SystemStructure.SysPermissionClass model);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool DeleteByMultiID(int[] Ids);
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        bool DeleteByID(int Id);
        /// <summary>
        /// 批量设置启用状态
        /// </summary>
        /// <param name="Ids">权限编号</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetEnableByMultiID(int[] Ids, bool IsEnable);
        /// <summary>
        /// 单个设置启用状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        bool SetEnableByID(int Id, bool IsEnable);
        /// <summary>
        /// 根据权限类别获取数据
        /// </summary>
        /// <param name="PermissionTypes">权限类别集合</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysPermissionClass> GetList(int[] PermissionTypes);
        /// <summary>
        /// 根据权限大类别获取数据
        /// </summary>
        /// <param name="CategroyType">权限大类别</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysPermissionClass> GetList(int CategroyType);
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SysPermissionClass GetModel(int ID);
    }
}
