using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 机票接口访问日志数据访问类接口
    /// </summary>
    /// Author：汪奇志 2011-05-10
    public interface ILogTicket
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志信息业务实体</param>
        /// <returns></returns>
        bool WLog(EyouSoft.Model.TicketStructure.MLogTicketInfo log);
        /// <summary>
        /// 获取日志信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.MLBLogInfo> GetLogs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TicketStructure.MLogTicketSearchInfo searchInfo);
    }
}
