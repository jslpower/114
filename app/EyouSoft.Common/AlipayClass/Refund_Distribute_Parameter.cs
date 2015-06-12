using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AlipayClass
{
    /// <summary>
    /// 退款接口中使用到的，分润方退款数据格式
    /// </summary>
    public class Refund_Distribute_Parameter
    {
        public Refund_Distribute_Parameter(string trade_no, string distribute_account, string _PlatformIntermediate_Account,
            string refundPrice, string refundremark)
        {
            Trade_No = trade_no;
            Distribute_Account = distribute_account;
            PlatformIntermediate_Account = _PlatformIntermediate_Account;
            RefundPrice = refundPrice;
            RefundRemark = refundremark;
        }

        /// <summary>
        /// 原付款支付宝交易号
        /// </summary>
        public string Trade_No { get; set; }
        /// <summary>
        /// 转出人Email（原分润帐户）
        /// </summary>
        public string Distribute_Account { get; set; }
        /// <summary>
        /// 转入人Email（平台中间帐户）
        /// </summary>
        public string PlatformIntermediate_Account { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public string RefundPrice { get; set; }
        /// <summary>
        /// 退款理由
        /// </summary>
        public string RefundRemark { get; set; }

        public override string ToString()
        {
            /*
            分润方退款格式：
    原付款支付宝交易号^0^退款理由
    |转出人Email（原分润帐户）^转出人userId^转入人Email（平台中间帐户）^转入人userId^退款金额^退款理由
            */
            return string.Format("{0}^0^{1}|{2}^^{3}^^{4}^{5}", Trade_No, RefundRemark, Distribute_Account, PlatformIntermediate_Account, RefundPrice, RefundRemark);
        }
    }
}