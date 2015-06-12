using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using AlipayClass;

/// <summary>
/// 查询电子协议签约接口
/// </summary>
public class Query_Customer
{
    private string gateway = "";                //网关地址
    private string _key = "";                    //交易安全校验码
    private string _input_charset = "";         //编码格式
    private string _sign_type = "";              //加密方式（签名方式）
    private string mysign = "";                 //加密结果（签名结果）
    private Dictionary<string, string> sPara = new Dictionary<string, string>();//要加密的字符串
    private string biz_type = "10004";  //机票对应的biz_type为10004

    public Query_Customer(string partner, string key, string signType, string input_charset, string user_email)
	{
        gateway = "https://www.alipay.com/cooperate/gateway.do?";
        _key = key.Trim();
        _input_charset = input_charset.ToLower();
        _sign_type = signType.ToUpper();
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

        //构造加密参数数组
        sParaTemp.Add("service", "query_customer_protocol");
        sParaTemp.Add("partner", partner);
        sParaTemp.Add("_input_charset", _input_charset);
        sParaTemp.Add("user_email", user_email);
        sParaTemp.Add("biz_type",biz_type);

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

    public Query_Customer_Result GetResult()
    {
        CreateSSL ssl = new CreateSSL(this.Create_url());
        string responseFromServer = ssl.GetResponse();


        Query_Customer_Result result = new Query_Customer_Result(responseFromServer);

        return result;
    }
}
