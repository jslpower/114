using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 系统类型定义数据访问接口
    /// </summary>
    /// 周文超 2010-06-22
    public interface ISysField
    {
        /// <summary>
        /// 获取系统类型定义实体集合
        /// </summary>
        /// <param name="FieldType">类型的类型(必须传值)</param>
        /// <param name="IsEnabled">是否启用(为null不作条件)</param>
        /// <param name="IsDefault">是否系统预定义类型(为null不作条件)</param>
        /// <returns>系统类型定义实体集合</returns>
        IList<Model.SystemStructure.SysFieldBase> GetSysFieldBaseList(Model.SystemStructure.SysFieldType FieldType, bool? IsEnabled
            , bool? IsDefault);

        /// <summary>
        /// 获取系统类型定义实体集合
        /// </summary>
        /// <param name="FieldType">类型的类型(为null不作条件)</param>
        /// <param name="IsEnabled">是否启用(为null不作条件)</param>
        /// <param name="IsDefault">是否系统预定义类型(为null不作条件)</param>
        /// <returns>系统类型定义实体集合</returns>
        IList<Model.SystemStructure.SysField> GetSysFieldList(Model.SystemStructure.SysFieldType? FieldType, bool? IsEnabled
            , bool? IsDefault);

        /// <summary>
        /// 新增系统类型定义
        /// </summary>
        /// <param name="model">系统类型定义实体</param>
        /// <returns>返回受影响行数</returns>
        int AddSysField(Model.SystemStructure.SysField model);

        /// <summary>
        /// 修改系统类型定义(只修改名称)
        /// </summary>
        /// <param name="FieldName">系统类型定义名称</param>
        /// <param name="FieldId">系统类型定义ID</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <returns>返回受影响行数</returns>
        int UpdateSysField(string FieldName, int FieldId, Model.SystemStructure.SysFieldType FieldType);

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="FieldIds">系统类型定义ID集合</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsEnabled(string FieldIds, Model.SystemStructure.SysFieldType FieldType, bool IsEnabled);
        /// <summary>
        /// 真实删除客户等级
        /// </summary>
        /// <param name="FieldIds">客户等级编号字符串</param>
        /// <param name="FieldType">客户等级类型</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string FieldIds, Model.SystemStructure.SysFieldType FieldType);
    }
}
