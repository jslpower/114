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
using System.IO;
using ControlLibrary;
using System.Collections.Generic;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 页面功能：导出Excel
    /// author:刘咏梅
    /// createTime:2010-11-09
    /// </summary>
    public partial class ReportToExcel : EyouSoft.Common.Control.BasePage
    {
        #region Priate Members
        private string typeName = "DayReportFile";
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {          
            BindReportList();
        }
        #endregion

        #region 初始化报表
        private void BindReportList()
        {
           
            int stats = 0;
            int Type = 0;
            string buyerId = this.SiteUserInfo.CompanyID;//供应商ID

            DateTime? startTime = null;
            if (!string.IsNullOrEmpty(Request.QueryString["StartTime"]))
                startTime = DateTime.Parse(Utils.InputText(Request.QueryString["StartTime"]));//开始时间
            DateTime? endTime = null;
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["StartTime"])))
                endTime = DateTime.Parse(Utils.InputText(Request.QueryString["EndTime"]));//结束时间

            int year = 0;
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Year"])))
                year = int.Parse(Utils.InputText(Request.QueryString["Year"]).ToString());//年
            int month = 0;
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Month"])))
                month = int.Parse(Utils.InputText(Request.QueryString["Month"]).ToString());//月

            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Stats"])))
                stats = int.Parse(Utils.InputText(Request.QueryString["Stats"]).ToString());//统计

            IList<EyouSoft.Model.TicketStructure.OrderInfo> OrderList = new List<EyouSoft.Model.TicketStructure.OrderInfo>();
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["Type"])))
            {
                Type = int.Parse(Utils.InputText(Context.Request.QueryString["Type"]));
                if (Type == 0)//日报表
                {
                    if (stats == 0)
                        OrderList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetDayReports(buyerId, startTime, endTime);//按条件查询明细                   
                }
                else
                {
                    OrderList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetMonReports(buyerId, year, month);//月报表
                    typeName = "MonthReportFile";
                }
            }           
            if (OrderList.Count != 0 && OrderList != null)
            {
                this.crpReportList.DataSource = OrderList;
                this.crpReportList.DataBind();
                ToExcel(crpReportList,OrderList);
            }            
            
            //释放资源
            OrderList = null;
        }
        #endregion   
   
        #region 转换百分比
        protected string GetPercent(decimal value)
        {
            string money = Utils.GetMoney(value);
            return (Convert.ToDecimal(money)).ToString() + "%";
        }
        #endregion
        #region 获取PNR
        /// <summary>
        /// 获取PNR
        /// </summary>
        /// <param name="OPNR">原始的PNR</param>
        /// <param name="PNR">更改后的PNR</param>
        /// <returns></returns>
        protected string GetPNR(string OPNR, string PNR)
        {
            string pnr = PNR== ""?OPNR:PNR;  
            return pnr;
        }
        #endregion

        #region 导出Excel
        public void ToExcel(System.Web.UI.Control ctl, IList<EyouSoft.Model.TicketStructure.OrderInfo> OrderList)
        {

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + typeName + ".xls");

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
