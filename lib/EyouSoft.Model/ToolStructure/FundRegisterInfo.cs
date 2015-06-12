/*Author：汪奇志 2010-11-09*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ToolStructure
{
    #region enum
    /// <summary>
    /// 账务管理-收款付款登记类型
    /// </summary>
    public enum FundRegisterType
    {
        /// <summary>
        /// 收款登记
        /// </summary>
        收款=0,
        /// <summary>
        /// 付款登记
        /// </summary>
        付款
    }

    /// <summary>
    /// 账务管理-收款付款方式
    /// </summary>
    public enum FundPayType
    {
        /// <summary>
        /// 现金_财务现金
        /// </summary>
        现金_财务现金=0,
        /// <summary>
        /// 签单挂账
        /// </summary>
        签单挂账,
        /// <summary>
        /// 银行电汇
        /// </summary>
        银行电汇,
        /// <summary>
        /// 转账支票
        /// </summary>
        转账支票,
        /// <summary>
        /// 现金_导游支付
        /// </summary>
        现金_导游支付,
        /// <summary>
        /// 现金收款
        /// </summary>
        现金收款,
        /// <summary>
        /// 支付宝支付
        /// </summary>
        支付宝支付
    }
    #endregion

    #region 财务管理-收款付款登记信息业务实体
    /// <summary>
    /// 财务管理-收款付款登记信息业务实体
    /// </summary>
    public class FundRegisterInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public FundRegisterInfo() { }

        /// <summary>
        /// 登记编号
        /// </summary>
        public string RegisterId { get; set; }
        /// <summary>
        /// 登记类型
        /// </summary>
        public FundRegisterType RegisterType { get; set; }
        /// <summary>
        /// 收款或付款编号
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 收款或付款日期
        /// </summary>
        public DateTime ItemTime { get; set; }
        /// <summary>
        /// 收款或付款人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 收款或付款金额
        /// </summary>
        public decimal ItemAmount { get; set; }
        /// <summary>
        /// 收款或付款方式
        /// </summary>
        public FundPayType PayType { get; set; }
        /// <summary>
        /// 是否开票
        /// </summary>
        public bool IsBilling { get; set; }
        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal BillingAmount { get; set; }
        /// <summary>
        /// 发票(收据)号
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion
}
