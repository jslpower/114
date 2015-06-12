using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 银行帐号信息数据访问接口层
    /// </summary>
    /// 创建人：张志瑜 2010-06-01
    public interface IBankAccount
    {
        /// <summary>
        /// 批量添加银行帐号
        /// </summary>
        /// <param name="models">银行帐号实体列表</param>
        /// <returns></returns>
        bool Add(IList<EyouSoft.Model.CompanyStructure.BankAccount> models);
        /// <summary>
        /// 删除公司所有银行帐号
        /// </summary>
        /// <param name="companyid">银行帐号所属的公司ID</param>
        /// <returns></returns>
        bool Delete(string companyid);
        /// <summary>
        /// 获得银行帐号信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.BankAccount> GetList(string companyId);
    }
}
