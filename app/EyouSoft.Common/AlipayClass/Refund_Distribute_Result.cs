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
    /// 退款接口中使用到的，分润方退款结果数据格式
    /// </summary>
    public class Refund_Distribute_Result
    {
        public Refund_Distribute_Result()
        {
        }

        public static Refund_Distribute_Result Load(string resultDetails)
        {
            Refund_Distribute_Result result = new Refund_Distribute_Result();

            /*
                     分润方退款结果数据格式：
    原付款支付宝交易号^0^处理结果码
                |转出人Email（原分润帐户）^转出人userId^转入人Email（平台中间帐户）^转入人userId^退款金额^处理结果码

                      * */

            string[] arr = resultDetails.Split('|');
            string[] arr1 = arr[0].Split('^');
            string[] arr2 = arr[1].Split('^');

            result.Trade_No = arr1[0];
            string resultCode1 = arr1[arr1.Length - 1].ToUpper();

            result.Distribute_Account = arr2[0];
            result.PlatformIntermediate_Account = arr2[2];
            result.RefundPrice = arr2[4];
            string resultCode2 = arr2[arr2.Length - 1].ToUpper();

            result.ErrorCode = resultCode2;

            if (resultCode1 == "SUCCESS" && resultCode2 == "SUCCESS")
            {
                result.IsSuccess = true;
            }
            else
            {
                result.IsSuccess = false;
            }

            return result;
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
        /// 处理结果是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 处理结果码
        /// </summary>
        public string ErrorCode { get; set; }
    }
}
