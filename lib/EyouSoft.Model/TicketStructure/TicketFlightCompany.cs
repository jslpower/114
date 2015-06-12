using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 航空公司基类
    /// </summary>
    /// 创建人：luofx 2010-10-21
    [Serializable]
    public class TicketFlightBase
    {
        /// <summary>
        /// 航空公司编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 航空公司名称
        /// </summary>
        public string AirportName { get; set; }

    }

    /// <summary>
    /// 航空公司实体类
    /// </summary>
    /// 创建人：luofx 2010-10-21
    [Serializable]
    public class TicketFlightCompany : TicketFlightBase
    {
        /// <summary>
        /// 航空公司代号
        /// </summary>
        public string AirportCode { get; set; }
    }

}
