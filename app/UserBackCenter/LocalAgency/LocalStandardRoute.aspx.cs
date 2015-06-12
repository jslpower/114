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

namespace UserBackCenter.LocalAgency
{
    /// <summary>
    /// 地接线路标准版
    /// 罗丽娥   2010-07-23
    /// </summary>
    public partial class LocalStandardRoute : EyouSoft.Common.Control.BackPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected string tblID = "LocalStandardRoute";
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected int MaxTourDays = 0;

        #region page_load
        protected void Page_Load(object sender, EventArgs e)
        {
            tblID = "LocalStandardRoute" + Guid.NewGuid().ToString();
            this.LocalStandardRoute_StandardPlan.ContainerID = tblID;
            this.LocalStandardRoute_tourpricestand.ContainerID = tblID;
            this.LocalStandardRoute_ServiceStandard.ContainerID = tblID;
            MaxTourDays = Utils.GetInt(System.Configuration.ConfigurationManager.AppSettings["MaxTourDays"]);

            this.Page.Title = "标准版发布地接线路信息";

            if (SiteUserInfo != null)
            {
                UserInfoModel = SiteUserInfo;
                this.LocalStandardRoute_tourpricestand.UserInfoModel = UserInfoModel;
                this.LocalStandardRoute_TourContactInfo.CompanyID = UserInfoModel.CompanyID;
                if (UserInfoModel.ContactInfo != null)
                {
                    this.LocalStandardRoute_TourContactInfo.ContactName = UserInfoModel.ContactInfo.ContactName;
                    this.LocalStandardRoute_TourContactInfo.ContactTel = UserInfoModel.ContactInfo.Tel;
                    this.LocalStandardRoute_TourContactInfo.ContactMQID = UserInfoModel.ContactInfo.MQ;
                }
                this.LocalStandardRoute_TourContactInfo.ContainerID = tblID;
            }
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(Utils.InputText(Request.QueryString["RouteId"])))
                    InitRouteInfo(Utils.InputText(Request.QueryString["RouteId"]));

