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

namespace UserBackCenter.FinanceManage
{
    /// <summary>
    /// 应付账款
    /// 创建人：鲁功源 2010-11-10
    /// </summary>
    public partial class AccountPayable : EyouSoft.Common.Control.BackPage
    {
        protected int intPageIndex = 1;
        private int intPageSize = 20;
        protected bool IsGrant = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.营销工具_财务管理))
            {
                Response.Clear();
                Response.Write("对不起，你正在使用的帐号没有操作该页面的权限！");
                Response.End();
            }
            IsGrant = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.财务管理_明细操作_登记_修改_删除);
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
            string SupperName = Utils.GetQueryStringValue("OperatorName"); //供应商名称
            string SupperType = Utils.GetQueryStringValue("TourCompanyName");//供应商类型
            DateTime? StartDate = Utils.GetDateTimeNullable(Request.QueryString["StartDate"]);
            DateTime? EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);
            this.rptAccountsPayable.DataSource = EyouSoft.BLL.ToolStructure.Payments.CreateInstance().GetList(intPageSize, intPageIndex, ref intRecordCount, 1, TourNo, RouteName, SupperName, SupperType, StartDate, EndDate, false,SiteUserInfo.CompanyID);
            this.rptAccountsPayable.DataBind();
            if (intRecordCount == 0)
                DataEmpty.Visible = true;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
    }
}
