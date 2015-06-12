using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserBackCenter.Order.TourAgency
{
    public partial class OrderUpdate : BackPage
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
                Response.Write(UpdateOrderState(orderID, Utils.GetQueryStringValue("state")));
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

                this.lblRouteName.Text = "<a href='/PrintPage/LineTourInfo.aspx?RouteID=" + model.RouteId + "' target='_blank'>" + model.RouteName + "</a>";
                this.lblLeaveDate.Text = model.LeaveDate.ToString("yyyy-MM-dd") + "(" + Utils.GetDayOfWeek((int)model.LeaveDate.DayOfWeek) + ") ";//出团日期(周几)
                this.lblCount.Text = tourModel.MoreThan.ToString();
                this.lblTourState.Text = tourModel.PowderTourStatus.ToString();
                this.lblCompanyName.Text = " <a  target=\"_blank\" href=\"" + Utils.GetShopUrl(tourModel.Publishers) + "\">" + tourModel.PublishersName + "</a>";
                #region 获得专线公司信息
                EyouSoft.Model.CompanyStructure.CompanyInfo comModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(tourModel.Publishers);
                if (comModel != null && comModel.ContactInfo != null)
                {
                    this.lblMq.Text = Utils.GetMQ(comModel.ContactInfo.MQ);
                    this.lblQQ.Text = Utils.GetQQ(comModel.ContactInfo.QQ);
                }

                #endregion
                this.txtAddPrice.Value = model.Add.ToString("0.00");
                this.txtReductPrice.Value = model.Reduction.ToString("0.00");
                this.lblLeaveCityandTracffic.Text = tourModel.StartTraffic + " " + tourModel.StartCityName;//出发交通和城市
                this.lbEndCityandTracffic.Text = tourModel.EndTraffic + " " + tourModel.EndCityName;//返程交通和城市
                //this.lbPrice_d.Text = tourModel.AdultPrice.ToString("F0");
                this.lbEnddate.Text = tourModel.EndDate;
                this.lbLeavedate.Text = tourModel.StartDate;
                this.lblOrder.Text = model.OrderNo;
                this.lblMsg.Text = tourModel.SetDec;
                this.lblAllMsg.Text = tourModel.TeamLeaderDec;
                this.txtContact.Value = model.VisitorContact;
                this.txtConTactTel.Value = model.VisitorTel;
                this.txtFzr.Value = model.TravelContact;
                this.txtFzrTel.Value = model.TravelTel;
                this.txtAdultCount.Value = model.AdultNum.ToString();
                this.txtChildCount.Value = model.ChildrenNum.ToString();
                this.txtOtherCount.Value = model.SingleRoomNum.ToString();
                this.lblAddDate.Text = model.IssueTime.ToString("yyyy-MM-dd") + "&nbsp" + model.OperatorName;
                this.txtRemark.Value = model.TravelNotes;
                this.lblCusRemark.Text = model.VisitorNotes;

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
                //市场价总价
                this.lblReailPriceAll.Text = Utils.FilterEndOfTheZeroDecimal(model.TotalSalePrice);
                //结算总价
                this.lblSettlePriceAll.Text = Utils.FilterEndOfTheZeroDecimal(model.TotalSettlementPrice);

                this.OrderCustomer1.TourOrderCustomer = model.Customers;

                #region 处理订单状态和支付状态
                this.pnlSave.Visible = false;
                this.pnlCanel.Visible = false;
                switch (model.OrderStatus)
                {
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社待处理:
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社已阅:
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商待处理:
                        this.lblOrderState.Text = "专线待处理";
                        this.pnlSave.Visible = true;
                        this.pnlCanel.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商预留:
                        this.lblOrderState.Text = "专线预留,留位日期:" + model.SaveDate == null ? "" : Convert.ToDateTime(model.SaveDate).ToString("yyyy-MM-dd");
                        this.pnlCanel.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.预留过期:
                        this.lblOrderState.Text = "留位过期";
                        this.pnlCanel.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.结单:
                        this.lblOrderState.Text = "已结单";
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.取消:
                        this.lblOrderState.Text = "已取消";
                        break;
                    case EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商已确定:
                        this.lblOrderState.Text = "专线已确定";
                        this.pnlCanel.Visible = true;
                        break;
                }


                this.pnlLitPay.Visible = false;
                this.pnlAllPay.Visible = false;
                this.pnlTuiKuan.Visible = false;
                switch (model.PaymentStatus)
                {
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客待支付:
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客未支付:
                        this.lblPayState.Text = "待支付";
                        this.pnlLitPay.Visible = true;
                        this.pnlAllPay.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客已支付定金:
                        this.lblPayState.Text = "已支付定金";
                        this.pnlAllPay.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.游客已支付全款:
                        this.lblPayState.Text = "已支付全款";
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付定金:
                        this.lblPayState.Text = "专线已收定金";
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付全款:
                        this.lblPayState.Text = "专线已收全款";
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.结账:
                        this.lblPayState.Text = "已付全款";
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.申请退款:
                        this.lblPayState.Text = "申请退款";
                        this.pnlTuiKuan.Visible = true;
                        break;
                    case EyouSoft.Model.NewTourStructure.PaymentStatus.已退款:
                        this.lblPayState.Text = "已退款";
                        break;
                }
                #endregion
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
            //游客联系人
            string contact = Utils.GetFormValue(this.txtContact.UniqueID);
            //游客联系电话
            string conTactTel = Utils.GetFormValue(this.txtConTactTel.UniqueID);
            //负责人
            string fzr = Utils.GetFormValue(this.txtFzr.UniqueID);
            if (fzr == "")
            {
                fzr = SiteUserInfo.ContactInfo.ContactName;
            }
            //负责人电话
            string fzrTel = Utils.GetFormValue(this.txtFzrTel.UniqueID);
            if (fzrTel == "")
            {
                fzrTel = SiteUserInfo.ContactInfo.Tel;
            }
            //增减价格
            decimal addPrice = 0;
            decimal.TryParse(Utils.GetFormValue(this.txtAddPrice.UniqueID), out addPrice);
            //增减结算价
            decimal reductPrice = 0;
            decimal.TryParse(Utils.GetFormValue(this.txtReductPrice.UniqueID), out reductPrice);

            //组团备注
            string remark = Utils.GetFormValue(this.txtRemark.UniqueID);

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

                    if (cbxVisitor.Contains((i + 1).ToString()))
                    {
                        model.IsSaveToTicketVistorInfo = true;
                        model.CompanyId = SiteUserInfo.CompanyID;
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
            orderModel.Add = addPrice;
            orderModel.Reduction = reductPrice;
            orderModel.AdultNum = adultCount;
            orderModel.ChildrenNum = childCount;
            orderModel.Customers = customerList;
            orderModel.IssueTime = DateTime.Now;
            orderModel.OperatorId = SiteUserInfo.ID;
            orderModel.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            orderModel.OrderStatus = EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商待处理;
            orderModel.SaveDate = null;
            orderModel.SingleRoomNum = otherCount;
            orderModel.Travel = SiteUserInfo.CompanyID;
            orderModel.TravelContact = fzr;
            orderModel.TravelName = SiteUserInfo.CompanyName;
            orderModel.TravelNotes = remark;
            orderModel.TravelTel = fzrTel;
            orderModel.VisitorContact = contact;
            orderModel.VisitorTel = conTactTel;
            orderModel.TotalSalePrice = adultCount * orderModel.PersonalPrice + childCount * orderModel.ChildPrice + otherCount * orderModel.MarketPrice + addPrice;
            orderModel.TotalSettlementPrice = adultCount * orderModel.SettlementAudltPrice + childCount * orderModel.SettlementChildrenPrice + reductPrice;

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

        private string UpdateOrderState(string orderID, string state)
        {
            //声明操作BLL
            EyouSoft.IBLL.NewTourStructure.ITourOrder orderBll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();
            bool result = false;
            switch (state)
            {
                //定金收取
                case "1":
                    result = orderBll.SetPaymentStatus(orderID, EyouSoft.Model.NewTourStructure.PaymentStatus.游客已支付定金);
                    break;
                //全款收取
                case "2":
                    result = orderBll.SetPaymentStatus(orderID, EyouSoft.Model.NewTourStructure.PaymentStatus.游客已支付全款);
                    break;
                //专线已退款
                case "3":
                    result = orderBll.SetPaymentStatus(orderID, EyouSoft.Model.NewTourStructure.PaymentStatus.已退款);
                    break;
                //订单取消
                case "4":
                    //组团取消订单时，如果订单支付状态为专线定金已收或全收，那么支付状态设为申请退款
                    EyouSoft.Model.NewTourStructure.MTourOrder orderModel = orderBll.GetModel(orderID);
                    if (orderModel != null)
                    {
                        if (orderModel.PaymentStatus == EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付定金 || orderModel.PaymentStatus == EyouSoft.Model.NewTourStructure.PaymentStatus.组团社已付全款)
                        {
                            orderBll.SetPaymentStatus(orderID, EyouSoft.Model.NewTourStructure.PaymentStatus.申请退款);
                        }
                    }
                    result = orderBll.SetOrderStatus(orderID, EyouSoft.Model.NewTourStructure.PowderOrderStatus.取消, DateTime.Now);
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
