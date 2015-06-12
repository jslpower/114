using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 系统数据统计数据访问接口
    /// </summary>
    /// 创建人:蒋胜蓝  2010-6-23
    public interface ISummaryCount
    {
        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SummaryCount GetSummary();
        /// <summary>
        /// 更新统计信息基数值
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false:失败 true:成功</returns>
        bool SetSummaryCount(EyouSoft.Model.SystemStructure.SummaryCount model);
    }
}
