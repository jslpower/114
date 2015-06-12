using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserBackCenter.TicketsCenter.StatisticsManage.PurchasingAnalysis
{
    /// <summary>
    /// 功能：统计分析-采购分析列表
    /// 开发人：刘玉灵  时间：2010-10-20
    /// </summary>
    public partial class PurchasingList : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindList();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindList()
        {

            string UserName = Utils.InputText(Server.UrlDecode(Request.QueryString["UserName"]));

            if (!string.IsNullOrEmpty(UserName))
            {
                this.txtUserName.Value = UserName;
            }
            int? TicketNumber = Utils.GetIntNull(Request.QueryString["TicketNumber"]);
            decimal? TicketMoney = Utils.GetDecimal(Request.QueryString["TicketMoney"]);
            if (TicketMoney == 0) TicketMoney = null;
            DateTime? SartDateTime = Utils.GetDateTimeNullable(Request.QueryString["SartDateTime"]);
            DateTime? EndDateTime = Utils.GetDateTimeNullable(Request.QueryString["EndDateTime"]);
            EyouSoft.Model.TicketStructure.BuyerAnalysisSearchInfo searchModel = new EyouSoft.Model.TicketStructure.BuyerAnalysisSearchInfo();
            searchModel.BuyerUName = UserName;
            searchModel.TicketTotalAmount = TicketMoney;
            searchModel.TicketTotalCount=TicketNumber;
            searchModel.OrderStartTime = SartDateTime;
            searchModel.OrderFinishTime = EndDateTime;
            
            IList<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo> list = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetBuyerAnalysis(searchModel);
            if (list == null || list.Count <= 0)
            {
                this.lblMsg.Text = "未找到相关数据!";
            }
            else
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            list = null;
            
            
        }

    }
}
