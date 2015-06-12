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
    /// 已处理订单
    /// 创建者：luofx 时间：2010-6-28
    /// </summary>
    public partial class OrderProcessed : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        private int intPageSize = 5;//默认显示5个组团
        protected int intPageIndex = 1;
        /// <summary>
        /// 公司ID
        /// </summary>
        private string CompanyId = string.Empty;
        /// <summary>
        /// 用户ID
        /// </summary>
        private string UserId = string.Empty;
        /// <summary>
        /// 未处理订单的团队信息集合
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> HavingOrderLists = null;

        private EyouSoft.Model.TourStructure.OrderState? State = null;
        protected bool isGrant = false;
        protected string OrderState = string.Empty;
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
            if (!string.IsNullOrEmpty(Request.QueryString["OrderState"]))
            {
                State = (EyouSoft.Model.TourStructure.OrderState)Enum.Parse(typeof(EyouSoft.Model.TourStructure.OrderState), Request.QueryString["OrderState"]);
                OrderState = Utils.GetQueryStringValue("OrderState").Trim();
            }

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
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            UserId = Request.QueryString["UserID"];
            dplUserList.SelectedValue = UserId;
            //switch (OrderState)
            //{
            //    case "0":
            //        State = EyouSoft.Model.TourStructure.OrderState.已成交;
            //        break;
            //    case "1":
            //        State = EyouSoft.Model.TourStructure.OrderState.已留位;
            //        break;
            //    case "2":
            //        State = EyouSoft.Model.TourStructure.OrderState.留位过期;
            //        break;
            //    case "3":
            //        State = EyouSoft.Model.TourStructure.OrderState.处理中;
            //        break;
            //    case "4":
            //        State = EyouSoft.Model.TourStructure.OrderState.不受理;
            //        break;
            //    default:
            //        State = null;
            //        break;
            //}
            EyouSoft.IBLL.TourStructure.ITour Tourbll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            HavingOrderLists = new List<EyouSoft.Model.TourStructure.HavingOrderTourInfo>();
            //获取已处理订单的团队信息集合 
            HavingOrderLists = Tourbll.GetHandledOrderTours(intPageSize, intPageIndex, ref intRecordCount, CompanyId, UserId, State);
            this.rpt_OrderProcessed.DataSource = HavingOrderLists;
            this.rpt_OrderProcessed.DataBind();
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            Tourbll = null;
            HavingOrderLists = null;
            if (rpt_OrderProcessed.Items.Count <= 0)
            {
                this.NoData.Visible = true;
            }
        }
        /// <summary>
        /// 获取处理button的问题信息
        /// </summary>
        /// <param name="ItemState">订单状态</param>
        /// <param name="savedate">留位日期</param>
        /// <returns></returns>
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
            //if (Utils.GetDateTime(savedate.ToString()) < DateTime.Now) {
            //    Result = "留位过期";
            //}
            return Result;
        }
        /// <summary>
        /// 更改button的样式
        /// </summary>
        /// <param name="cssType"></param>
        /// <returns></returns>
        protected string ChangeCss(string cssType)
        {
            string Result = string.Empty;
            if (OrderState == cssType.Trim())
            {
                Result = "style=\"color:Red;\"";
            }
            return Result;
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_OrderProcessed_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //强制将当前数据行转化为拥有订单的团队信息业务实体
                EyouSoft.Model.TourStructure.HavingOrderTourInfo HavingModel = (EyouSoft.Model.TourStructure.HavingOrderTourInfo)e.Item.DataItem;
                //查找嵌套Repeater
                Repeater rptChild = new Repeater();
                rptChild = (Repeater)e.Item.FindControl("rpt_OrderProcessedChild");
                if (rptChild != null)
                    Binding_OrderProcessedChild(rptChild, HavingModel);
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
        private void Binding_OrderProcessedChild(Repeater rptChild, EyouSoft.Model.TourStructure.HavingOrderTourInfo HavingModel)
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourOrder> TourOrderlists = new List<EyouSoft.Model.TourStructure.TourOrder>();
            EyouSoft.Model.TourStructure.OrderState[] orderstate = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.不受理, EyouSoft.Model.TourStructure.OrderState.已成交, EyouSoft.Model.TourStructure.OrderState.已留位, EyouSoft.Model.TourStructure.OrderState.留位过期, EyouSoft.Model.TourStructure.OrderState.处理中 };
            if (State != null)
            {

                EyouSoft.Model.TourStructure.OrderState[] orderstate2 = new EyouSoft.Model.TourStructure.OrderState[] { (EyouSoft.Model.TourStructure.OrderState)State };
                TourOrderlists = Ibll.GetOrderList(CompanyId, UserId, HavingModel.ID, "", null, null, null, orderstate2);
            }
            else
            {
                TourOrderlists = Ibll.GetOrderList(CompanyId, UserId, HavingModel.ID, "", null, null, null, orderstate);
            }
            //绑定数据
            rptChild.DataSource = TourOrderlists;
            rptChild.DataBind();
            HavingModel = null;
            TourOrderlists = null;
            Ibll = null;
        }
    }
}
