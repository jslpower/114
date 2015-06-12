using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 根据订单编号获的游客信息
    /// </summary>
    public partial class GetOrderInfo : EyouSoft.Common.Control.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PriorNum = 0;
                int NextNum = 0;
                string orderNo=Utils.GetQueryStringValue("SearchOrderNumber");

                IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellerinfo = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetTravells(orderNo);


                if (travellerinfo != null && travellerinfo.Count > 0)
                {
                    if (travellerinfo.Count > 1) //处理上一位下一位编号
                    {
                        PriorNum = 0;
                        NextNum = 2;
                    }
                    else
                    {
                        hidTraveJson.Visible = false;
                    }
                    //获取订单里面的pnr
                    EyouSoft.Model.TicketStructure.OrderInfo orderinfo = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetOrderInfoByNo(orderNo);
                    if (orderinfo != null)
                    {
                        foreach (EyouSoft.Model.TicketStructure.OrderTravellerInfo item in travellerinfo)
                        {
                            if (item.TravellerState == EyouSoft.Model.TicketStructure.TravellerState.作废成功 || item.TravellerState == EyouSoft.Model.TicketStructure.TravellerState.退票成功)
                            {
                                item.TicketNumber = "";
                            }
                        }
                        orderinfo = null;
                    }
                    txtpnr.Value = travellerinfo[0].TicketNumber;
                    txtTraName.Value = travellerinfo[0].TravellerName;
                    txtTraNum.Value = travellerinfo[0].CertNo;
                    hidTraveJson.Value = JsonConvert.SerializeObject(travellerinfo); //把目标对象序列化为Json字符
                    hfprior.Attributes.Add("showpage", PriorNum.ToString());
                    hfnext.Attributes.Add("showpage", NextNum.ToString());
                    //hfprior.Attributes.Add("onclick", "PrintTicketJournal.ShowPriorOrNext(" + PriorNum + ")");
                    //hfnext.Attributes.Add("onclick", "PrintTicketJournal.ShowPriorOrNext(" + NextNum + ")");
                }
                else
                {
                    Response.Clear();
                    Response.Write("暂无该订单信息");
                    Response.End();
                }
            }
        }
    }
}
