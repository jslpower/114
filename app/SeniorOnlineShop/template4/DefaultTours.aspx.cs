using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 高级网店模板4首页团队
    /// </summary>
    public partial class DefaultTours : BasePage
    {
        protected string CompanyId = string.Empty;
        protected string MqURlHtml = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cid = Utils.GetQueryStringValue("cid");
            int areaId = Utils.GetInt(Utils.GetQueryStringValue("aid"));
            string tss = Utils.GetQueryStringValue("tss");
            
            if (!string.IsNullOrEmpty(cid) && cid != null)
            {
                MqURlHtml = Utils.GetMQ(cid);
            }
            this.InitTours(cid, areaId, tss);
        }

        #region private members
        /// <summary>
        /// 初始旅游线路  0最新旅游线路 1 推荐旅游线路
        /// </summary>
        private void InitTours(string companyId, int areaId, string tourSpreadState)
        {
            EyouSoft.Model.NewTourStructure.MRouteSearch Search = new EyouSoft.Model.NewTourStructure.MRouteSearch();
            Search.StartDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            if (tourSpreadState == "0")
            {
                IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteLList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetShop4NearList(9, areaId, companyId, Search);
                if (RouteLList != null && RouteLList.Count > 0)
                {
                    rptTours.DataSource = RouteLList;
                    rptTours.DataBind();
                }
            }
            else
            {
                IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteRecommendList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetShop4RecommendList(9, areaId, companyId, Search);
                if (RouteRecommendList != null && RouteRecommendList.Count > 0)
                {
                    rptTours.DataSource = RouteRecommendList;
                    rptTours.DataBind();
                }
            }
        }

        protected string GetRecommendType(string state)
        {
            //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩
            string stateHtml = string.Empty;
            switch (state)
            {
                case "1": stateHtml = ""; break;
                case "2": stateHtml = "<img  style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_30.gif\" width=\"25\" height=\"15\" alt=\"推荐\"/>"; break;
                case "3": stateHtml = "<img style=\"margin-left:5px\"  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_35.gif\" width=\"25\" height=\"15\" alt=\"特价\"/>"; break;
                case "4": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_39.gif\" width=\"25\" height=\"15\" alt=\"豪华\"/>"; break;
                case "5": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_42.gif\" width=\"25\" height=\"15\" alt=\"热门\"/>"; break;
                case "6": stateHtml = "<img style=\"margin-left:5px\"  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_44.gif\" width=\"25\" height=\"15\" alt=\"新品\"/>"; break;
                case "7": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_48.gif\" width=\"25\" height=\"15\" alt=\"经典\"/>"; break;
                case "8": stateHtml = "<img style=\"margin-left:5px\" src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_46.gif\" width=\"25\" height=\"15\" alt=\"纯玩\"/>"; break;
                default: break;
            }
            return stateHtml;
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

        /// <summary>
        /// rptToursCreated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptToursCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex < 0) return;
            var Route = e.Item.DataItem as EyouSoft.Model.NewTourStructure.MShopRoute;
            if (Route == null) return;

            Literal ltrLeaveDate = e.Item.FindControl("ltrLeaveDate") as Literal;
            if (ltrLeaveDate != null)
            {
                ltrLeaveDate.Text = string.Format("{0}({1})", Route.LeaveDate.ToString("MM/dd"), Utils.ConvertWeekDayToChinese(Route.LeaveDate));
            }
            
            Literal ltrRemnantNumber = e.Item.FindControl("ltrRemnantNumber") as Literal;
            if (ltrRemnantNumber != null)
            {
                ltrRemnantNumber.Text = string.Format("剩{0}", Route.MoreThan);
            }

            Literal ltrPriceMS = e.Item.FindControl("ltrPriceMS") as Literal;
            if (ltrPriceMS != null)
            {
                ltrPriceMS.Text = string.Format("{0}/{1}", Route.RetailAdultPrice.ToString("C0"), Route.SettlementAudltPrice.ToString("C0"));
            }

            Literal ltrPriceTH = e.Item.FindControl("ltrPriceTH") as Literal;
            if (ltrPriceTH != null)
            {
                ltrPriceTH.Text = string.Format("{0}/{1}", Route.RetailChildrenPrice.ToString("C0"), Route.SettlementChildrenPrice.ToString("C0"));
            }

            Literal ltrPriceDFC = e.Item.FindControl("ltrPriceDFC") as Literal;
            if (ltrPriceDFC != null)
            {
                ltrPriceDFC.Text = Route.StartCityName;                  
            }
            
        }
        #endregion
    }
}
