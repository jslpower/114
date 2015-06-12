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

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-我的散客订单Ajax
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-26
    public partial class AjaxFITOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //记录条数
            int intRecordCount = 0;
            //页码
            int CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            int intPageSize = 15;
            MTourOrderSearch queryModel = new MTourOrderSearch();
            #region 查询实体
            queryModel.Order = 1;
            //订单区域
            queryModel.AreaType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("area"), -1) >= 0)
            {
                queryModel.AreaType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("area"));
            }
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetQueryStringValue("goTimeS");
            //出团时间结束
            queryModel.LeaveDateE = Utils.GetQueryStringValue("goTimeE");
            //关键字
            queryModel.OrderKey = Utils.GetQueryStringValue("keyWord");
            //订单状态
            if (Utils.GetQueryStringValue("status").Length > 0 && Utils.GetQueryStringValue("status") != "-1")
            {
                string[] status = Utils.GetQueryStringValue("status").Split(',');
                int i = status.Length;
                IList<PowderOrderStatus> lsPowderOrderStatus = new List<PowderOrderStatus>();
                while (i-- > 0)
                {
                    lsPowderOrderStatus.Add((PowderOrderStatus)Utils.GetInt(status[i]));
                }
                queryModel.PowderOrderStatus = lsPowderOrderStatus;
            }
            #endregion
            ITourOrder bll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();

            IList<MTourOrder> list = bll.GetTravelList(intPageSize, CurrencyPage, ref intRecordCount, Utils.GetQueryStringValue("companyID"), queryModel);
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = "/TeamService/FITOrders.aspx?";

                this.ExportPageInfo1.UrlParams.Add("area", Utils.GetQueryStringValue("area"));
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDateS.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.LeaveDateE.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.OrderKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }

    }
}
