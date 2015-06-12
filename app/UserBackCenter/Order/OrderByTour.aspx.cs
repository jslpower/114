using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.Order
{

    /// <summary>
    /// 最新订单
    /// DYZ   2012-01-10
    /// 组团社 预定
    /// </summary>
    public partial class OrderByTour : BackPage
    {
        /// <summary>
        /// 容器ID
        /// </summary>
        protected string tblID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUserInfo.CompanyRole.HasRole(CompanyType.组团))
            {
                Response.Clear();
                Response.Write("<script>javascript: window.location.href='/default.aspx';</script>");
                Response.End();
                return;
            }
            string dotype = Utils.GetQueryStringValue("dotype");
            this.OrderCustomer1.TxtAudltID = this.txtAdultCount.ClientID;
            this.OrderCustomer1.TxtChildID = this.txtChildCount.ClientID;

            if (!IsPostBack)
            {
                tblID = Guid.NewGuid().ToString();
                string tourID = Utils.GetQueryStringValue("tourID");
                string adult = Utils.GetQueryStringValue("adult");
                string child = Utils.GetQueryStringValue("child");
                string contact = Server.UrlDecode(Utils.GetQueryStringValue("contact"));
                string tel = Server.UrlDecode(Utils.GetQueryStringValue("tel"));

                PageInit(tourID, adult, child, contact, tel);
            }

            if (dotype == "save")
            {
                Response.Clear();
                Response.Write(FormSave());
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
        private void PageInit(string tourID, string adultCount, string childCount, string contact, string tel)
        {

            EyouSoft.IBLL.NewTourStructure.IPowderList tourBll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();

            EyouSoft.Model.NewTourStructure.MPowderList model = tourBll.GetModel(tourID);

            if (model != null)
            {
                EyouSoft.IBLL.NewTourStructure.IRoute routeBll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
                EyouSoft.Model.NewTourStructure.MRoute routeModel = routeBll.GetModel(model.RouteId);

                this.lblRouteName.Text = "<a href='/PrintPage/LineTourInfo.aspx?RouteID=" + model.RouteId + "' target='_blank'>" + model.RouteName + "</a>";
                this.lblLeaveDate.Text = model.LeaveDate.ToString("yyyy-MM-dd") + "(" + Utils.GetDayOfWeek((int)model.LeaveDate.DayOfWeek) + ")";
                this.lblCount.Text = model.MoreThan.ToString();
                if (model.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                {
                    this.lbPrice_d.Text = model.AdultPrice.ToString("F0");
                }
                this.lblTourState.Text = model.PowderTourStatus.ToString();
                this.lblCompanyName.Text = " <a  target=\"_blank\" href=\"" + Utils.GetShopUrl(model.Publishers) + "\">" + model.PublishersName + "</a>";


                #region 获得专线公司信息
                EyouSoft.Model.CompanyStructure.CompanyInfo comModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(model.Publishers);
                if (comModel != null && comModel.ContactInfo != null)
                {
                    this.lblMq.Text = Utils.GetMQ(comModel.ContactInfo.MQ);
                    this.lblQQ.Text = Utils.GetQQ(comModel.ContactInfo.QQ);
                }

                #endregion

                this.lblLeaveCityandTraffic.Text = model.StartTraffic + " " + model.StartCityName + " ";
                this.lblEndCityandTraffic.Text = model.EndTraffic + " " + model.EndCityName + " ";
                this.lbLeavedate.Text = model.StartDate;
                this.lbEnddate.Text = model.EndDate;
                this.lblMsg.Text = model.SetDec;
                this.lblAllMsg.Text = model.TeamLeaderDec;


                if (contact != "")
                {
                    this.txtFzr.Value = contact;
                }
                else
                {
                    this.txtFzr.Value = SiteUserInfo.ContactInfo.ContactName;
                }

                if (tel != "")
                {
                    this.txtFzrTel.Value = tel;
                }
                else
                {
                    this.txtFzrTel.Value = SiteUserInfo.ContactInfo.Tel;
                }

                if (adultCount != "")
                {
                    this.txtAdultCount.Value = adultCount;
                }
                if (childCount != "")
                {
                    this.txtChildCount.Value = childCount;
                }

                #region 团队 价格信息
                //市场价
                this.lblRetailAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.RetailAdultPrice);
                this.lblRetailChildrenPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.RetailChildrenPrice);
                this.lblMarketPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.MarketPrice);

                //结算价
                this.lblSettlementAudltPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SettlementAudltPrice);
                this.lblSettlementChildrenPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SettlementChildrenPrice);
                #endregion

                //处理总金额
                //市场价总价
                this.lblReailPriceAll.Text = Utils.FilterEndOfTheZeroDecimal(Utils.GetInt(adultCount) * model.RetailAdultPrice + Utils.GetInt(childCount) * model.RetailChildrenPrice);
                //结算总价
                this.lblSettlePriceAll.Text = Utils.FilterEndOfTheZeroDecimal(Utils.GetInt(adultCount) * model.SettlementAudltPrice + Utils.GetInt(childCount) * model.SettlementChildrenPrice);



            }

        }

        private string FormSave()
        {
            //声明操作BLL
            EyouSoft.IBLL.NewTourStructure.IPowderList tourBll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
            //创建新的订单
            EyouSoft.Model.NewTourStructure.MTourOrder orderModel = new EyouSoft.Model.NewTourStructure.MTourOrder();

            string tourID = Utils.GetQueryStringValue("tourID");
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
            //游客备注
            string cusRemark = Utils.GetFormValue(this.txtCusRemark.UniqueID);
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
            orderModel.Add = addPrice;
            orderModel.AdultNum = adultCount;
            orderModel.Reduction = reductPrice;
            #region 获得计划中价格信息
            decimal reAdultPrice = 0;
            decimal reChilePrice = 0;
            decimal reOtherPrice = 0;
            decimal seAdultPrice = 0;
            decimal seChildPrice = 0;

            EyouSoft.Model.NewTourStructure.MPowderList tourModel = tourBll.GetModel(tourID);
            if (tourModel != null)
            {
                reAdultPrice = tourModel.RetailAdultPrice;
                reChilePrice = tourModel.RetailChildrenPrice;
                reOtherPrice = tourModel.MarketPrice;
                seAdultPrice = tourModel.SettlementAudltPrice;
                seChildPrice = tourModel.SettlementChildrenPrice;

                //订单实体赋值
                orderModel.BusinessNotes = "";
                orderModel.DayNum = tourModel.Day;
                orderModel.LateNum = tourModel.Late;
                orderModel.LeaveDate = tourModel.LeaveDate;
                orderModel.RouteId = tourModel.RouteId;
                orderModel.RouteName = tourModel.RouteName;
                orderModel.ScheduleNum = tourModel.OrderPeopleNum;
                orderModel.TourId = tourModel.TourId;
                orderModel.TourNo = tourModel.TourNo;

            }
            else
            {
                Response.Clear();
                Response.Write("error");
                Response.End();
            }
            #endregion
            orderModel.ChildPrice = reChilePrice;
            orderModel.ChildrenNum = childCount;
            orderModel.Customers = customerList;
            orderModel.IssueTime = DateTime.Now;
            orderModel.MarketPrice = reOtherPrice;
            orderModel.OperationMsg = "";
            orderModel.OperatorId = SiteUserInfo.ID;
            orderModel.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            orderModel.OrderStatus = EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社待处理;
            orderModel.PaymentStatus = EyouSoft.Model.NewTourStructure.PaymentStatus.游客未支付;
            orderModel.PersonalPrice = reAdultPrice;
            orderModel.SaveDate = null;
            orderModel.SettlementAudltPrice = seAdultPrice;
            orderModel.SettlementChildrenPrice = seChildPrice;
            orderModel.SingleRoomNum = otherCount;
            orderModel.VisitorNotes = cusRemark;
            orderModel.Travel = SiteUserInfo.CompanyID;
            orderModel.TravelContact = fzr;
            orderModel.TravelName = SiteUserInfo.CompanyName;
            orderModel.TravelNotes = remark;
            orderModel.TravelTel = fzrTel;
            orderModel.VisitorContact = contact;
            orderModel.VisitorNotes = cusRemark;
            orderModel.VisitorTel = conTactTel;
            //计划销售总价
            orderModel.TotalSalePrice = adultCount * reAdultPrice + childCount * reChilePrice + otherCount * reOtherPrice + addPrice;
            //计算结算总价
            orderModel.TotalSettlementPrice = adultCount * seAdultPrice + childCount * seChildPrice + reductPrice;

            if (EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().AddOrUpdTourOrder(orderModel))
            {
                #region 发送短信模块
                EyouSoft.Model.SystemStructure.MSysSettingInfo SettingInfoModel = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSysSetting();
                //获得组团公司实体
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo travelComModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                ////获得专线或地接公司实体
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo tourComModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(tourModel.Publishers);
                if (SettingInfoModel != null && travelComModel != null && tourComModel != null)
                {
                    if (SettingInfoModel.OrderSmsCompanyTypes.Contains(travelComModel.CompanyLev))
                    {
                        string sendMsg = SettingInfoModel.OrderSmsTemplate;
                        sendMsg = sendMsg.Replace("[预订公司]", SiteUserInfo.CompanyName);
                        sendMsg = sendMsg.Replace("[预订联系电话]", SiteUserInfo.ContactInfo.Mobile);
                        sendMsg = sendMsg.Replace("[预订出发时间]", tourModel.LeaveDate.ToString("yyyy-MM-dd"));
                        sendMsg = sendMsg.Replace("[预订产品]", tourModel.RouteName);
                        sendMsg = sendMsg.Replace("[预订数量]", orderModel.AdultNum.ToString() + "成," + orderModel.ChildrenNum.ToString() + "儿");

                        #region 发送操作
                        EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo = new EyouSoft.Model.SMSStructure.SendMessageInfo();
                        sendMessageInfo.CompanyId = orderModel.Travel;
                        sendMessageInfo.CompanyName = orderModel.TravelName;
                        sendMessageInfo.UserId = this.SiteUserInfo.ID;
                        sendMessageInfo.UserFullName = SiteUserInfo.ContactInfo.ContactName;
                        sendMessageInfo.SMSContent = sendMsg;
                        sendMessageInfo.SendTime = DateTime.Now;
                        //添加要发送的手机号码
                        List<EyouSoft.Model.SMSStructure.AcceptMobileInfo> moblieList = new List<EyouSoft.Model.SMSStructure.AcceptMobileInfo>();
                        EyouSoft.Model.SMSStructure.AcceptMobileInfo mobileModel = new EyouSoft.Model.SMSStructure.AcceptMobileInfo();
                        mobileModel.IsEncrypt = false;
                        if (tourComModel.ContactInfo != null)
                        {
                            mobileModel.Mobile = tourComModel.ContactInfo.Mobile;
                        }
                        moblieList.Add(mobileModel);
                        sendMessageInfo.Mobiles = moblieList;

                        //发送通道
                        EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[Convert.ToInt32(SettingInfoModel.OrderSmsChannelIndex)];
                        sendMessageInfo.SendChannel = sendChannel;
                        sendMessageInfo.SendType = EyouSoft.Model.SMSStructure.SendType.直接发送;
                        EyouSoft.IBLL.SMSStructure.ISendMessage sBll = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance();
                        EyouSoft.Model.SMSStructure.SendResultInfo SendResultModel = sBll.Send(sendMessageInfo);
                        SendResultModel = null;
                        sendMessageInfo = null;

                        #endregion
                    }
                }
                #endregion
                return "ok";
            }
            else
            {
                return "服务器忙，请稍后再试!";
            }
            #endregion



        }
    }
}
