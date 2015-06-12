using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.AirTickets.tenpay.directpay
{
    public partial class Show : EyouSoft.Common.Control.FrontPage
    {
        protected string order_no;//订单号
        protected string total_fee;//支付金额
        protected string proName;//商品名称
        protected string proDetail;//商品描述
        protected string resultMess;//交易结果
        protected string orderid;
        protected void Page_Load(object sender, EventArgs e)
        { 
            order_no = Utils.GetQueryStringValue("order_no");
            total_fee = Utils.GetQueryStringValue("total_fee");
            proName = string.Format("同业114机票平台(订单号：{0})", order_no);
            EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            EyouSoft.Model.TicketStructure.OrderInfo info = orderBll.GetOrderInfoByNo(order_no);
            orderid = info.OrderId;
            if (info.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
                proDetail = string.Format("订单号：{0}/航程信息：单程 {1}/{2}至{3}/", order_no, info.LeaveTime.ToString("yyyy-MM-dd"), info.HomeCityName, info.DestCityName);
            else
                proDetail = string.Format("订单号：{0}/航程信息：去程 {1}/{2}至{3}/回程 {4}/{5}-{6}", order_no, info.LeaveTime.ToString("yyyy-MM-dd"), info.HomeCityName, info.DestCityName, info.ReturnTime.ToString("yyyy-MM-dd"), info.DestCityName, info.HomeCityName);
            resultMess = Utils.GetQueryStringValue("result") == "1" ? "交易成功" : "交易失败";
        }
    }
}
