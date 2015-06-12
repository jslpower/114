using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 银行帐号信息业务逻辑接口层
    /// </summary>
    /// 创建人：张志瑜 2010-06-02
    public interface IBankAccount
    {
        /// <summary>
        /// 获得银行帐号信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.BankAccount> GetList(string companyId);
    }
}
