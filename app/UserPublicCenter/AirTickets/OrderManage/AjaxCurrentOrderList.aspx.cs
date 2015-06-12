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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Collections.Generic;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 机票订单列表ajax页
    /// 2010-11
    /// 袁惠
    /// </summary>
    public partial class AjaxCurrentOrderList : EyouSoft.Common.Control.BasePage
    {
        protected int PageSize = 10;

        private IList<EyouSoft.Model.TicketStructure.OrderInfo> list = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                int recordCount = 0;
                int PageIndex = Utils.GetInt(Request.QueryString["Page"]);
                string state = Utils.GetQueryStringValue("State");
                string changeState = Utils.GetQueryStringValue("ChangeState");
                string Content = Utils.InputText(Request.QueryString["Content"]);  //查询文本框输入的值
                string type = Utils.InputText(Request.QueryString["TypeName"]);   //pnr|旅客姓名|订单编号  
                string strDate=Utils.InputText(Request.QueryString["Date"]); //固定日期    
                DateTime? date = null;
                EyouSoft.Model.TicketStructure.OrderState? OrdState=null;  //机票系统订单状态
                EyouSoft.Model.TicketStructure.OrderChangeType? OrdChangeType=null; //订单变更类型
                EyouSoft.Model.TicketStructure.OrderChangeState? OrdChangeState=null;  //订单变更状态

                EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo=new EyouSoft.Model.TicketStructure.OrderSearchInfo ();
                if(!string.IsNullOrEmpty(strDate))
                {
                    date = Utils.GetDateTime(strDate);
                }
                //订单状态
                bool IsOrdState = Enum.IsDefined(typeof(EyouSoft.Model.TicketStructure.OrderState), state);
                //订单变更类型
                bool IsOrdChaType = Enum.IsDefined(typeof(EyouSoft.Model.TicketStructure.OrderChangeType), state);   
                //订单变更状态
                bool IsOrdChaState=Enum.IsDefined(typeof(EyouSoft.Model.TicketStructure.OrderChangeState),changeState);   
                
                if (IsOrdState)
                {
                    OrdState = (EyouSoft.Model.TicketStructure.OrderState)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.OrderState), state);
                }
                if (IsOrdChaType)
                {
                    OrdChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.OrderChangeType), state);
                }
                if (IsOrdChaState)
                {
                    OrdChangeState = (EyouSoft.Model.TicketStructure.OrderChangeState)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.OrderChangeState), changeState);
                }
                searchInfo.FixedDate=date;
                searchInfo.OrderChangeState=OrdChangeState;
                searchInfo.OrderChangeType=OrdChangeType;
                searchInfo.OrderState=OrdState;
                switch (type)
                {
                    case "1": //pnr
                        searchInfo.PNR=Content;
                        break;
                    case "2": //旅客姓名
                        searchInfo.TravellerName=Content;
                        break;
                    case "3": //订单编号
                        searchInfo.TicketNumber=Content;
                        break;
                }
                searchInfo.BuyerCId=SiteUserInfo.CompanyID;
                list = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetOrderList(searchInfo, PageSize, PageIndex, ref recordCount);
                ltr_OrderCount.Text = recordCount.ToString();
                if (list != null && list.Count > 0)
                {
                    crptOrderList.DataSource = list;
                    crptOrderList.DataBind();
                    //绑定分页控件
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = recordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                    this.ExporPageInfoSelect1.LinkType = 3;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "CurrentOrderList.LoadData(this);", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "CurrentOrderList.LoadData(this);", 0);
                    list = null;
                }
                else
                {
                    crptOrderList.EmptyText = "<tr ><td colspan=\"13\"><div  style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无订单信息</div></td></tr>";
                    this.ExporPageInfoSelect1.Visible = false;
                }
            }           
        }
        
        /// <summary>
        /// 订单取消操作，隐藏和显示
        /// </summary>
        /// <param name="orderstate">订单状态</param>
        /// <param name="ordId">订单ID</param>
        /// <returns></returns>
        protected string OpearCancelOrder(string orderstate,string ordId)
        {
            EyouSoft.Model.TicketStructure.OrderState state = (EyouSoft.Model.TicketStructure.OrderState)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.OrderState), orderstate);
            if (state == EyouSoft.Model.TicketStructure.OrderState.等待审核 || state == EyouSoft.Model.TicketStructure.OrderState.拒绝审核 || state == EyouSoft.Model.TicketStructure.OrderState.审核通过 )
            {
                return "<a href=\"javascript:void(0)\" onclick=\"CurrentOrderList.CancelOrder('" + ordId + "')\">取消</a>";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取订单最新状态
        /// </summary>
        /// <param name="orderState">订单状态</param>
        /// <param name="OrderId">订单Id</param>
        /// <returns>订单最新状态</returns>
        protected string GetOrderState(string orderState, string OrderId)
        {
            IList<EyouSoft.Model.TicketStructure.TicketOrderLog> orderLogs = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetTicketOrderState(OrderId);
            if (orderLogs.Count > 0 && orderLogs != null)
            {
                orderState = orderLogs[orderLogs.Count - 1].State.ToString();
            }
            return orderState;
        }
    }
}
