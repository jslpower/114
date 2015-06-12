using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlipayClass;
using System.Collections.Specialized;
using EyouSoft.Common;

namespace UserBackCenter.TicketsCenter.alipay.repurchase
{
    public partial class Return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SortedDictionary<string, string> sArrary = GetRequestGet();
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
            string partner = AlipayParameters.Partner;                //合作身份者ID
            string key = AlipayParameters.Key;    //安全检验码
            string input_charset = AlipayParameters.Input_Charset;                     //字符编码格式 目前支持 gbk 或 utf-8
            string sign_type = AlipayParameters.SignType;                           //加密方式 不需修改
            string transport = "http";                         //访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http
            //////////////////////////////////////////////////////////////////////////////////////////////

            if (sArrary.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.QueryString["notify_id"], partner, key, input_charset, sign_type, transport);
                string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                string sign = Request.QueryString["sign"];  //获取支付宝反馈回来的sign结果
                string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的加密结果

                ////写日志记录（若要调试，请取消下面两行注释）
                //string sWord = "Return_URL:responseTxt=" + responseTxt + "\n return_url_log:sign=" + Request.QueryString["sign"] + "&mysign=" + mysign + "\n return回来的参数：" + aliNotify.PreSignStr;
                //AlipayFunction.log_result(Server.MapPath("../log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);

                //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
                if (responseTxt == "true" && sign == mysign)//验证成功
                {
                    //请根据您的业务逻辑来编写程序（以下代码仅作参考）
                    //获取支付宝的通知返回参数
                    string trade_no = Request.QueryString["trade_no"];      //支付宝交易号
                    string order_no = Request.QueryString["out_trade_no"];	//获取订单号
                    string total_fee = Request.QueryString["total_fee"];	//获取总金额
                    string subject = Request.QueryString["subject"];        //商品名称、订单名称
                    string body = Request.QueryString["body"];              //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];//买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];//交易状态


                    // string notify_id = Request.QueryString["notify_id"];
                    // string notify_time = Request.QueryString["notify_time"];
                    // string buyer_id = Request.QueryString["buyer_id"];

                    EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();

                    IList<EyouSoft.Model.TicketStructure.TicketPay> payList = orderBll.GetPayList("", EyouSoft.Model.TicketStructure.ItemType.供应商付款到平台_购买运价, order_no, "");
                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //为了保证不被重复调用，或重复执行数据库更新程序，请判断该笔交易状态是否是订单未处理状态
                        //string order_no="201011080010";
                        //string trade_no="2010110861004313";
                        //string total_fee="0.01";
                        //string buyer_email="enowalipay1@163.com";
                        //string seller_mailer="pay2@tongye114.com";
                        if (payList != null && payList.Count > 0)
                        {
                            if (payList[0].PayState != EyouSoft.Model.TicketStructure.PayState.交易完成)
                            {
                                EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().PayAfter(trade_no, Utils.GetDecimal(total_fee), EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, order_no, DateTime.Now, "");
                                Response.Write("购买运价航线成功，<a href='" + Domain.UserBackCenter + "/ticketscenter/freightmanage/freightadd.aspx" + "' >点此返回机票供应商后台</a>!");
                            }
                            if (payList[0].PayState == EyouSoft.Model.TicketStructure.PayState.交易完成)
                            {
                                Response.Write("购买运价航线成功，<a href='" + Domain.UserBackCenter + "/ticketscenter/freightmanage/freightadd.aspx" + "' >点此返回机票供应商后台</a>!");
                            }
                        }
                    }
                    else
                    {

                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }

                    // 请根据您的业务逻辑来编写程序（以上代码仅作参考）
                }
                else//验证失败
                {

                }
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}
