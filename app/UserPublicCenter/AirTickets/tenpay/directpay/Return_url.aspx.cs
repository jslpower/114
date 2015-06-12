using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tenpay;
using AlipayClass;
namespace UserPublicCenter.AirTickets.tenpay.directpay
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
                decimal total_fee = decimal.Parse(resHandler.getParameter("total_fee"))/100;
                //支付结果
                string pay_result = resHandler.getParameter("pay_result");
                //订单号
                string order_no = resHandler.getParameter("sp_billno");
                //支付账号
                string buyer_email = "";


                EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                EyouSoft.Model.TicketStructure.OrderInfo info = orderBll.GetOrderInfoByNo(order_no);
                if (info.OrderState == EyouSoft.Model.TicketStructure.OrderState.审核通过)
                {

                    IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list = EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance().GetTicketCompanyAccountList(info.SupplierCId);//获取供应商所有账户
                    string sellAccount = "";//供应商账户
                    EyouSoft.Model.TicketStructure.TicketCompanyAccount accountModel = list.FirstOrDefault(i => i.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通);//获取供应商账户实体
                    if (accountModel != null)
                        sellAccount = accountModel.AccountNumber;//赋值供应商账户

                    string batchNo = "";
                    decimal IntoRatio = decimal.Parse(TenpayParameters.TongyeFee);
                    IList<EyouSoft.Model.TicketStructure.TicketPay> payList = orderBll.GetPayList(info.OrderId, EyouSoft.Model.TicketStructure.ItemType.采购商付款到平台_订单, order_no, null);
                    if ("0".Equals(pay_result))
                    {
                        if (payList != null && payList.Count > 0)
                        {

                            orderBll.PayAfterCallBack(trade_no, total_fee, EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, buyer_email, "", order_no, DateTime.Now, "");
                        }
                        else
                        {

                            orderBll.PayBefore(info.OrderId, order_no, sellAccount, IntoRatio, info.BuyerUId, info.BuyerCId, total_fee, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, info.SupplierCId, "", out batchNo);
                            orderBll.PayAfterCallBack(trade_no, total_fee, EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, buyer_email, "", order_no, DateTime.Now, "");
                        }

                        //调用doShow, 打印meta值跟js代码,告诉财付通处理成功,并在用户浏览器显示show.aspx页面.
                        resHandler.doShow(string.Format("/AirTickets/tenpay/directpay/Show.aspx?order_no={0}&total_fee={1}&result={2}", order_no, total_fee, "1"));
                    }
                    else
                    {
                        //当做不成功处理
                        if (payList != null && payList.Count > 0)
                        {
                            orderBll.PayAfterCallBack(trade_no, total_fee, EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, buyer_email, "", order_no, DateTime.Now, "");
                        }
                        else
                        {
                            orderBll.PayBefore(info.OrderId, order_no, sellAccount, IntoRatio, info.BuyerUId, info.BuyerCId, total_fee, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, info.SupplierCId, "", out batchNo);
                            orderBll.PayAfterCallBack(trade_no, total_fee, EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, buyer_email, "", order_no, DateTime.Now, "");
                        }
                        resHandler.doShow(string.Format("/AirTickets/tenpay/directpay/Show.aspx?order_no={0}&total_fee={1}&result={2}", order_no, total_fee, "0"));
                        //Response.Write("支付失败");
                    }

                }

                //获取debug信息,建议把debug信息写入日志，方便定位问题
                //string debuginfo = resHandler.getDebugInfo();
                //Response.Write("<br/>debuginfo:" + debuginfo + "<br/>");
            }
            else
            {
               
                    Response.Write("认证签名失败");

                

            }
        }
    }
}
