using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 系统设置 业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-26
    public interface ISystemConfig
    {
        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="model">系统设置实体</param>
        /// <returns>0:Error;1:Success</returns>
        int UpdateSystemConfig(EyouSoft.Model.SystemStructure.SystemConfig model);

        /// <summary>
        /// 获取系统设置实体
        /// </summary>
        /// <returns>返回系统设置实体</returns>
        Model.SystemStructure.SystemConfig GetSystemConfig();
    }
}
