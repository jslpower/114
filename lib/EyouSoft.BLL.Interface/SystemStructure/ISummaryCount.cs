using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 系统数据统计业务逻辑接口
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
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SummaryCount_Edit_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SummaryCount_Edit,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SummaryCount_Edit_CODE)]
        bool SetSummaryCount(EyouSoft.Model.SystemStructure.SummaryCount model);
    }
}
