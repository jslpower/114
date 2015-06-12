using System;
using System.Collections;
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
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 线路列表
    /// 罗丽娥   2010-06-28
    /// </summary>
    public partial class AjaxRouteView : System.Web.UI.Page
    {
        /// <summary>
        /// 线路来源
        /// </summary>
        protected RouteSource routeSource;
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitRouteList();
            }
        }

        /// <summary>
        /// 初始化线路列表
        /// </summary>
        private void InitRouteList()
        {
            //记录条数
            int intRecordCount = 0;
            //页码
            int CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            int intPageSize = 15;
            IRoute bll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            //查询实体
            MRouteSearch queryModel = new MRouteSearch();
            #region 查询参数赋值
            routeSource = (RouteSource)Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1);
            //线路来源,专线，地接
            queryModel.RouteSource = routeSource;

            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);
            if (Utils.GetIntSign(Utils.GetQueryStringValue("lineType"), -1) >= 0)
            {
                queryModel.RouteType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType"));
            }
            //出发城市
            queryModel.StartCity = Utils.GetInt(Utils.GetQueryStringValue("goCity"), 0);
            //关键字
            queryModel.RouteKey = Utils.GetQueryStringValue("keyWord") == "线路名称" ? string.Empty : Utils.GetQueryStringValue("keyWord");
            #endregion
            //线路库列表
            IList<MRoute> list = bll.GetBackCenterList(intPageSize, CurrencyPage, ref intRecordCount, Utils.GetQueryStringValue("companyID"), queryModel);
            if (list != null && list.Count > 0)
            {
                //存在列表数据
                rpt_List.DataSource = list;
                rpt_List.DataBind();

                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = "/routeagency/routemanage/RouteView.aspx?";

                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goCity", queryModel.StartCity.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.RouteKey);
                this.ExportPageInfo1.UrlParams.Add("routeSource", Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1).ToString());
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }

        }

        #region 获取线路报价标准
        protected string GetRoutePrice(string RouteId)
        {
            string strPriceStandName = string.Empty;
            string strMarketPrice = string.Empty;
            string strPrice = string.Empty;
            string strRoomPrice = string.Empty;
            //TourUnion.BLL.LocalAgency.Route bll = new TourUnion.BLL.LocalAgency.Route();
            //IList<TourUnion.Model.LocalAgency.RoutePriceDetailInfo> list = bll.GetRoutePriceDetails(RouteId);
            //strPriceStandName += "<td class='typeprice' align='center'>";
            //strMarketPrice += "<td align='center' >";
            //strPrice += "<td align='center'>";
            //if (list != null && list.Count > 0)
            //{
            //    string tmpPriceStandID = string.Empty;
            //    string tmpPriceStandName = string.Empty;
            //    foreach (TourUnion.Model.LocalAgency.RoutePriceDetailInfo model in list)
            //    {
            //        if (model.PriceStandId != tmpPriceStandID)
            //            tmpPriceStandID = model.PriceStandId;
            //        if (model.PriceStandName != tmpPriceStandName)
            //        {
            //            tmpPriceStandName = model.PriceStandName;
            //            strPriceStandName += string.Format("{0}<br/>", model.PriceStandName);
            //        }
            //        if (model.CustomerLevelBasicType == TourUnion.Model.LocalAgency.CustomerLevelBasicType.门市 && model.PriceStandId == tmpPriceStandID)
            //        {
            //            strMarketPrice += string.Format("<span class='price2'>￥{0}</span>/<span class='price3'>{1}</span>", model.AdultPrice.ToString("F0"), model.ChildrenPrice.ToString("F0"));

            //        }
            //        else if (model.CustomerLevelBasicType == TourUnion.Model.LocalAgency.CustomerLevelBasicType.同行 && model.PriceStandId == tmpPriceStandID)
            //        {
            //            strPrice += string.Format("<span class='price2'>￥{0}</span>/<span class='price3'>{1}</span>", model.AdultPrice.ToString("F0"), model.ChildrenPrice.ToString("F0"));
            //        }
            //        else if (model.CustomerLevelBasicType == TourUnion.Model.LocalAgency.CustomerLevelBasicType.单房差 && model.PriceStandId == tmpPriceStandID)
            //        {
            //            strRoomPrice += string.Format("/<span class='price3'>{0}</span><br/>", model.ChildrenPrice.ToString("F0"));
            //            strRoomPrice += string.Format("/<span class='price3'>{0}</span><br/>", model.AdultPrice.ToString("F0"));
            //        }

            //    }
            //}
            //strPriceStandName += "</td>";
            //strMarketPrice += "</td>";
            //strPrice += "</td>";
            return strPriceStandName + strMarketPrice + strPrice + strRoomPrice;
        }
        #endregion
    }
}
