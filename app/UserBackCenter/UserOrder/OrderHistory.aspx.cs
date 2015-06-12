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
using EyouSoft.Security.Membership;
namespace UserBackCenter.UserOrder
{
    /// <summary>
    /// 历史团队
    /// 创建者：luofx 时间：2010-6-28
    /// </summary>
    public partial class OrderHistory : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        private int intPageSize = 3;//默认显示3个组团
        protected int intPageIndex = 1;
        protected int? TourDays = null;              //出团天数
        protected string TourCode = string.Empty;//团号
        protected string RouteName = string.Empty;//线路名称
        protected DateTime? BeginDate = null;     //出团日期开始
        protected DateTime? EndDate = null;       //出团日期结束

        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;
        /// <summary>
        /// 公司ID
        /// </summary>
        private string CompanyId = string.Empty;
        /// <summary>
        /// 用户ID
        /// </summary>
        private string UserId = string.Empty;
        /// <summary>
        /// 历史团队的团队信息集合
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> HavingOrderLists = null;
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
            isGrant = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.订单_收到的订单);
                CompanyId = this.SiteUserInfo.CompanyID;
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
            Utils.GetCompanyChildAccount(dplUserList, CompanyId);
            int intRecordCount = 0;
            #region 查询条件
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            if (Request.QueryString["UserID"] != null && !string.IsNullOrEmpty(Request.QueryString["UserID"]))
            {
                UserId = Utils.GetQueryStringValue("UserID");// Request.QueryString["UserID"];
            }
            else
            {
                UserId = null;
            }
            dplUserList.SelectedValue = UserId;
            TourDays = Utils.GetIntNull(Request.QueryString["TourDays"]);
            TourCode = Utils.GetQueryStringValue("TourCode");
            RouteName = Utils.GetQueryStringValue("RouteName"); 
            RouteName = Server.UrlDecode(RouteName).Trim();
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
            #endregion
            EyouSoft.IBLL.TourStructure.ITour Tourbll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            HavingOrderLists = new List<EyouSoft.Model.TourStructure.HavingOrderTourInfo>();
            //获取历史订单的团队信息集合 
            HavingOrderLists = Tourbll.GetHistorOrderTours(intPageSize, intPageIndex, ref intRecordCount, CompanyId, UserId, TourCode, RouteName, TourDays, BeginDate, EndDate);
            this.rpt_OrderHistory.DataSource = HavingOrderLists;
            this.rpt_OrderHistory.DataBind();
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            Tourbll = null;
            HavingOrderLists = null;
            if (rpt_OrderHistory.Items.Count <= 0)
            {
                this.NoData.Visible = true;
            }
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        protected string GetSateName(object ItemState, object savedate)
        {
            string Result = string.Empty;
            switch (ItemState.ToString())
            {
                case "0"://未处理页面：处理订单
                    Result = "处理订单";
                    break;
                case "1"://处理中
                    Result = "处理中";
                    break;
                case "2"://已留位
                    Result = "已留位到" + Utils.GetDateTime(savedate.ToString()).ToString("yyyy-MM-dd HH:mm");
                    break; ;
                case "3"://留位过期
                    Result = "留位过期";
                    break; ;
                case "4"://不受理
                    Result = "不受理";
                    break; ;
                case "5"://确认成交
                    Result = "确认成交";
                    break;
                default:
                    Result = "处理订单";
                    break;
            }
            //if (Utils.GetDateTime(savedate.ToString()) < DateTime.Now)
            //{
            //    Result = "留位过期";
            //}
            return Result;
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_OrderHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //强制将当前数据行转化为拥有订单的团队信息业务实体
                EyouSoft.Model.TourStructure.HavingOrderTourInfo HavingModel = (EyouSoft.Model.TourStructure.HavingOrderTourInfo)e.Item.DataItem;
                //查找嵌套Repeater
                Repeater rptChild = new Repeater();
                rptChild = (Repeater)e.Item.FindControl("rpt_OrderHistoryChild");
                if (rptChild != null)
                {
                    Binding_OrderHistoryChild(rptChild, HavingModel);
                }
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(intPageSize * (intPageIndex - 1) + (e.Item.ItemIndex + 1));
                HavingModel = null;
            }
        }
        /// <summary>
        /// 绑定子Repeater数据
        /// </summary>
        /// <param name="rptChild">Repeater</param>
        /// <param name="HavingModel">拥有订单的团队信息业务实体</param>
        private void Binding_OrderHistoryChild(Repeater rptChild, EyouSoft.Model.TourStructure.HavingOrderTourInfo HavingModel)
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourOrder> TourOrderlists = new List<EyouSoft.Model.TourStructure.TourOrder>();
            TourOrderlists = Ibll.GetOrderList(CompanyId, UserId, HavingModel.ID, "", null, null, null, null);
            //绑定数据
            rptChild.DataSource = TourOrderlists;
            rptChild.DataBind();
            HavingModel = null;
            TourOrderlists = null;
            Ibll = null;
        }
    }
}
