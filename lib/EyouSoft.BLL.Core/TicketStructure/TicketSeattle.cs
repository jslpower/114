using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 机场
    /// Author:刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public class TicketSeattle: EyouSoft.IBLL.TicketStructure.ITicketSeattle
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketSeattle idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketSeattle>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketSeattle CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketSeattle op = null;
            if(op==null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IBLL.TicketStructure.ITicketSeattle>();
            }
            return op;
        }
        #region ITicketSeattle 成员

        /// <summary>
        /// 获取机场信息集合
        /// </summary>
        /// <param name="InputStr">输入的字符串（为空或null 返回所有机场信息）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketSeattle> GetTicketSeattle(string InputStr)
        {
            //IList<EyouSoft.Model.TicketStructure.TicketSeattle> list = (List<EyouSoft.Model.TicketStructure.TicketSeattle>)
            //    EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.AirTickets.TicketSeattle);

            //if (list == null)
            //{
              return  idal.GetList(InputStr,0, false);
            //    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.AirTickets.TicketSeattle, list);
            //}
            //return string.IsNullOrEmpty(InputStr)?list:(from c in list where c.LongName.Trim().CompareTo(InputStr.Trim()) == 0 select c).ToList(); 
        }
        /// <summary>
        /// 获取机场信息集合(与运价关联)
        /// </summary>
        /// <param name="SeattleId">出发地编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketSeattle> GetTicketSeattleByFreight(int? SeattleId)
        {
            return idal.GetList(string.Empty, SeattleId??0, true);
        }
        /// <summary>
        /// 获取机场信息Model
        /// </summary>
        /// <param name="SeattleId">机场信息ID</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketSeattle GetTicketSeattleById(int SeattleId)
        {
            return idal.GetModel(SeattleId);
        }

        #endregion
    }
}