                #region 权限处理
                if (!String.IsNullOrEmpty(this.LocalStandardRoute_hidRouteID.Value))
                {
                    if (!this.CheckGrant(EyouSoft.Common.TravelPermission.地接_线路管理))
                    {
                        Utils.ResponseNoPermit("对不起，您没有地接_维护线路_修改权限");
                        return;
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Common.TravelPermission.地接_线路管理))
                    {
                        Utils.ResponseNoPermit("对不起，您没有地接_新增线路权限");
                        return;
                    }
                }
                #endregion
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    if (!IsCompanyCheck)
                    {
                        Response.Write("对不起，您的账号未审核，不能进行操作!");
                    }
                    else
                    {
                        Response.Write(InsertRouteInfo());
                    }
                    Response.End();
                }
                else if (flag.Equals("Exist", StringComparison.OrdinalIgnoreCase))
                {
                    string RouteName = Utils.InputText(Request.QueryString["RouteName"]);
                    Response.Clear();
                    if (!String.IsNullOrEmpty(RouteName))
                    {
                        Response.Write(IsExists(RouteName));
                    }
                    else
                    {
                        Response.Write(false);
                    }
                    Response.End();
                }
            }
        }
        #endregion

        #region 修改初始化线路信息
        /// <summary>
        /// 修改初始化线路信息
        /// </summary>
        /// <param name="TourID"></param>
        private void InitRouteInfo(string RouteID)
        {
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            EyouSoft.Model.TourStructure.RouteBasicInfo model = bll.GetRouteInfo(RouteID);
            if (model != null) 
            {
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
                {
                    this.TabLocalQuickRoute.Visible = true;
                    this.TabLocalStandardRoute.Visible = false;
                }
                else
                {
                    this.TabLocalQuickRoute.Visible = false;
                    this.TabLocalStandardRoute.Visible = true;
                }

                if (!String.IsNullOrEmpty(Utils.InputText(Request.QueryString["type"])) && Utils.InputText(Request.QueryString["type"]) == "edit")
                {
                    this.LocalStandardRoute_hidRouteID.Value = model.ID;
                }
                this.LocalStandardRoute_RouteName.Value = model.RouteName;
                this.LocalStandardRoute_TourDays.Value = model.TourDays.ToString();

                // 报价等级
                this.LocalStandardRoute_tourpricestand.RoutePriceDetails = model.PriceDetails;
                // 行程安排
                this.LocalStandardRoute_StandardPlan.RouteStandardPlanInfo = model.StandardPlans;
                // 包含项目
                //this.LocalStandardRoute_ServiceStandard.RouteServiceStandardInfo = model.ServiceStandard;
                // 服务标准
                this.LocalStandardRoute_Remark.Value = model.ServiceStandard.SpeciallyNotice;
                this.LocalStandardRoute_Service.Value = model.ServiceStandard.NotContainService;

                // 线路负责人
                this.LocalStandardRoute_TourContactInfo.ContactName = model.ContactName;
                this.LocalStandardRoute_TourContactInfo.ContactTel = model.ContactTel;
                this.LocalStandardRoute_TourContactInfo.ContactMQID = model.ContactMQID;
                this.LocalStandardRoute_TourContactInfo.CompanyID = UserInfoModel.CompanyID;
            }
            model = null;
            bll = null;
        }
        #endregion

        #region 判断线路名称是否存在
        private bool IsExists(string RouteName)
        {
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            return bll.Exists(UserInfoModel.CompanyID, RouteName);
        }
        #endregion

        #region 添加线路信息
        private bool InsertRouteInfo()
        {
            string RouteName = Utils.GetFormValue(this.LocalStandardRoute_RouteName.UniqueID);
            string TourDays = Utils.GetFormValue(this.LocalStandardRoute_TourDays.UniqueID);
            string hidRouteID = Utils.GetFormValue(this.LocalStandardRoute_hidRouteID.UniqueID);
            // 线路负责人
            string ContactName = Utils.GetFormValue("LocalStandardRoute_TourContact");
            string ContactTel = Utils.GetFormValue("LocalStandardRoute_TourContactTel");
            string ContactMQID = Utils.GetFormValue("LocalStandardRoute_TourContacMQ");
            string ContactUserName = Utils.GetFormValue("LocalStandardRoute_TourContactUserName");
            // 服务标准
            string ResideContent = Utils.GetFormValue("LocalStandardRouteResideContent");
            string DinnerContent = Utils.GetFormValue("LocalStandardRouteDinnerContent");
            string SightContent = Utils.GetFormValue("LocalStandardRouteSightContent");
            string CarContent = Utils.GetFormValue("LocalStandardRouteCarContent");
            string GuideContent = Utils.GetFormValue("LocalStandardRouteGuideContent");
            string TrafficContent = Utils.GetFormValue("LocalStandardRouteTrafficContent");
            string IncludeOtherContent = Utils.GetFormValue("LocalStandardRouteIncludeOtherContent");
            string Service = Utils.GetFormValue(this.LocalStandardRoute_Service.UniqueID);
            string Remark = Utils.GetFormValue(this.LocalStandardRoute_Remark.UniqueID);

            EyouSoft.Model.TourStructure.RouteBasicInfo model = new EyouSoft.Model.TourStructure.RouteBasicInfo();
            model.CompanyID = UserInfoModel.CompanyID;
            model.OperatorID = UserInfoModel.ID;
            model.CompanyName = UserInfoModel.CompanyName;
            model.IssueTime = DateTime.Now;
            model.RouteName = RouteName;
            model.TourDays = int.Parse(TourDays);
            model.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Standard;
            model.PriceDetails = InsertRoutePriceDetail();
            model.AreaType = EyouSoft.Model.SystemStructure.AreaType.地接线路;
            model.StandardPlans = InsertRouteStandardPlan();
            model.ContactMQID = ContactMQID;
            model.ContactName = ContactName;
            model.ContactTel = ContactTel;
            model.ContactUserName = ContactUserName;
            // 服务标准
            EyouSoft.Model.TourStructure.RouteServiceStandard RouteServiceModel = new EyouSoft.Model.TourStructure.RouteServiceStandard();
            if (!String.IsNullOrEmpty(ResideContent)) RouteServiceModel.ResideContent = ResideContent;
            if (!String.IsNullOrEmpty(DinnerContent)) RouteServiceModel.DinnerContent = DinnerContent;
            if (!String.IsNullOrEmpty(CarContent)) RouteServiceModel.CarContent = CarContent;
            if (!String.IsNullOrEmpty(SightContent)) RouteServiceModel.SightContent = SightContent;
            if (!String.IsNullOrEmpty(GuideContent)) RouteServiceModel.GuideContent = GuideContent;
            if (!String.IsNullOrEmpty(TrafficContent)) RouteServiceModel.TrafficContent = TrafficContent;
            if (!String.IsNullOrEmpty(IncludeOtherContent)) RouteServiceModel.IncludeOtherContent = IncludeOtherContent;
            if (!String.IsNullOrEmpty(Service)) RouteServiceModel.NotContainService = Service;
            if (!String.IsNullOrEmpty(Remark)) RouteServiceModel.SpeciallyNotice = Remark;
            model.ServiceStandard = RouteServiceModel;
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            if (!String.IsNullOrEmpty(hidRouteID))
            {
                model.ID = hidRouteID;
                return bll.UpdateRouteInfo(model);
            }
            else {
                model.ID = Guid.NewGuid().ToString();
                return bll.InsertRouteInfo(model);
            }
        }

        #region 写入线路报价信息
        /// <summary>
        /// 写入线路报价信息
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.RoutePriceDetail> InsertRoutePriceDetail()
        {
            string[] PriceStand = Utils.GetFormValues("drpPriceRank");
            string[] hidCustomerLevelID = Utils.GetFormValues("hidCustomerLevelID");

            IList<EyouSoft.Model.TourStructure.RoutePriceDetail> priceList = new List<EyouSoft.Model.TourStructure.RoutePriceDetail>();
            int i = 0;
            if (PriceStand != null)
            {
                foreach (string pricestand in PriceStand)
                {
                    IList<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail> list = new List<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail>();
                    EyouSoft.Model.TourStructure.RoutePriceDetail priceModel = new EyouSoft.Model.TourStructure.RoutePriceDetail();
                    priceModel.PriceStandId = pricestand;

                    foreach (string customerlevelid in hidCustomerLevelID)
                    {
                        string[] PeoplePrice = Utils.GetFormValues("PeoplePrice" + customerlevelid);
                        string[] ChildPrice = Utils.GetFormValues("ChildPrice" + customerlevelid);

                        EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail model = new EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail();
                        if (!String.IsNullOrEmpty(PeoplePrice[i]) && Utils.GetDecimal(PeoplePrice[i]) != 0)
                            model.AdultPrice = decimal.Parse(PeoplePrice[i]);
                        else
                            model.AdultPrice = 0;
                        if (!String.IsNullOrEmpty(ChildPrice[i]) && Utils.GetDecimal(ChildPrice[i]) != 0)
                            model.ChildrenPrice = decimal.Parse(ChildPrice[i]);
                        else
                            model.ChildrenPrice = 0;
                        model.CustomerLevelId = int.Parse(customerlevelid);
                        list.Add(model);
                        model = null;
                    }
                    priceModel.PriceDetail = list;
                    list = null;
                    priceList.Add(priceModel);
                    priceModel = null;
                    i++;
                }
            }
            return priceList;
        }
        #endregion

        #region 写入线路行程信息
        /// <summary>
        /// 写入线路行程信息
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.RouteStandardPlan> InsertRouteStandardPlan()
        {
            string[] DayJourney = Utils.GetFormValues("DayJourney");
            IList<EyouSoft.Model.TourStructure.RouteStandardPlan> routeList = new List<EyouSoft.Model.TourStructure.RouteStandardPlan>();

            // 写入线路行程信息     
            if (DayJourney != null)
            {
                foreach (string dayplan in DayJourney)
                {
                    EyouSoft.Model.TourStructure.RouteStandardPlan model = new EyouSoft.Model.TourStructure.RouteStandardPlan();
                    model.PlanDay = int.Parse(dayplan);
                    model.PlanInterval = Utils.GetFormValue("SightArea" + dayplan);
                    model.Vehicle = Utils.GetFormValue("Vehicle" + dayplan);
                    model.TrafficNumber = Utils.GetFormValue("TrafficNumber" + dayplan);
                    model.House = Utils.GetFormValue("Resideplan" + dayplan);
                    model.Dinner = Utils.GetFormValue("DinnerPlan" + dayplan);
                    model.PlanContent = Utils.GetFormValue("JourneyInfo" + dayplan);
                    routeList.Add(model);
                    model = null;
                }
            }
            return routeList;
        }
        #endregion
        #endregion
    }
}

