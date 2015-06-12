using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 平台机票供应商（采购商）公司账户信息数据接口
    /// </summary>
    /// 创建人：luofx 2010-10-25
    public interface ITicketCompanyAccount
    {
        /// <summary>
        /// 获取公司账户信息实体(机票平台)
        /// </summary>
        /// <param name="AcountId">帐号Id（主键）</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketCompanyAccount GetModel(int AcountId);
        /// <summary>
        /// 添加机票公司账户信息
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns>1：成功，0：失败</returns>
         int Add(EyouSoft.Model.TicketStructure.TicketCompanyAccount model);
        /// <summary>
        /// 修改机票公司账户信息
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns>1：成功，0：失败</returns>
         int Update(EyouSoft.Model.TicketStructure.TicketCompanyAccount model);
       /// <summary>
         /// 删除公司账户
       /// </summary>
       /// <param name="TicketIds">公司账户信息Id</param>
         /// <returns>大于1：成功，0：失败</returns>
         int Delete(params int[] TicketIds);
        /// <summary>
        /// 根据公司Id获取机票平台的公司账户信息
        /// </summary>
        /// <param name="CompanyId">公司Id</param>
        /// <returns>返回公司账户信息集合</returns>
         IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> GetList(string CompanyId);

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
