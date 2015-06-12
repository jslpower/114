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
    ///专线公司与组团社交易明细页： 预订成功列表、留位过期列表、不受理列表
    /// luofx 2010-8-27
    /// </summary>
    public partial class HasTradedTeam : EyouSoft.Common.Control.BasePage
    {
        private int PageSize = 20;
        private int PageIndex = 1;
        protected string TradedState = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.IsLogin)
                {
                    InitPage();
                }
                else
                {
                    Utils.ShowError("对不起，你还未登录或登录过期，请登录！", "");
                    return;
                }
            }
        }
        private void InitPage()
        {
            string BuyCompanyID = Request.QueryString["BuyCompanyID"];
            string CompanyId = this.SiteUserInfo.CompanyID;
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(BuyCompanyID))
            {
                EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
                IList<EyouSoft.Model.TourStructure.TourOrder> TourOrderlists = new List<EyouSoft.Model.TourStructure.TourOrder>();
                EyouSoft.Model.TourStructure.OrderState? Sate = null;
                if (!string.IsNullOrEmpty(Request.QueryString["OrderState"]))
                {
                    Sate = (EyouSoft.Model.TourStructure.OrderState)Enum.Parse(typeof(EyouSoft.Model.TourStructure.OrderState), Request.QueryString["OrderState"]);
                    ltrOrderSateText.Text = Sate.ToString() + "列表";
                    TradedState = Sate.ToString();
                }
                TourOrderlists = Ibll.GetOrderList(PageSize, PageIndex, ref RecordCount, 1, CompanyId, BuyCompanyID, Sate);
                //绑定数据
                rptHasTradedTeam.DataSource = TourOrderlists;
                rptHasTradedTeam.DataBind();
                TourOrderlists = null;
                Ibll = null;
                if (rptHasTradedTeam.Items.Count < 1)
                {
                    NoData.Visible = true;
                }
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.intRecordCount = RecordCount;
                this.ExportPageInfo1.PageLinkURL = Request.Url.ToString()+"&";
            }
            else
            {
                NoData.Visible = true;
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptHasTradedTeam_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(PageSize * (PageIndex - 1) + (e.Item.ItemIndex + 1));
            }
        }
    }
}
