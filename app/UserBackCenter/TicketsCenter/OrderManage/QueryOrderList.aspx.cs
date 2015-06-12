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
    /// 根据条件查询订单信息
    /// 罗丽娥   2010-10-20
    /// </summary>
    public partial class QueryOrderList : EyouSoft.Common.Control.BackPage
    {
        private int intPageSize = 20, CurrencyPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitOrderList();
            }
        }

        #region 初始化订单列表 
        /// <summary>
        /// 初始化订单列表
        /// </summary>
        private void InitOrderList()
        {
            int intRecordCount = 0;
            int Type = Utils.GetInt(Utils.GetQueryStringValue("type"),0);//当前查询类型：type=1搜索查询，其余则 精确查询
            
            EyouSoft.Model.TicketStructure.RateType? RateType = null;//机票类型
            EyouSoft.Model.TicketStructure.OrderState? OrderState = null;//订单状态
            EyouSoft.Model.TicketStructure.OrderChangeType? OrderChangeType = null;//订单变更类型
            EyouSoft.Model.TicketStructure.OrderChangeState? OrderChangeState = null;//订单变更状态

            // 搜索查询
            int? FlightId = Utils.GetIntNull(Utils.GetQueryStringValue("flightid"));//航空公司 
            int? HomeCityId = Utils.GetIntNull(Utils.GetQueryStringValue("homecityid"));//始发地
            int? DestCityId = Utils.GetIntNull(Utils.GetQueryStringValue("destcityid"));//目的地
            int? rateTypeId = Utils.GetIntNull(Request.QueryString["ratetype"]);//机票类型ID
            if (rateTypeId.HasValue)//如果机票类型ID有效
            {
                //将ID转换为 机票类型
                RateType = (EyouSoft.Model.TicketStructure.RateType)rateTypeId.Value;
            }
            //订单生成时段
            DateTime? StartTime = Utils.GetDateTimeNullable(Request.QueryString["starttime"]);//开始时间
            DateTime? EndTime = Utils.GetDateTimeNullable(Request.QueryString["endtime"]);//结束时间
            int tmpOrderState = Utils.GetInt(Request.QueryString["orderstate"], 0);//订单状态ID
            int tmpOrderChangeState = Utils.GetInt(Request.QueryString["orderchangestate"]);//订单变更状态ID
            //if (Utils.GetQueryStringValue("orderchangestate") != string.Empty)
            //{
            //    OrderChangeState = (EyouSoft.Model.TicketStructure.OrderChangeState)Utils.GetInt(Utils.GetQueryStringValue("orderchangestate"));
            //}

            //订单状态处理
            /*
             退票,作废,改期,改签
             这四种状态，属于 订单变更类型
             * */
            if (tmpOrderState == 7)
            {
                OrderChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.退票;
            }
            else if(tmpOrderState == 8)
            {
                OrderChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.作废;
            }
            else if(tmpOrderState == 9)
            {
                OrderChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.改期;
            }
            else if(tmpOrderState == 10)
            {
                OrderChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.改签;
            }else{
                OrderState = (EyouSoft.Model.TicketStructure.OrderState)tmpOrderState;
            }

            //如果当前的订单状态 属于 订单变更类型
            //则初始化 属于对应变更类型的变更状态
            if (OrderChangeType.HasValue)//属于
            {
                OrderChangeState = (EyouSoft.Model.TicketStructure.OrderChangeState)tmpOrderChangeState;
            }

            // 精确查询
            string OrderNo = Server.UrlDecode(Utils.GetQueryStringValue("orderno"));
            string TicketNo = Server.UrlDecode(Utils.GetQueryStringValue("ticketno"));
            string TravellerName = Server.UrlDecode(Utils.GetQueryStringValue("travellername"));
            string PNR = Utils.GetQueryStringValue("PNR");

            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = null;
            EyouSoft.IBLL.TicketStructure.ITicketOrder bll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            
            if (Type == 1)//模糊查询
            {
                list = bll.ShouSuoSearch(SiteUserInfo.CompanyID, FlightId, RateType, HomeCityId, DestCityId, StartTime, EndTime, OrderState, OrderChangeType, OrderChangeState,intPageSize,CurrencyPage,ref intRecordCount);
            }
            else//精确查询 
            {
                list = bll.precisionSearch(SiteUserInfo.CompanyID, OrderNo, TicketNo, TravellerName, PNR);
            }

            //如果是精确查询，则隐藏分页控件
            if (Type != 1)
            {
                ExportPageInfo1.Visible = false;
            }

            if (list != null && list.Count > 0)
            {
                this.QueryOrderList_rptOrderList.DataSource = list;
                this.QueryOrderList_rptOrderList.DataBind();

                //如果是 搜索查询 ，则初始化分页控件
                if (Type == 1)
                {
                    this.ExportPageInfo1.intPageSize = intPageSize;
                    this.ExportPageInfo1.intRecordCount = intRecordCount;
                    this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                    this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExportPageInfo1.UrlParams.Add("type", Type.ToString());
                    this.ExportPageInfo1.UrlParams.Add("flightid", FlightId.HasValue ? FlightId.ToString() : "");
                    this.ExportPageInfo1.UrlParams.Add("homecityid", HomeCityId.HasValue ? HomeCityId.ToString() : "");
                    this.ExportPageInfo1.UrlParams.Add("destcityid", DestCityId.HasValue ? DestCityId.ToString() : "");
                    this.ExportPageInfo1.UrlParams.Add("ratetype", RateType.HasValue ? ((int)RateType).ToString() : "");
                    this.ExportPageInfo1.UrlParams.Add("starttime", StartTime.HasValue ? StartTime.Value.ToString("yyyy-MM-dd") : "");
                    this.ExportPageInfo1.UrlParams.Add("endtime", EndTime.HasValue ? EndTime.Value.ToString("yyyy-MM-dd") : "");
                    this.ExportPageInfo1.UrlParams.Add("orderstate", OrderState.HasValue ? ((int)OrderState).ToString() : "");
                    this.ExportPageInfo1.UrlParams.Add("orderchangestate", OrderChangeType.HasValue ? ((int)OrderChangeState).ToString() : "");
                }
            }
            else {
                this.QueryOrderList_pnlNoData.Visible = true;   
            }

            this.QueryOrderList_lblOrderCount.Text = list.Count.ToString();
        }
        #endregion

        /// <summary>
        /// 根据供应商ID,获取出票成功率
        /// </summary>
        /// <param name="SupplierCId"></param>
        /// <returns></returns>
        protected decimal GetSuccessRate(string SupplierCId)
        {
            decimal SuccessRate = 0;
            EyouSoft.Model.TicketStructure.TicketWholesalersInfo model = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(SupplierCId);
            if (model != null)
            {
                SuccessRate = model.SuccessRate;
            }
            model = null;
            return SuccessRate;
        }
    }
}
