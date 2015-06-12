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
using EyouSoft.Common.Function;
using AlipayClass;
using tenpay;

namespace UserBackCenter.TicketsCenter.OrderManage
{
    /// <summary>
    /// 订单详细页
    /// 罗丽娥   2010-10-19
    /// </summary>
    public partial class OrderDetailInfo : EyouSoft.Common.Control.BackPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 容器ID
        /// </summary>
        protected string ContainerID = "OrderDetailInfo";
        /// <summary>
        /// 退票、作废、改期、改签、处理要显示的文字信息
        /// </summary>
        protected string OrderStateText = string.Empty;
        /// <summary>
        /// 申请退票、作废、改期、改签的人数 
        /// </summary>
        protected int OrderChangeCount = 0;
        /// <summary>
        /// 是否是单程 true：单程   false：往返程
        /// </summary>
        protected EyouSoft.Model.TicketStructure.FreightType FreightType = EyouSoft.Model.TicketStructure.FreightType.单程;

        /// <summary>
        /// 当前请求订单状态
        /// </summary>
        protected EyouSoft.Model.TicketStructure.OrderState? OrderState = null;
        /// <summary>
        /// 当前请求订单变更类型
        /// </summary>
        protected EyouSoft.Model.TicketStructure.OrderChangeType? ChangeType = null;

        protected string showtype = string.Empty;
        protected string OrderId = string.Empty;

        protected int PeopleCount = 0, BuyInsCount = 0, BuyItineraryCount = 0;
        protected decimal ItineraryPrice = 0, EMSPrice = 0;

        /// <summary>
        /// 订单明细实体
        /// </summary>
        private EyouSoft.Model.TicketStructure.OrderInfo orderInfo = null;
        /// <summary>
        /// 订单变更信息
        /// </summary>
        private EyouSoft.Model.TicketStructure.OrderChangeInfo orderChangeInfo = null;
        

        #region page_load
        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = Utils.GetQueryStringValue("flag");
            if (flag == "isalipayrefund")
            {
                IsAlipayRefund();
            }
            ContainerID += Guid.NewGuid().ToString();

            OrderId = Utils.GetQueryStringValue("orderid");//订单ID
            //根据订单ID获取订单明细
            EyouSoft.IBLL.TicketStructure.ITicketOrder ibll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            orderInfo = ibll.GetOrderInfoById(OrderId);

            //判断订单是否存在
            if (orderInfo == null)//不存在
            {
                //提示用户当前订单不存在
                Utils.ResponseNoPermit("对不起，当前订单不存在");
            }

            //根据订单ID获取订单最新的变更信息
            orderChangeInfo = ibll.GetLatestChange(OrderId);

            //初始化请求订单状态
            int tmpOrderStateid = Utils.GetInt(Request.QueryString["orderstate"], -1);//请求的订单状态ID
            if (tmpOrderStateid != -1)//有效
            {
                OrderState = (EyouSoft.Model.TicketStructure.OrderState)tmpOrderStateid;
                this.OrderDetailInfo_hidOrderState.Value = tmpOrderStateid.ToString();
            }

            //if (!OrderState.HasValue)
            //{
            //    Utils.ResponseNoPermit("参数错误");
            //}

