using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：银行账户实体
    /// </summary>
    [Serializable]
    public class BankAccount
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BankAccount() { }

        #region 属性
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司或个人帐号户名
        /// </summary>
        public string BankAccountName { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public BankAccountType AccountType { get; set; }
        #endregion
    }

    /// <summary>
    /// 银行帐号类型
    /// </summary>
    public enum BankAccountType
    { 
        /// <summary>
        /// 公司帐号
        /// </summary>
        公司 = 0,

        /// <summary>
        /// 个人帐号
        /// </summary>
        个人 = 1
    }
}
