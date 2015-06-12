using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace IMFrame.RouteAgency.TourOrder
{
    /// <summary>
    /// MQ查看(专线)团队订单列表页
    /// 开发人：刘玉灵   时间：2010-8-16
    /// </summary>
    public partial class IMOrderList : EyouSoft.ControlCommon.Control.MQPage
    {
        /// <summary>
        /// 是否有权限操作订单
        /// </summary>
        protected bool isGrant = false;
        /// <summary>
        /// 团队ID
        /// </summary>
        protected string TourId = "";
        /// <summary>
        /// URL订单状态 0:未处理  1:己处理(所有) 2:己成交 3：己留位  4：留位过期  5:不受理
        /// </summary>
        protected int intOrderState = 0;

        protected EyouSoft.Model.TourStructure.OrderState[] OrderState;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.
                //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.订单_管理栏目))
                //{
                //    Response.Write("对不起，你正在使用的帐号没有操作该页面的权限！");
                //    Response.End();
                //    return;
                //}
                isGrant = true;//this.CheckGrant(EyouSoft.Common.TravelPermission.订单_收到的订单);
                this.GetTourInfo();
            }
            Utils.GetCrossDomainScript();

        }
        /// <summary>
        /// 获的团队及订单统计信息 
        /// </summary>
        protected void GetTourInfo()
        {
            TourId = Utils.InputText(Request.QueryString["TourId"]);
            intOrderState = Utils.GetInt(Request.QueryString["intState"]);
            switch (intOrderState)
            {
                case 1:
                    OrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.已成交, EyouSoft.Model.TourStructure.OrderState.已留位, EyouSoft.Model.TourStructure.OrderState.留位过期, EyouSoft.Model.TourStructure.OrderState.不受理, EyouSoft.Model.TourStructure.OrderState.处理中 };
                    break;
                case 2:
                    OrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.已成交 };
                    break;
                case 3:
                    OrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.已留位 };
                    break;
                case 4:
                    OrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.留位过期 };
                    break;
                case 5:
                    OrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.不受理 };
                    break;
                default:
                    OrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.未处理, EyouSoft.Model.TourStructure.OrderState.处理中 };
                    break;
            }

            EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo TourModel = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetMQRemindTourInfo(SiteUserInfo.CompanyID, TourId, OrderState);
            if (TourModel != null)
            {
                this.labTourNo.Text = TourModel.TourNo;
                this.labRouteName.Text = TourModel.RouteName.ToString();
                this.labLeaveDate.Text = "【"+TourModel.LeaveDate.ToShortDateString() + "出团】";
                this.labBuyCompanyNumber.Text = TourModel.BuyCompanyNumber.ToString();
                this.labBuySumNumber.Text = TourModel.BuySeatNumber.ToString();
                this.BindOrderList();
            }
            else
            {
                Response.Write("对不起，未找到该团队的订单信息！");
                Response.End();
                return;
            }
            TourModel = null;
        }


        /// <summary>
        /// 获的团队下的订单详细信息
        /// </summary>
        private void BindOrderList()
        {
            //IList<EyouSoft.Model.TourStructure.TourOrder> OrderList = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderList(SiteUserInfo.CompanyID, "", TourId, "", "", null, null, OrderState);
            int refCount =0;
            EyouSoft.Model.NewTourStructure.MTourOrderSearch searchModel = new EyouSoft.Model.NewTourStructure.MTourOrderSearch();
            searchModel.TourId = TourId;
            IList<EyouSoft.Model.NewTourStructure.MTourOrder> oList = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetPublishersAllList(100, 1, ref refCount, SiteUserInfo.CompanyID, searchModel);

            if (oList != null && oList.Count > 0)
            {
                this.repOrderList.DataSource = oList;
                this.repOrderList.DataBind();
            }
            //OrderList = null;
        }


        protected string GetSateName(object ItemState, object savedate)
        {
            string Result = string.Empty;
            switch (ItemState.ToString())
            {
                case "1":
                    Result = "处理中";
                    break;
                case "2":
                    Result = "已留位到" + Utils.GetDateTime(savedate.ToString()).ToShortDateString();
                    break; ;
                case "3":
                    Result = "留位过期";
                    break; ;
                case "4":
                    Result = "不受理";
                    break; ;
                case "5":
                    Result = "确认成交";
                    break;
                default:
                    Result = "处理订单";
                    break;
            }
            return Result;
        }


    }
}
