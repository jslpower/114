using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 系统信息 业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-26
    public interface ISystemInfo
    {
        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="model">系统信息实体(区域联系人实体集合为null或者count小于等于0时不修改区域联系人)</param>
        /// <returns>-3:添加区域联系人失败;-2:删除区域联系人失败;-1:修改系统信息失败;0:系统实体为空;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SystemInfo_Edit_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SystemInfo_Edit,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]", 
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SystemInfo_Edit_CODE)]
        int UpdateSystemInfo(EyouSoft.Model.SystemStructure.SystemInfo model);

        /// <summary>
        /// 获取系统信息(同时获取所有的区域联系人)
        /// </summary>
        /// <returns>返回系统信息实体</returns>
        EyouSoft.Model.SystemStructure.SystemInfo GetSystemInfoModel();

        /// <summary>
        /// 根据系统ID获取系统信息(不含区域联系人)
        /// </summary>
        /// <returns>返回系统信息实体</returns>
        EyouSoft.Model.SystemStructure.SystemInfo GetSystemModel();

        /// <summary>
        /// 设置平台配置，返回1成功，其它失败
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <returns></returns>
        int SetSysSettings(EyouSoft.Model.SystemStructure.MSysSettingInfo info);
        /// <summary>
        /// 获取平台配置
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.MSysSettingInfo GetSysSetting();
    }
}
