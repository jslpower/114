using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 机场信息数据接口
    /// </summary>
    /// 创建人：luofx 2010-10-25
    public interface ITicketSeattle
    {
        /// <summary>
        /// 获取机场信息集合
        /// </summary>
        /// <param name="InputString">输入的字符串</param>
        /// <param name="SeattleId">与运价关联的始发地编号</param>
        /// <param name="IsFreight">是否与运价关联</param>
        /// <returns>返回符合条件的机场信息集合</returns>
        IList<EyouSoft.Model.TicketStructure.TicketSeattle> GetList(string InputString, int SeattleId, bool IsFreight);
        /// <summary>
        /// 获取机场信息实体
        /// </summary>
        /// <param name="SeattleId">机场信息Id</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketSeattle GetModel(int SeattleId);  
   
                /// <summary>
        /// 根据目的地获取机场信息ID主键
        /// </summary>
        /// <param name="SeattleName">机场名字</param>
        /// <returns>0:无该机场信息；大于0:有机场信息</returns>
        int GetSeattleIdByName(string SeattleName);
    }
}
