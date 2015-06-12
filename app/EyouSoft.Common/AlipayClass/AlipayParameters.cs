using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;


namespace AlipayClass
{
    /// <summary>
    /// AlipayParameters 的摘要说明
    /// </summary>
    public class AlipayParameters
    {
        /// <summary>
        /// 合作身份者ID
        /// </summary>
        public static string Partner = WebConfigurationManager.AppSettings["partner"];
        /// <summary>
        /// 安全检验码
        /// </summary>
        public static string Key = WebConfigurationManager.AppSettings["ali_key"];
        /// <summary>
        /// 签约支付宝账号或卖家支付宝帐户
        /// </summary>
        public static string Seller_mailer = WebConfigurationManager.AppSettings["ali_seller_mailer"];
        /// <summary>
        /// 字符编码格式
        /// </summary>
        public static string Input_Charset = WebConfigurationManager.AppSettings["ali_input_charset"];
        /// <summary>
        /// 加密方式
        /// </summary>
        public static string SignType = WebConfigurationManager.AppSettings["ali_sign_type"];

        public static string DomainPath = WebConfigurationManager.AppSettings["ali_DomainPath"];
        /// <summary>
        /// 支付宝交易费
        /// </summary>
        public static string AlipayFee = WebConfigurationManager.AppSettings["ali_alipayFee"];
        /// <summary>
        /// 平台交易费
        /// </summary>
        public static string TongyeFee = WebConfigurationManager.AppSettings["tongyeFee"];
    }
}
