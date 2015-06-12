using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserBackCenter.Order.RouteAgency
{
    public partial class OrderStateUpdate : BackPage
    {
        /// <summary>
        /// 容器ID
        /// </summary>
        protected string tblID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            this.OrderCustomer1.TxtAudltID = this.txtAdultCount.ClientID;
            this.OrderCustomer1.TxtChildID = this.txtChildCount.ClientID;
            string orderID = Utils.GetQueryStringValue("orderID");
            if (!IsPostBack)
            {
                tblID = Guid.NewGuid().ToString();
                PageInit(orderID);
            }

            if (dotype == "save")
            {
                Response.Clear();
                Response.Write(FormSave());
                Response.End();
            }
            if (dotype == "state")
            {
                Response.Clear();
                Response.Write(UpdateOrderState(orderID, Utils.GetQueryStringValue("state"), Utils.GetQueryStringValue("date")));
                Response.End();
            }
        }

        /// <summary>
        /// 页面初始化方法
        /// </summary>
        /// <param name="tourID"></param>
        /// <param name="adultCount"></param>
        /// <param name="childCount"></param>
        /// <param name="contact"></param>
        /// <param name="tel"></param>
        private void PageInit(string orderID)
        {

            EyouSoft.IBLL.NewTourStructure.ITourOrder orderBll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();

            EyouSoft.Model.NewTourStructure.MTourOrder model = orderBll.GetModel(orderID);

            if (model != null)
            {
                EyouSoft.IBLL.NewTourStructure.IPowderList tourBll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
                EyouSoft.Model.NewTourStructure.MPowderList tourModel = tourBll.GetModel(model.TourId);
                if (tourModel == null)
                {
                    Utils.Show("error");
                    return;
                }
                this.lblRouteName.Text = "<a href='/PrintPage/LineTourInfo.aspx?RouteID=" + model.RouteId + "' target='_blank'>" + model.RouteName + "</a>";
                this.lblCompanyName.Text = " <a  target=\"_blank\" href=\"" + Utils.GetShopUrl(model.Travel) + "\">" + model.TravelName + "</a>";

                #region 获得组团公司信息
                EyouSoft.Model.CompanyStructure.CompanyInfo comModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(model.Travel);
                if (comModel != null && comModel.ContactInfo != null)
                {
                    this.lblMq.Text = Utils.GetMQ(comModel.ContactInfo.MQ);
                    this.lblQQ.Text = Utils.GetQQ(comModel.ContactInfo.QQ);
                }

                #endregion
                this.txtAddPrice.Value = model.Reduction.ToString("0.00");
                this.lblTourState.Text = tourModel.PowderTourStatus.ToString();
                this.lblCount.Text = tourModel.MoreThan.ToString();
                this.txtRouteRemarks.Value = model.BusinessNotes;
                this.lblRemarks.Text = model.TravelNotes;
                this.lblLeaveDate.Text = model.LeaveDate.ToString("yyyy-MM-dd") + "(" + Utils.GetDayOfWeek((int)model.LeaveDate.DayOfWeek) + ") ";//出团日期(周几)
                this.lblLeaveCityandTracffic.Text = tourModel.StartTraffic + " " + tourModel.StartCityName;//出发交通和城市
                this.lbEndCityandTracffic.Text = tourModel.EndTraffic + " " + tourModel.EndCityName;//返程交通和城市
                this.lbLeavedate.Text = tourModel.StartDate;
                this.lbEnddate.Text = tourModel.EndDate;
                this.lblMsg.Text = tourModel.SetDec;
                this.lblAllMsg.Text = tourModel.TeamLeaderDec;
                this.lblContact.Text = model.VisitorContact;
                this.lblConTactTel.Text = model.VisitorTel;
                this.lblFzr.Text = model.TravelContact;
                this.lblFzrTel.Text = model.TravelTel;
                this.txtAdultCount.Value = model.AdultNum.ToString();
                this.txtChildCount.Value = model.ChildrenNum.ToString();
                this.txtOtherCount.Value = model.SingleRoomNum.ToString();
                this.lblAddDate.Text = model.IssueTime.ToString("yyyy-MM-dd") + "&nbsp" + model.OperatorName;
                this.lblCusRemark.Text = model.VisitorNotes;
                this.lblOrder.Text = model.OrderNo;
                #region 团队 价格信息
                //市场价
                this.lblRetailAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.PersonalPrice);
                this.lblRetailChildrenPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.ChildPrice);
                this.lblMarketPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.MarketPrice);

                //结算价
                this.lblSettlementAudltPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SettlementAudltPrice);
                this.lblSettlementChildrenPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SettlementChildrenPrice);
                #endregion

                //处理总金额
                //结算总价
                this.lblSettlePriceAll.Text = Utils.FilterEndOfTheZeroDecimal(model.TotalSettlementPrice);

                this.OrderCustomer1.TourOrderCustomer = model.Customers;

                #region 处理订单状态和支付状态
                this.pnlSave.Visible = false;
                this.pnlYuLiu.Visible = false;
                this.pnlQueDing.Visible = false;
                this.pnlJieDan.Visible = false;
                switch (model.OrderStatus)
                {
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社待处理:
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社已阅:
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商待处理:
                        this.lblOrderState.Text = "未处理";
                        this.pnlSave.Visible = true;
                        this.pnlYuLiu.Visible = true;
                        this.pnlQueDing.Visible = true;

                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商预留:
                        this.lblOrderState.Text = "专线预留,留位日期:" + Convert.ToDateTime(model.SaveDate).ToString("yyyy-MM-dd");
                        this.pnlSave.Visible = true;
                        this.pnlQueDing.Visible = true;

                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.预留过期:
                        this.lblOrderState.Text = "留位过期";
                        this.pnlSave.Visible = true;
                        this.pnlQueDing.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.结单:
                        this.lblOrderState.Text = "已结单";
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.取消:
                        this.lblOrderState.Text = "已取消";
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商已确定:
                        this.lblOrderState.Text = "专线已确定";
                        this.pnlJieDan.Visible = true;
                        break;
                }


                this.pnlLitPay.Visible = false;
                this.pnlAllPay.Visible = false;
                switch (model.PaymentStatus)
                {
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客待支付:
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客未支付:
                        this.lblPayState.Text = "游客未付款";
                        this.pnlLitPay.Visible = true;
                        this.pnlAllPay.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客已支付定金:
                        this.lblPayState.Text = "游客已支付定金";
                        this.pnlLitPay.Visible = true;
                        this.pnlAllPay.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客已支付全款:
                        this.lblPayState.Text = "游客已支付全款";
                        this.pnlLitPay.Visible = true;
                        this.pnlAllPay.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付定金:
                        this.lblPayState.Text = "已收定金";
                        this.pnlAllPay.Visible = true;
                        this.pnlSave.Visible = false;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付全款:
                        this.lblPayState.Text = "已收全款";
                        this.pnlSave.Visible = false;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.结账:
                        this.lblPayState.Text = "该单已结账";
                        this.pnlSave.Visible = false;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.申请退款:
                        this.lblPayState.Text = "申请退款";
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.已退款:
                        this.lblPayState.Text = "已退款";
                        break;
                }
                #endregion


                EyouSoft.Model.NewTourStructure.MRoute routeModel = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(model.RouteId);
                if (routeModel != null)
                {
                    //如果线路是铁定发团则不显示预留
                    if (routeModel.IsCertain)
                    {
                        this.pnlYuLiu.Visible = false;
                    }
                    //如果国际线预定金额小于0，那么隐藏收取定金
                    if (routeModel.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                    {
                        if (routeModel.AdultPrice <= 0 && routeModel.ChildrenPrice <= 0)
                        {
                            this.pnlLitPay.Visible = false;
                        }
                    }
                    else
                    {
                        this.pnlLitPay.Visible = false;
                    }
                }

            }

        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <returns></returns>
        private string FormSave()
        {
            string orderID = Utils.GetQueryStringValue("orderID");
            //声明操作BLL
            EyouSoft.IBLL.NewTourStructure.ITourOrder orderBll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();
            //获得订单实体
            EyouSoft.Model.NewTourStructure.MTourOrder orderModel = orderBll.GetModel(orderID);

            //成人数
            int adultCount = Utils.GetInt(Utils.GetFormValue(this.txtAdultCount.UniqueID));
            if (adultCount == 0)
            {
                return "请输入成人数!";
            }
            //儿童数
            int childCount = Utils.GetInt(Utils.GetFormValue(this.txtChildCount.UniqueID));
            //单房差
            int otherCount = Utils.GetInt(Utils.GetFormValue(this.txtOtherCount.UniqueID));
            //增减结算价
            decimal addReduction = 0;
            decimal.TryParse(Utils.GetFormValue(this.txtAddPrice.UniqueID), out addReduction);
            //专线备注
            string routeRemarks = Utils.GetFormValue(this.txtRouteRemarks.UniqueID);

            #region 处理旅客
            string[] txtName = Utils.GetFormValues("txtName");
            string[] txtTel = Utils.GetFormValues("txtTel");
            string[] txtCard = Utils.GetFormValues("txtCard");
            string[] txtCardS = Utils.GetFormValues("txtCardS");
            string[] txtCardT = Utils.GetFormValues("txtCardT");
            string[] sltSex = Utils.GetFormValues("sltSex");
            string[] sltChild = Utils.GetFormValues("sltChild");
            string[] txtNumber = Utils.GetFormValues("txtNumber");
            string[] txtRemarks = Utils.GetFormValues("txtRemarks");
            string[] cbxVisitor = Utils.GetFormValues("cbxVisitor");


            IList<EyouSoft.Model.NewTourStructure.MTourOrderCustomer> customerList = new List<EyouSoft.Model.NewTourStructure.MTourOrderCustomer>();

            if (txtName.Length > 0)
            {
                for (int i = 0; i < txtName.Length; i++)
                {
                    EyouSoft.Model.NewTourStructure.MTourOrderCustomer model = new EyouSoft.Model.NewTourStructure.MTourOrderCustomer();
                    model.CertificatesType = EyouSoft.Model.TicketStructure.TicketCardType.身份证;
                    model.CompanyId = SiteUserInfo.CompanyID;
                    if (cbxVisitor.Contains((i + 1).ToString()))
                    {
                        model.IsSaveToTicketVistorInfo = true;
                    }
                    model.Mobile = txtTel[i];
                    model.CradType = sltChild[i] == "0" ? EyouSoft.Model.TicketStructure.TicketVistorType.成人 : EyouSoft.Model.TicketStructure.TicketVistorType.儿童;
                    model.IdentityCard = txtCard[i];
                    model.IssueTime = DateTime.Now;
                    model.Notes = txtRemarks[i];
                    model.OtherCard = txtCardT[i];
                    model.Passport = txtCardS[i];
                    model.Sex = sltSex[i] == "0" ? EyouSoft.Model.CompanyStructure.Sex.男 : EyouSoft.Model.CompanyStructure.Sex.女;
                    model.SiteNo = txtNumber[i];
                    model.VisitorName = txtName[i];

                    customerList.Add(model);
                }
            }
            #endregion

            #region 订单实体赋值

            orderModel.Reduction = addReduction;
            orderModel.AdultNum = adultCount;
            orderModel.ChildrenNum = childCount;
            orderModel.Customers = customerList;
            orderModel.IssueTime = DateTime.Now;
            orderModel.OperatorId = SiteUserInfo.ID;
            orderModel.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            orderModel.OrderStatus = EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商已阅;
            orderModel.SaveDate = null;
            orderModel.SingleRoomNum = otherCount;
            orderModel.Travel = "";

            orderModel.BusinessNotes = routeRemarks;
            //专线计算结算价 需要加减结算价 
            orderModel.TotalSettlementPrice = adultCount * orderModel.SettlementAudltPrice + childCount * orderModel.SettlementChildrenPrice + addReduction;

            if (EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().AddOrUpdTourOrder(orderModel))
            {
                return "ok";
            }
            else
            {
                return "服务器忙，请稍后再试!";
            }
            #endregion
        }

        /// <summary>
        /// 订单状态和支付状态修改
        /// </summary>
        /// <param name="orderID">订单编号</param>
        /// <param name="state">状态</param>
        /// <param name="dateTime">预留日期</param>
        /// <returns></returns>
        private string UpdateOrderState(string orderID, string state, string dateTime)
        {
            //声明操作BLL
            EyouSoft.IBLL.NewTourStructure.ITourOrder orderBll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();
            bool result = false;
            switch (state)
            {
                //订单预留
                case "5":
                    result = orderBll.SetOrderStatus(orderID, EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商预留, Utils.GetDateTimeNullable(dateTime));
                    break;
                //订单确定
                case "6":
                    result = orderBll.SetOrderStatus(orderID, EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商已确定, DateTime.Now);
                    break;
                //结单
                case "7":
                    result = orderBll.SetOrderStatus(orderID, EyouSoft.Model.NewTourStructure.PowderOrderStatus.结单, DateTime.Now);
                    break;
                //定金已收  
                case "8":
                    result = orderBll.SetPaymentStatus(orderID, EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付定金);
                    break;
                //全款已收
                case "9":
                    result = orderBll.SetPaymentStatus(orderID, EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付全款);
                    break;
            }
            if (result)
            {
                return "ok";
            }
            return "服务器忙,请稍后再试!";
        }
    }
}
