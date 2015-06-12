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
    /// 退款接口中使用到的，平台退款结果数据格式
    /// </summary>
    public class Refund_Platform_Result
    {
        public Refund_Platform_Result()
        {
        }

        public static Refund_Platform_Result Load(string resultDetails)
        {

            /*
                     平台退款格式：
    原付款支付宝交易号^退款金额^处理结果码$被收费人Email^被收费人userId^退款金额^处理结果码
             或者 
             原付款支付宝交易号^退款金额^处理结果码

                      * */

            Refund_Platform_Result result = new Refund_Platform_Result();

            string[] arr = resultDetails.Split('$');

            if (arr.Length > 1)
            {
                string[] arr1 = arr[0].Split('^');
                string[] arr2 = arr[1].Split('^');

                result.Trade_No = arr1[0];
                result.RefundPrice = arr1[1];
                string resultCode = arr1[arr1.Length - 1].ToUpper();
                result.IsRefundSuccess = resultCode == "SUCCESS" ? true : false;
                result.RefundErrorCode = resultCode;

                result.Refund_Charges_Account = arr2[0];
                result.Refund_Charges_Price = arr2[2];
                string resultCode2 = arr2[arr2.Length - 1].ToUpper();
                result.IsRefund_ChargesSuccess = resultCode2 == "SUCCESS" ? true : false;
                result.IsRefund_ChargesSuccess = true;
                result.Refund_Charges_ErrorCode = resultCode2;
            }
            else
            {
                string[] arr1 = arr[0].Split('^');
                result.Trade_No = arr1[0];
                result.RefundPrice = arr1[1];
                string resultCode = arr1[arr1.Length - 1].ToUpper();
                result.IsRefundSuccess = resultCode == "SUCCESS" ? true : false;
                result.RefundErrorCode = resultCode;

                result.Refund_Charges_Account = string.Empty;
                result.Refund_Charges_Price = "0";
                result.IsRefund_ChargesSuccess = true;
            }

            return result;
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
        /// 退交易是否成功
        /// </summary>
        public bool IsRefundSuccess { get; set; }
        /// <summary>
        /// 退交易 处理结果码
        /// </summary>
        public string RefundErrorCode { get; set; }
        /// <summary>
        /// 被收费人Email（也就是在交易的时候支付宝收取服务费的账户）
        /// </summary>
        public string Refund_Charges_Account { get; set; }
        /// <summary>
        /// 退收费金额
        /// </summary>
        public string Refund_Charges_Price { get; set; }
        /// <summary>
        /// 退收费是否成功
        /// </summary>
        public bool IsRefund_ChargesSuccess { get; set; }
        /// <summary>
        /// 退收费 处理结果码
        /// </summary>
        public string Refund_Charges_ErrorCode { get; set; }
    }
}
