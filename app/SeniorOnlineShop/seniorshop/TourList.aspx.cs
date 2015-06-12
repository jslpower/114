using System;
using System.Collections;
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
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;
namespace SeniorOnlineShop.seniorshop
{
    public partial class TourList : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        private int PageSize = 20;
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
        //mq链接
        protected string MqURl = string.Empty;       

        protected void Page_Load(object sender, EventArgs e)
        {
            this.AddJavaScriptInclude("/DatePicker/WdatePicker.js", true, false);

            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                CurrencyPage = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                GetAllLeaveCity();
                BindTourList();
                GetMq();
            }
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
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


            //zxb 20100903 加入判断：CityID是否存在于SaleList集合中
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
        protected void BindTourList()
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
            }
            Search.StartDate = Utils.GetDateTime(Utils.GetQueryStringValue("StartDate"));

            string EndDate = Utils.GetQueryStringValue("EndDate");
            if (EndDate != null && EndDate != "")
            {
                this.txtEndDate.Value = Utils.GetDateTime(Utils.GetQueryStringValue("EndDate")).ToString("yyyy-MM-dd");
            }
            Search.EndDate = Utils.GetDateTime(Utils.GetQueryStringValue("EndDate"));

            IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetShopList(PageSize, CurrencyPage, ref intRecordCount, this.Master.CompanyId, Search);
            if (RouteList != null && RouteList.Count > 0)
            {
                this.repTourList.DataSource = RouteList;
                this.repTourList.DataBind();
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;               
            }
            else
            {
                this.repTourList.EmptyText = "<table  width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\" bordercolor=\"#C9DEEF\"><tr><td height=\"100px\" colspan=\"8\" align=\"center\">对不起，暂无旅游线路信息！</td></tr></table>";
            }
            RouteList = null;
        }

        protected string GetMq()
        {
            MqURl = Utils.GetMQ(this.Master.CompanyId);
            return MqURl;
        }


        protected string TeamPlanDesAndPrices(string routeID, string RouteSource,string teamPlabDes,string Prices)
        {              
            string TeamPlanDesAndPricesHtml = string.Empty;
            if (RouteSource == "地接社添加")
            {
                TeamPlanDesAndPricesHtml = "<td align=\"center\">暂无</td><td align=\"center\" class=\"td_noDefault\">电询</td>";
            }
            else
            {
                IList<EyouSoft.Model.NewTourStructure.MPowderList> PowerList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(routeID);
                if (PowerList != null && PowerList.Count > 0)
                {
                    TeamPlanDesAndPricesHtml = "<td align=\"center\">" + teamPlabDes + "</td><td align=\"center\" class=\"td_noDefault\">￥" + Prices + "起</td>";
                }
                else
                {
                    TeamPlanDesAndPricesHtml = "<td align=\"center\">暂无</td><td align=\"center\" class=\"td_noDefault\">电询</td>";
                }
            }
            return TeamPlanDesAndPricesHtml;
        }

        /// <summary>
        /// 1 线路来源于地接:  当前登录公司是组团，预定跳转到用户后台单团预定 其它身份就跳转到用户后台首页
        /// 2 线路来源于专线:  先判断线路下是否有计划的，如果有计划的，在判断公司身份的，如果公司身份中包含组团身份 到用户后台 单团预定
        ///                    如果公司身份中包含专线身份的，跳转到用户后台专线待定的 如果包含地接身份的，跳转到用户后台首页
        ///                    如果没有计划,也要判断公司身份的，如果公司身份包含组团身份的 到用户后台 单团预定 其它身份都跳转到用户后台首页的  
        /// </summary>
        /// <returns></returns>
        protected string GetOrderByRoute(string routeID, object o)
        {             
            string GetOrderByRouteHtml = string.Empty;
            if (IsLogin)
            {
                EyouSoft.Model.NewTourStructure.RouteSource rSource = (EyouSoft.Model.NewTourStructure.RouteSource)o;
                if (rSource == EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                {
                    if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        GetOrderByRouteHtml = "<a  target=\"_blank\" href='" + EyouSoft.Common.Domain.UserBackCenter + "/TeamService/SingleGroupPre.aspx?routeId=" + routeID + "&isZT=true' class=\"goumai0\">预定</a>";
                    }
                    else
                    {
                        GetOrderByRouteHtml = "<a  alt='该操作需要组团身份' href='" + EyouSoft.Common.Domain.UserBackCenter + "' class=\"goumai0\">预定</a>";
                    }
                }
                else
                {
                    IList<EyouSoft.Model.NewTourStructure.MPowderList> PowerList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(routeID);
                    if (PowerList != null && PowerList.Count > 0)
                    {
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"   href=\"" + EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?routeId=" + routeID.ToString() + "&isZT=true\">预定</a>";
                        }
                        else if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\" href='" + EyouSoft.Common.Domain.UserBackCenter + "/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + PowerList[0].TourId + "'>预定</a>";
                        }
                        else
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" href='" + EyouSoft.Common.Domain.UserBackCenter + "' class=\"goumai0\">预定</a>";
                        }
                    }
                    else
                    {
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"  href='" + EyouSoft.Common.Domain.UserBackCenter + "/TeamService/SingleGroupPre.aspx?routeId=" + routeID + "'>预定</a>";
                        }
                        else
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"  href=\"" + EyouSoft.Common.Domain.UserBackCenter + "\">预定</a>";
                        }
                    }
                }
            }
            else
            {
                GetOrderByRouteHtml = "<a target=\"_blank\"  class=\"goumai0\">预定</a>";
            }
            return GetOrderByRouteHtml;
        }


        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        /// <summary>
        /// 线路推荐状态
        /// </summary>
        /// <returns></returns>
        protected string GetRouteName(string RecommendType)
        {
            //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品  7 经典 8 纯玩
            string stateHtml = string.Empty;
            switch (RecommendType)
            {
                case "1": stateHtml = ""; break;
                case "2": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_30.gif\" width=\"25\" height=\"15\" alt=\"推荐\"/>"; break;
                case "3": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_35.gif\" width=\"25\" height=\"15\" alt=\"特价\"/>"; break;
                case "4": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_39.gif\" width=\"25\" height=\"15\" alt=\"豪华\"/>"; break;
                case "5": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_42.gif\" width=\"25\" height=\"15\" alt=\"热门\"/>"; break;
                case "6": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_44.gif\" width=\"25\" height=\"15\" alt=\"新品\"/>"; break;
                case "7": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_48.gif\" width=\"25\" height=\"15\" alt=\"经典\"/>"; break;
                case "8": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_46.gif\" width=\"25\" height=\"15\" alt=\"纯玩\"/>"; break;
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

    }
}
