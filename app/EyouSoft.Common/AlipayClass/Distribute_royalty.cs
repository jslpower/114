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
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace AlipayClass
{
    /// <summary>
    /// 分润
    /// </summary>
    public class Distribute_royalty
    {
        private string gateway = "";                //网关地址
        private string _key = "";                    //交易安全校验码
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";              //加密方式（签名方式）
        private string mysign = "";                 //加密结果（签名结果）
        private Dictionary<string, string> sPara = new Dictionary<string, string>();//要加密的字符串

        public Distribute_royalty(string partner, string key, string signType, string input_charset, string out_trade_no, string out_bill_no,
            string royalty_type, string royalty_parameters)
        {
            gateway = "https://www.alipay.com/cooperate/gateway.do?";
            _key = key.Trim();
            _input_charset = input_charset.ToLower();
            _sign_type = signType.ToUpper();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            //构造加密参数数组
            sParaTemp.Add("service", "distribute_royalty");
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("_input_charset", _input_charset);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("out_bill_no", out_bill_no);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);


            sPara = AlipayFunction.Para_filter(sParaTemp);
            //获得签名结果
            mysign = AlipayFunction.Build_mysign(sPara, _key, _sign_type, _input_charset);
        }


        /// <summary>
        /// 构造请求URL（GET方式请求）
        /// </summary>
        /// <returns>请求url</returns>
        private string Create_url()
        {
            string strUrl = gateway;
            string arg = AlipayFunction.Create_linkstring_urlencode(sPara);	//把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            strUrl = strUrl + arg + "sign=" + mysign + "&sign_type=" + _sign_type;
            return strUrl;
        }


        public Distribute_royalty_Result GetResult()
        {
            CreateSSL ssl = new CreateSSL(this.Create_url());
            string responseFromServer = ssl.GetResponse();


            Distribute_royalty_Result result = new Distribute_royalty_Result(responseFromServer);

            return result;
        }

    }
}
