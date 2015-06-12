using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 系统信息 数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    public interface ISystemInfo
    {
        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="model">系统信息实体</param>
        /// <returns>返回受影响行数</returns>
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
        /// <param name="removeKeys">不处理的键组</param>
        /// <returns></returns>
        int SetSysSettings(EyouSoft.Model.SystemStructure.MSysSettingInfo info,string[] removeKeys);
        /// <summary>
        /// 获取平台配置
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.MSysSettingInfo GetSysSetting();
    }
}
