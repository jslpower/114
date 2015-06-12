using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 消费明细
    /// </summary>
    public partial class GetExpenseDetails : System.Web.UI.Page //EyouSoft.Common.Control.BackPage
    {
        private int PageSize = 10;
        private int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                this.GetExpenseDetailList();
            }
        }

        /// <summary>
        /// 消费明细
        /// </summary>
        protected void GetExpenseDetailList()
        {
            int intRecordCount = 0;
            IList<EyouSoft.Model.SMSStructure.SendTotalInfo> ExpenseDetailList = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetExpenseDetails(PageSize, PageIndex, ref intRecordCount, Request.QueryString["CompanyId"]);
            if (intRecordCount > 0)
            {
                this.GetExpenseDetail_ExporPageInfoSelect.intPageSize = PageSize;
                this.GetExpenseDetail_ExporPageInfoSelect.intRecordCount = intRecordCount;
                this.GetExpenseDetail_ExporPageInfoSelect.CurrencyPage = PageIndex;

                this.GetExpenseDetail_ExporPageInfoSelect.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.GetExpenseDetail_ExporPageInfoSelect.AttributesEventAdd("onclick", "AccountInfo.ExpenseDetailLoadData(this);", 1);
                //this.GetExpenseDetail_ExporPageInfoSelect.AttributesEventAdd("onchange", "AccountInfo.ExpenseDetailLoadData(this);", 0);

                this.GetExpenseDetail_repExpenseDetail.DataSource = ExpenseDetailList;
                this.GetExpenseDetail_repExpenseDetail.DataBind();
                if (intRecordCount > PageSize)
                {
                    this.GetExpenseDetail_ExporPageInfoSelect.Visible = true;
                }
                else
                {
                    this.GetExpenseDetail_ExporPageInfoSelect.Visible = false;
                }
                
                //汇总
                this.GetExpenseDetail_tbSumCountAndMoney.Visible = true;
                EyouSoft.Model.SMSStructure.AccountExpenseCollectInfo model = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetAccountExpenseCollectInfo(Request.QueryString["CompanyId"]);
                if (model != null)
                {
                    this.GetExpenseDetail_labSumSendCount.Text = model.SentMessageCount.ToString();
                    this.GetExpenseDetail_labSumSendMoney.Text = model.ExpenseAmount.ToString("F2");
                }
                model = null;
            }
            else
            {
                this.GetExpenseDetail_ExporPageInfoSelect.Visible = false;
                this.GetExpenseDetail_repExpenseDetail.EmptyText = "<div>暂无消费明细</div>";
            }
            ExpenseDetailList = null;

        }

    }
}
