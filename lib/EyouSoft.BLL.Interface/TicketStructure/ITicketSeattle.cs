using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    ///功能描述: 机场信息接口
    /// Author：刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public interface ITicketSeattle
    {
        /// <summary>
        /// 获取机场信息集合
        /// </summary>
        /// <param name="InputStr">输入的字符串（为空或null 返回所有机场信息）</param>
        /// <returns>返回符合条件的机场信息集合</returns>
        IList<EyouSoft.Model.TicketStructure.TicketSeattle> GetTicketSeattle(string InputStr);
        /// <summary>
        /// 获取机场信息集合(与运价关联)
        /// </summary>
        /// <param name="SeattleId">出发地编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketSeattle> GetTicketSeattleByFreight(int? SeattleId);
        /// <summary>
        /// 获取机场信息实体
        /// </summary>
        /// <param name="SeattleId">机场信息Id</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketSeattle GetTicketSeattleById(int SeattleId);
    }
}
