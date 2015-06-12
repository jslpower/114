using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace SiteOperationsCenter.LineManage.RouteAgency
{
    public partial class AddOrderByRoute : YunYingPage
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

                this.lblRouteName.Text = "<a href='/PrintPage/LineTourInfo.aspx?routeID=" + model.RouteId + "' target='_blank'>" + model.RouteName + "</a>";
                this.lblLeaveDate.Text = model.LeaveDate.ToString("yyyy-MM-dd");
                this.lblCount.Text = model.MoreThan.ToString();

                this.lblLeaveCity.Text = model.StartCityName;
                this.lblCar.Text = model.StartTraffic + " &nbsp; " + model.EndTraffic;
                this.lblLeaveCon.Text = model.StartDate;
                this.lblBackCon.Text = model.EndDate;
                this.lblMsg.Text = model.SetDec;
                this.lblAllMsg.Text = model.TeamLeaderDec;


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
                this.lblReailPriceAll.Text = "0";
                //结算总价
                this.lblSettlePriceAll.Text = "0";


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
            string travelID = Utils.GetFormValue(this.hideTravelID.UniqueID);
            string travelName = Utils.GetFormValue(this.txtTravel.UniqueID);
            string fzr = Utils.GetFormValue(this.txtFzr.UniqueID);
            //负责人电话
            string fzrTel = Utils.GetFormValue(this.txtFzrTel.UniqueID);
           
            //增减结算价
            decimal addPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtAddPrice.UniqueID));
            //游客备注
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
                    model.CompanyId = "";//SiteUserInfo.CompanyID
                    if (cbxVisitor.Contains((i + 1).ToString()))
                    {
                        model.IsSaveToTicketVistorInfo = true;
                    }
                    model.ContactTel = txtTel[i];
                    model.CradType = sltChild[i] == "1" ? EyouSoft.Model.TicketStructure.TicketVistorType.成人 : EyouSoft.Model.TicketStructure.TicketVistorType.儿童;
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
            orderModel.Reduction = addPrice;
            orderModel.AdultNum = adultCount;

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
            #endregion
            orderModel.ChildPrice = reChilePrice;
            orderModel.ChildrenNum = childCount;
            orderModel.Customers = customerList;
            orderModel.IssueTime = DateTime.Now;
            orderModel.MarketPrice = reOtherPrice;
            orderModel.OperationMsg = "";
            orderModel.OperatorId = "";//SiteUserInfo.ID;
            orderModel.OperatorName = "";// SiteUserInfo.ContactInfo.ContactName;
            orderModel.OrderStatus = EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社待处理;
            orderModel.PaymentStatus = EyouSoft.Model.NewTourStructure.PaymentStatus.游客未支付;
            orderModel.PersonalPrice = reAdultPrice;
            orderModel.SaveDate = null;
            orderModel.SettlementAudltPrice = seAdultPrice;
            orderModel.SettlementChildrenPrice = seChildPrice;
            orderModel.SingleRoomNum = otherCount;
            orderModel.Travel = travelID; 
            orderModel.TravelName = travelName;
            orderModel.TravelContact = fzr;
            orderModel.TravelTel = fzrTel;
            orderModel.VisitorContact = contact;
            orderModel.VisitorTel = conTactTel;

            orderModel.BusinessNotes = remark;
            orderModel.TotalSalePrice = adultCount * reAdultPrice + childCount * reChilePrice + otherCount * reOtherPrice + addPrice;
            orderModel.TotalSettlementPrice = adultCount * seAdultPrice + childCount * seChildPrice;

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
    }
}
