using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EyouSoft.Common;

namespace AlipayClass
{
    /// <summary>
    ///  退款接口中使用到的，平台退款数据格式
    /// </summary>
    public class Refund_Platform_Parameter
    {
        /*
         当支付宝收费费用不为0时
         平台退款格式：
    原付款支付宝交易号^退款金额^退款理由
        $被收费人Email（也就是在交易的时候支付宝收取服务费的账户）^被收费人userId^退款金额^退款理由

           */
        public Refund_Platform_Parameter(string tradeNo, string refundPrice, string refundRemark,
            string refund_charges_account, string refund_charges_price, string refund_charges_remark)
        {
            Trade_No = tradeNo;
            RefundPrice = refundPrice;
            RefundRemark = refundRemark;
            Refund_Charges_Account = refund_charges_account;
            Refund_Charges_Price = refund_charges_price;
            Refund_Charges_Remark = refund_charges_remark;
        }

        /*
        当支付宝收费费用为0时
        平台退款格式：
    原付款支付宝交易号^退款金额^退款理由
          */
        public Refund_Platform_Parameter(string tradeNo, string refundPrice, string refundRemark
            )
        {
            Trade_No = tradeNo;
            RefundPrice = refundPrice;
            RefundRemark = refundRemark;
            Refund_Charges_Price = "0";
        }

        public override string ToString()
        {
            if (Refund_Charges_Price != "0")
            {
                return string.Format("{0}^{1}^{2}${3}^^{4}^{5}", Trade_No, RefundPrice, RefundRemark, Refund_Charges_Account,
                    Refund_Charges_Price, Refund_Charges_Remark);
            }
            else
            {
                return string.Format("{0}^{1}^{2}", Trade_No, RefundPrice, RefundRemark);
            }
        }
        /// <summary>
        /// 原付款支付宝交易号
        /// </summary>
        public string Trade_No { get; set; }
        /// <summary>
        /// 退交易金额
        /// </summary>
        public string RefundPrice { get; set; }
        /// <summary>
        /// 退交易理由
        /// </summary>
        public string RefundRemark { get; set; }
        /// <summary>
        /// 被收费人Email（也就是在交易的时候支付宝收取服务费的账户）
        /// </summary>
        public string Refund_Charges_Account { get; set; }
        /// <summary>
        /// 退收费金额
        /// </summary>
        public string Refund_Charges_Price { get; set; }
        /// <summary>
        /// 退收费理由
        /// </summary>
        public string Refund_Charges_Remark { get; set; }

        /// <summary>
        /// 在支付宝，平台退款到采购商的时候，根据退款金额计算要从支付宝退回的支付宝手续费
        /// </summary>
        /// <param name="refundFee">退款金额(以元为单位)</param>
        /// <returns></returns>
        public static decimal ComputeAlipayFee(decimal refundFee)
        {
            decimal alipayFeeDiscount = decimal.Parse(AlipayClass.AlipayParameters.AlipayFee);

            decimal alipayFee = refundFee * alipayFeeDiscount;

            string fee = alipayFee.ToString();
            fee = Utils.FilterEndOfTheZeroString(fee);

            if (fee.Contains("."))
            {
                int dotIndex = fee.IndexOf('.');
                if (fee.Length - 1 - dotIndex > 2)
                {
                    fee = fee.Substring(0, dotIndex + 2 + 1);
                }
                else
                {
                    fee = fee.Substring(0, fee.Length);
                }
            }

            return decimal.Parse(fee);
        }

    }
}
