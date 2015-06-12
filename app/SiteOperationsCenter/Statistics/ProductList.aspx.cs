using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 产品数列表
    /// 罗丽娥   2010-09-19
    /// </summary>
    public partial class ProductList :EyouSoft.Common.Control.YunYingPage
    {
        protected string CompanyID = string.Empty;
        private int intPageSize = 20, CurrencyPage = 1, count = 0;
        protected int AreaID = 0;
        protected DateTime? StartTime = null, EndTime = null;
        protected int OldSortId = -1, Order_0 = 0, Order_1 = 0, Order_2 = 0, Order_3 = 0, Order_4 = 0;
        protected string SortID = string.Empty;

        private EyouSoft.Model.TourStructure.TourDisplayType disType = EyouSoft.Model.TourStructure.TourDisplayType.出团日期;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_专线商行为分析))
            {
                Utils.ResponseNoPermit("对不起，您没有统计分析_专线商行为分析权限!");
                return;
            }
            CompanyID = Request.QueryString["companyId"];
            AreaID = Utils.GetInt(Utils.GetQueryStringValue("areaId"));
            
            if (!String.IsNullOrEmpty(Utils.GetQueryStringValue("startTime")))
            {
                StartTime = DateTime.Parse(Utils.GetQueryStringValue("startTime"));
            }            
            if (!String.IsNullOrEmpty(Utils.GetQueryStringValue("endTime")))
            {
                EndTime = DateTime.Parse(Utils.GetQueryStringValue("endTime"));
            }

            if (AreaID != 0)
            {
                this.spanArea.Visible = false;
            }

            int tmpDistype = Utils.GetInt(Utils.GetQueryStringValue("DisType"), 1);
            if (tmpDistype == 0)
            {
                disType = EyouSoft.Model.TourStructure.TourDisplayType.线路产品;
            }

            if (!Page.IsPostBack)
            {
                if (disType == EyouSoft.Model.TourStructure.TourDisplayType.线路产品)
                {
                    this.drpOrderByRouteOrDate.Items.FindByValue("0").Selected = true;
                }
                else {
                    this.drpOrderByRouteOrDate.Items.FindByValue("1").Selected = true;
                }

                InitAreaList();

                InitTourStatByOrderInfo();
            }
            this.drpOrderByRouteOrDate.Attributes.Add("onchange", "ProductList.GetList(this.options[this.selectedIndex].value);");

        }

        #region 绑定公司的线路区域
        /// <summary>
        /// 绑定公司的线路区域
        /// </summary>
        private void InitAreaList()
        {
            IList<EyouSoft.Model.SystemStructure.AreaBase> list = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(CompanyID);
            this.pl_selRouteArea.Items.Add(new ListItem("请选择", "0"));
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.AreaBase model in list)
                {
                    this.pl_selRouteArea.Items.Add(new ListItem(model.AreaName, model.AreaId.ToString()));
                }
            }
            list = null;
        }
        #endregion

        #region 初始化产品信息
        /// <summary>
        /// 初始化产品信息
        /// </summary>
        private void InitTourStatByOrderInfo()
        {
            int intRecordCount = 0,intOrderIndex = -1;

            string RouteName = Utils.GetQueryStringValue("RouteName");
            int strAreaID = Utils.GetInt(Utils.GetQueryStringValue("strareaId"),0);
            if (strAreaID == 0 && AreaID != 0)
            {                
                strAreaID = AreaID;
            }
            this.pl_txtRouteName.Value = RouteName;
            this.pl_selRouteArea.Items.FindByValue(strAreaID.ToString()).Selected = true;

            #region 根据订单类型排序
            SortID = Utils.GetQueryStringValue("SortID");
            string strSortID = Utils.GetQueryStringValue("OldSortID");
            if(!String.IsNullOrEmpty(SortID))
            {
                switch (SortID)
                { 
                    case "0":    // 成交订单
                        if (strSortID == "1" || strSortID == "-1")
                        {
                            intOrderIndex = 0;
                            OldSortId = 0;
                            Order_0 = 0;
                        }
                        else {
                            intOrderIndex = 1;
                            OldSortId = 1;
                            Order_0 = 1;
                        }
                        break;
                    case "1":    // 留位订单
                        if (strSortID == "2" || strSortID == "-1")
                        {
                            intOrderIndex = 3;
                            OldSortId = 3;
                            Order_1 = 3;
                        }
                        else {
                            intOrderIndex = 2;
                            OldSortId = 2;
                            Order_1 = 2;
                        }
                        break;
                    case "2":   //  留位过期
                        if (strSortID == "4" || strSortID == "-1")
                        {
                            intOrderIndex = 5;
                            OldSortId = 5;
                            Order_2 = 5;
                        }
                        else {
                            intOrderIndex = 4;
                            OldSortId = 4;
                            Order_2 = 4;
                        }
                        break;
                    case "3":   // 不受理订单
                        if (strSortID == "6" || strSortID == "-1")
                        {
                            intOrderIndex = 7;
                            OldSortId = 7;
                            Order_3 = 7;
                        }
                        else {
                            intOrderIndex = 6;
                            OldSortId = 6;
                            Order_3 = 6;
                        }
                        break;
                    case "4":   // 被查看次数
                        if (strSortID == "8" || strSortID == "-1")
                        {
                            intOrderIndex = 9;
                            OldSortId = 9;
                            Order_4 = 9;
                        }
                        else {
                            intOrderIndex = 8;
                            OldSortId = 8;
                            Order_4 = 8;
                        }
                        break;
                    default:
                        intOrderIndex = 0;
                        OldSortId = 0;
                        break;
                }
            }
            #endregion

            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"),1);
            IList<EyouSoft.Model.TourStructure.TourStatByOrderInfo> list = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetTourStatByOrderInfo(intPageSize, CurrencyPage, ref intRecordCount, intOrderIndex, disType, CompanyID, strAreaID, RouteName, StartTime, EndTime);
            if(intRecordCount > 0)
            {
                this.tr_NoData.Visible = false;

                this.rptTourStatByOrderInfo.DataSource = list;
                this.rptTourStatByOrderInfo.DataBind();

                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
            }else{
                this.tr_NoData.Visible = true;
            }
            list = null;
        }
        #endregion

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (CurrencyPage - 1) * intPageSize;
        }

        protected string GetRouteName(string TourID, string RouteName, string TourNo)
        {
            string tmp = string.Empty;
            if (!String.IsNullOrEmpty(TourNo))
            {
                if (disType == EyouSoft.Model.TourStructure.TourDisplayType.线路产品)
                {
                    tmp += string.Format("<a href='{0}/PrintPage/TeamInformationPrintPage.aspx?TourId={1}' title='{2}' target='_blank'>{2}<a>",Domain.UserBackCenter, TourID, RouteName);                   
                }
                else
                {
                    tmp += string.Format("【{0}】<a href='{3}/PrintPage/TeamInformationPrintPage.aspx?TourId={1}' title='{2}' target='_blank'>{2}<a>", TourNo, TourID, RouteName,Domain.UserBackCenter);
                }
            }else{
                tmp += string.Format("<a href='{0}/PrintPage/TeamInformationPrintPage.aspx?TourId={1}' title='{2}' target='_blank'>{2}<a>",Domain.UserBackCenter, TourID, RouteName);
            }
            return tmp;
        }

        #region 查询
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string RouteName = Utils.GetFormValue("pl_txtRouteName");
            DateTime? StartTime = null;
            if (!String.IsNullOrEmpty(Utils.GetQueryStringValue("startTime")))
            {
                StartTime = DateTime.Parse(Utils.GetQueryStringValue("startTime"));
            }
            DateTime? EndTime = null;
            if (!String.IsNullOrEmpty(Utils.GetQueryStringValue("endTime")))
            {
                EndTime = DateTime.Parse(Utils.GetQueryStringValue("endTime"));
            }
            int strAreaID = 0;
            if (!String.IsNullOrEmpty(Utils.GetFormValue("pl_selRouteArea ")))
            {
                strAreaID = Utils.GetInt(Utils.GetFormValue("pl_selRouteArea "));
            }

            string url = string.Format("{0}?CompanyId={1}&startTime={2}&endTime={3}&RouteName={4}&strareaId={5}&areaId={6}", Request.ServerVariables["SCRIPT_NAME"], CompanyID, StartTime, EndTime, Server.UrlEncode(RouteName), strAreaID, AreaID);
            Response.Redirect(url);
        }
        #endregion
    }
}
