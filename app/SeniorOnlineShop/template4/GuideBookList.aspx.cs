using System;
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
namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 出游指南列表页
    /// </summary>
    /// 鲁功源 2010-11-12
    public partial class GuideBookList : EyouSoft.Common.Control.FrontPage
    {
        protected master.T4 cMaster = null;
        private int pageIndex = 0, infoType = 0, pageSize = 50, recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            cMaster = (SeniorOnlineShop.master.T4)this.Master;
            if (cMaster == null)
                return;
            cMaster.CTAB = master.T4TAB.出游指南;
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        #region 初始化列表
        protected void InitPageData()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = null;
            infoType = Utils.GetInt(Request.QueryString["t"], 0);
            string keyword = Utils.GetQueryStringValue("k");
            string CompanyId = Utils.GetQueryStringValue("cid").Length > 0 ? Utils.GetQueryStringValue("cid") : cMaster.CompanyId;
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            if (infoType > 0)
            {
                sTypeName.InnerText = "-> "+((EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)infoType).ToString();
                list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, CompanyId, keyword, (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)infoType);
            }
            else
            {
                list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, CompanyId, keyword);
            }
            RpTripGuides.DataSource = list;
            RpTripGuides.DataBind();
            if (recordCount == 0)
                DataEmpty.Visible = true;
            //绑定分页控件
            this.ExporPageByBtn1.intPageSize = pageSize;//每页显示记录数
            this.ExporPageByBtn1.intRecordCount = recordCount;
            this.ExporPageByBtn1.CurrencyPage = pageIndex;
            this.ExporPageByBtn1.UrlParams = Request.QueryString;
            this.ExporPageByBtn1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            list = null;
        }
        #endregion
    }
}
