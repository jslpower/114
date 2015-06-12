using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    /// 平台机票供应商（采购商）公司账户信息 接口
    /// Author:刘咏梅
    /// CreateTime:2010-11-08
    /// </summary>
    public interface ITicketCompanyAccount
    {
        /// <summary>
        /// 添加机票公司账户信息
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns>true：成功，false：失败</returns>
        bool AddTicketCompanyAccount(EyouSoft.Model.TicketStructure.TicketCompanyAccount model);
        /// <summary>
        /// 修改机票公司账户信息
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns>true：成功，false：失败</returns>
        bool UpdateTicketCompanyAccount(EyouSoft.Model.TicketStructure.TicketCompanyAccount model);
        /// <summary>
        /// 删除公司账户
        /// </summary>
        /// <param name="TicketIds">公司账户信息Id</param>
        /// <returns>大于true：成功，false：失败</returns>
        bool DeleteTicketCompanyAccount(params int[] TicketIds);
        /// <summary>
        /// 根据公司Id获取机票平台的公司账户信息
        /// </summary>
        /// <param name="CompanyId">公司Id</param>
        /// <returns>返回公司账户信息集合</returns>
        IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> GetTicketCompanyAccountList(string CompanyId);

         /// <summary>
        /// 根据接口类型和公司ID返回公司账户实体
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="accountType">账户类型</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketCompanyAccount GetModel(string companyId, EyouSoft.Model.TicketStructure.TicketAccountType accountType);

        /// <summary>
        /// 根据公司编号及支付接口类型设置是否加入支付圈电子协议签约
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="isSign">是否加入支付圈电子协议签约</param>
        /// <returns></returns>
        bool SetIsSign(string companyId, EyouSoft.Model.TicketStructure.TicketAccountType accountType, bool isSign);
    }
}
