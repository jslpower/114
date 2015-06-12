using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Adpost.Common.ExporPage;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;

namespace IMFrame.RouteAgency.TourOrder
{
    /// <summary>
    /// 页面功能:ajax调用批发商的订单列表
    /// 章已泉 2009-9-10
    ///MQ修改 luofx 2010-8-16
    /// </summary>
    public partial class AjaxOrderList : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string GetInformationType = string.Empty;
        protected string intState = string.Empty;
        private string OrderType = string.Empty;
        protected int UserMQNumber = 0;
        protected string Md5PassWd = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsLogin)
            {
                if (!IsPostBack)
                {
                    InitData();
                }
            }
        }

        private void InitData()
        {
            EyouSoft.SSOComponent.Entity.UserInfo operatorModel = this.SiteUserInfo;
            IList<PowderOrderStatus> OrderStatus = new List<PowderOrderStatus>();
            //订单类型
            OrderType = Request.QueryString["OrderType"];
            string OrderSate = EyouSoft.Common.Utils.GetQueryStringValue("OrderSate");

            switch (OrderType)
            {
                case "OrdersReceived"://待处理订单
                    OrderStatus.Add(PowderOrderStatus.组团社待处理);
                    OrderStatus.Add(PowderOrderStatus.组团社已阅);
                    OrderStatus.Add(PowderOrderStatus.专线商待处理);
                    GetInformationType = "OrdersReceived";
                    break;
                case "OrderProcessed"://处理中订单
                    OrderStatus.Add(PowderOrderStatus.专线商预留);
                    OrderStatus.Add(PowderOrderStatus.专线商已确定);
                    OrderStatus.Add(PowderOrderStatus.结单);
                    OrderStatus.Add(PowderOrderStatus.取消);
                    GetInformationType = "OrderProcessed";
                    break;
            }
            int intRecordCount = 0;
            int intPageSize = 6;
            int intPageIndex = 1;
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //查询实体
            MTourOrderSearch searchModel = new MTourOrderSearch();
            searchModel.PowderOrderStatus = OrderStatus;
            searchModel.LeaveDateS = DateTime.Now.ToString();

            IList<EyouSoft.Model.NewTourStructure.MTourOrder> OrderList =
                EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetPublishersAllList(intPageSize, intPageIndex, ref intRecordCount, this.SiteUserInfo.CompanyID, searchModel);

            if (intRecordCount > 0)
            {
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "ajaxOrderListLoadData(this,\"" + GetInformationType + "\",\"" + OrderSate + "\")", 0);
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "ajaxOrderListLoadData(this,\"" + GetInformationType + "\",\"" + OrderSate + "\")", 1);
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.PageLinkCount = 7;
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = intPageIndex;
                this.ExporPageInfoSelect1.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.MostEasyNewButtonStyle;
                this.Repeater1.DataSource = OrderList;
                this.Repeater1.DataBind();
                if (intRecordCount <= intPageSize)
                {
                    ExporPageInfoSelect1.Visible = false;
                }
            }
            else
            {
                ExporPageInfoSelect1.Visible = false;
                Nodata.Visible = true;
            }
        }

    }
}
