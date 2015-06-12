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
    /// 外发的订单：所有订单
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class OrdersAllOutSource : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        private int intPageSize = 15, intPageIndex = 1;
        private string BuyCompanyId = string.Empty;//组团社公司ID
        private string SellCompanyId = null;//专线商公司ID
        protected string RouteName = string.Empty;
        protected int AreaId = 0;
        protected DateTime? BeginDate = null;
        protected DateTime? EndDate = null;
        protected DateTime? OrderBeginDate = null;
        protected DateTime? OrderEndDate = null;

        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;
        protected string ShowOrderBeginDate = null;
        protected string ShowOrderEndDate = null;
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
            this.OrderOutSourceTab1.TabIndex = 0;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {            
            BindRouteCompany();
            int intRecordCount = 0;
            #region 初始化查询条件
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
            RouteName = Utils.GetQueryStringValue("RouteName");
            RouteName = Server.UrlDecode(RouteName).Trim();
            AreaId = Utils.GetInt(Request.QueryString["AreaId"],-1);
            if (AreaId > 0)
            {
                dplRouteInfo.SelectedValue = AreaId.ToString();
            }

            //专线供应商
            SellCompanyId = Utils.GetQueryStringValue("RouteCompany");
            dplRouteCompany.SelectedValue = SellCompanyId;
            #endregion
            EyouSoft.IBLL.TourStructure.ITourOrder Orderbll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            //某个组团社的订单数统计 
            IList<EyouSoft.Model.TourStructure.AreaAndOrderNum> lists = new List<EyouSoft.Model.TourStructure.AreaAndOrderNum>();
            lists = Orderbll.GetAreaAndOrderNum(BuyCompanyId, SellCompanyId, RouteName, 0, BeginDate, EndDate, OrderBeginDate, OrderEndDate);
            BindRouteInfo(lists);
            List<EyouSoft.Model.TourStructure.AreaAndOrderNum> BindList = new List<EyouSoft.Model.TourStructure.AreaAndOrderNum>();
            if (AreaId > 0)
            {
                BindList = ((List<EyouSoft.Model.TourStructure.AreaAndOrderNum>)lists).FindAll(item => {
                    return item.AreaId == AreaId;
                });
            }
            else
            {
                BindList = (List<EyouSoft.Model.TourStructure.AreaAndOrderNum>)lists;
            }
            rpt_OrdersAllOutSource.DataSource = BindList;
            rpt_OrdersAllOutSource.DataBind();
            intRecordCount = BindList.Count;
            //是否有订单统计信息
            if (rpt_OrdersAllOutSource.Items.Count < 1)
            {
                this.NoData.Visible = true;
            }
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            lists = null;
            Orderbll = null;
        }
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindRouteInfo( IList<EyouSoft.Model.TourStructure.AreaAndOrderNum> lists)
        {
            dplRouteInfo.DataTextField = "AreaName";
            dplRouteInfo.DataValueField = "AreaId";
            dplRouteInfo.DataSource = lists;
            dplRouteInfo.DataBind();
            dplRouteInfo.Items.Insert(0, new ListItem("所有线路", ""));

            //EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            //dplRouteInfo.DataValueField = "AreaId";
            //dplRouteInfo.DataTextField = "AreaName";
            //dplRouteInfo.DataSource = Ibll.GetAttentionTourByAreaStats(BuyCompanyId, null);
            //dplRouteInfo.DataBind();
            //dplRouteInfo.Items.Insert(0, new ListItem("所有线路", ""));
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
