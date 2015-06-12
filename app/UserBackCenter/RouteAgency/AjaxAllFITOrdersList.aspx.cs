using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;

namespace UserBackCenter.RouteAgency
{
    public partial class AjaxAllFITOrdersList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //分页参数
            int intRecordCount = 0,//记录条数
             CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),//页码
             intPageSize = 15;//每页条数
            MTourOrderSearch queryModel = new MTourOrderSearch();
            #region 查询实体
            //专线ID
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"));
            //排序字段(出发时间排序)
            queryModel.Order = Utils.GetInt(Utils.GetQueryStringValue("sortType"), 2);
            //排序规则
            queryModel.IsDesc = Utils.GetQueryStringValue("sort").Length > 0 ? Utils.GetQueryStringValue("sort") == "desc" : true;
            //订单区域
            queryModel.AreaType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("area"), -1) > 0)
            {
                queryModel.AreaType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("area"));
            }
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetQueryStringValue("goTimeS");
            //出团时间结束
            queryModel.LeaveDateE = Utils.GetQueryStringValue("goTimeE");
            //关键字
            queryModel.OrderKey = Utils.GetQueryStringValue("keyWord");
            string[] status;
            int i;
            //订单状态
            if (Utils.GetQueryStringValue("status").Length > 0 && Utils.GetQueryStringValue("status") != "-1")
            {
                status = Utils.GetQueryStringValue("status").Split(',');
                i = status.Length;
                IList<PowderOrderStatus> lsPowderOrderStatus = new List<PowderOrderStatus>();
                while (i-- > 0)
                {
                    lsPowderOrderStatus.Add((PowderOrderStatus)Utils.GetInt(status[i]));
                }
                queryModel.PowderOrderStatus = lsPowderOrderStatus;
            }
            //支付状态
            if (Utils.GetQueryStringValue("paymentStatus").Length > 0)
            {
                IList<PaymentStatus> lsPaymentStatus = new List<PaymentStatus>();
                status = Utils.GetQueryStringValue("paymentStatus").Split(',');
                i = status.Length;
                while (i-- > 0)
                {
                    lsPaymentStatus.Add((PaymentStatus)Utils.GetInt(status[i]));
                }
                queryModel.PaymentStatus = lsPaymentStatus;
            }
            #endregion
            ITourOrder bll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();

            IList<MTourOrder> list = bll.GetPublishersAllList(
                intPageSize,
                CurrencyPage,
                ref intRecordCount,
                Utils.GetQueryStringValue("companyID"),
                queryModel);
            if (list != null && list.Count > 0)
            {
                pnlNodata.Visible = false;
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = "/RouteAgency/AllFITOrders.aspx?";

                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("area", queryModel.AreaType.ToString());
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDateS);
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.LeaveDateE);
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.OrderKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
                ExportPageInfo1.Visible = false;
            }
        }
    }
}
