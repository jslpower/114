using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Collections.Generic;

namespace SeniorOnlineShop.shop
{
    public partial class Industry : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        private int PageSize = 10;
        private int PageIndex = 1;
        protected int RecordCount = 0;
        protected string CompanyId = string.Empty;
        #endregion
        protected SeniorOnlineShop.master.GeneralShop Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master = (SeniorOnlineShop.master.GeneralShop)this.Page.Master;
            Master.HeadIndex = 2;
            Page.Title = string.Format(PageTitle.EShop_Info_Title, Master.CompanyInfo.CompanyName);
            AddMetaTag("description", string.Format(PageTitle.EShop_Info_Des, Master.CompanyInfo.CompanyName, CityModel.CityName));
            AddMetaTag("keywords", string.Format(PageTitle.EShop_Keywords, Master.CompanyInfo.CompanyName, CityModel.CityName));
            SecondMenu1.CompanyId = Master.CompanyId;
            SecondMenu1.CurrCompanyType = Master.CompanyInfo.CompanyRole;
            SecondMenu1.CurrMenuIndex = 5;
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!IsPostBack)
            {
                GetIndustrylList();
            }
        }

        private void GetIndustrylList()
        {
            EyouSoft.Model.NewsStructure.MQueryPeerNews mquery=new EyouSoft.Model.NewsStructure.MQueryPeerNews();
            mquery.CompanyId = Master.CompanyId;
            IList<EyouSoft.Model.NewsStructure.MPeerNews> PeerNewList = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().GetGetPeerNewsList(PageSize, PageIndex, ref RecordCount,mquery);
            if (PeerNewList != null && PeerNewList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = RecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.rptNewsList.DataSource = PeerNewList;
                this.rptNewsList.DataBind();
            }
            else
            {
                this.rptNewsList.EmptyText = "无同业资讯！";
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        /// <summary>
        /// 获取快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }
    }
}
