using System;
using System.Collections;
using System.Collections.Generic;
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

namespace UserBackCenter.TicketsCenter.OrderManage
{
    /// <summary>
    /// 根据订单状态和业务类型查询订单列表
    /// 罗丽娥  2010-10-20
    /// </summary>
    public partial class OrderListByStateANDType : EyouSoft.Common.Control.BackPage
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        protected EyouSoft.Model.TicketStructure.OrderState? OrderState = null;
        protected string tmpOrderState = string.Empty;
        /// <summary>
        /// 业务类型
        /// </summary>
        protected EyouSoft.Model.TicketStructure.RateType RateType = EyouSoft.Model.TicketStructure.RateType.团队散拼;
        /// <summary>
        /// 订单变更类型
        /// </summary>
        protected EyouSoft.Model.TicketStructure.OrderChangeType? ChangeType = null;
        protected string tmpChangeType = string.Empty;
        /// <summary>
        /// 订单统计类型
        /// </summary>
        protected EyouSoft.Model.TicketStructure.OrderStatType? OrderStatType = null;

        protected string strOrderStaType = string.Empty;

        private int CurrencyPage = 0,intPageSize = 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitOrderList();

                strOrderStaType = OperatorMethod();

                OrderStatType = (EyouSoft.Model.TicketStructure.OrderStatType)Utils.GetInt(Utils.GetQueryStringValue("Orderstattype"));

                if (OrderStatType == EyouSoft.Model.TicketStructure.OrderStatType.待改期)
                { 
                    
                }
            }
        }

        #region 根据订单状态和业务类型初始化订单数据
        private void InitOrderList()
        {
            int intRecordCount = 0;
            if (Utils.GetQueryStringValue("orderstate") != string.Empty)
            {
                OrderState = (EyouSoft.Model.TicketStructure.OrderState)Utils.GetInt(Utils.GetQueryStringValue("orderstate"));
                tmpOrderState = Utils.GetQueryStringValue("orderstate");
            }
            if (Utils.GetQueryStringValue("ratetype") != string.Empty)
            {
                RateType = (EyouSoft.Model.TicketStructure.RateType)Utils.GetInt(Utils.GetQueryStringValue("ratetype"));
            }
            if (Utils.GetQueryStringValue("changetype") != string.Empty)
            {
                ChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)Utils.GetInt(Utils.GetQueryStringValue("changetype"));
                tmpChangeType = Utils.GetQueryStringValue("changetype");
            }
            EyouSoft.Model.TicketStructure.OrderChangeState? ChangeState = null;
            if (OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成)
            {
                ChangeState = EyouSoft.Model.TicketStructure.OrderChangeState.申请;
            }

            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetSupplierHandelStatsDetail(SiteUserInfo.CompanyID, RateType, OrderState, ChangeType,ChangeState,intPageSize,CurrencyPage,ref intRecordCount);
            if (list != null && list.Count > 0)
            {
                this.OrderListByStateANDType_rptOrderList.DataSource = list;
                this.OrderListByStateANDType_rptOrderList.DataBind();

                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams.Add("orderstate", OrderState.HasValue ? ((int)OrderState.Value).ToString() : "");
                this.ExportPageInfo1.UrlParams.Add("ratetype", ((int)RateType).ToString());
                this.ExportPageInfo1.UrlParams.Add("changetype", ChangeType.HasValue ? ((int)ChangeType).ToString() : "");
                this.ExportPageInfo1.UrlParams.Add("Orderstattype", OrderStatType.HasValue ? ((int)OrderStatType).ToString() : "");
            }
            else
            {
                this.OrderListByStateANDType_pnlNodata.Visible = true;
            }
            list = null;
        }
        #endregion

        protected string GetFlightName(string FlightId)
        {
            string tmpVal = string.Empty;
            EyouSoft.Model.TicketStructure.TicketFlightCompany model = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(Convert.ToInt32(FlightId));
            if (model != null)
            {
                tmpVal = model.AirportName;
            }
            model = null;
            return tmpVal;
        }

        private string OperatorMethod()
        {
            string tmpVal = string.Empty;
            if (OrderState == EyouSoft.Model.TicketStructure.OrderState.等待审核)
            {
                tmpVal = "审核";
            }
            else if (OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)
            {
                tmpVal = "出票";
            }
            else if(OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成)
            {
                if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.退票)
                {
                    tmpVal = "退票";
                }
                else if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.改期)
                {
                    tmpVal = "改期";
                }
                else if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.改签)
                {
                    tmpVal = "改签";
                }
                else {
                    tmpVal = "作废";
                }
            }
            return tmpVal;
        }
    }
}
