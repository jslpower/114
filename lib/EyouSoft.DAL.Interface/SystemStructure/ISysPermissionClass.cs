using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-05
    /// 描述：系统权限子类别数据层接口
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
        /// 删除
        /// </summary>
        /// <param name="Id">主键编号集合(1,2,3)</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string Ids);
        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="Id">权限编号(1,2,3)</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetEnable(string Id, bool IsEnable);
        /// <summary>
        /// 根据权限类别，权限大类别获取数据
        /// </summary>
        /// <param name="PermissionTypes">权限类别集合</param>
        /// <param name="CategroyType">权限大类别 >0返回指定大类别的数据，否则返回全部</param>
        /// <param name="LoadPermission">是否立即读取明细权限</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysPermissionClass> GetList(string PermissionTypes, int CategroyType, bool LoadPermission);
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SysPermissionClass GetModel(int ID);
    }
}
