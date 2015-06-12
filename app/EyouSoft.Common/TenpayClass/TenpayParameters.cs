using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web;

namespace tenpay
{
    /// <summary>
    /// 财付通支付接口使用参数集合
    /// </summary>
    public class TenpayParameters
    {
        /// <summary>
        /// 平台商户ID
        /// </summary>
        public static string Bargainor_ID = WebConfigurationManager.AppSettings["bargainor_id"];
        /// <summary>
        /// 安全检验码
        /// </summary>
        public static string Key = WebConfigurationManager.AppSettings["ten_key"];
        /// <summary>
        /// 财付通 平台中间帐户
        /// </summary>
        public static string Seller_mailer = WebConfigurationManager.AppSettings["ten_seller_mailer"];
        /// <summary>
        /// 加密方式
        /// </summary>
        public static string SignType = WebConfigurationManager.AppSettings["ten_sign_type"];
        /// <summary>
        /// 字符编码格式
        /// </summary>
        public static string Input_Charset = WebConfigurationManager.AppSettings["ten_input_charset"];

        public static string DomainPath = WebConfigurationManager.AppSettings["ten_DomainPath"];

        /// <summary>
        /// 财付通交易手续费
        /// </summary>
        public static string AlipayFee = WebConfigurationManager.AppSettings["tenpayfee"];
        /// <summary>
        /// 平台交易费
        /// </summary>
        public static string TongyeFee = WebConfigurationManager.AppSettings["tongyeFee"];
        /// <summary>
        /// 证书相对地址
        /// </summary>
        public static string PfxPath = WebConfigurationManager.AppSettings["pfxPath"];
        /// <summary>
        /// 证书密码
        /// </summary>
        public static string PfxPwd = WebConfigurationManager.AppSettings["pfxPwd"];
    }
}
