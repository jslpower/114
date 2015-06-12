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
    /// 地接线路快速发布
    /// 罗丽娥   2010-07-22
    /// </summary>
    public partial class LocalQuickRoute : EyouSoft.Common.Control.BackPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected string tblID = "LocalQuickRoute", FCKID = "LocalQuickRoute_divFCK";
        protected string QuickPlan = string.Empty;
        protected EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        private string ContactName = string.Empty, ContactTel = string.Empty, ContactMQID = "0";
        protected int MaxTourDays = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblID = "LocalQuickRoute" + Guid.NewGuid().ToString();
            FCKID = "LocalQuickRoute_divFCK" + Guid.NewGuid().ToString().Replace("-", "");
            this.LocalQuickRoute_tourpricestand.ContainerID = tblID;
            MaxTourDays = Utils.GetInt(System.Configuration.ConfigurationManager.AppSettings["MaxTourDays"]);

            if (SiteUserInfo != null)
            {
                UserInfoModel = SiteUserInfo;
                this.LocalQuickRoute_tourpricestand.UserInfoModel = UserInfoModel;
                if (UserInfoModel.ContactInfo != null)
                {
                    ContactName = UserInfoModel.ContactInfo.ContactName;
                    ContactTel = UserInfoModel.ContactInfo.Tel;
                    ContactMQID = UserInfoModel.ContactInfo.MQ;
                }
            }
            this.Page.Title = "快速发布地接线路信息";
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(Utils.InputText(Request.QueryString["RouteId"])))
                    InitRouteInfo(Utils.InputText(Request.QueryString["RouteId"]));

                #region 权限处理
                if (!String.IsNullOrEmpty(this.LocalQuickRoute_hidRouteID.Value))
                {
                    if (!this.CheckGrant(EyouSoft.Common.TravelPermission.地接_线路管理))
                    {
                        Utils.ResponseNoPermit("对不起，您没有地接_维护线路_修改权限");
                        return; 
                    }
                }
                else {
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
                string flag = Utils.InputText(Request.QueryString["flag"]);
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
                else {
                    this.TabLocalQuickRoute.Visible = false;
                    this.TabLocalStandardRoute.Visible = true;
                }

                if (!String.IsNullOrEmpty(Utils.InputText(Request.QueryString["type"])) && Utils.InputText(Request.QueryString["type"]) == "edit")
                {
                    this.LocalQuickRoute_hidRouteID.Value = model.ID;
                }
                this.LocalQuickRoute_RouteName.Value = model.RouteName;
                this.LocalQuickRoute_TourDays.Value = model.TourDays.ToString();

                // 报价等级
                this.LocalQuickRoute_tourpricestand.RoutePriceDetails = model.PriceDetails;
                QuickPlan = model.QuickPlan;
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
            string RouteName = Utils.GetFormValue(this.LocalQuickRoute_RouteName.UniqueID);
            string TourDays = Utils.GetFormValue(this.LocalQuickRoute_TourDays.UniqueID);
            string QuickPlan = Utils.EditInputText(Server.UrlDecode(Request.Form["LocalQuickRoute_divFCK"]));
            if (QuickPlan == string.Empty || QuickPlan == "点击添加行程信息")
            {
                QuickPlan = string.Empty;
            }
            string hidRouteID = Utils.GetFormValue(this.LocalQuickRoute_hidRouteID.UniqueID);

            EyouSoft.Model.TourStructure.RouteBasicInfo model = new EyouSoft.Model.TourStructure.RouteBasicInfo();
            model.RouteName = RouteName;
            model.TourDays = int.Parse(TourDays);
            model.CompanyID = UserInfoModel.CompanyID;
            model.CompanyName = UserInfoModel.CompanyName;
            model.ContactMQID = ContactMQID;
            model.ContactName = ContactName;
            model.ContactTel = ContactTel;
            model.ContactUserName = UserInfoModel.UserName;
            model.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Quick;
            model.PriceDetails = InsertRoutePriceDetail();
            model.AreaType = EyouSoft.Model.SystemStructure.AreaType.地接线路;
            model.QuickPlan = QuickPlan;
            model.IsAccept = false;
            model.OperatorID = UserInfoModel.ID;
            model.IssueTime = DateTime.Now;
            model.AreaId = 0;
            model.LeaveCityId = 0;
            model.RouteTheme = null;
            model.SaleCity = null;
            model.ServiceStandard = null;
            model.StandardPlans = null;
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            if (!String.IsNullOrEmpty(hidRouteID))
            {
                model.ID = hidRouteID;
                return bll.UpdateRouteInfo(model);
            }
            else
            {
                model.ID = Guid.NewGuid().ToString();
                return bll.InsertRouteInfo(model);
            }
        }

        #region 写入线路报价信息
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
        #endregion
    }
}
