using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace SeniorOnlineShop
{
    /// <summary>
    /// 高级网店散拼计划 推荐线路
    /// </summary>
    /// 周文超 2010-11-12
    public partial class TourList : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        private int PageSize = 25;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int CurrencyPage = 1;
        /// <summary>
        /// 初始化日历当前月时期
        /// </summary>
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        /// <summary>
        /// 返回页面地址
        /// </summary>
        protected string ReturnUrl = "";
        /// <summary>
        /// 当前的CityID  是否有效
        /// </summary>
        protected bool isValidCity = false;
        //mq
        protected string mqUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AddJavaScriptInclude("/DatePicker/WdatePicker.js", true, false);
            this.Master.CTAB = SeniorOnlineShop.master.T4TAB.散拼计划;

            if (EyouSoft.Common.Utils.GetInt(Utils.GetQueryStringValue("Page")) > 1)
            {
                CurrencyPage = EyouSoft.Common.Utils.GetInt(Utils.GetQueryStringValue("Page"));
            }
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);

            if (this.Master.CompanyId != "")
            {
                mqUrl = Utils.GetMQ(this.Master.CompanyId);
            }

            string State = string.Empty; //2 推荐线路 1 近期线路
            if (Utils.GetQueryStringValue("State") != "" && Utils.GetQueryStringValue("State") != null)
            {
                State = Utils.GetQueryStringValue("State");
            }
            if (!Page.IsPostBack)
            {
                GetAllLeaveCity();
                BindTourList(State);
                InitAreas();
            }
            //设置Title.....
            IList<EyouSoft.Model.SystemStructure.AreaBase> arealist = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(this.Master.CompanyId);
            string strarea = string.Empty;
            foreach (EyouSoft.Model.SystemStructure.AreaBase area in arealist)
            {
                strarea = strarea + "、" + area.AreaName;
            }
            this.Title = string.Format(EyouSoft.Common.PageTitle.SeniorShop_Title, strarea.TrimStart(new char[] { '、' }) + "散拼线路行程单");
        }

        /// <summary>
        /// 获的公司的所有销售城市
        /// </summary>
        protected void GetAllLeaveCity()
        {
            IList<EyouSoft.Model.SystemStructure.CityBase> SaleList = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanySaleCity(this.Master.CompanyId);

            if (SaleList != null && SaleList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.CityBase item in SaleList)
                {
                    this.dropLeaveCity.Items.Add(new ListItem(item.CityName.ToString() + "出港", item.CityId.ToString()));
                }

            }
            SaleList = null;
            this.dropLeaveCity.Items.Insert(0, new ListItem("全部", "0"));
            int tmpCityId = Utils.GetInt(Request.QueryString["CityId"]);
            if (tmpCityId >= 0)
            {
                ListItem item = this.dropLeaveCity.Items.FindByValue(tmpCityId.ToString());
                if (item != null)
                {
                    item.Selected = true;
                    isValidCity = true;
                }
            }
        }

        /// <summary>
        /// 绑定团队列表
        /// </summary>
        protected void BindTourList(string state)
        {
            int intRecordCount = 0;
            EyouSoft.Model.NewTourStructure.MRouteSearch Search = new EyouSoft.Model.NewTourStructure.MRouteSearch();
            if (Utils.GetQueryStringValue("CityId") != "" && Utils.GetQueryStringValue("CityId") != null)
            {
                this.dropLeaveCity.Items.FindByValue(Utils.GetQueryStringValue("CityId")).Selected = true;
            }
            Search.StartCity = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            if (Utils.GetQueryStringValue("RouteName") != "" && Utils.GetQueryStringValue("RouteName") != null)
            {
                this.txtRouteName.Value = Utils.GetQueryStringValue("RouteName");
            }
            Search.RouteKey = Utils.GetQueryStringValue("RouteName");
            if (Utils.GetQueryStringValue("Days") != "" && Utils.GetQueryStringValue("Days") != null)
            {
                this.txtDay.Value = Utils.GetQueryStringValue("Days");
            }
            Search.DayNum = Utils.GetInt(Utils.GetQueryStringValue("Days"));
            string StartDate = Utils.GetQueryStringValue("StartDate");
            if (StartDate != null && StartDate != "")
            {
                this.txtStartDate.Value = Utils.GetDateTime(Utils.GetQueryStringValue("StartDate")).ToString("yyyy-MM-dd");
                Search.StartDate = Utils.GetDateTime(Utils.GetQueryStringValue("StartDate"));
            }
            else
            {
                Search.StartDate = Utils.GetDateTime(DateTime.Now.ToString("d"));
            }

            string EndDate = Utils.GetQueryStringValue("EndDate");
            if (EndDate != null && EndDate != "")
            {
                this.txtEndDate.Value = Utils.GetDateTime(Utils.GetQueryStringValue("EndDate")).ToString("yyyy-MM-dd");
            }
            Search.EndDate = Utils.GetDateTime(Utils.GetQueryStringValue("EndDate"));

            if (Utils.GetQueryStringValue("AreaId") != "" && Utils.GetQueryStringValue("AreaId") != null)
            {
                Search.AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId"));
            }

            if (state == "2")
            {
                IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteRecommendList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetShop4RecommendList(PageSize, CurrencyPage, ref intRecordCount, this.Master.CompanyId, Search);
                if (RouteRecommendList != null && RouteRecommendList.Count > 0)
                {
                    this.rptTourList.DataSource = RouteRecommendList;
                    this.rptTourList.DataBind();
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.UrlHref;
                    this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                    this.ExporPageInfoSelect1.LinkType = 3;
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                }
                else
                {
                    trNoData.Visible = true;
                }
                RouteRecommendList = null;
            }
            else
            {
                IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteNearList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetShop4NearList(PageSize, CurrencyPage, ref intRecordCount, this.Master.CompanyId, Search);
                if (RouteNearList != null && RouteNearList.Count > 0)
                {
                    this.rptTourList.DataSource = RouteNearList;
                    this.rptTourList.DataBind();
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.UrlHref;
                    this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                    this.ExporPageInfoSelect1.LinkType = 3;
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                }
                else
                {
                    trNoData.Visible = true;
                }
                RouteNearList = null;
            }
        }

        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitAreas()
        {
            var areas = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(this.Master.CompanyId);
            if (areas != null && areas.Count > 0)
            {
                this.rptAreas.DataSource = areas;
                this.rptAreas.DataBind();
            }
            areas = null;
        }

        protected string GetRecommendType(string state)
        {
            //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩
            string stateHtml = string.Empty;
            switch (state)
            {
                case "1": stateHtml = ""; break;
                case "2": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_30.gif\" width=\"25\" height=\"15\" alt=\"推荐\"/>"; break;
                case "3": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_35.gif\" width=\"25\" height=\"15\" alt=\"特价\"/>"; break;
                case "4": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_39.gif\" width=\"25\" height=\"15\" alt=\"豪华\"/>"; break;
                case "5": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_42.gif\" width=\"25\" height=\"15\" alt=\"热门\"/>"; break;
                case "6": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_44.gif\" width=\"25\" height=\"15\" alt=\"新品\"/>"; break;
                case "7": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_48.gif\" width=\"25\" height=\"15\" alt=\"经典\"/>"; break;
                case "8": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_46.gif\" width=\"25\" height=\"15\" alt=\"纯玩\"/>"; break;
                default: break;
            }
            return stateHtml;
        }

        /// <summary>
        /// 格式化出团日期
        /// </summary>
        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}/{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
        }

        /// <summary>
        /// 获取团队预定页面链接
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        protected string GetOrderByRoute(string tourId)
        {
            string GetOrderByRouteHtml = string.Empty;
            if (IsLogin)
            {
                if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"   href=\"" + EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?TourID=" + tourId + "\">预定</a>";
                }
            }
            else
            {
                GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\"  class=\"goumai0\">预定</a>";
            }
            return GetOrderByRouteHtml;
        }


        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }
    }
}
