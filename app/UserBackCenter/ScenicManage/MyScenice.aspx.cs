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
using System.Text;
using EyouSoft.Model.ScenicStructure;


namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 景区列表页
    /// 功能：显示查询以后获得景区的列表
    /// 创建人：方琪
    /// 创建时间： 2011-10-21  
    /// </summary>
    public partial class MyScenice : EyouSoft.Common.Control.BackPage
    {
        #region 参数
        protected int intPageIndex = 1;
        private int intPageSize = 5;
        private string CompanyId = string.Empty;
        private string SceniceId = string.Empty;
        private string SceniceName = string.Empty;
        IList<EyouSoft.Model.ScenicStructure.MScenicArea> SceniceListAndTickets = null;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //AddGenericLink("text/css", "stylesheet", CssManage.GetCssFilePath("rightnew"));

            //公司没有通过审核，隐藏添加景区按钮
            if(!IsCompanyCheck)
            {
                divAddScenic.Visible = false;
            }

            // 判断用户是否登录，如果没有登录跳转到登录页面，如果有登录，初始化用户对象UserInfoModel
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
            }
            CompanyId = this.SiteUserInfo.CompanyID;
            if (!IsPostBack)
            {
                InitPage();
            }


        }

        private void InitPage()
        {
            int intRecordCount = 0;
            #region 初始化查询条件
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //SceniceId = Utils.GetQueryStringValue("SceniceId");
            SceniceName = Utils.GetQueryStringValue("SceniceName");
            this.txtSceniceName.Value = SceniceName;
            #endregion
            EyouSoft.Model.ScenicStructure.MSearchSceniceArea Search = new EyouSoft.Model.ScenicStructure.MSearchSceniceArea();
            Search.CompanyId = CompanyId;
            Search.ScenicName = SceniceName;
            SceniceListAndTickets = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetListAndTickets(intPageSize, intPageIndex, ref intRecordCount ,Search);
            this.repSceniceList.DataSource = SceniceListAndTickets;
            this.repSceniceList.DataBind();
            if (SceniceListAndTickets.Count <= 0)
            {
                this.NoData.Visible = true;
            }
            SceniceListAndTickets = null;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }

        /// <summary>
        /// 绑定门票类型集合
        /// </summary>
        /// <param name="TicketsList">门票集合</param>
        /// <param name="ScenicId">景区编号</param>
        /// <returns></returns>
        protected string GetList(IList<EyouSoft.Model.ScenicStructure.MScenicTickets> TicketsList, string ScenicId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class=\"hr_5\"></div><div style=\" text-align:left;\"><span class=\"blue_btn\"><a class=\"a_AddTicket\" title=\"增加门票类型\" href=\"ScenicManage/AddOrModifyTickets.aspx?scenic={0}\">增加门票类型</a></span></div><div class=\"hr_5\"></div> \n", ScenicId));
            if (TicketsList != null)
            {
                sb.Append("<table width=\"99%\" border=\"1\" align=\"left\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#9dc4dc\"> \n");
                sb.Append("<tr  class=\"list_basicbg\">");
                sb.Append("<th>门票类型</th><th>市场价</th><th>有效时间</th><th>支付</th><th>状态</th><th>管理</th></tr>");
                //遍历通过景区编号查询到的门票明细的集合
                //foreach 读取集合里边的值
                foreach (EyouSoft.Model.ScenicStructure.MScenicTickets ticketsList in TicketsList)
                {
                    if (ticketsList.StartTime != null && ticketsList.EndTime != null)
                    {
                        sb.Append(string.Format("<tr><td align=\"left\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td><td align=\"center\">{3}</td><td align=\"center\">{4}</td><td align=\"center\"><a class=\"a_updateTicket\" title=\"修改门票类型\"  href=\"/ScenicManage/AddOrModifyTickets.aspx?tickets={5}&scenic={6}\">修改</a></td></tr>",Utils.GetText2( ticketsList.TypeName,20,false), ticketsList.RetailPrice.ToString("C0"), Convert.ToDateTime(ticketsList.StartTime).ToString("yyyy-MM-dd") + "至" + Convert.ToDateTime(ticketsList.EndTime).ToString("yyyy-MM-dd"), ticketsList.Payment,GetStatus(ticketsList.Status,ticketsList.ExamineStatus), ticketsList.TicketsId, ScenicId));
                    }
                    else
                    {
                        sb.Append(string.Format("<tr><td align=\"left\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">长期有效</td><td align=\"center\">{2}</td><td align=\"center\">{3}</td><td align=\"center\"><a class=\"a_updateTicket\" title=\"修改门票类型\" href=\"/ScenicManage/AddOrModifyTickets.aspx?tickets={4}&scenic={5}\">修改</a></td></tr>",Utils.GetText2( ticketsList.TypeName,20,false), ticketsList.RetailPrice.ToString("C0"), ticketsList.Payment, GetStatus(ticketsList.Status, ticketsList.ExamineStatus), ticketsList.TicketsId, ScenicId));
                    }
                }
                sb.Append("</table><div class=\"hr_5\"></div>");
            }
            return sb.ToString();
        }

        protected string GetStatus(ScenicTicketsStatus status, ExamineStatus examineStatus) 
        {
            if (examineStatus == ExamineStatus.待审核)
            {
                return ExamineStatus.待审核.ToString();
            }
            else
            {
                return status.ToString();
            }
        }
    }
}
