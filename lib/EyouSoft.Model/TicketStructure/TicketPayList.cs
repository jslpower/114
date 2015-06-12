using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    #region 机票金额流水明细

    /// <summary>
    /// 机票金额流水明细
    /// </summary>
    /// 周文超 2010-10-21
    [Serializable]
    public class TicketPay
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketPay() { }

        /// <summary>
        /// 编号
        /// </summary>
        public string PayId { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 项目类型（订单、变更、运价购买）
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// 支付接口类型
        /// </summary>
        public TicketAccountType PayType { get; set; }

        /// <summary>
        /// 当前操作员公司编号
        /// </summary>
        public string CurrCompanyId { get; set; }

        /// <summary>
        /// 当前操作员编号
        /// </summary>
        public string CurrUserId { get; set; }

        /// <summary>
        /// 提交到支付接口的交易号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 支付接口返回的交易号
        /// </summary>
        public string PayNumber { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayPrice { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        public PayState PayState { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }

    #endregion

    #region 机票系统-支付明细 交易状态

    /// <summary>
    /// 交易状态
    /// </summary>
    public enum PayState
    {
        /// <summary>
        /// 未提交到支付接口
        /// </summary>
        未提交到支付接口 = 0,
        /// <summary>
        /// 交易完成
        /// </summary>
        交易完成 = 1,
        /// <summary>
        /// 交易失败
        /// </summary>
        交易失败 = 2,
        /// <summary>
        /// 支付接口正在处理
        /// </summary>
        支付接口正在处理 = 3
    }

    #endregion

    #region 机票系统-支付明细记录项的类型

    /// <summary>
    /// 机票金额流水明细记录项的类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 采购商付款到平台(订单)
        /// </summary>
        采购商付款到平台_订单 = 0,
        /// <summary>
        /// 供应商付款到平台(购买运价)
        /// </summary>
        供应商付款到平台_购买运价 = 1,
        /// <summary>
        /// 平台到供应商(订单分润)
        /// </summary>
        平台到供应商_订单 = 2,
        /// <summary>
        /// 平台到采购商(订单拒绝出票)
        /// </summary>
        平台到采购商_订单 = 3,
        /// <summary>
        /// 平台到采购商(变更退票)
        /// </summary>
        平台到采购商_变更 = 4,
        /// <summary>
        /// 供应商到平台(变更退票)
        /// </summary>
        供应商到平台_变更 = 5
    }

    #endregion
}
