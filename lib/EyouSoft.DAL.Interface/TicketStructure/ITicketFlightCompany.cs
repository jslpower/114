using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 航空公司数据接口
    /// </summary>
    /// 创建人：luofx 2010-10-25
    public interface ITicketFlightCompany
    {
        /// <summary>
        /// 获取航空公司集合
        /// </summary>
        /// <param name="CompanyName">航空公司名字（null或空时不做条件）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> GetList(string CompanyName);
        /// <summary>
        /// 获取航空公司实体
        /// </summary>
        /// <param name="id">航空公司id</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketFlightCompany GetModel(int id);
    }
}
