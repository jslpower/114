using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 系统类型定义业务逻辑接口(系统线路主题、景点主题、客户等级、酒店周边)
    /// </summary>
    /// 周文超 2010-06-23
    public interface ISysField
    {
        /// <summary>
        /// 获取景点主题
        /// </summary>
        /// <returns></returns>
        IList<Model.SystemStructure.SysFieldBase> GetSightTheme();
        /// <summary>
        /// 获取客户等级
        /// </summary>
        /// <returns></returns>
        IList<Model.SystemStructure.SysFieldBase> GetCustomerLevel();
        /// <summary>
        /// 获取线路主题
        /// </summary>
        /// <returns></returns>
        IList<Model.SystemStructure.SysFieldBase> GetRouteTheme();
        /// <summary>
        /// 获取周边环境
        /// </summary>
        /// <returns></returns>
        IList<Model.SystemStructure.SysFieldBase> GetHotelArea();

        /// <summary>
        /// 根据系统类型定义ID获取名称
        /// </summary>
        /// <param name="FieldId">系统类型定义ID</param>
        /// <param name="SysFieldType">系统类型定义类型</param>
        /// <returns>系统类型定义名称</returns>
        string GetFieldNameById(int FieldId, Model.SystemStructure.SysFieldType SysFieldType);

        /// <summary>
        /// 获取已启用的系统类型定义集合(缓存取值)
        /// </summary>
        /// <param name="SysFieldType">系统类型定义类型</param>
        /// <returns>系统类型定义集合</returns>
        IList<Model.SystemStructure.SysFieldBase> GetSysFieldBaseList(Model.SystemStructure.SysFieldType SysFieldType);

        /// <summary>
        /// 获取系统类型定义集合(运营后台用)
        /// </summary>
        /// <param name="SysFieldType">系统类型定义类型(为null不作条件)</param>
        /// <returns>系统类型定义集合</returns>
        IList<Model.SystemStructure.SysField> GetSysFieldList(Model.SystemStructure.SysFieldType? SysFieldType);

        /// <summary>
        /// 新增系统类型定义
        /// </summary>
        /// <param name="model">系统类型定义实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Add_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Add,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""FieldType"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""FieldName"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Add_CODE)]
        int AddSysField(Model.SystemStructure.SysField model);

        /// <summary>
        /// 修改系统类型定义(只修改名称)
        /// </summary>
        /// <param name="FieldName">系统类型定义名称</param>
        /// <param name="FieldId">系统类型定义ID</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Edit_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Edit,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":2,""Attribute"":""FieldType"",""AttributeType"":""val""},{""Index"":0,""Attribute"":""FieldName"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""FieldId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Edit_CODE)]
        int UpdateSysField(string FieldName, int FieldId, Model.SystemStructure.SysFieldType FieldType);

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="FieldId">系统类型定义ID集合</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_SetIsEnabled_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_SetIsEnabled,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":1,""Attribute"":""FieldType"",""AttributeType"":""val""},{""Index"":0,""Attribute"":""FieldId"",""AttributeType"":""array""},{""Index"":2,""Attribute"":""IsEnabled"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_SetIsEnabled_CODE)]
        bool SetIsEnabled(int[] FieldId, Model.SystemStructure.SysFieldType FieldType, bool IsEnabled);
        /// <summary>
        /// 真实删除客户等级
        /// </summary>
        /// <param name="FieldId">客户等级编号集合</param>
        /// <param name="FieldType">客户等级类型</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Del_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Del,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":1,""Attribute"":""FieldType"",""AttributeType"":""val""},{""Index"":0,""Attribute"":""FieldId"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysField_Del_CODE)]
        bool Delete(int[] FieldId, Model.SystemStructure.SysFieldType FieldType);
    }
}
