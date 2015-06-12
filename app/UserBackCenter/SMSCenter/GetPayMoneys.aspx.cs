using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 充值明细
    /// </summary>
    public partial class GetPayMoneys : System.Web.UI.Page //EyouSoft.Common.Control.BackPage
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
                this.GetPayMoneyList();
            }
        }

        /// <summary>
        /// 充值明细
        /// </summary>
        protected void GetPayMoneyList()
        {
            int intRecordCount = 0;
            IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> PayMoneyList = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetPayMoneys(PageSize, PageIndex, ref intRecordCount, Request.QueryString["CompanyId"]);
            if (intRecordCount > 0)
            {
                this.GetPayMoneys_ExporPageInfoSelect.intPageSize = PageSize;
                this.GetPayMoneys_ExporPageInfoSelect.intRecordCount = intRecordCount;
                this.GetPayMoneys_ExporPageInfoSelect.CurrencyPage = PageIndex;
                this.GetPayMoneys_ExporPageInfoSelect.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.GetPayMoneys_ExporPageInfoSelect.AttributesEventAdd("onclick", " AccountInfo.PayMoneyLoadData(this);", 1);
               // this.GetPayMoneys_ExporPageInfoSelect.AttributesEventAdd("onchange", " AccountInfo.PayMoneyLoadData(this);", 0);

                this.GetPayMoneys_repPayMoneyList.DataSource = PayMoneyList;
                this.GetPayMoneys_repPayMoneyList.DataBind();
                if (intRecordCount > PageSize)
                {
                    this.GetPayMoneys_ExporPageInfoSelect.Visible = true;
                }
                else {
                    this.GetPayMoneys_ExporPageInfoSelect.Visible = false;
                }
            }
            else
            {
                this.GetPayMoneys_ExporPageInfoSelect.Visible = false;
                this.GetPayMoneys_repPayMoneyList.EmptyText = "<div >暂无充值明细</div>";
            }
            PayMoneyList = null;

        }

    }
}
