using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlipayClass;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Generic;
using System.IO;
using System.Collections.Specialized;

public partial class refund_notify_url : System.Web.UI.Page
{
    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        AlipayFunction.log_result(Server.MapPath("../log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", ex.Message + "\n" + ex.StackTrace + "\n" + ex.TargetSite);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Error += new EventHandler(Page_Error);
        SortedDictionary<string, string> sArrary = GetRequestPost();
        ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
        string partner = AlipayParameters.Partner;                //合作身份者ID
        string key = AlipayParameters.Key;    //安全检验码
        string input_charset = AlipayParameters.Input_Charset;                     //字符编码格式 目前支持 gbk 或 utf-8
        string sign_type = AlipayParameters.SignType;                           //加密方式 不需修改
        string transport = "http";                         //访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http
        //////////////////////////////////////////////////////////////////////////////////////////////

        if (sArrary.Count > 0)//判断是否有带返回参数
        {
            AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.Form["notify_id"], partner, key, input_charset, sign_type, transport);
            string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string sign = Request.Form["sign"];         //获取支付宝反馈回来的sign结果
            string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的加密结果

            //写日志记录（若要调试，请取消下面两行注释）
            string sWord = "refund_Notify1_URL:responseTxt=" + responseTxt + "\n notify_url_log:sign=" + Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + aliNotify.PreSignStr;


            //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (responseTxt == "true" && sign == mysign)//验证成功
            {
                //请根据您的业务逻辑来编写程序（以下代码仅作参考）
                //获取支付宝的通知返回参数
                string notify_id = Request.Form["notify_id"];
                string notify_time = Request.Form["notify_time"];

                string batch_no = Request.Form["batch_no"];

                sWord += "\n batch_no: " + batch_no;
                string success_num = Request.Form["success_num"];
                sWord += "\n success_num: " + success_num;
                string result_details = Request.Form["result_details"];
                sWord += "\n result_details: " + result_details;


                Refund_Distribute_Result result = Refund_Distribute_Result.Load(result_details);

                EyouSoft.IBLL.TicketStructure.ITicketOrder OrderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                IList<EyouSoft.Model.TicketStructure.TicketPay> PayList = OrderBll.GetPayList(string.Empty, EyouSoft.Model.TicketStructure.ItemType.供应商到平台_变更, string.Empty, batch_no);

                
                if (result.IsSuccess)//成功
                {
                    bool isResult = false;
                    if (PayList != null && PayList.Count > 0)
                    {
                        EyouSoft.Model.TicketStructure.TicketPay PayModel = PayList[0];

                        // 供应商退票到平台成功之后更新明细记录状态
                        isResult = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(
                            result.Trade_No, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易完成, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batch_no);

                        AlipayFunction.log_result(Server.MapPath("../log/供应商退款到平台—" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);

                        // 供应商退款成功之后发起平台退款
                        if (isResult)
                        {
                            string BatchNo = string.Empty;
                            // 平台退款到采购商
                            bool aa = OrderBll.BackOrDisableTicketBeforePC(PayModel.ItemId, PayModel.TradeNo, PayModel.PayPrice, PayModel.CurrUserId, PayModel.PayType, PayModel.CurrCompanyId, PayModel.Remark, out BatchNo);

                            //发起平台退款
                            Get(result.Trade_No, PayModel.PayPrice, BatchNo);
                        }
                    }
                    PayList = null;
                }
                else
                {
                    if (PayList != null && PayList.Count > 0)
                    {
                        EyouSoft.Model.TicketStructure.TicketPay PayModel = PayList[0];

                        // 供应商退票到平台成功之后更新明细记录状态
                        EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(result.Trade_No, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batch_no);
                        PayModel = null;
                    }
                    AlipayFunction.log_result(Server.MapPath("../log/供应商退款到平台—" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);
                }
                sWord += "\n TRUE";
                Response.Write("success");
            }
            else//验证失败
            {
                sWord += "\n FAIL";
                Response.Write("fail");
            }

        }
        else
        {
            Response.Write("无通知参数");
        }
    }

    /// <summary>
    /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestPost()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.Form;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
        }

        return sArray;
    }

    public void Get(string tradeno, decimal payprice, string BatchNo)
    {
        string partner = AlipayParameters.Partner;                                     //合作身份者ID
        string key = AlipayParameters.Key;                         //安全检验码
        string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
        string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改

        string notify_url = AlipayParameters.DomainPath + "/TicketsCenter/alipay/refund/notify2_url.aspx";
        string batch_no = BatchNo;
        string refund_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string batch_num = "1";

       

        string sWord = "Send refund_Notify2_URL:"+batch_no;

        //string trade_no = Request.Form["trade_no"];

       
        //string detail_data = "2010110531330647^0.02^理由$alipay-test01@alipay.com^^0^理由";
        //string detail_data = "2010110849913847^0.02^理由";

        //decimal aliprice = payprice * decimal.Parse(AlipayParameters.AlipayFee);
        //计算支付宝退款金额对应的手续费
        //decimal alipayFee = Refund_Platform_Parameter.ComputeAlipayFee(PlatformRefund);
        decimal aliPrice = Refund_Platform_Parameter.ComputeAlipayFee(payprice); 
        Refund_Platform_Parameter parameter = null;

        if (aliPrice <= 0)
        {
            parameter = new Refund_Platform_Parameter(tradeno, payprice.ToString("F2"), "平台退款");
        }
        else {
            parameter = new Refund_Platform_Parameter(tradeno, payprice.ToString("F2"), "平台退款", 
                AlipayParameters.Seller_mailer, aliPrice.ToString("F2"), "支付宝服务费");
        }
        string detail_data = parameter.ToString();

        RefundNoPwd refund = new RefundNoPwd(partner, key, sign_type, input_charset, notify_url, batch_no, refund_date, batch_num, detail_data);

        string url = refund.Create_url();
        
        CreateSSL ssl = new CreateSSL(url);
        string responseFromServer = ssl.GetResponse();

        Distribute_royalty_Result result = new Distribute_royalty_Result(responseFromServer);

        if (result.IsSuccess)
        {
            sWord += "\n TRUE";
        }
        else
        {
            sWord += "\n " + result.ErrorCode;
        }
    }
}
