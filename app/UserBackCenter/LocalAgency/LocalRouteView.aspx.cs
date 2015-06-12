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

namespace UserBackCenter.LocalAgency
{
    /// <summary>
    /// 地接线路列表
    /// 罗丽娥   2010-07-23
    /// </summary>
    public partial class LocalRouteView : EyouSoft.Common.Control.BackPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        private int intPageSize = 10, CurrencyPage = 1;
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected bool IsBrowseGrant = false, IsAddGrant = false, IsUpdateGrant = false, IsDeleteGrant = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUserInfo != null)
            {
                UserInfoModel = SiteUserInfo;
            }
            if (!Page.IsPostBack)
            {
                InitRouteList();
            }
            this.Page.Title = "地接线路库";

            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.地接_线路管理))
            {
                Utils.ResponseNoPermit("对不起，您没有地接_管理栏目权限!");
                return;
            }
            //if (this.CheckGrant(EyouSoft.Common.TravelPermission.地接_新增线路))
            //{
            IsAddGrant = true;
            //}
            //if (this.CheckGrant(EyouSoft.Common.TravelPermission.地接_维护线路_修改))
            //{
            IsUpdateGrant = true;
            //}
            //if (this.CheckGrant(EyouSoft.Common.TravelPermission.地接_维护线路_删除))
            //{
            IsDeleteGrant = true;
            //}

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]) && Request.QueryString["flag"] == "del")
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("del", StringComparison.OrdinalIgnoreCase))
                {
                    string RouteIdList = Server.UrlDecode(Utils.InputText(Request.QueryString["RouteIdList"]));
                    Response.Clear();
                    Response.Write(DeleteData(RouteIdList));
                    Response.End();
                }
            }
        }

        #region 初始化线路列表
        /// <summary>
        /// 初始化线路列表
        /// </summary>
        private void InitRouteList()
        {
            int intRecordCount = 0;
            int RouteDays = 0;
            int[] AreaId = UserInfoModel.AreaId;
            string RouteName = string.Empty, ContactName = string.Empty;
            RouteName = Server.UrlDecode(Utils.InputText(Request.QueryString["RouteName"]));
            RouteDays = Utils.GetInt(Utils.InputText(Request.QueryString["TourDays"]), 0);
            ContactName = Server.UrlDecode(Utils.InputText(Request.QueryString["ContactName"]));
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            IList<EyouSoft.Model.TourStructure.RouteBasicInfo> list = null;
            // 所有的线路列表
            list = bll.GetLocaRoutes(intPageSize, CurrencyPage, ref intRecordCount, UserInfoModel.CompanyID, UserInfoModel.ID, RouteName, RouteDays, ContactName, null, null);
            if (list != null && list.Count > 0)
            {
                this.rptLocalRouteView.DataSource = list;
                this.rptLocalRouteView.DataBind();
            }
            else
            {
                this.pnlNodata.Visible = true;
            }
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = CurrencyPage;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";

            this.LocalRouteView_RouteName.Value = RouteName;
            if (RouteDays != 0)
                this.LocalRouteView_TourDays.Value = RouteDays.ToString();
            this.LocalRouteView_ContactName.Value = ContactName;

            list = null;
            bll = null;
        }
        #endregion

        #region 获取线路报价标准
        protected void rptLocalRouteView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EyouSoft.Model.TourStructure.RouteBasicInfo RouteModel = (EyouSoft.Model.TourStructure.RouteBasicInfo)e.Item.DataItem;
                if (RouteModel != null)
                {
                    IList<EyouSoft.Model.TourStructure.RoutePriceDetail> list = RouteModel.PriceDetails;
                    string strPriceStandName = string.Empty;
                    string strMarketPrice = string.Empty;
                    string strPrice = string.Empty;
                    if (list != null && list.Count > 0)
                    {
                        strPriceStandName = "<td class=\"typeprice\" align=\"center\">";
                        strMarketPrice = "<td align=\"center\">";
                        strPrice = "<td align=\"center\">";
                        foreach (EyouSoft.Model.TourStructure.RoutePriceDetail priceModel in list)
                        {
                            strPriceStandName += string.Format("{0}：<br/>", priceModel.PriceStandName);
                            IList<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail> detailList = priceModel.PriceDetail;
                            if (detailList != null && detailList.Count > 0)
                            {
                                foreach (EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail detailModel in detailList)
                                {
                                    if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.门市)
                                    {
                                        strMarketPrice += string.Format("<span class=\"price2\">￥{0}</span>/<span class=\"price3\">{1}</span>", detailModel.AdultPrice.ToString("F0"), detailModel.ChildrenPrice.ToString("F0"));
                                    }
                                    else if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.同行)
                                    {
                                        strPrice += string.Format("<span class=\"price2\">￥{0}</span>/<span class=\"price3\">{1}</span>", detailModel.AdultPrice.ToString("F0"), detailModel.ChildrenPrice.ToString("F0"));
                                    }
                                    else if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差)
                                    {
                                        strMarketPrice += string.Format("/<span class=\"price3\">{0}</span>元<br />", detailModel.ChildrenPrice.ToString("F0"));
                                        strPrice += string.Format("/<span class=\"price3\">{0}</span>元<br />", detailModel.AdultPrice.ToString("F0"));
                                    }
                                }
                            }
                            detailList = null;
                        }
                        strPriceStandName += "</td>";
                        strMarketPrice += "</td>";
                        strPrice += "</td>";
                    }
                    list = null;

                    System.Web.UI.WebControls.Literal ltrPriceInfo = (System.Web.UI.WebControls.Literal)e.Item.FindControl("ltrPriceInfo");
                    ltrPriceInfo.Text = strPriceStandName + strMarketPrice + strPrice;
                }
            }
        }
        #endregion

        #region 删除线路信息
        /// <summary>
        /// 删除线路信息
        /// </summary>
        /// <param name="RouteIdList"></param>
        private bool DeleteData(string RouteIdList)
        {
            if (!String.IsNullOrEmpty(RouteIdList))
            {
                string[] arrRoute = RouteIdList.Split(',');
                EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
                return bll.DeleteRoutes(arrRoute);
            }
            else
                return false;
        }
        #endregion
    }
}
