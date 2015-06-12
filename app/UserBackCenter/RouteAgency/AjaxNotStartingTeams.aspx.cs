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
    public partial class AjaxNotStartingTeams : EyouSoft.Common.Control.BasePage
    {
        private int intPageSize = 5;
        protected int intPageIndex = 1;
        private int CurrentAreaId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            int intRecordCount = 0;
            string CompanyID = string.Empty;
            CompanyID = this.SiteUserInfo.CompanyID;
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            if (Request.QueryString["CurrentAreaId"] != null && !string.IsNullOrEmpty(Request.QueryString["CurrentAreaId"]))
            {

                CurrentAreaId = Utils.GetInt(Request.QueryString["CurrentAreaId"], -1);
                IList<EyouSoft.Model.TourStructure.TourBasicInfo> lists = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetNotStartingTours(intPageSize, intPageIndex, ref intRecordCount, CompanyID, CurrentAreaId);
                rptTourBasicInfo.DataSource = lists;
                rptTourBasicInfo.DataBind();
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = intPageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                if (rptTourBasicInfo.Items.Count < 1)
                {
                    NoData.Visible = true;
                }
                lists = null;
            }
        }
    }
}
