using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common.Control;
using EyouSoft.Model.CompanyStructure;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 线路右边4块新闻咨询：最新旅游线路，广告，热点咨询，供求信息
    /// 孙川 2010-05-12
    /// </summary>
    public partial class UCLineRight : UserControl
    {
        protected int CityId = 0;
        protected string ImageServerPath = "";
        protected string NewestRouteList = "";//最新线路html
        protected string HotLists = "";//热点咨询html
        protected string SupplyList = "";//供求信息html
        private string ReturnUrl = "";
        public string HotCityName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
        }

        /// <summary>
        /// 获得最新线路信息列表
        /// </summary>
        private void GetNewestRouteList()
        {
            //显示签约的专线商有团队的上架线路
            IList<EyouSoft.Model.NewTourStructure.MRoute> TourList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetNewRouteList(10);
            if (TourList != null && TourList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                foreach (EyouSoft.Model.NewTourStructure.MRoute item in TourList)
                {
                    strAdvList.AppendFormat("<li><a href='{0}' target=\"_blank\" title=\"{2}\">{1}</a></li>", EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(item.Id, CityId) + "?ReturnUrl=" + ReturnUrl, EyouSoft.Common.Utils.GetText(item.RouteName, 16), item.RouteName);
                }
                NewestRouteList = strAdvList.ToString();
            }
            else
            {
                NewestRouteList = "<li>暂无最新线路信息</li>";
            }
        }

        /// <summary>
        /// 获取旅游企业
        /// </summary>
        protected void GetNewsOrdeyBy()
        {
            //获取旅游企业[10条]
            EyouSoft.Model.CompanyStructure.QueryNewCompany company = new EyouSoft.Model.CompanyStructure.QueryNewCompany();
            company.IsShowNoCheck = true;
            company.B2BDisplay = EyouSoft.Model.CompanyStructure.CompanyB2BDisplay.侧边推荐;
            company.CompanyTypes = new CompanyType?[] { CompanyType.地接, CompanyType.专线 };
            company.OrderIndex = 3;
            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> HotList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(10, company);
            StringBuilder strHotList = new StringBuilder();
            if (HotList != null && HotList.Count > 0)
            {
                for (int i = 0; i < HotList.Count; i++)
                {
                    strHotList.AppendFormat("<li><a href='{0}' title='{2}' target=\"_blank\">{1}</a></li>", EyouSoft.Common.Utils.GetDomainByCompanyId(HotList[i].ID, CityId), EyouSoft.Common.Utils.GetText(HotList[i].CompanyName, 16), HotList[i].CompanyName);
                }
            }
            else
            {
                strHotList.Append("<ul><li>暂无旅游企业信息</li></ul>");
            }
            HotLists = strHotList.ToString();
        }

        /// <summary>
        /// 获得供求信息列表
        /// </summary>
        private void GetSupplyList()
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> ExchangeList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(10, null, 0, null);
            if (ExchangeList != null && ExchangeList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                foreach (EyouSoft.Model.CommunityStructure.ExchangeList item in ExchangeList)
                {
                    strAdvList.AppendFormat("<li><a href='{0}' target=\"_blank\" title=\"{2}\">{1}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(item.ID, CityId), EyouSoft.Common.Utils.GetText(item.ExchangeTitle, 16), item.ExchangeTitle);
                }
                SupplyList = strAdvList.ToString();
            }
            else
            {
                SupplyList = "<li>暂无供求信息</li>";
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            FrontPage cPage = this.Page as FrontPage;
            if (cPage != null)
            {
                CityId = cPage.CityId;
                bool isExtendSite = EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().IsExtendSite(CityId);
                if (!isExtendSite)
                {
                    HotCityName = cPage.CityModel.CityName;
                }
                ImageServerPath = cPage.ImageServerUrl;
            }
            GetNewestRouteList();//获取最新旅游线路
            GetNewsOrdeyBy();//获取旅游企业
            GetSupplyList();//获取供求信息
            base.OnPreRender(e);
        }
    }
}