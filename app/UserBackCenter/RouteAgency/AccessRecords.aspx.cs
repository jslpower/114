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
using Adpost.Common;
using EyouSoft.Common;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 未出发团队
    /// 创建者：luofx 时间：2010-7-1
    /// </summary>
    public partial class AccessRecords :EyouSoft.Common.Control.BasePage
    {
        private int intPageSize = 20, intPageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsLogin)
            {
                if (!IsPostBack)
                {
                    InitPage();
                }
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "对不起你还没登陆,请登录！");
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            int intRecordCount = 0;
            string TourID = string.Empty;
            if (Request.QueryString["TourID"] != null && !string.IsNullOrEmpty(Request.QueryString["TourID"]))
            {
                TourID = Utils.InputText(Request.QueryString["TourID"]);
                EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
                this.rptRecords.DataSource = Ibll.GetTourVisitedHistorys(intPageSize, intPageIndex, ref intRecordCount, TourID);
                this.rptRecords.DataBind();             
                Ibll = null;
            }
            this.ExportPageInfo1.LinkType = 4;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            if (rptRecords.Items.Count <= 0)
            {
                this.NoData.Visible = true;
            }
        }
        /// <summary>
        /// 序号赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_AccessRecords_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(intPageSize * (intPageIndex - 1) + (e.Item.ItemIndex + 1));
            }
        }
    }
}
