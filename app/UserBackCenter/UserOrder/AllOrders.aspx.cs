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

namespace UserBackCenter.UserOrder
{
    /// <summary>
    /// 外发的订单：所有订单-详细订单
    /// 创建者：luofx 时间：2010-7-10
    /// </summary>
    public partial class AllOrders : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        private int intPageSize = 15;
        protected int intPageIndex = 1; 
        private string BuyCompanyId = string.Empty;//组团社公司ID
        private string SellCompanyId = string.Empty;//专线商公司ID
        protected int AreaId = 0;
        protected string RouteName = string.Empty;
        protected DateTime? BeginDate = null;
        protected DateTime? EndDate = null;
        protected DateTime? OrderBeginDate = null;
        protected DateTime? OrderEndDate = null;

        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;
        protected string ShowOrderBeginDate = null;
        protected string ShowOrderEndDate = null;
        protected decimal taxBaseAmount = 0;
        protected bool isGrant = false;

        protected string ContionType = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.订单_管理栏目)) 
            //{
            //    Response.Clear();
            //    Response.Write("对不起，你正在使用的帐号没有操作改页面的权限！");
            //    Response.End();
            //}
            isGrant = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.订单_外发的订单);
            BuyCompanyId = this.SiteUserInfo.CompanyID;
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
            int intRecordCount = 0;
            BindRouteCompany();
            #region 初始化查询条件
            EyouSoft.Model.TourStructure.OrderState? State = null;
            if (Request.QueryString["ContionType"] != null)
            {
                ContionType = Utils.GetQueryStringValue("ContionType");
                switch (ContionType.ToLower())
                {
                    case "transaction"://已成交订单
                        State = EyouSoft.Model.TourStructure.OrderState.已成交;
                        break;
                    case "untreated"://未处理订单
                        State = EyouSoft.Model.TourStructure.OrderState.未处理;
                        break;
                    case "reservation"://已留位订单
                        State = EyouSoft.Model.TourStructure.OrderState.已留位;
                        break;
                    case "notaccepted"://不受理
                        State = EyouSoft.Model.TourStructure.OrderState.不受理;
                        break;
                    case "reservationdue"://留位过期
                        State = EyouSoft.Model.TourStructure.OrderState.留位过期;
                        break;
                    default:
                        State = null;
                        break;
                }
            }

            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            BeginDate = Utils.GetDateTimeNullable(Request.QueryString["BeginDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["BeginDate"]))
            {
                ShowBeginDate = Utils.GetDateTime(Request.QueryString["BeginDate"]).ToShortDateString();
            }
            EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["EndDate"]))
            {
                ShowEndDate = Utils.GetDateTime(Request.QueryString["EndDate"]).ToShortDateString();
            }

            OrderBeginDate = Utils.GetDateTimeNullable(Request.QueryString["OrderBeginDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OrderBeginDate"]))
            {
                ShowOrderBeginDate = Utils.GetDateTime(Request.QueryString["OrderBeginDate"]).ToShortDateString();
            }
            OrderEndDate = Utils.GetDateTimeNullable(Request.QueryString["OrderEndDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OrderEndDate"]))
            {
                ShowOrderEndDate = Utils.GetDateTime(Request.QueryString["OrderEndDate"]).ToShortDateString();
            }
            RouteName = Utils.InputText(Request.QueryString["RouteName"]);
            RouteName = Server.UrlDecode(RouteName).Trim();
            //专线供应商
            if (Request.QueryString["RouteCompany"] != null && !string.IsNullOrEmpty(Request.QueryString["RouteCompany"]))
            {
                SellCompanyId = Utils.InputText(Request.QueryString["RouteCompany"]);
                dplRouteCompany.SelectedValue = SellCompanyId;
            }
            AreaId = Utils.GetInt(Request.QueryString["AreaId"], 0);
            #endregion
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourOrder> TourOrderlists = new List<EyouSoft.Model.TourStructure.TourOrder>();

            TourOrderlists = Ibll.GetOrderList(intPageSize, intPageIndex, ref intRecordCount, 1, SellCompanyId, BuyCompanyId, RouteName,AreaId, BeginDate, EndDate, State, OrderBeginDate, OrderEndDate);
            rptAllOrders.DataSource = TourOrderlists;
            rptAllOrders.DataBind();
            if (rptAllOrders.Items.Count < 1)
            {
                NoData.Visible = true;
            }
            Ibll = null;
            TourOrderlists = null;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 绑定专线公司
        /// </summary>
        private void BindRouteCompany()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyFavor Ibll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();
            dplRouteCompany.DataTextField = "CompanyName";
            dplRouteCompany.DataValueField = "ID";
            dplRouteCompany.DataSource = Ibll.GetListCompany(BuyCompanyId);
            dplRouteCompany.DataBind();
            Ibll = null;
            dplRouteCompany.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 所有订单 | 未处理订单 | 成功交易 | 已留位 | 留位到期 | 未受理 
        /// </summary>
        protected string ChangeCss(string ContionType)
        {
            string Result = string.Empty;
            if (Request["ContionType"] != null && Request["ContionType"] == ContionType)
            {
                Result = "liston";
            }
            else
            {
                if (Request["ContionType"] == null && ContionType == "all" && string.IsNullOrEmpty(Result))
                {
                    Result = "liston";
                }
                else
                {
                    Result = "listun";
                }
            }
            return Result;
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_AllOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EyouSoft.Model.TourStructure.TourOrder model = (EyouSoft.Model.TourStructure.TourOrder)e.Item.DataItem;
                //团队状态，推广状态名
                Literal ltrOrderState = (Literal)e.Item.FindControl("ltrOrderState");
                if (ltrOrderState != null)
                {
                    int state = (int)model.OrderState;
                    switch (state)
                    {
                        case 0:
                            ltrOrderState.Text = "未处理";
                            break;
                        case 1:
                            ltrOrderState.Text = "处理中";
                            break;
                        case 2:
                            ltrOrderState.Text = "已留位";
                            break;
                        case 3:
                            ltrOrderState.Text = "留位过期";
                            break;
                        case 4:
                            ltrOrderState.Text = "不受理";
                            break;
                        default:
                            ltrOrderState.Text = "已成交";
                            break;
                    }
                }
                taxBaseAmount += model.SumPrice;
                model = null;
            }
        }
    }
}
