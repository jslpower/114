using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using AlipayClass;

namespace AlipayClass
{
    /// <summary>
    /// 即时到账退款（无密）
    /// </summary>
    public class RefundNoPwd
    {
        private string gateway = "";                //网关地址
        private string _key = "";                    //交易安全校验码
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";              //加密方式（签名方式）
        private string mysign = "";                 //加密结果（签名结果）
        private Dictionary<string, string> sPara = new Dictionary<string, string>();//要加密的字符串

        public RefundNoPwd(string partner, string key, string signType, string input_charset, string notify_url,
            string batch_no, string refund_date, string batch_num, string detail_data)
        {
            gateway = "https://www.alipay.com/cooperate/gateway.do?";
            _key = key.Trim();
            _input_charset = input_charset.ToLower();
            _sign_type = signType.ToUpper();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            //构造加密参数数组
            sParaTemp.Add("service", "refund_fastpay_by_platform_nopwd");
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("_input_charset", _input_charset);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("batch_no", batch_no);
            sParaTemp.Add("refund_date", refund_date);
            sParaTemp.Add("batch_num", batch_num);
            sParaTemp.Add("detail_data", detail_data);

            sPara = AlipayFunction.Para_filter(sParaTemp);
            //获得签名结果
            mysign = AlipayFunction.Build_mysign(sPara, _key, _sign_type, _input_charset);

        }


        /// <summary>
        /// 构造请求URL（GET方式请求）
        /// </summary>
        /// <returns>请求url</returns>
        public string Create_url()
        {
            string strUrl = gateway;
            string arg = AlipayFunction.Create_linkstring_urlencode(sPara);	//把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            strUrl = strUrl + arg + "sign=" + mysign + "&sign_type=" + _sign_type;
            return strUrl;
        }
    }
}
