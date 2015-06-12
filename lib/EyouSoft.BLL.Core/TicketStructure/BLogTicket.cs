using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 机票接口访问日志业务逻辑类
    /// </summary>
    /// Author：汪奇志 2011-05-10
    public class BLogTicket:EyouSoft.IBLL.TicketStructure.ILogTicket
    {
        private readonly IDAL.TicketStructure.ILogTicket dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ILogTicket>();

        #region CreateInstance
        /// <summary>
        /// 创建团队信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ILogTicket CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ILogTicket op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ILogTicket>();
            }
            return op;
        }
        #endregion

        #region public members
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志信息业务实体</param>
        /// <returns></returns>
        public bool WLog(EyouSoft.Model.TicketStructure.MLogTicketInfo log)
        {
            if (log == null) return true;

            return dal.WLog(log);
        }

        /// <summary>
        /// 获取日志信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.MLBLogInfo> GetLogs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TicketStructure.MLogTicketSearchInfo searchInfo)
        {
            return dal.GetLogs(pageSize, pageIndex, ref recordCount, searchInfo);
        }
        #endregion
    }
}
