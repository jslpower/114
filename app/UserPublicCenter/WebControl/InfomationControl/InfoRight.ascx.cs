using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;

namespace UserPublicCenter.WebControl.InfomationControl
{
    /// <summary>
    /// 所有新闻显示页面右边显示的控件
    /// mk 2010-4-1
    /// </summary>
    public partial class InfoRight : UserControl
    {
        protected int CityId = 0;
        protected string ImageServerPath = "";
        protected string NewestRouteList = "";//最新线路html
        protected string TourCompanyList = "";//旅游企业html
        protected string SupplyList = "";//供求信息html
        private string ReturnUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
        }

        /// <summary>
        /// 获得最新线路信息列表
        /// </summary>
        private void GetNewestRouteList()
        {
            IList<EyouSoft.Model.NewTourStructure.MRoute> list =
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetNewRouteList(10);
            if (list != null && list.Count > 0)
            {
                var strAdvList = new StringBuilder();
                foreach (var item in list)
                {
                    strAdvList.AppendFormat("<li><a href='{0}' target=\"_blank\">{1}</a></li>", EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(item.Id, CityId) + "?ReturnUrl=" + ReturnUrl, EyouSoft.Common.Utils.GetText(item.RouteName, 18));
                }
                NewestRouteList = strAdvList.ToString();
            }
            else
            {
                NewestRouteList = "<li>暂无最新线路信息</li>";
            }
        }

        /// <summary>
        /// 获得旅游企业信息列表
        /// </summary>
        private void GetTourCompanyList()
        {
            //获取旅游企业[10条]
            EyouSoft.Model.CompanyStructure.QueryNewCompany company = new EyouSoft.Model.CompanyStructure.QueryNewCompany();
            company.IsShowNoCheck = true;
            company.B2BDisplay = EyouSoft.Model.CompanyStructure.CompanyB2BDisplay.侧边推荐;
            company.CompanyTypes = new EyouSoft.Model.CompanyStructure.CompanyType?[]
                { EyouSoft.Model.CompanyStructure.CompanyType.地接, EyouSoft.Model.CompanyStructure.CompanyType.专线 };
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

            TourCompanyList = strHotList.ToString();
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
                    strAdvList.AppendFormat("<li><a href='{0}' target=\"_blank\">{1}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(item.ID, CityId), EyouSoft.Common.Utils.GetText(item.ExchangeTitle, 18));
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
                ImageServerPath = cPage.ImageServerUrl;
            }
            GetNewestRouteList();//获取最新旅游线路
            GetTourCompanyList();//获取旅游企业
            GetSupplyList();//获取供求信息
            base.OnPreRender(e);
        }
    }
}