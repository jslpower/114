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
using System.Collections.Generic;
using EyouSoft.Common;
using System.IO;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 导入报表合计查询结果到Excel中
    /// liuym 
    /// 2010-11-18
    /// </summary>
    public partial class ReportTotalToExcel : EyouSoft.Common.Control.BasePage
    {
        #region Public Method
        /// <summary>
        /// 总记录
        /// </summary>
        public int RecordCount = 0;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime = null;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime = null;
        /// <summary>
        /// 报表类型
        /// </summary>
        public int Type = 0;
        /// <summary>
        /// 统计类型
        /// </summary>
        public int Stats = 1;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //初始化日报表 合计查询
                InitReportTotal();
            }
        }
        #endregion

        #region 初始化报表
        private void InitReportTotal()
        {
            string buyerId = this.SiteUserInfo.CompanyID;//供应商ID

            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["StartTime"])))
                StartTime = DateTime.Parse(Utils.InputText(Request.QueryString["StartTime"]));//开始时间


            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["EndTime"])))
                EndTime = DateTime.Parse(Utils.InputText(Request.QueryString["EndTime"]));//结束时间

            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Stats"])))
                Stats = int.Parse(Utils.InputText(Request.QueryString["Stats"]).ToString());//统计


            IList<EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo> OrderList = null;
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["Type"])))
            {
                Type = int.Parse(Utils.InputText(Context.Request.QueryString["Type"]));
                if (Type == 0)//日报表
                {

                    OrderList = new List<EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo>();
                    EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo orderInfo = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetBuyerOrderReportsTotal(buyerId, StartTime, EndTime);
                    if (orderInfo != null)
                        OrderList.Add(orderInfo);//按条件查询合计                   
                }
            }
            if (OrderList.Count != 0 && OrderList != null)
            {
                this.crpReportTotalList.DataSource = OrderList;
                this.crpReportTotalList.DataBind();
                ToExcel(this.crpReportTotalList, OrderList);
            }       
            //释放资源
            OrderList = null;
        }
        #endregion   

        #region 导出Excel
        public void ToExcel(System.Web.UI.Control ctl, IList<EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo> OrderList)
        {

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=DayReportFile.xls");

            HttpContext.Current.Response.Charset = "UTF-8";

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;

            HttpContext.Current.Response.ContentType = "application/ms-excel";

            ctl.Page.EnableViewState = false;

            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
        #endregion
    }
}
