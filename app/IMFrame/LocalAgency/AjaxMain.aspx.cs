using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
namespace IMFrame.LocalAgency
{
    /// <summary>
    /// IM线路列表Ajax
    /// 创建者：袁惠 创建时间：2010-4-26
    /// </summary>
    public partial class AjaxMain : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string Url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化页面 
        /// </summary>
        private void InitPage()
        {
            MRouteSearch queryModel = new MRouteSearch();
            queryModel.RouteSource = RouteSource.地接社添加;
            int intPageSize = 7;
            int intRecordCount = 0;
            int intPageindex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            IList<EyouSoft.Model.NewTourStructure.MRoute> RouteList =
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetBackCenterList(intPageSize, intPageindex, ref intRecordCount, this.SiteUserInfo.CompanyID, queryModel);
            if (RouteList.Count > 0 && RouteList != null)
            {
                this.rptRoutes.DataSource = RouteList;
                this.rptRoutes.DataBind();
                this.ExporPageInfoSelect2.PageLinkCount = 7;
                ExporPageInfoSelect2.CurrencyPage = intPageindex;
                this.ExporPageInfoSelect2.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect2.intPageSize = intPageSize;
                this.ExporPageInfoSelect2.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect2.AttributesEventAdd("onclick", "LoadData(this)", 1);
                RouteList = null;
            }
            else
            {
                rptRoutes.Visible = false;
                this.NoData.Visible = true;

            }
        }
    }
}
