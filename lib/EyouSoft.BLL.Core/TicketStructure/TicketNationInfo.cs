using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 机票平台-国籍基础信息BLL
    /// </summary>
    /// 创建人：luofx 2010-11-8
    public class TicketNationInfo : IBLL.TicketStructure.ITicketNationInfo
    {
        private readonly IDAL.TicketStructure.ITicketNationInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<IDAL.TicketStructure.ITicketNationInfo>();
        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketNationInfo() { }

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketNationInfo CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketNationInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketNationInfo>();
            }
            return op;
        }
        /// <summary>
        /// 获取国家信息列表
        /// </summary>
        /// <param name="CountryName">国家名称（为空时：查询所有；不为空时，按国家名称筛选）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketNationInfo> GetList(string CountryName)
        {
            IList<EyouSoft.Model.TicketStructure.TicketNationInfo> list = (IList<EyouSoft.Model.TicketStructure.TicketNationInfo>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.AirTickets.TicketNational);
            if (list == null)
            {
                list = idal.GetList(string.Empty);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.AirTickets.TicketNational,list);
            }

            return string.IsNullOrEmpty(CountryName)?list:(from c in list where c.CountryName.Trim().CompareTo(CountryName) == 0 select c).ToList();
        }
    }
}
