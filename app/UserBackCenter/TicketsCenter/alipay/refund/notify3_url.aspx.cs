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
using System.Collections.Generic;
using System.Collections.Specialized;
using EyouSoft.Common;

namespace UserBackCenter.TicketsCenter.alipay.refund
{
    /// <summary>
    /// 中间平台直接退款给采购商
    /// </summary>
    /// 罗丽娥   2010-11-09
    public partial class notify3_url : System.Web.UI.Page
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
                string sWord = "refund_Notify3_URL:responseTxt=" + responseTxt + "\n notify_url_log:sign=" + Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + aliNotify.PreSignStr;

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

                    /*
                     平台退款格式：
    原付款支付宝交易号^退款金额^处理结果码$被收费人Email^被收费人userId^退款金额^处理结果码

                      * */

                    Refund_Platform_Result result = Refund_Platform_Result.Load(result_details);

                    sWord += "\n RefundSuccess:" + result.IsRefundSuccess;
                    sWord += "\n Refund_Charges_Success:" + result.IsRefund_ChargesSuccess;

                    sWord += "\n Trade_No:"+result.Trade_No;
                    sWord += "\n price:" + result.RefundPrice;

                    IList<EyouSoft.Model.TicketStructure.TicketPay> PayList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetPayList(string.Empty, EyouSoft.Model.TicketStructure.ItemType.平台到采购商_订单,string.Empty, batch_no);
                    if (result.IsRefundSuccess && result.IsRefund_ChargesSuccess)
                    {
                        sWord += "\n paylist:" + PayList.Count;
                        if (PayList != null && PayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = PayList[0];
                            decimal? price = PayModel.PayPrice;
                            DateTime? time = DateTime.Now;

                            // 拒绝出票完成后更新订单状态为‘拒绝出票’，并修改支付明细状态
                            bool aa = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(result.Trade_No, price, EyouSoft.Model.TicketStructure.PayState.交易完成, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, time, batch_no);
                            PayModel = null;
                            sWord += "\n aa:"+ aa;
                        }
                        PayList = null;
                    }
                    else {
                        if (PayList != null && PayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = PayList[0];

                            EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(result.Trade_No, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batch_no);
                            PayModel = null;
                        }
                        PayList = null;
                    }

                    Response.Write("success");
                }
                else//验证失败
                {
                    sWord += "\n FAIL";
                    Response.Write("fail");
                }

                AlipayFunction.log_result(Server.MapPath("../log/" +"Refund3-"+ DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);
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
    }
}
