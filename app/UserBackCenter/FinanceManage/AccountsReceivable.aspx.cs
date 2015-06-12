using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.FinanceManage
{
    /// <summary>
    /// 应收帐款
    /// 创建者：luofx 时间：2010-11-10
    /// </summary>
    public partial class AccountsReceivable : EyouSoft.Common.Control.BackPage
    {
        protected int intPageIndex = 1;
        private int intPageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.营销工具_财务管理))
            {
                Response.Clear();
                Response.Write("对不起，你正在使用的帐号没有操作改页面的权限！");
                Response.End();
            }
            
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            int intRecordCount = 0;
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            string TourNo = Utils.GetQueryStringValue("TourNo");
            string RouteName = Utils.GetQueryStringValue("RouteName");
            string OperatorName = Utils.GetQueryStringValue("OperatorName");
            string TourCompanyName = Utils.GetQueryStringValue("TourCompanyName");
            DateTime? StartDate = Utils.GetDateTimeNullable(Request.QueryString["StartDate"]);
            DateTime? EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);
            string CompanyId = this.SiteUserInfo.CompanyID;
            this.rptAccountsReceivable.DataSource = EyouSoft.BLL.ToolStructure.Receivables.CreateInstance().GetList(intPageSize, intPageIndex, ref intRecordCount, 1, TourNo, RouteName, OperatorName, TourCompanyName, StartDate, EndDate, false, CompanyId);
            this.rptAccountsReceivable.DataBind();
            if (rptAccountsReceivable.Items.Count <= 0)
            {
                this.NoData.Visible = true;
            }
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
    }
}
