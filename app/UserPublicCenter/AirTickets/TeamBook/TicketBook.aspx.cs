using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserPublicCenter.AirTickets.TeamBook
{   
    /// <summary>
    /// 页面功能：预定机票
    /// 开发人：xuty 开发时间:2010-10-21
    /// </summary>
    public partial class TicketBook : EyouSoft.Common.Control.FrontPage
    {
        protected int touristCount;//旅客人数
        protected string companyId;//采购商公司ID
        protected string startDate;//出发时间
        protected string backDate;//返回时间
        protected string freightDate;//运价有效期
        protected string freightDateBack;//运价有效期
        protected string id;//运价ID
        protected string startCity;//出发地(编号|三字码)
        protected string toCity;//目的地(编号|三字码)
        protected string airType;//航班类型
        protected string startCityName;//始发地名称
        protected string toCityName;//目的地名称
        protected int peopleTypeInt;//旅客类型(内宾,外宾)
        protected EyouSoft.Model.TicketStructure.TravellerType peopleType;//旅客类型(内宾,外宾)
        protected EyouSoft.Model.TicketStructure.TicketFreightInfo info;//当前运价信息
        protected EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo supplierInfo;//供应商信息
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Naviagtion = AirTicketNavigation.团队预定散拼;
            this.Title = "预定-组团预定/散拼-机票";
             string method = Utils.GetFormValue("method");//当前操作
             if(method=="")//初次进来显示运价信息
             {
                startDate = Utils.GetQueryStringValue("startDate");
                backDate = Utils.GetQueryStringValue("backDate");
                startCity = Utils.GetQueryStringValue("startCity");
                toCity = Utils.GetQueryStringValue("toCity");
                airType = Utils.GetQueryStringValue("airType");
                peopleTypeInt = Utils.GetInt(Utils.GetQueryStringValue("peopleType"));
                peopleType = (EyouSoft.Model.TicketStructure.TravellerType)peopleTypeInt;
                id = Utils.GetQueryStringValue("id");
                info = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().GetModel(id);//获取运价信息实体
               
                if (info != null)
                {
                    freightDate = GetDate(info);
                    freightDateBack = GetBackDate(info);
                    startCityName = info.NoGadHomeCityIdName;
                    toCityName = info.NoGadDestCityName;
                    supplierInfo = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(info.Company.ID);
                    //touristCount = info.MaxPCount;
                    touristCount = 100;
                    companyId = SiteUserInfo.CompanyID;
                }
             }
             if (method == "save")//下订单
             {
                id = Utils.GetFormValue("id");
                info = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().GetModel(id);
                supplierInfo = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(info.Company.ID);
                string[] touristNames = Utils.GetFormValues("tb_tourName1");
                string[] tourTypes = Utils.GetFormValues("tb_tourType1");
                string[] tourSex = Utils.GetFormValues("tb_tourSex1");
                string[] cerTypes = Utils.GetFormValues("tb_cerType1");
                string[] cerNos = Utils.GetFormValues("tb_cerNo1");
                int length = touristNames.Length;
                List<EyouSoft.Model.TicketStructure.TicketVistorInfo> visitorList=new List<EyouSoft.Model.TicketStructure.TicketVistorInfo>();
                List<EyouSoft.Model.TicketStructure.OrderTravellerInfo> orderVisitorList=new List<EyouSoft.Model.TicketStructure.OrderTravellerInfo>();
                int isTableNum=0;//是否购买行程单的人数
                EyouSoft.IBLL.TicketStructure.ITicketVisitor visitorBll = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance();
                 for (int i = 0; i < length; i++)
                {
                    EyouSoft.Model.TicketStructure.TicketVistorInfo visitorInfo = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                    EyouSoft.Model.TicketStructure.OrderTravellerInfo orderVisitorInfo=new EyouSoft.Model.TicketStructure.OrderTravellerInfo();
                    //添加订单旅客
                    orderVisitorInfo.TravellerName=touristNames[i];
                    orderVisitorInfo.CertNo=cerNos[i];
                    orderVisitorInfo.TravellerId = Guid.NewGuid().ToString();
                    orderVisitorInfo.CertType=(EyouSoft.Model.TicketStructure.TicketCardType) Utils.GetInt(cerTypes[i]);
                    orderVisitorInfo.Gender=(EyouSoft.Model.CompanyStructure.Sex)Utils.GetInt(tourSex[i]);
                    orderVisitorInfo.InsPrice=0;
                    orderVisitorInfo.TravellerType = (EyouSoft.Model.TicketStructure.TicketVistorType)Utils.GetInt(tourTypes[i]);
                    orderVisitorInfo.IsBuyIns=Utils.GetFormValue("tb_isInsure1_" + (i + 1))=="1";
                    orderVisitorInfo.IsBuyItinerary=Utils.GetFormValue("tb_isTable1_" + (i + 1))=="1";
                    if(orderVisitorInfo.IsBuyItinerary)
                        isTableNum++;
                    orderVisitorInfo.TravellerState=EyouSoft.Model.TicketStructure.TravellerState.正常;
                    orderVisitorList.Add(orderVisitorInfo);
                 
                    if (Utils.GetFormValue("tb_isOften1_" + (i + 1)) == "1")
                    { 
                        //添加常旅客
                        if (IsLetter(touristNames[i]))
                            visitorInfo.EnglishName = touristNames[i];
                        else
                            visitorInfo.ChinaName = touristNames[i];
                        visitorInfo.Id = orderVisitorInfo.TravellerId;
                        visitorInfo.CardType = (EyouSoft.Model.TicketStructure.TicketCardType)Utils.GetInt(cerTypes[i]);
                        visitorInfo.CardNo = cerNos[i];
                        visitorInfo.VistorType = (EyouSoft.Model.TicketStructure.TicketVistorType)Utils.GetInt(tourTypes[i]);
                        visitorInfo.IssueTime = DateTime.Now;
                        visitorInfo.ContactSex = orderVisitorInfo.Gender;
                        visitorInfo.CompanyId = SiteUserInfo.CompanyID;
                        if(!visitorBll.VisitorIsExists(visitorInfo.CardNo,SiteUserInfo.CompanyID,""))
                         visitorList.Add(visitorInfo);
                    }
                }
                 //添加常旅客到数据库
               
                visitorBll.AddTicketVisitorList(visitorList);
                
                //构造订单信息
                EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll=EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                EyouSoft.Model.TicketStructure.OrderInfo orderInfo=new EyouSoft.Model.TicketStructure.OrderInfo();
                orderInfo.Travellers = orderVisitorList;
                orderInfo.TravellerType = (EyouSoft.Model.TicketStructure.TravellerType)Utils.GetInt(Utils.GetFormValue("peopleType"));
                if (info.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                {
                    orderInfo.TotalAmount = (info.FromSetPrice + info.ToSetPrice + info.FromFuelPrice + info.ToFuelPrice + info.ToBuildPrice + info.FromBuildPrice) * length + supplierInfo.ServicePrice * isTableNum;

                 }
                else
                {
                    orderInfo.TotalAmount = (info.FromSetPrice + info.FromFuelPrice + info.FromBuildPrice) * length + supplierInfo.ServicePrice * isTableNum;
                }
                if (isTableNum > 0)
                {
                    orderInfo.TotalAmount += supplierInfo.DeliveryPrice;
                }
                orderInfo.BuyerCId = SiteUserInfo.CompanyID;
                orderInfo.BuyerCName = SiteUserInfo.CompanyName;
                orderInfo.BuyerContactAddress = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID).CompanyAddress;
                orderInfo.BuyerContactMobile = SiteUserInfo.ContactInfo.Mobile;
                orderInfo.BuyerContactMQ = SiteUserInfo.ContactInfo.MQ;
                orderInfo.BuyerContactName = SiteUserInfo.ContactInfo.ContactName;
                orderInfo.BuyerRemark =Utils.InputText(Utils.GetFormValue("tb_remark"),250);//特殊要求备注
                orderInfo.BuyerUId = SiteUserInfo.ID;
                orderInfo.DestCityId = info.NoGadDestCityId;
                orderInfo.DestCityName = info.NoGadDestCityName;
                orderInfo.EMSPrice = supplierInfo.DeliveryPrice;
                orderInfo.FlightId = info.FlightId;
                orderInfo.FreightType = info.FreightType;
                orderInfo.HomeCityId = info.NoGadHomeCityId;
                orderInfo.HomeCityName = info.NoGadHomeCityIdName;
                orderInfo.ItineraryPrice = supplierInfo.ServicePrice;
                orderInfo.LeaveTime = DateTime.Parse(Utils.GetFormValue("startDate"));
                EyouSoft.Model.TicketStructure.OrderRateInfo rateInfo = new EyouSoft.Model.TicketStructure.OrderRateInfo();
                rateInfo.DestCityId = info.NoGadDestCityId;
                rateInfo.DestCityName = info.NoGadDestCityName;
                rateInfo.FlightId = info.FlightId;
                rateInfo.FreightType = info.FreightType;
                rateInfo.HomeCityId = info.NoGadHomeCityId;
                rateInfo.HomeCityName = info.NoGadHomeCityIdName;
                rateInfo.LBuildPrice = info.FromBuildPrice;
                rateInfo.LeaveDiscount = info.FromReferRate;
                rateInfo.LeaveFacePrice = info.FromReferPrice;
                rateInfo.LeavePrice = info.FromSetPrice;
                //计算去程运价有效期
                rateInfo.LeaveTimeLimit = GetDate(info);
                rateInfo.LFuelPrice = info.FromFuelPrice;
                rateInfo.MaxPCount = info.MaxPCount;
                rateInfo.RBuildPrice = info.ToBuildPrice;
                rateInfo.ReturnDiscount = info.ToReferRate;
                rateInfo.ReturnFacePrice = info.ToReferPrice;
                rateInfo.ReturnPrice = info.ToSetPrice;
              //计算回程运价有效期
               rateInfo.ReturnTimeLimit = GetBackDate(info);
               rateInfo.RFuelPrice = info.ToFuelPrice;
                rateInfo.SupplierRemark = info.SupplierRemark;
                orderInfo.OrderRateInfo = rateInfo;
                string act=Utils.GetFormValue("act");
                if (act == "0")
                    orderInfo.OrderState = EyouSoft.Model.TicketStructure.OrderState.等待审核;
                else
                    orderInfo.OrderState = EyouSoft.Model.TicketStructure.OrderState.审核通过;
                orderInfo.OrderTime = DateTime.Now;
                orderInfo.PCount = length;
                orderInfo.RateType = info.RateType;
                orderInfo.ReturnTime = Utils.GetDateTime(Utils.GetFormValue("backDate"),DateTime.Now);
                orderInfo.SupplierCId = info.Company.ID;
                orderInfo.SupplierCName = info.Company.CompanyName;
              
                orderInfo.OrderId = Guid.NewGuid().ToString();
                if (orderBll.CreateOrder(orderInfo))
                {
                    Utils.ResponseMeg(true, orderInfo.OrderId);
                }
                else
                {
                    Utils.ResponseMegError();
                }
                
                
            }
        }
        /// <summary>
        /// 验证首字母是否为字母
        /// </summary>
        /// <param name="CheckString"></param>
        /// <returns></returns>
        private bool IsLetter(string CheckString)
        {
            bool IsTrue = false;
            if (!string.IsNullOrEmpty(CheckString))
            {
                Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                IsTrue = rx.IsMatch(CheckString.Substring(0, 1));
                rx = null;
            }
            return !IsTrue;
        }
        /// <summary>
        /// 计算去程运价有效期
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected string GetDate(EyouSoft.Model.TicketStructure.TicketFreightInfo info)
        {
            if (info.FromSelfDate.HasValue)
                return info.FromSelfDate.Value.ToString("yyyy-MM-dd");
            else if (info.FromForDay != string.Empty)
                return info.FromForDay;
            else
            {
                string thedate = "";
                if (info.FreightStartDate.HasValue)
                    thedate = info.FreightStartDate.Value.ToString("yyyy-MM-dd");
                if (info.FreightEndDate.HasValue)
                    thedate += "至" + info.FreightEndDate.Value.ToString("yyyy-MM-dd");
                if (thedate == "")
                    thedate = "无有效期";
                return thedate;
            }
        }
        /// <summary>
        /// 计算回程程运价有效期
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected string GetBackDate(EyouSoft.Model.TicketStructure.TicketFreightInfo info)
        {
            if (info.ToSelfDate.HasValue)
                return info.ToSelfDate.Value.ToString("yyyy-MM-dd");
            else if (info.ToForDay != string.Empty)
                return info.ToForDay;
            else
            {
                string thedate = "";
                if (info.FreightStartDate.HasValue)
                    thedate = info.FreightStartDate.Value.ToString("yyyy-MM-dd");
                if (info.FreightEndDate.HasValue)
                    thedate += "至" + info.FreightEndDate.Value.ToString("yyyy-MM-dd");
                if (thedate == "")
                    thedate = "无有效期";
                return thedate;
            }
        }
    }
}
