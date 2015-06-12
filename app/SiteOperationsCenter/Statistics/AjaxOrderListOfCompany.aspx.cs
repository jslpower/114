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

using System.Text.RegularExpressions;
using System.Linq.Expressions;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// ajax关于组团公司的订单
    /// luofx 2010-9-17
    /// </summary>
    public partial class AjaxOrderListOfCompany : EyouSoft.Common.Control.YunYingPage
    {
        #region 变量
        /// <summary>
        /// 排序类型
        /// </summary>
        protected int SortType = 0;
        /// <summary>
        /// 下订单开始时间
        /// </summary>
        protected string BeginTime = string.Empty;
        /// <summary>
        /// 下订单结束时间
        /// </summary>
        protected string EndTime = string.Empty;
        protected int PageIndex = 1;
        private int PageSize = 12;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.统计分析_组团社行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, true);
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            //组团社的公司ID
            string CompanyID = EyouSoft.Common.Utils.GetQueryStringValue("companyid");
            if (!string.IsNullOrEmpty(CompanyID))
            {
                int RoutCompanyCount = 0;
                string RouteCompanyIDs = string.Empty;
                int RecordCount = 0;
                DateTime? StartDate = null, EndDate = null;
                BeginTime = Request.QueryString["BeginTime"];
                EndTime = Request.QueryString["EndTime"];
                SortType = EyouSoft.Common.Utils.GetInt(Request.QueryString["SortType"], 0);
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);
                StartDate = EyouSoft.Common.Utils.GetDateTimeNullable(BeginTime);
                EndDate = EyouSoft.Common.Utils.GetDateTimeNullable(EndTime);
                EyouSoft.Model.TourStructure.OrderState? Sate = null;
                int OrderState = EyouSoft.Common.Utils.GetInt(Request.QueryString["OrderState"]);
                if (OrderState > 0 && OrderState < 6)
                {
                    Sate = (EyouSoft.Model.TourStructure.OrderState)Enum.Parse(typeof(EyouSoft.Model.TourStructure.OrderState), OrderState.ToString());
                }
                //线路名称
                string RouteName = EyouSoft.Common.Utils.GetQueryStringValue("RouteName");
                //排序类型
                int OrderType = EyouSoft.Common.Utils.GetInt(Request.QueryString["OrderType"], 1);
                IList<EyouSoft.Model.TourStructure.TourOrder> lists = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderList(PageSize, PageIndex, ref RecordCount, OrderType, "", CompanyID, RouteName, 0, null, null, Sate, StartDate, EndDate);               
                if (lists != null && lists.Count > 0)
                {
                    #region 排序
                    switch (SortType)
                    {
                        case 1:
                            if (OrderType == 1)
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.RouteName, p.TourNo descending
                                                                       select p;
                            }
                            else
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.RouteName, p.TourNo ascending
                                                                       select p;
                            }
                            break;
                        case 2:
                            if (OrderType == 1)
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.PeopleNumber descending
                                                                       select p;
                            }
                            else
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.PeopleNumber ascending
                                                                       select p;
                            }
                            break;
                        case 3:
                            if (OrderType == 1)
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.OrderState descending
                                                                       select p;
                            }
                            else
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.OrderState ascending
                                                                       select p;
                            }
                            break;
                        case 4:
                            if (OrderType == 1)
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.TourCompanyName descending
                                                                       select p;
                            }
                            else
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.TourCompanyName ascending
                                                                       select p;
                            }
                            break;
                        default:
                            if (OrderType == 1)
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.IssueTime descending
                                                                       select p;
                            }
                            else
                            {
                                this.rpt_CompanyOrderList.DataSource = from p in lists
                                                                       orderby p.IssueTime ascending
                                                                       select p;
                            }
                            break;
                    }
                #endregion
                    this.rpt_CompanyOrderList.DataBind();
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = RecordCount;                    
                    this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "OrderListOfCompany.LoadData(this);", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "OrderListOfCompany.LoadData(this);", 0);                    
                    NoData.Visible = false;
                    ((List<EyouSoft.Model.TourStructure.TourOrder>)lists).ForEach(p =>
                    {
                        if (!RouteCompanyIDs.Contains(p.TourCompanyId))
                        {
                            RouteCompanyIDs += p.TourCompanyId + ",";
                            RoutCompanyCount = RoutCompanyCount + 1;
                        }
                    });
                }
                #region 处理表单头部统计信息
                ltrCount.Text = RecordCount.ToString();
                ltrRouteCompanyNum.Text = RoutCompanyCount.ToString();
                #endregion
                lists = null;
            }
        }
        /// <summary>
        /// 处理排序：标题，排序值，修改样式相关的逻辑
        /// </summary>
        /// <param name="SType">排序类型：0.时间；1.团号/线路名称;..等</param>
        /// <returns></returns>
        protected string GetAboutOrderInfo(int SType)
        {
            string Result = "title=\"{0}\" class=\"{1}\" sorttype=\"{2}\" ordertype=\"{3}\"";
            int OrderType = EyouSoft.Common.Utils.GetInt(Request.QueryString["OrderType"], 1);
            string CTitle = OrderType == 0 ? "从高到低排列" : "从低到高排列";
            string otype = "1";
            string CssClass = string.Empty;
            if (SortType == SType)
            {
                CssClass = OrderType == 1 ? "DataListDownGreen" : "DataListUpGreen";
                otype = OrderType == 1 ? "0" : "1";
            }
            else
            {
                CssClass = "DataListDownGray";
            }
            switch (SType)
            {
                case 1:
                    CTitle = "点击团号/线路名称" + CTitle;
                    Result = string.Format(Result, CTitle, CssClass, "1", otype);
                    break;
                case 2:
                    CTitle = "点击预订人数" + CTitle;
                    Result = string.Format(Result, CTitle, CssClass, "2", otype);
                    break;
                case 3:
                    CTitle = "点击订单状态" + CTitle;
                    Result = string.Format(Result, CTitle, CssClass, "3", otype);
                    break;
                case 4:
                    CTitle = "点击所属批发商" + CTitle;
                    Result = string.Format(Result, CTitle, CssClass, "4", otype);
                    break;
                default:
                    CTitle = "点击时间" + CTitle;
                    Result = string.Format(Result, CTitle, CssClass, "0", otype);
                    break;
            }
            return Result;
        }
        /// <summary>
        /// 绑定数据项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_CompanyOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(PageSize * (PageIndex - 1) + (e.Item.ItemIndex + 1));
            }
        }
    }

}