            //初始化请求订单变更类型
            int tmpChangeTypeId = Utils.GetInt(Request.QueryString["changetype"], -1);//请求的订单变更类型ID
            if (tmpChangeTypeId != -1)
            {
                ChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)tmpChangeTypeId;
                this.OrderDetailInfo_hidChangeType.Value = tmpChangeTypeId.ToString();
            }

            //判断当前请求类型
            showtype = Utils.GetQueryStringValue("type");//请求类型

            /*
             * 如果不是订单查看请求，则判断请求的订单处理状态 是否与 订单的当前状态  是否同步
             * */
            if (!showtype.Equals("search", StringComparison.OrdinalIgnoreCase))
            {
                if (OrderState == EyouSoft.Model.TicketStructure.OrderState.等待审核
                    || OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)
                {
                    if (OrderState != orderInfo.OrderState)
                    {
                        Utils.ResponseNoPermit("当前订单的状态已经被修改");
                    }
                }

                if (OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成)
                {
                    if (orderChangeInfo == null)
                    {
                        Utils.ResponseNoPermit("当前订单不能【退/废/改/签】处理");
                    }
                    if (ChangeType != orderChangeInfo.ChangeType
                       || orderChangeInfo.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.接受
                       || orderChangeInfo.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.拒绝)
                    {
                        Utils.ResponseNoPermit("当前订单的状态已经被修改");
                    }
                }
            }

            if (!Page.IsPostBack)
            {
                if (ChangeType.HasValue)//是否有订单变更请求信息
                {
                    OrderStateText = ChangeType.ToString();
                }
                if (orderChangeInfo != null)//是否有订单变更信息
                {
                    OrderChangeCount = orderChangeInfo.PCount;
                }

                if (!showtype.Equals("search", StringComparison.OrdinalIgnoreCase))//【订单处理】请求
                {
                    if (OrderState == EyouSoft.Model.TicketStructure.OrderState.等待审核)
                    {
                        this.OrderDetailInfo_lblFlightCode.Visible = this.OrderDetailInfo_lblBackFlightCode.Visible = false;
                        this.OrderDetailInfo_txtFlightCode.Visible = this.OrderDetailInfo_txtBackFlightCode.Visible = true;
                        this.td_PNR.Visible = this.td_PNR1.Visible = true;
                        this.OrderDetailInfo_txtUpdatePNR.Visible = true;
                        this.OrderDetailInfo_txtFlightCode.Visible = this.OrderDetailInfo_txtBackFlightCode.Visible = true;
                        this.OrderDetailInfo_txtLeavePrice.Visible = true;
                        this.OrderDetailInfo_lblLeavePrice.Visible = false;
                        this.OrderDetailInfo_txtLeaveFacePrice.Visible = true;
                        this.OrderDetailInfo_lblLeaveFacePrice.Visible = false;

                        this.OrderDetailInfo_txtLeaveDiscount.Visible = true;//去程参考扣率 用于编辑
                        this.ltrLeaveDiscount.Visible = true;//去程参考扣率 后面的%
                        this.OrderDetailInfo_lblLeaveDiscount.Visible = false;//去程参考扣率 用于显示

                        this.OrderDetailInfo_txtLOtherPrice.Visible = true;//燃油,用于编辑
                        this.OrderDetailInfo_txtLOtherPrice2.Visible = true;//机建,用于编辑
                        this.OrderDetailInfo_lblLOtherPrice.Visible = false;//燃油/机建,用于显示
                        this.OrderDetailInfo_txtReturnPrice.Visible = true;
                        this.OrderDetailInfo_lblReturnPrice.Visible = false;
                        this.OrderDetailInfo_txtReturnFacePrice.Visible = true;
                        this.OrderDetailInfo_lblReturnFacePrice.Visible = false;

                        this.OrderDetailInfo_txtReturnDiscount.Visible = true;//回程参考扣率 用于编辑
                        this.ltrReturnDiscount.Visible = true;//回程参考扣率 后面的%
                        this.OrderDetailInfo_lblReturnDiscount.Visible = false;//回程参考扣率 用于显示

                        this.OrderDetailInfo_txtROtherPrice.Visible = true;//燃油，用于编辑
                        this.OrderDetailInfo_txtROtherPrice2.Visible = true;//机建,用于编辑
                        this.OrderDetailInfo_lblROtherPrice.Visible = false;//燃油/机建,用于显示

                        this.td_PayType.Visible = this.td_PayType1.Visible = false;
                    }
                    else if (OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)
                    {
                        this.OrderDetailInfo_txtFlightCode.Visible = this.OrderDetailInfo_txtBackFlightCode.Visible = true;
                        this.OrderDetailInfo_lblFlightCode.Visible = this.OrderDetailInfo_lblBackFlightCode.Visible = false;
                    }
                    
                }
                InitOrderDetail(OrderId);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                Response.Clear();
                Response.Write(SaveData(OrderState));
                Response.End();
            }
        }
        #endregion

        /// <summary>
        /// 初始化订单信息
        /// </summary>
        /// <param name="OrderId"></param>
        private void InitOrderDetail(string OrderId)
        {
            EyouSoft.Model.TicketStructure.OrderInfo model = orderInfo;
            //EyouSoft.Model.TicketStructure.OrderInfo model = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetOrderInfoById(OrderId);
            if (model != null)
            {
                this.OrderDetailInfo_hidOrderId.Value = model.OrderId;
                this.OrderDetailInfo_lblOrderNo.Text = model.OrderNo;

                #region 航班信息
                OrderState = model.OrderState;
                EyouSoft.Model.TicketStructure.TicketFlightCompany FlightModel = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompanyById(model.FlightId.ToString(), string.Empty);
                if (FlightModel != null)
                {
                    this.OrderDetailInfo_lblFlightName.Text = FlightModel.AirportName;
                }
                FlightModel = null;

                this.OrderDetailInfo_lblHomeCityId.Text = this.OrderDetailInfo_lblBackDestCityId.Text = model.HomeCityName;
                this.OrderDetailInfo_lblDestCityId.Text = this.OrderDetailInfo_lblBackHomeCityId.Text = model.DestCityName;
                this.OrderDetailInfo_lblFlightCode.Text = this.OrderDetailInfo_txtFlightCode.Text = model.LFlightCode;
                this.OrderDetailInfo_lblBackFlightCode.Text = this.OrderDetailInfo_txtBackFlightCode.Text = model.RFlightCode;
                this.OrderDetailInfo_lblLeaveTime.Text = model.LeaveTime.ToString("yyyy-MM-dd");
                this.OrderDetailInfo_lblBackTime.Text = model.ReturnTime.ToString("yyyy-MM-dd");
                this.OrderDetailInfo_lblTravellerType.Text = this.OrderDetailInfo_lblBackTravellerType.Text = model.TravellerType.ToString();
                if (!String.IsNullOrEmpty(model.PNR))
                {
                    this.OrderDetailInfo_txtUpdatePNR.Text = this.OrderDetailInfo_lblUpdatePNR.Text = model.PNR;
                }
                else {
                    this.OrderDetailInfo_lblPNR.Text = model.OPNR;
                }
                this.OrderDetailInfo_lblSupplierName.Text = model.SupplierCName;
                this.OrderDetailInfo_lblRateType.Text = model.RateType.ToString();

                EyouSoft.Model.TicketStructure.OrderRateInfo OrderRateModel = model.OrderRateInfo;
                if (OrderRateModel != null)
                {
                    FreightType = OrderRateModel.FreightType;
                    this.OrderDetailInfo_lblFreightType.Text = OrderRateModel.FreightType.ToString();
                    
                    //去程
                    this.OrderDetailInfo_lblLeaveFacePrice.Text = this.OrderDetailInfo_txtLeaveFacePrice.Text = OrderRateModel.LeaveFacePrice.ToString("F2");
                    //参考扣率 百分比显示
                    this.OrderDetailInfo_lblLeaveDiscount.Text = OrderRateModel.LeaveDiscount.ToString("F1") + "%";
                    this.OrderDetailInfo_txtLeaveDiscount.Text = OrderRateModel.LeaveDiscount.ToString("F1");
                    this.OrderDetailInfo_lblLeaveTimeLimit.Text = OrderRateModel.LeaveTimeLimit;//运价有效期
                    this.OrderDetailInfo_lblLeavePrice.Text = this.OrderDetailInfo_txtLeavePrice.Text = OrderRateModel.LeavePrice.ToString("F2");
                    this.OrderDetailInfo_lblMaxPCount.Text  = OrderRateModel.MaxPCount.ToString();//人数上限
                    //燃油/机建 用于显示
                    this.OrderDetailInfo_lblLOtherPrice.Text = OrderRateModel.LFuelPrice.ToString("F2") + "/" + OrderRateModel.LBuildPrice.ToString("F2");
                    //燃油，用于编辑
                    this.OrderDetailInfo_txtLOtherPrice.Text = OrderRateModel.LFuelPrice.ToString("F2");
                    //机建，用于编辑
                    this.OrderDetailInfo_txtLOtherPrice2.Text = OrderRateModel.LBuildPrice.ToString("F2");
                    //回程
                    this.OrderDetailInfo_lblReturnFacePrice.Text = OrderRateModel.ReturnFacePrice.ToString("F2");//面价,用于显示
                    this.OrderDetailInfo_txtReturnFacePrice.Text = OrderRateModel.ReturnFacePrice.ToString("F2");//面价，用于编缉
                    this.OrderDetailInfo_lblReturnDiscount.Text  = OrderRateModel.ReturnDiscount.ToString("F1")+"%";//扣率，用于显示
                    this.OrderDetailInfo_txtReturnDiscount.Text = OrderRateModel.ReturnDiscount.ToString("F1");//扣率，用于编辑
                    this.OrderDetailInfo_lblReturnTimeLimit.Text = OrderRateModel.ReturnTimeLimit;//运价有效期
                    this.OrderDetailInfo_lblMaxPCount1.Text = OrderRateModel.MaxPCount.ToString();//人数上限
                    this.OrderDetailInfo_lblReturnPrice.Text = this.OrderDetailInfo_txtReturnPrice.Text = OrderRateModel.ReturnPrice.ToString("F2");
                    
                    //燃油/机建 用于显示
                    this.OrderDetailInfo_lblROtherPrice.Text = OrderRateModel.RFuelPrice.ToString("F2") + "/" + OrderRateModel.RBuildPrice.ToString("F2");

                    this.OrderDetailInfo_txtROtherPrice.Text = OrderRateModel.RFuelPrice.ToString("F2");//燃油，用于编辑
                    this.OrderDetailInfo_txtROtherPrice2.Text = OrderRateModel.RBuildPrice.ToString("F2");//机建，用于编辑
                    this.OrderDetailInfo_lblSupplierRemark.Text = OrderRateModel.SupplierRemark;//供应商备注
                }
                OrderRateModel = null;
                #endregion

                #region 供应商信息
                EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo SupplierModel = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(model.SupplierCId);
                if (SupplierModel != null)
                {
                    this.OrderDetailInfo_lblContactName.Text = SupplierModel.ContactName;
                    this.OrderDetailInfo_lblWorkTime.Text = SupplierModel.WorkStartTime + "-" + SupplierModel.WorkEndTime;
                    //this.OrderDetailInfo_lblSystemNo.Text = SupplierModel.ICPNumber;
                    this.OrderDetailInfo_lblProxyLev.Text = SupplierModel.ProxyLev;
                    this.OrderDetailInfo_lblWebSite.Text = SupplierModel.WebSite;
                    this.OrderDetailInfo_lblContactTel.Text = SupplierModel.ContactTel;
                    this.OrderDetailInfo_lblSuccessRate.Text = (SupplierModel.SuccessRate * 100).ToString("F1") + "%";
                    this.OrderDetailInfo_lblHandleNum.Text = SupplierModel.HandleNum.ToString();
                    this.OrderDetailInfo_lblSubmitNum.Text = SupplierModel.SubmitNum.ToString();
                    this.OrderDetailInfo_lblRefundAvgTime.Text = SupplierModel.RefundAvgTime.ToString("F2");
                    this.OrderDetailInfo_lblNoRefundAvgTime.Text = SupplierModel.NoRefundAvgTime.ToString("F2");
                }
                SupplierModel = null;
                #endregion

                #region 旅客信息
                IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> TravellerList = model.Travellers;
                if (TravellerList != null && TravellerList.Count > 0)
                {
                    this.OrderDetailInfo_lblCustomerCount.Text = TravellerList.Count.ToString();

                    if (OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成 && showtype != "search")
                    {
                        if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.退票)
                        {
                            TravellerList = TravellerList.Where(Item => Item.TravellerState == EyouSoft.Model.TicketStructure.TravellerState.申请退票).ToList();
                        }
                        else if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.改期)
                        {
                            TravellerList = TravellerList.Where(Item => Item.TravellerState == EyouSoft.Model.TicketStructure.TravellerState.申请改期).ToList();
                        }
                        else if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.改签)
                        {
                            TravellerList = TravellerList.Where(Item => Item.TravellerState == EyouSoft.Model.TicketStructure.TravellerState.申请改签).ToList(); 
                        }
                        else if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.作废)
                        {
                            TravellerList = TravellerList.Where(Item => Item.TravellerState == EyouSoft.Model.TicketStructure.TravellerState.申请作废).ToList();
                        }
                    }
                    this.OrderDetailInfo_rptTravellerInfo.DataSource = TravellerList;
                    this.OrderDetailInfo_rptTravellerInfo.DataBind();

                    PeopleCount = TravellerList.Count;
                    BuyInsCount = TravellerList.Where(Item => Item.IsBuyIns).ToList().Count;
                    BuyItineraryCount = TravellerList.Where(Item => Item.IsBuyItinerary).ToList().Count;

                    this.OrderDetailInfo_lblTravellerCount.Text = TravellerList.Count.ToString();//旅客人数
                        //申请 退/废/改/签 的人数
                    this.OrderDetailInfo_lblRefundCount.Text = TravellerList.Count.ToString();//乘客人数
                    this.OrderDetailInfo_lblByInsCount.Text = BuyInsCount.ToString();
                    this.OrderDetailInfo_lblBuyItinerary.Text = BuyItineraryCount.ToString();
                }
                TravellerList = null;
                #endregion

                ItineraryPrice = model.ItineraryPrice;
                EMSPrice = model.EMSPrice;
                this.OrderDetailInfo_lblPayPrice.Text = this.OrderDetailInfo_lblPayPrice1.Text = model.TotalAmount.ToString("F2");
                
                string tmpPayType = model.PayType.ToString();
                this.OrderDetailInfo_lblPayType.Text = tmpPayType != "0" ? tmpPayType : "暂无";

                #region 订单处理状态
                IList<EyouSoft.Model.TicketStructure.TicketOrderLog> LogList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetTicketOrderState(OrderId);
                if (LogList != null && LogList.Count > 0)
                {
                    this.OrderDetailInfo_rptOrderLog.DataSource = LogList;
                    this.OrderDetailInfo_rptOrderLog.DataBind();
                }
                LogList = null;
                #endregion

                #region 采购商联系方式
                this.OrderDetailInfo_lblBuyerCName.Text = model.BuyerCName;
                this.OrderDetailInfo_lblBuyerContactAddress.Text = model.BuyerContactAddress;
                this.OrderDetailInfo_lblBuyerContactName.Text = model.BuyerContactName;
                this.OrderDetailInfo_lblBuyerContactMobile.Text = model.BuyerContactMobile;
                this.OrderDetailInfo_lblBuyerRemark.Text = model.BuyerRemark;
                this.OrderDetailInfo_ltrMQ.Text = Utils.GetMQ(model.BuyerContactMQ);
                #endregion
            }
        }

        /// <summary>
        /// 显示旅客人员票号
        /// </summary>
        /// <param name="TravellerId">旅客ID</param>
        /// <param name="TicketNumber">票号</param>
        /// <returns></returns>
        protected string ShowTicketNumber(string TravellerId,string TicketNumber)
        {
            //根据当前订单的订单状态 显示不同样式的 票号
            string tmpVal = string.Empty;
            if (OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)//如果是支付成功
            {
                //显示 可用于编辑的票号 输入框
                tmpVal = string.Format("<input type=\"text\" id=\"OrderDetailInfo_TicketNumber{0}\" name=\"OrderDetailInfo_TicketNumber{0}\" size=\"5\"", TravellerId);
            }
            else {
                //直接显示票号信息
                tmpVal = TicketNumber;
            }
            return tmpVal;
        }

        #region 根据状态修改订单状态及数据保存
        /// <summary>
        /// 根据状态修改订单状态及数据保存
        /// </summary>
        private bool SaveData(EyouSoft.Model.TicketStructure.OrderState? orderstate)
        {
            bool IsResult = false;
            string tmpOrderId = this.OrderDetailInfo_hidOrderId.Value;
            EyouSoft.IBLL.TicketStructure.ITicketOrder OrderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            EyouSoft.Model.TicketStructure.OrderInfo OrderModel = orderInfo;

            //EyouSoft.Model.TicketStructure.OrderState tmpOrderState = (EyouSoft.Model.TicketStructure.OrderState)Utils.GetInt(Utils.GetQueryStringValue("orderstate"),0);
            //EyouSoft.Model.TicketStructure.OrderChangeType tmpChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)Utils.GetInt(Utils.GetQueryStringValue("changetype"),0);


            if (OrderState == EyouSoft.Model.TicketStructure.OrderState.等待审核)
            {
                #region 审核通过
                //判断当前订单状态与 请求的订单状态是否同步
                if (OrderState.Value != OrderModel.OrderState)
                {
                    Utils.ResponseMeg(false, "页面已经过期");
                }
                string FlightCode = Utils.GetFormValue(OrderDetailInfo_txtFlightCode.UniqueID);//航班号
                string ReturnFlightCode = Utils.GetFormValue(OrderDetailInfo_txtBackFlightCode.UniqueID);//回程航班号
                string PNR = Utils.GetFormValue(OrderDetailInfo_txtUpdatePNR.UniqueID);//更改PNR
                decimal LeaveFacePrice = 
                    Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtLeaveFacePrice.UniqueID), 0);//去程面价
                decimal LeaveDiscount = 
                    Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtLeaveDiscount.UniqueID), 0);//去程参考扣率
                decimal LeavePrice = 
                    Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtLeavePrice.UniqueID), 0);//去程结算价
                string tmpLFuelPrice = Utils.GetFormValue(OrderDetailInfo_txtLOtherPrice.UniqueID);//去程燃油
                string tmpLBuildPrice = Utils.GetFormValue(OrderDetailInfo_txtLOtherPrice2.UniqueID);//去程机建
                decimal LFuelPrice = 0, LBuildPrice = 0;//燃油 ，机建
                LFuelPrice = Utils.GetDecimal(tmpLFuelPrice);//燃油
                LBuildPrice = Utils.GetDecimal(tmpLBuildPrice);//机建

                decimal ReturnFacePrice = 
                    Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtReturnFacePrice.UniqueID), 0);//回程面价
                decimal ReturnDiscount = 
                    Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtReturnDiscount.UniqueID), 0);//回程参考扣率
                decimal ReturnPrice = 
                    Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtReturnPrice.UniqueID), 0);//回程结算价
                decimal RFuelPrice = Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtROtherPrice.UniqueID));//回程燃油
                decimal RBuildPrice = Utils.GetDecimal(Utils.GetFormValue(OrderDetailInfo_txtROtherPrice2.UniqueID));//回程机建
                
                EyouSoft.Model.TicketStructure.OrderInfo model = new EyouSoft.Model.TicketStructure.OrderInfo();
                model.OrderId = OrderModel.OrderId;
                EyouSoft.Model.TicketStructure.OrderRateInfo RateModel = new EyouSoft.Model.TicketStructure.OrderRateInfo();
                RateModel.LeaveFacePrice = LeaveFacePrice;
                RateModel.LeaveDiscount = LeaveDiscount;
                RateModel.LeavePrice = LeavePrice;
                RateModel.LFuelPrice = LFuelPrice;
                RateModel.LBuildPrice = LBuildPrice;
                RateModel.ReturnFacePrice = ReturnFacePrice;
                RateModel.ReturnDiscount = ReturnDiscount;
                RateModel.ReturnPrice = ReturnPrice;
                RateModel.RFuelPrice = RFuelPrice;
                RateModel.RBuildPrice = RBuildPrice;
                model.OrderRateInfo = RateModel;
                // 保险费
                decimal InsPrice = 0;
                //计算 购买行程单的人数,购买保险的人数
                int ItineraryPriceCount = 0;//购买行程单的人数
                int InsPriceCount = 0;//购买保险的人数
                //遍历旅客信息列表
                foreach (EyouSoft.Model.TicketStructure.OrderTravellerInfo travel in OrderModel.Travellers)
                {
                    if (travel.IsBuyItinerary)
                    {
                        ItineraryPriceCount++;
                    }
                    if (travel.IsBuyIns)
                    {
                        InsPriceCount++;
                    }
                }
                //根据 是否有购买行程单的人 ，判断是否计算 快递费
                int emsCount = 0;
                if (ItineraryPriceCount > 0)
                {
                    emsCount = 1;
                }
                // 订单费用=（结算价格+燃油/机建）*人数+保险*购买保险人数+行程单*购买行程单人数+快递费

                //去程 （结算价格+燃油/机建）*人数
                decimal LeaveTotalPrice =
                    (RateModel.LeavePrice + RateModel.LFuelPrice + RateModel.LBuildPrice) * OrderModel.PCount;
                //回程 （结算价格+燃油/机建）*人数
                decimal ReturnTotalPrice =
                    (RateModel.ReturnPrice + RateModel.RFuelPrice + RateModel.RBuildPrice) * OrderModel.PCount;

                model.TotalAmount = LeaveTotalPrice
                    + ReturnTotalPrice
                    + InsPrice * InsPriceCount  //保险*购买保险人数
                    + OrderModel.ItineraryPrice * ItineraryPriceCount   //行程单*购买行程单人数
                    + OrderModel.EMSPrice * emsCount;   //购买行程单人数大于0，则算一份快递费

                RateModel = null;
                //更新订单价格信息，更新航班号，更新PNR，审核通过
                IsResult = OrderBll.UpdatePrice(model) 
                    && OrderBll.UpdateFlightCode(OrderModel.OrderId,FlightCode,ReturnFlightCode) 
                    && OrderBll.UpdatePNR(OrderModel.OrderId,PNR) 
                    && OrderBll.SupplierCheckOrder(OrderModel.OrderId, SiteUserInfo.ID, SiteUserInfo.CompanyID);
                model = null;

                Utils.ResponseMeg(IsResult, IsResult ? "修改订单成功" : "修改订单失败");

                #endregion
            }
            else if (OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)
            {
                //判断当前订单状态与 请求的订单状态是否同步
                if (OrderState.Value != OrderModel.OrderState)
                {
                    Utils.ResponseMeg(false, "页面已经过期");
                }
                #region 出票完成
                string[] tmpTravellerID = Utils.GetFormValues("OrderDetailInfo_hidTravellerID");//旅客人员ID数组
                IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> TravellerList = 
                    new List<EyouSoft.Model.TicketStructure.OrderTravellerInfo>();//旅客人员 列表
                for (int i = 0; i < tmpTravellerID.Length; i++)
                {
                    EyouSoft.Model.TicketStructure.OrderTravellerInfo TravellerModel = new EyouSoft.Model.TicketStructure.OrderTravellerInfo();
                    //根据旅客人员ID，获取对应的票号信息
                    string TicketNumber = Utils.GetFormValue("OrderDetailInfo_TicketNumber" + tmpTravellerID[i]);//票号
                    TravellerModel.TravellerId = tmpTravellerID[i];//旅客人员ID
                    TravellerModel.TicketNumber = TicketNumber;//票号
                    TravellerList.Add(TravellerModel);//将旅客人员 添加到 列表中
                    TravellerModel = null;
                }
                string BatchNo = string.Empty;

                decimal PlatformRefund = 0;   //  平台收取费用
                decimal PayPrice = 0;// 供应商分账金额
                /*
                 *  不同的供应商 跟平台 之间 是 有 不同的 平台交易费比例的
                 *  根据需求，现在 供应商的费率 暂时统一  费率存储在Config里
                 *  从订单帐户信息中获取到的 费率 是在 支付成功的时候 保存的，
                 *  以下单支付的时候的费率为准，来计算
                 * */
                decimal Discount = 0;// 平台交易费比例，即 供应商费率 

                // 获取公司平台供应商账户信息
                #region 获取公司平台供应商账户信息
                string AccountNumber = string.Empty;//供应商帐号
                string PayNumber = string.Empty;//支付接口交易号
                EyouSoft.Model.TicketStructure.OrderAccountInfo AccountModel = 
                    OrderBll.GetOrderAccountInfo(OrderModel.OrderId);//订单帐户信息
                if (AccountModel != null)
                {
                    AccountNumber = AccountModel.SellAccount;
                    //AccountNumber = "enowalipay2@163.com";
                    Discount = AccountModel.Discount;//供应商费率
                    PayNumber = AccountModel.PayNumber;
                }
                AccountModel = null;
                #endregion
                //Discount = decimal.Parse("0.5");
                

                if (OrderModel.OrderRateInfo != null)
                {
                    // 单程 平台收取费用 = ([(四舍五入)](去程结算价 + 去程燃油/去程机建)*交易费比率)) * 总人数
                    PlatformRefund = decimal.Parse(((
                        OrderModel.OrderRateInfo.LeavePrice 
                        + OrderModel.OrderRateInfo.LFuelPrice 
                        + OrderModel.OrderRateInfo.LBuildPrice) * Discount).ToString("F2")) 
                        * OrderModel.PCount;

                    //如果是来回程，则需加上 回程的费用
                    if (OrderModel.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                    {
                        PlatformRefund = decimal.Parse(((
                            OrderModel.OrderRateInfo.LeavePrice 
                            + OrderModel.OrderRateInfo.LFuelPrice 
                            + OrderModel.OrderRateInfo.LBuildPrice
                            +OrderModel.OrderRateInfo.ReturnPrice 
                            + OrderModel.OrderRateInfo.RFuelPrice 
                            + OrderModel.OrderRateInfo.RBuildPrice) * Discount).ToString("F2")) 
                            * OrderModel.PCount; 
                    }
                }

                // 供应商分账金额 = 下单总金额 - 平台收取费用
                PayPrice = OrderModel.TotalAmount - PlatformRefund;

                // 出票完成前写入支付明细记录并更新旅客票号
                IsResult = OrderBll.OutPutTicketBefore(
                    OrderModel.OrderNo,OrderModel.OrderId,SiteUserInfo.ID,
                    SiteUserInfo.CompanyID,PayPrice, OrderModel.PayType,TravellerList,"出票",out BatchNo);

                if(IsResult==false){
                    Utils.ResponseMeg(false,"操作失败，请稍候再试");
                }

                #region 分润
                if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                {
                    #region 支付宝分润
                    string partner = AlipayParameters.Partner;                                     //合作身份者ID
                    string key = AlipayParameters.Key;                         //安全检验码
                    string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
                    string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改

                    string out_trade_no = OrderModel.OrderNo;
                    string out_bill_no = BatchNo;//DateTime.Now.ToString("yyyyMMddHHmmss");
                    string royalty_type = "10";
                    string royalty_parameters = string.Empty;

                    // 分润接口信息集
                    Royalty_Parameters parameters = new Royalty_Parameters();
                    parameters.Add(new Royalty_Parameter(AccountNumber, PayPrice.ToString("F2"), "出票完成"));
                    royalty_parameters = parameters.CreateParametersString();

                    // 分润
                    Distribute_royalty dis = new Distribute_royalty(partner, key, sign_type, input_charset, out_trade_no, out_bill_no, royalty_type, royalty_parameters);

                    // 分润返回XML结果集
                    Distribute_royalty_Result result = dis.GetResult();

                    bool tmp = false;
                    EyouSoft.Model.TicketStructure.PayState PayState = EyouSoft.Model.TicketStructure.PayState.未提交到支付接口;
                    if (result.IsSuccess)//分润成功
                    {
                        // 出票完成后更新订单状态为‘出票完成’，并更新供应商收款金额
                        PayState = EyouSoft.Model.TicketStructure.PayState.交易完成;
                        tmp = OrderBll.PayAfterCallBack(PayNumber, PayPrice, PayState, OrderModel.PayType, AccountNumber, "出票成功", OrderModel.OrderNo, DateTime.Now, BatchNo);
                    }
                    else//分润失败
                    {
                        //分润失败，修改支付明细记录的状态
                        PayState = EyouSoft.Model.TicketStructure.PayState.交易失败;
                        tmp = OrderBll.PayAfterCallBack(PayNumber, PayPrice, PayState, OrderModel.PayType, AccountNumber, result.ErrorCode, OrderModel.OrderNo, DateTime.Now, BatchNo);
                    }

                    if (result.IsSuccess && tmp)//分润成功，订单修改成功
                    {
                        Utils.ResponseMeg(true, "分账成功，订单修改成功");
                    }
                    else if (result.IsSuccess == false)//分润失败
                    {
                        Utils.ResponseMeg(false, "分账失败（错误码："+result.ErrorCode+"），请稍候再试");
                    }
                    else if (result.IsSuccess == true && tmp == false)//分润成功，订单修改失败
                    {
                        Utils.ResponseMeg(false, "分账成功，订单状态修改失败，请即时联系客服");
                    }
                    #endregion
                }
                else if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通)
                {
                    TenpayGet(OrderModel.OrderNo, OrderModel.TotalAmount, PayPrice, PlatformRefund, BatchNo, PayNumber, AccountNumber, 1);
                }
                #endregion
                #endregion
            }
            else if (OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成)
            {
                if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.退票 || ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.作废)
                {
                    #region 退票或作废

                    #region 获取变更信息
                    string ChangeID = string.Empty;//变更ID
                    int ChangePCount = 0;//变更人数
                    EyouSoft.Model.TicketStructure.OrderChangeInfo ChangeModel = orderChangeInfo;
                        //OrderBll.GetLatestChange(OrderModel.OrderId);
                    ChangeID = ChangeModel.ChangeId;
                    ChangePCount = ChangeModel.PCount;
                    #endregion

                    #region 获取订单账户信息
                    /*
                     *  不同的供应商 跟平台 之间 是 有 不同的 平台交易费比例的
                     *  根据需求，现在 供应商的费率 暂时统一  费率存储在Config里
                     *  从订单帐户信息中获取到的 费率 是在 支付成功的时候 保存的，
                     *  以下单支付的时候的费率为准，来计算
                     * */
                    string AccountNumber = string.Empty;//供应商帐户
                    string PayNumber = string.Empty;//支付接口交易号
                    decimal Discount = 0;//平台交易费率 即供应商费率
                    EyouSoft.Model.TicketStructure.OrderAccountInfo AccountModel = OrderBll.GetOrderAccountInfo(OrderModel.OrderId);
                    if (AccountModel != null)
                    {
                        AccountNumber = AccountModel.SellAccount;
                        //AccountNumber = "enowalipay2@163.com";
                        PayNumber = AccountModel.PayNumber;
                        Discount = AccountModel.Discount;
                    }
                    AccountModel = null;
                    #endregion

                    //Discount = decimal.Parse("0.5");

                    #region 费用计算
                    //退款金额 = 一张票要退的钱*退款人数

                    decimal HandFee = 
                        Utils.GetDecimal(Utils.GetFormValue(this.OrderDetailInfo_txtFee.UniqueID));// 单人退票手续费手续费

                    decimal PlatformRefund = 0;// 平台应退总金额   (一张票要收的平台交易手续费*退款人数) 

                    decimal SRefundAmount = 0;// 供应商应退金额    退款金额 - 平台应退总金额 
                    

                    decimal TicketPrice = 0;//一张票的价格     (去程(回程)结算价 + 去程(回程)燃油/去程(回程)机建)

                    decimal OneTicketRefundPrice = 0;//一张票要退的钱    一张票的价格-单人退票手续费手续费

                    decimal OneTicketTongyeFee = 0;//一张票要收的平台交易手续费  [四舍五入](一张票的价格)*交易费比率)


                    /*
                     * 计算 单张票的价格，一张票要退的钱，一张票要收的平台交易手续费
                     * */
                    if (OrderModel.OrderRateInfo != null)
                    {
                        if (OrderModel.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
                        {
                            TicketPrice = OrderModel.OrderRateInfo.LeavePrice + OrderModel.OrderRateInfo.LFuelPrice + OrderModel.OrderRateInfo.LBuildPrice;
                            OneTicketRefundPrice = TicketPrice - HandFee;
                            OneTicketTongyeFee = decimal.Parse((TicketPrice*Discount).ToString("F2"));
                        }
                        else if (OrderModel.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                        {
                            TicketPrice = OrderModel.OrderRateInfo.LeavePrice 
                                + OrderModel.OrderRateInfo.LFuelPrice 
                                + OrderModel.OrderRateInfo.LBuildPrice
                                + OrderModel.OrderRateInfo.ReturnPrice 
                                + OrderModel.OrderRateInfo.RFuelPrice 
                                + OrderModel.OrderRateInfo.RBuildPrice;
                            OneTicketRefundPrice = TicketPrice - HandFee;
                            OneTicketTongyeFee = decimal.Parse((TicketPrice*Discount).ToString("F2"));
                        }  
                    }

                    /*
                     * 计算 平台应退金额，供应商应退金额
                     * */
                    PlatformRefund = OneTicketTongyeFee * ChangePCount;
                    SRefundAmount = OneTicketRefundPrice * ChangePCount - PlatformRefund;

                    //判断 供应商所填的 退票手续费 是否大于等于 单张票的价格
                    if (HandFee >= TicketPrice)//大于等于
                    {
                        Utils.ResponseMeg(false, "填写的单人退票手续费 " + Utils.GetMoney(HandFee) + "元 ,不能大于单张票的价格 " + Utils.GetMoney(TicketPrice) + "元");
                    }

                    //判断一个人要退的钱 是否 小于 一张票要收的平台交易手续费
                    if (OneTicketRefundPrice < OneTicketTongyeFee)//小于
                    {
                        Utils.ResponseMeg(false, "单人要退的钱 " + Utils.GetMoney(OneTicketRefundPrice) + "元 ,不能小于单人所收的平台交易手续费 " + Utils.GetMoney(OneTicketTongyeFee)+"元");
                    }
                    #endregion

                    string BatchNo = string.Empty;

                    #region 写交易金额明细
                    //支付宝退款
                    if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                    {
                        // 判断供应商退款金额是否大于0,如果供应商退款的金额为0时直接调用平台退款
                        if (SRefundAmount > 0)//大于0
                        {
                            // 判断是否有写入支付记录
                            IList<EyouSoft.Model.TicketStructure.TicketPay> PayList = OrderBll.GetPayList(OrderModel.OrderId, EyouSoft.Model.TicketStructure.ItemType.供应商到平台_变更, string.Empty, string.Empty);
                            //if (PayList == null || PayList.Count == 0 || PayList.Where(item => item.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成).Count() == 0)
                            //{
                                // 供应商退款到平台
                                IsResult = OrderBll.BackOrDisableTicketBeforeGP(ChangeID, OrderModel.OrderNo, HandFee, SRefundAmount, SRefundAmount + PlatformRefund, SiteUserInfo.ID, OrderModel.PayType, SiteUserInfo.CompanyID, out BatchNo, "退票或作废");
                            //}
                        }
                        else//供应商退款的金额为0
                        {
                            // 判断是否有写入支付记录
                            IList<EyouSoft.Model.TicketStructure.TicketPay> PayList = OrderBll.GetPayList(OrderModel.OrderId, EyouSoft.Model.TicketStructure.ItemType.平台到采购商_变更, string.Empty, string.Empty);
                            //if (PayList == null || PayList.Count == 0 || PayList.Where(item => item.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成).Count() == 0)
                            //{
                                // 平台直接退款给采购商
                                IsResult = OrderBll.BackOrDisableTicketBeforePC(ChangeID, OrderModel.OrderNo, PlatformRefund, SiteUserInfo.ID, OrderModel.PayType, SiteUserInfo.CompanyID, "平台退款到采购商", out BatchNo);
                           // }
                        }
                    }
                    else//财付通退款
                    {
                        // 供应商退款到平台
                        IsResult = OrderBll.BackOrDisableTicketBeforeGP(ChangeID, OrderModel.OrderNo, HandFee, SRefundAmount, SRefundAmount + PlatformRefund, SiteUserInfo.ID, OrderModel.PayType, SiteUserInfo.CompanyID, out BatchNo, "退票或作废");
                    }

                    if (IsResult == false)
                    {
                        Utils.ResponseMeg(false, "写入支付记录操作失败，请稍候再试");
                    }
                    
                    #endregion

                    #region 供应商退款到平台

                    if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                    {
                        #region 支付宝退款
                        string partner = AlipayParameters.Partner;                                     //合作身份者ID
                        string key = AlipayParameters.Key;                         //安全检验码
                        string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
                        string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改

                        string batch_no = BatchNo;
                        string refund_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string batch_num = "1";
                        // 判断供应商退款金额是否大于0,如果供应商退款的金额为0时直接调用平台退款
                        if (SRefundAmount > 0)//大于0 ，从供应商先退款到平台   
                        {
                            #region
                            string notify_url = AlipayParameters.DomainPath + "/TicketsCenter/alipay/refund/notify_url.aspx";

                            Refund_Distribute_Parameter parameter = new Refund_Distribute_Parameter(
                                PayNumber,
                                AccountNumber, 
                                AlipayParameters.Seller_mailer, 
                                SRefundAmount.ToString("F2"), 
                                "供应商退款");
                            //string detail_data = "2010110849342347^0^理由|enowalipay1@163.com^^pay2@tongye114.com^^0.01^理由";
                            string detail_data = parameter.ToString();

                            RefundNoPwd refund = new RefundNoPwd(partner, key, sign_type, input_charset, notify_url, batch_no, refund_date, batch_num, detail_data);

                            string url = refund.Create_url();

                            CreateSSL ssl = new CreateSSL(url);

                            string responseFromServer = ssl.GetResponse();

                            Distribute_royalty_Result result = new Distribute_royalty_Result(responseFromServer);

                            IsResult = result.IsSuccess;

                            if (IsResult==false)
                            {
                                Utils.ResponseMeg(false, "调用接口进行退款失败（错误码："+result.ErrorCode+"）,请联系客服");
                            }
                            else
                            {
                                //因为支付宝的退款成功信息，是通过异步通知的方式通知
                                //返回到客户端后，在客户端需要启用 实时的请求 查询 数据库，查看退款成功或者失败
                                Response.Clear();
                                Response.Write(string.Format("{{success:'1',message:'{0}',paytype:'{1}',changeid:'{2}',batchno:'{3}'}}",
                                "退款请求提交成功，正在退款中...",
                                "2",
                                ChangeID,
                                batch_no));
                                Response.End();
                            }
                            #endregion
                        }
                        else//供应商退款的金额为0 平台直接退款给采购商
                        {
                            #region

                            //计算支付宝退款金额对应的手续费
                            decimal alipayFee = Refund_Platform_Parameter.ComputeAlipayFee(PlatformRefund);
                            string detail_data = string.Empty;
                            if (alipayFee > 0)
                            {
                                Refund_Platform_Parameter parameter = new Refund_Platform_Parameter(
                                    PayNumber, PlatformRefund.ToString("F2"), "平台退款",
                                    AlipayParameters.Seller_mailer,
                                    alipayFee.ToString("F2"), "平台退款");
                                detail_data = parameter.ToString();
                            }
                            else
                            {
                                Refund_Platform_Parameter parameter = new Refund_Platform_Parameter(
                                    PayNumber, PlatformRefund.ToString("F2"), "平台退款");
                                detail_data = parameter.ToString();
                            }
                            string notify_url = AlipayParameters.DomainPath + "/TicketsCenter/alipay/refund/notify2_url.aspx";


                            //Refund_Platform_Parameter parameter = new Refund_Platform_Parameter(PayNumber, PlatformRefund.ToString("F2"), "平台退款");
                           // string detail_data = parameter.ToString();

                            RefundNoPwd refund = new RefundNoPwd(partner, key, sign_type, input_charset, notify_url, batch_no, refund_date, batch_num, detail_data);

                            string url = refund.Create_url();

                            CreateSSL ssl = new CreateSSL(url);

                            string responseFromServer = ssl.GetResponse();

                            Distribute_royalty_Result result = new Distribute_royalty_Result(responseFromServer);

                            IsResult = result.IsSuccess;

                            if (IsResult == false)
                            {
                                Utils.ResponseMeg(false, "调用接口进行退款失败（错误码：" + result.ErrorCode + "）,请联系客服");
                            }
                            else
                            {
                                //因为支付宝的退款成功信息，是通过异步通知的方式通知
                                //返回到客户端后，在客户端需要启用 实时的请求 查询 数据库，查看退款成功或者失败
                                Response.Clear();
                                Response.Write(string.Format("{{success:'1',message:'{0}',paytype:'{1}',changeid:'{2}',batchno:'{3}'}}",
                                "退款请求提交成功，正在退款中...",
                                "2",
                                ChangeID,
                                batch_no));
                                Response.End();
                            }

                            #endregion
                        }

                        #endregion
                    }
                    else if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通)
                    {
                        IsResult = TenpayGet(OrderModel.OrderNo, OrderModel.TotalAmount, SRefundAmount, PlatformRefund, BatchNo, PayNumber, AccountNumber, 2);
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region 改期或改签
                    string ChangeID = string.Empty;
                    EyouSoft.Model.TicketStructure.OrderChangeInfo ChangeModel = orderChangeInfo;
                        //OrderBll.GetLatestChange(OrderModel.OrderId);
                    ChangeID = ChangeModel.ChangeId;

                    //进行改期或改签
                    IsResult = OrderBll.CheckOrderChange(ChangeID, SiteUserInfo.ID, string.Empty, EyouSoft.Model.TicketStructure.OrderChangeState.接受);
                    if (ChangeType == EyouSoft.Model.TicketStructure.OrderChangeType.改签)
                    {
                        Utils.ResponseMeg(IsResult, IsResult ? "改签成功" : "改签失败，请稍候再试");
                    }
                    else
                    {
                        Utils.ResponseMeg(IsResult, IsResult ? "改期成功" : "改期失败，请稍候再试");
                    }
                    #endregion
                }
            }
            return IsResult;
        }
        #endregion

        #region 财付通分账、退款
        /// <summary>
        /// 财付通分账、退款
        /// </summary>
        /// <param name="orderno">商家订单号</param>
        /// <param name="ordertotalfee">订单总金额</param>
        /// <param name="payprice">type=1(供应商分账金额),type=2(供应商应退金额)</param>
        /// <param name="platformprice">type=1(平台中间帐户分账金额),type=2(平台中间帐户应退金额)</param>
        /// <param name="batchno">批次号</param>
        /// <param name="paynumber">财付通交易号</param>
        /// <param name="accountnumber">供应商财付通帐户</param>
        /// <param name="type">type=1(分账),type=2(供应商退款到平台),type=3(平台退款到供应商)</param>
        /// <returns></returns>
        private bool TenpayGet(string orderno,decimal ordertotalfee,decimal payprice,decimal platformprice,string batchno,string paynumber,string accountnumber,int type)
        {
            bool IsResult = false;
            //商户号
            string bargainor_id = TenpayParameters.Bargainor_ID;
            //密钥
            string key = TenpayParameters.Key;

            //创建请求对象
            BaseSplitRequestHandler reqHandler = new BaseSplitRequestHandler(Context);

            //通信对象
            TenpayHttpClient httpClient = new TenpayHttpClient();

            //应答对象
            ScriptClientResponseHandler resHandler = new ScriptClientResponseHandler();

            #region 设置请求参数
            //-----------------------------
            //设置请求参数
            //-----------------------------
            reqHandler.init();
            reqHandler.setKey(key);

            if (type == 1)   // 分账
            {
                reqHandler.setGateUrl("https://mch.tenpay.com/cgi-bin/split.cgi");

                reqHandler.setParameter("cmdno", "3");
                reqHandler.setParameter("total_fee", (ordertotalfee * 100).ToString("F0"));				//商品金额,以分为单位
                //业务类型
                reqHandler.setParameter("bus_type", "97");
                reqHandler.setParameter("bus_args", accountnumber + "^" + (payprice * 100).ToString("F0") + "^1|" + TenpayParameters.Seller_mailer + "^" + (platformprice * 100).ToString("F0") + "^2");
            }
            else if (type == 2)    // 供应商退款
            {
                reqHandler.setGateUrl("https://mch.tenpay.com/cgi-bin/split_rollback.cgi");

                reqHandler.setParameter("cmdno", "95");
                reqHandler.setParameter("total_fee", (ordertotalfee * 100).ToString("F0"));
                //退款ID，同个ID财付通认为是同一个退款,格式为109+10位商户号+8位日期+7位序列号
                reqHandler.setParameter("refund_id", "109" + bargainor_id + batchno);
                reqHandler.setParameter("bus_type", "97");
                
                //设置退款请求中 的退款金额 ， 各帐户的退款金额   金额 以  分为单位
                string bus_args = ((payprice + platformprice) * 100).ToString("F0");//设置退款金额
                if (payprice > 0)
                {
                    bus_args += "|" + accountnumber + "^" + (payprice * 100).ToString("F0");
                }
                if (platformprice > 0)
                {
                    bus_args += "|" + TenpayParameters.Seller_mailer + "^" + (platformprice * 100).ToString("F0");
                }

                reqHandler.setParameter("bus_args",bus_args);
            }
            else   // 平台退款
            {
                reqHandler.setGateUrl("https://mch.tenpay.com/cgi-bin/refund_b2c_split.cgi");   // 平台退款
                reqHandler.setParameter("cmdno", "93");
                reqHandler.setParameter("total_fee", (ordertotalfee * 100).ToString("F0"));
                //退款ID，同个ID财付通认为是同一个退款,格式为109+10位商户号+8位日期+7位序列号
                reqHandler.setParameter("refund_id", "109" + bargainor_id + batchno);
                reqHandler.setParameter("refund_fee", ((payprice + platformprice) * 100).ToString("F0"));
            }
            reqHandler.setParameter("version", "4");
            reqHandler.setParameter("fee_type", "1");
            reqHandler.setParameter("bargainor_id", bargainor_id);		//商户号
            reqHandler.setParameter("sp_billno", orderno);				//商家订单号
            reqHandler.setParameter("transaction_id", paynumber);	//财付通交易单号
            reqHandler.setParameter("return_url", "http://127.0.0.1/");			//后台系统调用，必现填写为http://127.0.0.1/

            #endregion

            //-----------------------------
            //设置通信参数
            //-----------------------------
            //证书必须放在用户下载不到的目录，避免证书被盗取
            httpClient.setCertInfo(Server.MapPath(TenpayParameters.PfxPath), TenpayParameters.PfxPwd);

            string requestUrl = reqHandler.getRequestURL();
            //设置请求内容
            httpClient.setReqContent(requestUrl);
            //设置超时
            httpClient.setTimeOut(10);

            string rescontent = "";

            EyouSoft.IBLL.TicketStructure.ITicketOrder OrderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            IList<EyouSoft.Model.TicketStructure.TicketPay> tmpPayList = null;
            if (type == 2)
            {
                tmpPayList = OrderBll.GetPayList(string.Empty, EyouSoft.Model.TicketStructure.ItemType.供应商到平台_变更, orderno, batchno);
            }
            else if (type == 3)
            {
                tmpPayList = OrderBll.GetPayList(string.Empty, EyouSoft.Model.TicketStructure.ItemType.平台到采购商_变更, orderno, batchno);
            }

            //后台调用
            if (httpClient.call())
            {
                //获取结果
                rescontent = httpClient.getResContent();

                resHandler.setKey(key);
                //设置结果参数
                resHandler.setContent(rescontent);

                //判断签名及结果
                if (resHandler.isTenpaySign() && resHandler.getParameter("pay_result") == "0")//成功
                {
                    #region 分账或者退款成功
                    if (type == 1)   // 分账
                    {
                        // 分账成功，更新交易状态
                        IsResult = OrderBll.PayAfterCallBack(paynumber, payprice, EyouSoft.Model.TicketStructure.PayState.交易完成, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, accountnumber, "出票成功", orderno, DateTime.Now, batchno);
                        if (IsResult)
                        {
                            Utils.ResponseMeg(true, "分账成功，订单修改成功");
                        }
                        else
                        {
                            Utils.ResponseMeg(false, "分账成功，订单状态修改失败，请即时联系客服");
                        }
                    }
                    else if (type == 2)   // 供应商退款
                    {
                        if (tmpPayList != null && tmpPayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                            // 供应商退票到平台成功之后更新明细记录状态
                            bool tmp = OrderBll.PayAfterCallBack(paynumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易完成, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batchno);
                            // 供应商退款成功之后发起平台退款
                            if (tmp)//修改 供应商退款 支付记录成功
                            {
                                string tmpBatchNo = string.Empty;
                                // 平台退款到采购商 写入 支付记录
                                IsResult = OrderBll.BackOrDisableTicketBeforePC(PayModel.ItemId, PayModel.TradeNo, platformprice, PayModel.CurrUserId, PayModel.PayType, PayModel.CurrCompanyId, PayModel.Remark, out tmpBatchNo);

                                if (IsResult)
                                {
                                    //发起平台退款
                                    IsResult = TenpayGet(PayModel.TradeNo, ordertotalfee, payprice, platformprice, tmpBatchNo, paynumber, string.Empty, 3);
                                }
                                else
                                {
                                    Utils.ResponseMeg(false, "供应商退款到平台成功，供应商退款支付记录修改失败，平台退款到采购商失败");
                                }
                            }
                            else
                            {
                                Utils.ResponseMeg(false, "供应商退款到平台成功，供应商退款支付记录修改失败，平台退款到采购商失败");
                            }
                        }
                        tmpPayList = null;
                    }
                    else   // 平台退款
                    {
                        if (tmpPayList != null && tmpPayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                            // 拒绝出票完成后更新订单状态为‘拒绝出票’，并修改支付明细状态
                            IsResult = OrderBll.PayAfterCallBack(paynumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易完成, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batchno);

                            if (IsResult)
                            {
                                Utils.ResponseMeg(true, "退款成功，订单修改成功");
                            }
                            else
                            {
                                Utils.ResponseMeg(false, "退款成功，订单修改失败，请及时联系客服");
                            }
                            
                            PayModel = null;
                        }
                        tmpPayList = null;
                    }
                    #endregion
                }
                else
                {
                    #region 返回结果未签名
                    //错误时，返回结果未签名。
                    //如包格式错误或未确认结果的，请使用原来订单号重新发起，确认结果，避免多次操作
                    if (type == 1)   // 分账
                    {
                        OrderBll.PayAfterCallBack(paynumber, payprice, EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, accountnumber, resHandler.getParameter("pay_result"), orderno, DateTime.Now, batchno);
                        IsResult = false;
                        Utils.ResponseMeg(false, "分账失败（错误码：" + resHandler.getParameter("pay_result") + "），请稍候再试");
                    }  
                    else if (type == 2)     // 供应商退款
                    {
                        if (tmpPayList != null && tmpPayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                            OrderBll.PayAfterCallBack(paynumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batchno);
                            PayModel = null;
                            Utils.ResponseMeg(false, "供应商退款到平台失败（错误码：" + resHandler.getParameter("pay_result") + "），请稍候再试");
                        }
                        IsResult = false;
                        tmpPayList = null;
                    }
                    else   // 平台退款
                    { 
                        if (tmpPayList != null && tmpPayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                            EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(paynumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batchno);
                            PayModel = null;

                            Utils.ResponseMeg(false, 
                                "供应商退款到平台成功，平台退款到采购商失败（错误码：" + resHandler.getParameter("pay_result") + "），请及时联系客服");
                        }
                        IsResult = false;
                        tmpPayList = null;
                    }
                    #endregion
                }
            }
            else
            {
                #region 后台调用通信失败
                //后台调用通信失败
                if (type == 1)   // 分账
                {
                    OrderBll.PayAfterCallBack(paynumber, payprice, EyouSoft.Model.TicketStructure.PayState.交易失败, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, accountnumber, "出票失败-后台调用通信失败", orderno, DateTime.Now, batchno);
                    IsResult = false;
                    Utils.ResponseMeg(false, "操作失败，有可能因为网络原因，请求已经处理，但未收到应答。请及时检查相关帐户，并联系客服");
                }
                else if (type == 2)     // 供应商退款
                {
                    if (tmpPayList != null && tmpPayList.Count > 0)
                    {
                        EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                        OrderBll.PayAfterCallBack(paynumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batchno);
                        PayModel = null;
                    }
                    IsResult = false;
                    tmpPayList = null;
                    Utils.ResponseMeg(false, "操作失败，有可能因为网络原因，请求已经处理，但未收到应答。请及时检查相关帐户，并联系客服");
                }
                else
                {
                    if (tmpPayList != null && tmpPayList.Count > 0)
                    {
                        EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                        EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(paynumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, batchno);
                        PayModel = null;
                    }

                    Utils.ResponseMeg(false, "操作失败，有可能因为网络原因，请求已经处理，但未收到应答。请及时检查相关帐户，并联系客服");
                    IsResult = false;
                    tmpPayList = null;
                }
                #endregion
            }
            return IsResult;
        }
        #endregion

        /// <summary>
        /// 检查支付宝退款是否成功
        /// </summary>
        private void IsAlipayRefund()
        {
            OrderId = Utils.GetQueryStringValue("orderid");//订单ID
            //根据订单ID获取订单明细
            EyouSoft.IBLL.TicketStructure.ITicketOrder ibll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            orderInfo = ibll.GetOrderInfoById(OrderId);

            //判断订单是否存在
            if (orderInfo == null)//不存在
            {
                //提示用户当前订单不存在
                Utils.ResponseMeg(false,"对不起，当前订单不存在");
            }

            string changeId = Utils.GetQueryStringValue("changeid");
            string batchno = Utils.GetQueryStringValue("batchno");
            IList<EyouSoft.Model.TicketStructure.TicketPay> list = ibll.GetPayList(changeId, null, string.Empty,string.Empty);

            if (list != null && list.Count > 0)
            {
                IEnumerable<EyouSoft.Model.TicketStructure.TicketPay> orderedList = list.OrderBy(o => (int)o.ItemType);
                foreach (EyouSoft.Model.TicketStructure.TicketPay ticketpay in orderedList)
                {
                    if (ticketpay.ItemType == EyouSoft.Model.TicketStructure.ItemType.供应商到平台_变更)
                    {
                        if (ticketpay.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成)
                        {
                            Response.Clear();
                            Response.Write(
                                string.Format("{{success:'1',message:'供应商退款到平台中间帐户成功，正在进行平台到采购商退款...',search:'{0}'}}", "1"));
                            Response.End();
                        }
                        else if (ticketpay.PayState == EyouSoft.Model.TicketStructure.PayState.交易失败)
                        {
                            Utils.ResponseMeg(false, "操作失败（错误码：" + ticketpay.Remark + "），请及时联系客服");
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write(
                                string.Format("{{success:'1',message:'正在进行供应商到平台退款...',search:'{0}'}}", "1"));
                            Response.End();
                        }
                    }
                    else if (ticketpay.ItemType == EyouSoft.Model.TicketStructure.ItemType.平台到采购商_变更)
                    {
                        if (ticketpay.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成)
                        {
                            Utils.ResponseMeg(true, "退款成功，修改订单成功");
                        }
                        else if (ticketpay.PayState == EyouSoft.Model.TicketStructure.PayState.交易失败)
                        {
                            Utils.ResponseMeg(false, "操作失败（错误码：" + ticketpay.Remark + "），请及时联系客服");
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write(
                                string.Format("{{success:'1',message:'正在进行平台到采购商退款...',search:'{0}'}}", "1"));
                            Response.End();
                        }
                    }
                }
            }
            else
            {
                Utils.ResponseMeg(false, "退款失败，请稍候再试");
            }

        }

    }
}
