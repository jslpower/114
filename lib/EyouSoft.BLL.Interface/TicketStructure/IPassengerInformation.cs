using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    /// 乘客信息接口
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public interface IPassengerInformation
    {
        /// <summary>
        /// 根据团队票编号获取乘客信息
        /// </summary>
        /// <param name="groupTicketID">团队票编号</param>
        /// <returns>乘客信息集合</returns>
        IList<EyouSoft.Model.TicketStructure.PassengerInformation> GetPassengerInformation(int groupTicketID);
    }
}
