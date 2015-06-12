using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 高级网店模版4最新旅游动态列表
    /// </summary>
    /// 周文超 2010-11-22
    public partial class NewsList : EyouSoft.Common.Control.FrontPage
    {
        protected master.T4 cMaster = null;
        private int pageIndex = 0, pageSize = 50, recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            cMaster = (SeniorOnlineShop.master.T4)this.Master;
            if (cMaster == null)
                return;
            cMaster.CTAB = master.T4TAB.首页;
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        #region 初始化列表

        protected void InitPageData()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopNews> list = new List<EyouSoft.Model.ShopStructure.HighShopNews>();
            string keyword = Utils.GetQueryStringValue("k");
            string CompanyId = Utils.GetQueryStringValue("cid").Length > 0 ? Utils.GetQueryStringValue("cid") : cMaster.CompanyId;
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);

            ltrHeadLink.Text = string.Format("当前位置：<a href=\"{0}\">首页</a>-&gt;<strong>旅游动态</strong>", Utils.GenerateShopPageUrl2("/default", CompanyId));

            list = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetWebList(pageSize, pageIndex, ref recordCount, CompanyId, keyword);

            RpTripGuides.DataSource = list;
            RpTripGuides.DataBind();
            if (recordCount == 0)
                DataEmpty.Visible = true;
            //绑定分页控件
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.UrlHref;
            this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
            this.ExporPageInfoSelect1.LinkType = 3;
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            list = null;
        }

        #endregion
    }
}
