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
    /// 外发的订单：最近交易的订单
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class OrdersOutSource : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        private int intPageSize = 15, intPageIndex = 1;
        protected string RouteName = string.Empty;
        protected DateTime? BeginDate = null;
        protected DateTime? EndDate = null;
        //下单日期
        private DateTime? OrderBeginDate = null;
        private DateTime? OrderEndDate = null;

        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;
        protected string ShowOrderBeginDate = null;
        protected string ShowOrderEndDate = null;
        protected string ContionType = string.Empty;
        /// <summary>
        /// 组团社公司ID
        /// </summary>
        private string BuyCompanyId = string.Empty;
        /// <summary>
        /// 专线商公司ID
        /// </summary>
        private string SellCompanyId = string.Empty;
        protected bool isGrant = false;
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

            //设置高亮选项卡
            this.OrderOutSourceTab1.TabIndex = 1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            BindRouteCompany();
            int intRecordCount = 0;
            EyouSoft.Model.TourStructure.OrderState? State = null;
            #region 初始化查询条件            
            if (!string.IsNullOrEmpty(Request.QueryString["ContionType"]))
            {
                ContionType = Request.QueryString["ContionType"].ToLower();
                switch (ContionType)
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
                    default:
                        State = null;
                        break;
                }
            }
            //筛选时间：下单日期
            if (Request.QueryString["QueryTime"] != null && !string.IsNullOrEmpty(Request.QueryString["QueryTime"]))
            {
                int days = Utils.GetInt(Request.QueryString["QueryTime"], 0);
                if (days < 1)
                {
                    OrderBeginDate = null;
                    OrderEndDate = null;
                }
                else
                {
                    OrderBeginDate = DateTime.Now.AddDays(-days);
                    OrderEndDate = DateTime.Now;                    
                }
                dplselect_Time.SelectedValue = Utils.GetInt(Request.QueryString["QueryTime"], 30).ToString();
            }
            else
            {
                OrderBeginDate = DateTime.Now.AddDays(-30);
                OrderEndDate = DateTime.Now;
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
            if (Request.QueryString["OrderBeginDate"] != null)
            {
                OrderBeginDate = Utils.GetDateTimeNullable(Request.QueryString["OrderBeginDate"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["OrderBeginDate"]))
            {
                ShowOrderBeginDate = Utils.GetDateTime(Request.QueryString["OrderBeginDate"]).ToShortDateString();
            }
            if (Request.QueryString["OrderEndDate"] != null)
            {
                OrderEndDate = Utils.GetDateTimeNullable(Request.QueryString["OrderEndDate"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["OrderEndDate"]))
            {
                ShowOrderEndDate = Utils.GetDateTime(Request.QueryString["OrderEndDate"]).ToShortDateString();
            }
            RouteName = Utils.GetQueryStringValue("RouteName");
            RouteName = Server.UrlDecode(RouteName).Trim();
            //专线供应商
            SellCompanyId = Utils.GetQueryStringValue("RouteCompany");
            dplRouteCompany.SelectedValue = SellCompanyId;
            #endregion
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourOrder> TourOrderlists = new List<EyouSoft.Model.TourStructure.TourOrder>();
            //最近交易订单
            TourOrderlists = Ibll.GetOrderList(intPageSize, intPageIndex, ref intRecordCount, 1, SellCompanyId, BuyCompanyId, RouteName, 0, BeginDate, EndDate, State, OrderBeginDate, OrderEndDate);
            rpt_OrdersOutSource.DataSource = TourOrderlists;
            rpt_OrdersOutSource.DataBind();
            //是否有订单信息
            if (rpt_OrdersOutSource.Items.Count < 1)
            {
                this.NoData.Visible = true;
            }
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            Ibll = null;
            TourOrderlists = null;
        }
        /// <summary>
        /// 全部 | 已成交订单 | 未处理订单 | 已留位订单 
        /// </summary>
        protected string ChangeCss(string ContionType)
        {
            string Result = string.Empty;
            if (Request["ContionType"] != null && Request["ContionType"] == ContionType)
            {
                Result = "ff0000";
            }
            else
            {
                if (Request["ContionType"] == null && ContionType == "all" && string.IsNullOrEmpty(Result))
                    Result = "ff0000";
            }
            return Result;
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
    }
}
