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

namespace SeniorOnlineShop.shop
{
    public partial class Hotel : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        private int CurrencyPage = 1;
        private int PageSize = 10;
        protected int RecordCount = 0;
        protected bool IsLogins = false;
        protected string CompanyId = string.Empty;
        #endregion
        protected SeniorOnlineShop.master.GeneralShop Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master = (SeniorOnlineShop.master.GeneralShop)this.Page.Master;
            Master.HeadIndex = 4;
            Page.Title = string.Format(PageTitle.ScenicDetail_Title, CityModel.CityName, Master.CompanyInfo.CompanyName);
            AddMetaTag("description", string.Format(PageTitle.ScenicDetail_Des, CityModel.CityName, Master.CompanyInfo.CompanyName));
            AddMetaTag("keywords", string.Format(EyouSoft.Common.PageTitle.ScenicDetail_Keywords, Master.CompanyInfo.CompanyName));
            SecondMenu1.CompanyId = Master.CompanyId;
            SecondMenu1.CurrCompanyType = Master.CompanyInfo.CompanyRole;
            SecondMenu1.CurrMenuIndex = 4;
            if (!IsPostBack)
            {
                GetHotelList();
            }
        }
        private void GetHotelList()
        {
            
        }

        //分页
        protected void InitPage()
        {
            var Params = Request.QueryString;
            var tmpParams = new System.Collections.Specialized.NameValueCollection();
            if (Params != null && Params.Count > 0)
            {
                foreach (var s in Params.AllKeys)
                {
                    if (!string.IsNullOrEmpty(s) && s.ToLower() != "urltype" && s.ToLower() != "requestsource" && s.ToLower() != "t")
                    {
                        tmpParams.Add(s, Params[s]);
                    }
                }
            }
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = tmpParams;
        }
    }
}
