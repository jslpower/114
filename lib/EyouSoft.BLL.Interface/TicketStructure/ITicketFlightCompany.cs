using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    ///功能描述：常旅客接口
    /// Author：刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public interface ITicketFlightCompany
    {
        /// <summary>
        ///从缓存中获取航空公司列表 
        /// </summary>
        /// <param name="CompanyName">航空公司名字（null或空时不做条件）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> GetTicketFlightCompanyListByCache(string CompanyName);
        /// <summary>
        ///获取航空公司列表 
        /// </summary>
        /// <param name="CompanyName">航空公司名字（null或空时不做条件）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> GetTicketFlightCompanyList(string CompanyName);
        /// <summary>
        /// 获取航空公司实体
        /// </summary>
        /// <param name="Id">航空公司ID</param>
        /// <param name="CompanyName">航空公司名称</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketFlightCompany GetTicketFlightCompanyById(string Id,string CompanyName);
        /// <summary>
        /// 获取航空公司实体
        /// </summary>
        /// <param name="id">航空公司id</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketFlightCompany GetTicketFlightCompany(int id);
    }
}
