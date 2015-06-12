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

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 客户后台：已成交客户
    /// 罗伏先   2010-07-22
    /// </summary>
    public partial class TradedCustomers : EyouSoft.Common.Control.BackPage
    {
        private int intPageSize = 12;
        protected int intPageIndex = 1;
        protected DateTime? BeginDate = null;
        protected DateTime? EndDate = null;
        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;

        private string SellCompanyID = string.Empty;
        /// <summary>
        /// 帐号ID
        /// </summary>
        private string UserID = string.Empty;
        protected string BuyCompanyName = string.Empty;
        protected int SonAccount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
                SellCompanyID = this.SiteUserInfo.CompanyID;
                UserID = this.SiteUserInfo.ID;
                if (!IsPostBack)
                {
                    BindSonAccount();
                    InitPage();
                }            
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            #region 查询条件
            UserID = Utils.GetQueryStringValue("UserID");
            if (Request.QueryString["Page"] != null)
            {
                intPageIndex = Utils.GetInt(Request.QueryString["Page"]);
            }
            dplUserList.SelectedValue = UserID;
            BuyCompanyName = Utils.GetQueryStringValue("BuyCompanyName");
            BuyCompanyName=Server.UrlDecode(BuyCompanyName).Trim();
            BeginDate = Utils.GetDateTimeNullable(Request.QueryString["BeginDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["BeginDate"]))
            {
                ShowBeginDate = Utils.GetDateTime(Request.QueryString["BeginDate"]).ToShortDateString();
            }
            EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["EndDate"]))
            {
                ShowEndDate = Utils.GetDateTime(Request.QueryString["EndDate"]).ToShortDateString();
            }
            #endregion
            int intRecordCount = 0;
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            IList<EyouSoft.Model.TourStructure.OrderStatistics> TourOrderlists = new List<EyouSoft.Model.TourStructure.OrderStatistics>();
            TourOrderlists = Ibll.GetOrderStatistics(intPageSize, intPageIndex, ref intRecordCount, 1, SellCompanyID, UserID, BuyCompanyName, BeginDate, EndDate);
            rptTradedCustomers.DataSource = TourOrderlists;
            rptTradedCustomers.DataBind();
            if (rptTradedCustomers.Items.Count < 1)
            {
                NoData.Visible = true;
            }
            Ibll = null;
            TourOrderlists = null;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 绑定子帐号
        /// </summary>
        private void BindSonAccount()
        {
            Utils.GetCompanyChildAccount(dplUserList, SellCompanyID);
            if (dplUserList.Items.Count > 0)
            {
                SonAccount = dplUserList.Items.Count-1;
            }
        }
    }
}
