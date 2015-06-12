using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 银行帐号信息业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-02
    public class BankAccount : EyouSoft.IBLL.CompanyStructure.IBankAccount
    {
        private readonly EyouSoft.IDAL.CompanyStructure.IBankAccount idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.IBankAccount>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.IBankAccount CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.IBankAccount op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.IBankAccount>();
            }
            return op;
        }

        /// <summary>
        /// 获得银行帐号信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.BankAccount> GetList(string companyId)
        {
            return idal.GetList(companyId);
        }
    }
}
