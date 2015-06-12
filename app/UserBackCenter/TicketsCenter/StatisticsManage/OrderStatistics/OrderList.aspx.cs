using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace UserBackCenter.TicketsCenter.StatisticsManage.OrderStatistics
{
    /// <summary>
    /// 功能：统计分析-订单统计列表
    /// 开发人：刘玉灵  时间：2010-10-20
    /// </summary>
    public partial class OrderList : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["isExport"]))
            {
                ExportExcel();
                return;
            }

            if (!Page.IsPostBack)
            {
                BindOrderState();
                this.BindOrderList();

            }
        }


        protected void BindOrderList()
        {
            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = GetOrderList();
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            else
            {
                this.lblMsg.Text = "未找到相关数据";
                this.panIsShow.Visible = false;
            }
            list = null;
        }

        /// <summary>
        /// 绑定订单列表
        /// </summary>
        private IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList()
        {
            string OrderNumber = Utils.InputText(Server.UrlDecode(Request.QueryString["OrderNumber"]));
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                this.txtOrderNumber.Value = OrderNumber;
            }
            int? StartDestination = Utils.GetIntNull(Request.QueryString["StartDestination"], null);
            if (StartDestination == 0) StartDestination = null;
            int? EndDestination = Utils.GetIntNull(Request.QueryString["EndDestination"], null);
            if (EndDestination == 0) EndDestination = null;
            DateTime? SartDateTime = Utils.GetDateTimeNullable(Request.QueryString["SartDateTime"]);
            DateTime? EndDateTime = Utils.GetDateTimeNullable(Request.QueryString["EndDateTime"]);
            string OrderType = Utils.GetQueryStringValue("OrderType");
            string TwoOrderType = Utils.GetQueryStringValue("TwoOrderType");
            EyouSoft.Model.TicketStructure.OrderState? orderState = null;
            EyouSoft.Model.TicketStructure.OrderChangeState? changestate = null;
            EyouSoft.Model.TicketStructure.OrderChangeType? changeType = null;

            this.hide_Or_sltOrderState.Value = OrderType;
            this.hide_Or_sltOrdChangetype.Value = TwoOrderType;

            switch (OrderType)
            {
                case "等待审核": orderState = EyouSoft.Model.TicketStructure.OrderState.等待审核; break;
                case "拒绝审核": orderState = EyouSoft.Model.TicketStructure.OrderState.拒绝审核; break;
                case "审核通过": orderState = EyouSoft.Model.TicketStructure.OrderState.审核通过; break;
                case "支付成功": orderState = EyouSoft.Model.TicketStructure.OrderState.支付成功; break;
                case "拒绝出票": orderState = EyouSoft.Model.TicketStructure.OrderState.拒绝出票; break;
                case "出票完成": orderState = EyouSoft.Model.TicketStructure.OrderState.出票完成; break;
                case "无效订单": orderState = EyouSoft.Model.TicketStructure.OrderState.无效订单; break;
                case "退票": changeType = EyouSoft.Model.TicketStructure.OrderChangeType.退票; break;
                case "作废": changeType = EyouSoft.Model.TicketStructure.OrderChangeType.作废; break;
                case "改期": changeType = EyouSoft.Model.TicketStructure.OrderChangeType.改期; break;
                case "改签": changeType = EyouSoft.Model.TicketStructure.OrderChangeType.改签; break;

            }

            switch (TwoOrderType)
            {
                case "申请": changestate = EyouSoft.Model.TicketStructure.OrderChangeState.申请; break;
                case "拒绝": changestate = EyouSoft.Model.TicketStructure.OrderChangeState.拒绝; break;
                case "接受": changestate = EyouSoft.Model.TicketStructure.OrderChangeState.接受; break;
            }
            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetOrderStats(OrderNumber, SiteUserInfo.CompanyID, StartDestination, EndDestination, SartDateTime, EndDateTime, orderState, changeType, changestate);
            return list;

        }



        #region 订单状态控件绑定
        private void BindOrderState()
        {
            string[] orderstate = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderState));
            Or_sltOrderState.Items.Add(new ListItem("-请选择-", "0"));
            if (orderstate.Length > 0)
            {
                for (int i = 0; i < orderstate.Length; i++)
                {
                    Or_sltOrderState.Items.Add(new ListItem(orderstate[i], orderstate[i]));
                }
            }
            string[] changetype = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderChangeType));
            if (changetype.Length > 0)
            {
                string types = "";
                for (int i = 0; i < changetype.Length; i++)
                {
                    Or_sltOrderState.Items.Add(new ListItem("所有" + changetype[i], changetype[i]));
                    types = types + changetype[i] + ",";
                }
                Or_hidchangeType.Value = types;
            }
            string[] changestate = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderChangeState));
            Or_sltOrdChangetype.Items.Add(new ListItem("-请选择-", "0"));
            if (changestate.Length > 0)
            {
                for (int i = 0; i < changestate.Length; i++)
                {
                    Or_sltOrdChangetype.Items.Add(new ListItem(changestate[i], changestate[i]));
                }
            }
        }
        #endregion

        /// <summary>
        /// 导出Excel
        /// </summary>
        protected void ExportExcel()
        {
            //构造tableHTML
            StringBuilder strTable = new StringBuilder();
            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = GetOrderList();
            if (list != null)
            {
                strTable.Append("<table  width='835' border='1' align='center' cellpadding='0' cellspacing='0' bordercolor='#7dabd8'>");
                strTable.Append("<tr><td width='80' height='30' align='center' bgcolor='#EEF7FF'>订单号</td><td align='center' bgcolor='#EEF7FF' width='80' >公司名</td><td align='center' bgcolor='#EEF7FF' width='80'>旅客数</td><td align='center' bgcolor='#EEF7FF' width='80'>航程</td><td align='center' bgcolor='#EEF7FF' width='126'>航程起始日</td><td align='center' bgcolor='#EEF7FF' width='80'>运价类型</td><td align='center' bgcolor='#EEF7FF' width='80'>支付时间</td><td align='center' bgcolor='#EEF7FF' width='120'>支付金额(元)</td><td align='center' bgcolor='#EEF7FF' width='140'>出票耗时（小时）</td></tr>");
                for (int i = 0; i < list.Count; i++)
                {
                    strTable.Append("<tr>");
                    strTable.Append("<td align='center' height='30'>" + list[i].OrderNo + "&nbsp;</td>");
                    strTable.Append("<td align='center'>" + list[i].BuyerCName + "</td>");
                    strTable.Append("<td align='center'>" + list[i].PCount + "</td>");
                    strTable.Append("<td align='center'>" + list[i].HomeCityName + "-" + list[i].DestCityName + "</td>");
                    if (list[i].FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
                    {
                        strTable.Append("<td align='center'>" + list[i].LeaveTime.ToString("yyyy-MM-dd") + "</td>");
                        strTable.Append("<td align='center'>单程</td>");
                    }
                    else
                    {
                        strTable.Append("<td align='center'>" + list[i].LeaveTime.ToString("yyyy-MM-dd") + "/" + list[i].ReturnTime.ToString("yyyy-MM-dd") + "</td>");
                        strTable.Append("<td align='center'>来回程</td>");
                    }
                    strTable.Append("<td align='center'>" + list[i].PayTime.ToString("yyyy-MM-dd") + "</td>");
                    strTable.Append("<td align='center'>" + list[i].BalanceAmount.ToString("0.00") + "</td>");
                    strTable.Append("<td align='center'>" + list[i].ElapsedTime + "</td>");
                    strTable.Append("</tr>");
                }
                strTable.Append("</table>");
            }


            if (strTable.ToString().Length > 0)
            {
                Response.Clear();
                //流内容编码
                
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=Journal.xls");
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                //Response.AddHeader("Content-Length", strTable.Length.ToString());
                // 指定文件类型 
                Response.ContentType = "application/ms-excel";
                // 把文件流发送到客户端
                Response.Write(strTable);
                // 停止页面的执行 
                Response.End();
            }
        }


    }
}
