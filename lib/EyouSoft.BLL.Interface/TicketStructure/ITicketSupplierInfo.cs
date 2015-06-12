using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    ///功能描述：供应商接口
    /// Author：刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public interface ITicketSupplierInfo
    {
        /// <summary>
        /// 获取供应商实体
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo GetSupplierInfo(string CompanyId);

        /// <summary>
        /// 修改供应商基本信息
        /// </summary>
        /// <param name="SupplierInfo"></param>
        /// <returns></returns>
        bool UpatetSupplierInfo(EyouSoft.Model.TicketStructure.TicketWholesalersInfo SupplierInfo);

        /// <summary>
        /// 修改供应商详细信息
        /// </summary>
        /// <param name="Model">供应商详细信息实体</param>
        /// <returns></returns>
        bool UpatetSupplierDetailsInfo(EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo Model);

         /// <summary>
        /// 获取指定公司的出票成功率
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <returns>出票成功率</returns>
        decimal GetSuccessRate(string CompanyId);
    }
}
