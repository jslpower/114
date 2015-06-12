using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 航空公司业务逻辑层
    /// Author:刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public class TicketFlightCompany: EyouSoft.IBLL.TicketStructure.ITicketFlightCompany
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketFlightCompany idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketFlightCompany>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketFlightCompany CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketFlightCompany op = null;
            if(op==null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IBLL.TicketStructure.ITicketFlightCompany>();
            }
            return op;
        }

        #region ITicketFlightCompany 成员
        /// <summary>
        /// 从缓存中获取航空公司列表
        /// </summary>
        /// <param name="CompanyName">航空公司名字（null或空时不做条件）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> GetTicketFlightCompanyListByCache(string CompanyName)
        {
            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> list = (IList<EyouSoft.Model.TicketStructure.TicketFlightCompany>)
                EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.AirTickets.TicketFlightCompany);
            if (list == null)
            {
                list = idal.GetList(string.Empty);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.AirTickets.TicketFlightCompany, list);
            }
            return (from c in list where c.AirportName.CompareTo(CompanyName) == 0 select c).ToList();  
        }     

        /// <summary>
        /// 从缓存获取航空公司实体
        /// </summary>
        /// <param name="Id">航空公司Id</param>
        /// <param name="CompanyName">航空公司名称为空时，不做条件</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketFlightCompany GetTicketFlightCompanyById(string Id,string CompanyName)
        {
            if (int.Parse(Id) <= 0)
                return null;

            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> list = GetTicketFlightCompanyListByCache(CompanyName);
            list = list.Where(Item => (Item.Id ==int.Parse(Id))).ToList();
            if (list == null || list.Count <= 0)
                return null;
            else
                return list[0];
        }

        /// <summary>
        /// 查询航空公司列表
        /// </summary>
        /// <param name="CompanyName">航空公司名称为null时，不做条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> GetTicketFlightCompanyList(string CompanyName)
        {
            return idal.GetList(CompanyName);
        }

     
        /// <summary>
        /// 获取航空公司Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketFlightCompany GetTicketFlightCompany(int id)
        {
            return idal.GetModel(id);
        }

        #endregion
    }
}
