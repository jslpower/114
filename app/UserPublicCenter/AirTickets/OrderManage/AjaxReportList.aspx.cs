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
using System.IO;
using EyouSoft.Common.Function;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 页面功能：机票订单管理——报表列表页
    /// 开发人：刘咏梅     
    /// 开发时间：2010-10-19
    /// </summary>
    public partial class AjaxReportList : EyouSoft.Common.Control.BasePage
    {
        #region Public Members  
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount = 0;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startTime = null;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endTime = null;
        /// <summary>
        /// 年
        /// </summary>
        public int year = 0;
        /// <summary>
        /// 月
        /// </summary>
        public int month = 0;
        /// <summary>
        /// 统计类型：0:：查询明细；1：查询合计
        /// </summary>
        public int stats = 0;       
        /// <summary>
        /// 报表类型，默认为0:日报表，1:为月报表
        /// </summary>
        public int Type = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!Page.IsPostBack)
            {               
                //初始化报表
                BindReportList();               
            }
        }
         #endregion

        #region 初始化报表
        private void BindReportList()
        {           
            string buyerId = this.SiteUserInfo.CompanyID;//供应商ID
            
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["StartTime"])))
                startTime = DateTime.Parse(Utils.InputText(Request.QueryString["StartTime"]));//开始时间


            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["EndTime"])))
                endTime = DateTime.Parse(Utils.InputText(Request.QueryString["EndTime"]));//结束时间
        
           if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Year"])))         
               year = int.Parse(Utils.InputText(Request.QueryString["Year"]).ToString());//年           
           
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Month"])))           
                month = int.Parse(Utils.InputText(Request.QueryString["Month"]).ToString());//月

            
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["Stats"])))          
                stats = int.Parse(Utils.InputText(Request.QueryString["Stats"]).ToString());//统计


            IList<EyouSoft.Model.TicketStructure.OrderInfo> OrderList = new List<EyouSoft.Model.TicketStructure.OrderInfo>();
            if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["Type"])))
            {                
                Type =int.Parse(Utils.InputText(Context.Request.QueryString["Type"]));              
                if (Type == 0)//日报表
                {
                    if (stats == 0)
                        OrderList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetDayReports(buyerId, startTime, endTime);//按条件查询明细                  
                }
                else
                {
                    OrderList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetMonReports(buyerId, year, month);//月报表
                }               
            }
            if (OrderList.Count != 0 && OrderList != null)
            {
                RecordCount = OrderList.Count;       
                this.crpReportList.DataSource = OrderList;               
                this.crpReportList.DataBind();              
            }
            else
            {    
                this.crpReportList.EmptyText = "抱歉，暂时没有数据！";
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
        protected string GetPNR(string OPNR,string PNR)
        {
            string pnr = PNR!=""?PNR:OPNR;          
            return pnr;
        }
        #endregion
    }
}
