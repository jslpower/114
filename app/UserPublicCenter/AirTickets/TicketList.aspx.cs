using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AirTickets
{
    /// <summary>
    /// author dj
    /// date 2011-6-1
    /// 机票列表页
    /// </summary>
    public partial class TicketList : EyouSoft.Common.Control.FrontPage
    {
        protected IList<EyouSoft.Model.TicketStructure.SpecialFares> sflist = null;

        protected int pagesize = 40;
        protected int pageindex;
        protected int totalcount;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }

            #region 动态添加Title以及keywords，description
            Page.Title = "特价机票_K位机票_免票_同业114特价机票频道";
            AddMetaTag("description", "同业114提供普通机票，免票，控票，特价机票，打折机票，更多超低价机票任您挑选，国航1折特价机票和折扣机票优惠多多，尽在tongye114。搜索上百家旅游预订网站机票报价和航空公司直 销机票价格,方便您的出行。");
            AddMetaTag("keywords", "特价机票，免票，控票，特价机票网，特价机票预订，机票频道，特价机票退票");
            #endregion
        }

        private void Bind()
        {
            pageindex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);
            sflist =  EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().GetSpecialFares(pagesize, pageindex, ref totalcount);
            BindPage();
            BindUpPage();

        }


        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPage()
        {
            this.ExportPageInfo.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfo.IsUrlRewrite = true;
                this.ExportPageInfo.Placeholder = "#PageIndex#";
                this.ExportPageInfo.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfo.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo.UrlParams = Request.QueryString;
            }
            this.ExportPageInfo.intPageSize = pagesize;
            this.ExportPageInfo.CurrencyPage = pageindex;
            this.ExportPageInfo.intRecordCount = totalcount;
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindUpPage()
        {
            this.ExportPageInfoUp.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfoUp.IsUrlRewrite = true;
                this.ExportPageInfoUp.Placeholder = "#PageIndex#";
                this.ExportPageInfoUp.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfoUp.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfoUp.UrlParams = Request.QueryString;
            }
            this.ExportPageInfoUp.intPageSize = pagesize;
            this.ExportPageInfoUp.CurrencyPage = pageindex;
            this.ExportPageInfoUp.intRecordCount = totalcount;
        }
    }
}
