using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tenpay;
using AlipayClass;
using EyouSoft.Common;
namespace UserBackCenter.tenpay.directpay
{
    public partial class Return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //密钥
            string key = TenpayParameters.Key;

            //创建PayResponseHandler实例
            PayResponseHandler resHandler = new PayResponseHandler(Context);
            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //交易单号
                string trade_no = resHandler.getParameter("transaction_id");
                //金额金额,以分为单位
                decimal total_fee = Convert.ToDecimal(resHandler.getParameter("total_fee")) / 100;
                //支付结果
                string pay_result = resHandler.getParameter("pay_result");
                //订单号
                string order_no = resHandler.getParameter("sp_billno");
                //支付账号
                string buyer_email = "";
                EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                string sWord = "Notify_URL:trade_no=" + trade_no + "\n total_fee:sign=" + total_fee + "&order_no=" + order_no + "";

                IList<EyouSoft.Model.TicketStructure.TicketPay> payList = orderBll.GetPayList("", EyouSoft.Model.TicketStructure.ItemType.供应商付款到平台_购买运价, order_no, "");
                if ("0".Equals(pay_result))
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
                            EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().PayAfter(trade_no, total_fee, EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, "", order_no, DateTime.Now, "");
                            //调用doShow, 打印meta值跟js代码,告诉财付通处理成功,并在用户浏览器显示show.aspx页面.

                           // AlipayFunction.log_result(Server.MapPath("../log/运价购买 " + order_no + "-" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord + "||支付完成，修改状态完成");
                            resHandler.doShow(string.Format(Domain.UserBackCenter + "/TicketsCenter/tenpay/Show.aspx?logId={0}", payList[0].ItemId));
                        }
                        if (payList[0].PayState != EyouSoft.Model.TicketStructure.PayState.交易完成)
                        {
                           // AlipayFunction.log_result(Server.MapPath("../log/运价购买 " + order_no + "-" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord + "||支付完成，修改状态未完成!");
                            resHandler.doShow(string.Format(Domain.UserBackCenter + "/TicketsCenter/tenpay/Show.aspx?logId={0}", payList[0].ItemId));
                        }
                    }
                    else
                    {
                       // AlipayFunction.log_result(Server.MapPath("../log/运价购买 " + order_no + "-" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord + "||payList count 为0!");
                        Response.Write("未找到相关订单!");
                        //Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }
                }
                else
                {
                   // AlipayFunction.log_result(Server.MapPath("../log/运价购买 " + order_no + "-" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord + "||交易失败");
                    Response.Write("交易失败");
                    Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                }

            }

            //获取debug信息,建议把debug信息写入日志，方便定位问题

            else
            {
                Response.Write("认证签名失败");
            }
        }
    }
}
