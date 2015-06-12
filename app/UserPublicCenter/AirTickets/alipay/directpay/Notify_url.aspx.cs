using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AlipayClass;

namespace UserPublicCenter.AirTickets.directpay
{
    public partial class Notify_url : System.Web.UI.Page
    {
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

                ////写日志记录（若要调试，请取消下面两行注释）
           string sWord = "Notify_URL:responseTxt=" + responseTxt + "\n notify_url_log:sign=" + Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + aliNotify.PreSignStr;


                //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
                if (responseTxt == "true" && sign == mysign)//验证成功
                {
                    //请根据您的业务逻辑来编写程序（以下代码仅作参考）
                    //获取支付宝的通知返回参数
                    string trade_no = Request.Form["trade_no"];         //支付宝交易号
                    string order_no = Request.Form["out_trade_no"];     //获取订单号
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string trade_status = Request.Form["trade_status"]; //交易状态



                    //string notify_id = Request.QueryString["notify_id"];
                    //string notify_time = Request.QueryString["notify_time"];
                    //string buyer_id = Request.QueryString["buyer_id"];

                    //string gmt_create = Request.QueryString["gmt_create"];
                    //string gmt_payment = Request.QueryString["gmt_payment"];

                    sWord += "\n TRUE";

                
                    //表示交易成功（TRADE_FINISHED/TRADE_SUCCESS）
                    EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
             
                    EyouSoft.Model.TicketStructure.OrderInfo info = orderBll.GetOrderInfoByNo(order_no);
                    if (info.OrderState == EyouSoft.Model.TicketStructure.OrderState.审核通过)
                    {
                        IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list = EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance().GetTicketCompanyAccountList(info.SupplierCId);//获取供应商所有账户
                        string sellAccount = "";//供应商账户
                        EyouSoft.Model.TicketStructure.TicketCompanyAccount accountModel = list.FirstOrDefault(i => i.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝);//获取供应商账户实体
                        if (accountModel != null)
                            sellAccount = accountModel.AccountNumber;//赋值供应商账户
                        
                        string batchNo = "";
                        decimal IntoRatio = decimal.Parse(AlipayParameters.TongyeFee);
                        IList<EyouSoft.Model.TicketStructure.TicketPay> payList = orderBll.GetPayList(info.OrderId, EyouSoft.Model.TicketStructure.ItemType.采购商付款到平台_订单, order_no, null);


                        if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
                        {
                            sWord += "/n 交易成功";
                            //为了保证不被重复调用，或重复执行数据库更新程序，请判断该笔交易状态是否是订单未处理状态
                            //string order_no="201011080010";
                            //string trade_no="2010110861004313";
                            //string total_fee="0.01";
                            //string buyer_email="enowalipay1@163.com";
                            //string seller_mailer="pay2@tongye114.com";
                            if (payList != null && payList.Count > 0)
                            {
                                sWord += "/n you";
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                            else
                            {
                                sWord += "/n meiyou";
                                orderBll.PayBefore(info.OrderId, order_no, sellAccount, IntoRatio, info.BuyerUId, info.BuyerCId, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, info.SupplierCId, "", out batchNo);
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                        }
                        else
                        {
                            sWord += "/n 交易失败";
                            if (payList != null && payList.Count > 0)
                            {
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                            else
                            {
                                orderBll.PayBefore(info.OrderId, order_no, sellAccount, IntoRatio, info.BuyerUId, info.BuyerCId, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, info.SupplierCId, "", out batchNo);
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                        }

                        AlipayFunction.log_result(Server.MapPath("../log/" + order_no +"-"+ DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);
                    }
                    Response.Write("success");
                }
                else//验证失败
                {
                    //sWord += "\n FAIL";
                    Response.Write("fail");
                }


            }
            else
            {
                Response.Write("无通知参数");
            }


        }

        void Page_Error(object sender, EventArgs e)
        {
            //Exception ex = Server.GetLastError();
            //AlipayFunction.log_result(Server.MapPath("../log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", ex.Message + "\n" + ex.Source);
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
