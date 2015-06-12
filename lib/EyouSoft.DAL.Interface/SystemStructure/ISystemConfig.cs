using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 系统设置 数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    public interface ISystemConfig
    {
        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="model">系统设置实体</param>
        /// <returns>返回受影响行数</returns>
        int UpdateSystemConfig(EyouSoft.Model.SystemStructure.SystemConfig model);

        /// <summary>
        /// 获取系统设置实体
        /// </summary>
        /// <returns>返回系统设置实体</returns>
        Model.SystemStructure.SystemConfig GetSystemConfig();
    }
}
