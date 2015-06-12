using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AlipayClass;
namespace UserPublicCenter.AirTickets
{
    public partial class Return_url :EyouSoft.Common.Control.FrontPage
    {

        protected string order_no;//订单号
        protected string total_fee;//交易总金额
        protected string proName;//商品名称
        protected string proDetail;//商品描述
        protected string buyerAccount;//买家账号
        protected string resultMess;//交易结果
        protected string orderid;//订单编号

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "完成-组团预定/散拼-机票";
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
                    order_no = Request.QueryString["out_trade_no"];	//获取订单号
                    total_fee = Request.QueryString["total_fee"];	//获取总金额
                    string subject = Request.QueryString["subject"];        //商品名称、订单名称
                    string body = Request.QueryString["body"];              //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];//买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];//交易状态
                    int sOld_trade_status = 0;							    //获取商户数据库中查询得到该笔交易当前的交易状态
                    proName =string.Format("同业114机票平台(订单号：{0})",order_no);
                    buyerAccount = buyer_email;
                    string notify_id = Request.QueryString["notify_id"];
                    string notify_time = Request.QueryString["notify_time"];
                    string buyer_id = Request.QueryString["buyer_id"];

                    //打印页面
                    //lbTrade_no.Text = trade_no;
                    //lbOut_trade_no.Text = order_no;
                    //lbTotal_fee.Text = total_fee;
                    //lbSubject.Text = subject;
                    //lbBody.Text = body;
                    //lbBuyer_email.Text = buyer_email;
                    //lbTrade_status.Text = trade_status;
                    //lbVerify.Text = "验证成功";

                    //lblNotifyId.Text = notify_id;
                    //lblNotifyTime.Text = notify_time;
                    //lblBuyerID.Text = buyer_id;

                    //假设：
                    //sOld_trade_status="0"	表示订单未处理；
                    //sOld_trade_status="1"	表示交易成功（TRADE_FINISHED/TRADE_SUCCESS）
                   
                    EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                    EyouSoft.Model.TicketStructure.OrderInfo info = orderBll.GetOrderInfoByNo(order_no);
                    orderid = info.OrderId;
                    if (info.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
                        proDetail = string.Format("订单号：{0}/航程信息：单程 {1}/{2}至{3}/", order_no, info.LeaveTime.ToString("yyyy-MM-dd"), info.HomeCityName, info.DestCityName);
                    else
                        proDetail = string.Format("订单号：{0}/航程信息：去程 {1}/{2}至{3}/回程 {4}/{5}-{6}", order_no, info.LeaveTime.ToString("yyyy-MM-dd"), info.HomeCityName, info.DestCityName,info.ReturnTime,info.DestCityName,info.HomeCityName);
                    if (info.OrderState == EyouSoft.Model.TicketStructure.OrderState.审核通过)
                    {
                        IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list = EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance().GetTicketCompanyAccountList(info.SupplierCId);//获取供应商所有账户
                        string sellAccount = "";//供应商账户
                        EyouSoft.Model.TicketStructure.TicketCompanyAccount accountModel = list.FirstOrDefault(i => i.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝);//获取供应商账户实体
                        if (accountModel != null)
                            sellAccount = accountModel.AccountNumber;//赋值供应商账户

                        string batchNo = "";
                        //decimal IntoRatio = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(info.SupplierCId).IntoRatio;
                        decimal IntoRatio = decimal.Parse(AlipayParameters.TongyeFee);
                        IList<EyouSoft.Model.TicketStructure.TicketPay> payList = orderBll.GetPayList(info.OrderId, EyouSoft.Model.TicketStructure.ItemType.采购商付款到平台_订单, order_no, null);


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
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                            else
                            {
                                orderBll.PayBefore(info.OrderId, order_no, sellAccount, IntoRatio, info.BuyerUId, info.BuyerCId, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, info.SupplierCId, "", out batchNo);
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                            
                            resultMess = "交易成功";
                            //if (sOld_trade_status < 1)
                            //{
                            //    //根据订单号更新订单，把订单状态处理成交易成功
                            //}
                        }
                        else
                        {
                            if (payList != null && payList.Count > 0)
                            {
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                            else
                            {
                                orderBll.PayBefore(info.OrderId, order_no, sellAccount, IntoRatio, info.BuyerUId, info.BuyerCId, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, info.SupplierCId, "", out batchNo);
                                orderBll.PayAfterCallBack(trade_no, decimal.Parse(total_fee), EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, buyer_email, "", order_no, DateTime.Now, "");
                            }
                            //Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                            resultMess = "交易失败";
                        }
                    }
                    else
                    {
                        resultMess = info.OrderState.ToString();
                    }
                   // 请根据您的业务逻辑来编写程序（以上代码仅作参考）
                }
                else//验证失败
                {
                    resultMess = "验证失败";
                }
            }
            else
            {
                resultMess = "无返回参数";
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

        protected virtual void AddGenericLink(string type, string relation, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = type;
            link.Attributes["rel"] = relation;
            link.Attributes["href"] = href;
            Page.Header.Controls.Add(link);
        }

        /// <summary>
        /// 添加Javascript外部文件到html页面
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="placeInBottom">是否添加在html代码底部</param>
        /// <param name="addDeferAttribute">是否添加defer属性</param>
        public virtual void AddJavaScriptInclude(string url, bool placeInBottom, bool addDeferAttribute)
        {
            if (placeInBottom)
            {
                string script = "<script type=\"text/javascript\"" + (addDeferAttribute ? " defer=\"defer\"" : string.Empty) + " src=\"" + url + "\"></script>";
                ClientScript.RegisterStartupScript(GetType(), url.GetHashCode().ToString(), script);
            }
            else
            {
                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes["type"] = "text/javascript";
                script.Attributes["src"] = url;
                if (addDeferAttribute)
                {
                    script.Attributes["defer"] = "defer";
                }

                Page.Header.Controls.Add(script);
            }
        }
    }
}
