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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 未出发团队：再发布
    /// 创建者：luofx 时间：2010-7-8
    /// </summary>
    public partial class AddTourAgain : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        private string CompanyID = string.Empty;
        protected int RouteArea = 0;
        protected string TourID = string.Empty;
        protected string TemplateTourID = string.Empty;
        protected string RouteName = string.Empty;
        protected string AreaName = string.Empty;
        //同行
        protected string PeerAdultPrice = string.Empty;
        protected string PeerChildrenPrice = string.Empty;
        //门市
        protected string StoreAdultPrice = string.Empty;
        protected string StoreChildrenPrice = string.Empty;
        protected int RecentLeaveCount = 0;
        protected int RemnantNumber = 0;
        protected string LeaveDate = string.Empty;
        protected DateTime LeaveTime;
        protected string LeaveDateDayOfWeek = string.Empty;
        //总人数
        private int PlanPeopleCount = 0;

        protected string ThisTableContainerID = "AddTourAgain" + Guid.NewGuid();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TourID = Utils.GetQueryStringValue("TourID");
            AddTourAgain_tourpricestand.ContainerID = ThisTableContainerID;
            AddTourAgain_tourpricestand.ReleaseType = "AddTourAgain";
            AddTourAgain_tourpricestand.UserInfoModel = this.SiteUserInfo;
            CompanyID = this.SiteUserInfo.CompanyID;
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "update")
            {
                Update();
                return;
            }
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
            if (!string.IsNullOrEmpty(TourID))
            {
                EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
                EyouSoft.Model.TourStructure.TourInfo model = new EyouSoft.Model.TourStructure.TourInfo();
                model = Ibll.GetTourInfo(TourID);
                if (model != null)
                {
                    TemplateTourID = model.ParentTourID;
                    RouteName = model.RouteName;
                    RouteArea = model.AreaId;
                    AreaName = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(model.AreaId).AreaName;
                    LeaveDate = model.LeaveDate.ToShortDateString();
                    LeaveTime = model.LeaveDate;
                    LeaveDateDayOfWeek = model.LeaveDate.DayOfWeek.ToString();
                    PeerAdultPrice = string.Format("{0:F2}", model.TravelAdultPrice);
                    PeerChildrenPrice = string.Format("{0:F2}", model.TravelChildrenPrice);

                    StoreAdultPrice = string.Format("{0:F2}", model.RetailAdultPrice);
                    StoreChildrenPrice = string.Format("{0:F2}", model.RetailChildrenPrice);
                    RemnantNumber = model.RemnantNumber;
                    #region 报价等级
                    List<EyouSoft.Model.TourStructure.TourPriceDetail> priceList = new List<EyouSoft.Model.TourStructure.TourPriceDetail>();
                    priceList = (List<EyouSoft.Model.TourStructure.TourPriceDetail>)model.TourPriceDetail;
                    AddTourAgain_tourpricestand.TourPriceDetails = model.TourPriceDetail;

                    IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> list = Ibll.GetChildrenTours(TemplateTourID);
                    if (list != null && list.Count > 0)
                    {
                        DateTime[] TimeList = new DateTime[list.Count];
                        TimeList = (from c in list where true select c.LeaveDate).ToArray();
                        string[] TourCodeList = new string[list.Count];
                        TourCodeList = (from c in list where true select c.TourCode).ToArray();
                        IsoDateTimeConverter iso = new IsoDateTimeConverter();
                        iso.DateTimeFormat = "yyyy-M-d";
                        this.AddTourAgain_hidChildLeaveDateList.Value = JsonConvert.SerializeObject(TimeList, iso);
                        this.AddTourAgain_hidChildTourCodeList.Value = JsonConvert.SerializeObject(TourCodeList);
                    }
                    priceList = null;
                    #endregion
                    if (model.ReleaseType == 0)
                    {
                        ltrAddNewPlan.Text = string.Format("<a href=\"{0}\" rel='TapAddTourAgain'>增加发布{1}计划</a>", "/routeagency/addstandardtour.aspx", model.AreaName);
                    }
                    else
                    {
                        ltrAddNewPlan.Text = string.Format("<a href=\"{0}\" rel='TapAddTourAgain'>增加发布{1}计划</a>", "/routeagency/addquicktour.aspx", model.AreaName);
                    }
                }
                Ibll = null;
                model = null;
            }
        }
        /// <summary>
        /// 提交发布
        /// </summary>
        private void Update()
        {
            string hidTourNo = Utils.GetFormValue("hidTourLeaveDate").Trim(',');
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = new EyouSoft.Model.TourStructure.TourInfo();
            model = Ibll.GetTourInfo(TourID); ;
            EyouSoft.IBLL.TourStructure.ITour TourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> ChildrensTourList = new List<EyouSoft.Model.TourStructure.ChildrenTourInfo>();
            string[] tmpLeaveDate = hidTourNo.Split(',');
            if (string.IsNullOrEmpty(hidTourNo.Trim(',')))
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'请选择出团日期！'}]");
                Response.End();
            }
            PlanPeopleCount = Utils.GetInt(Utils.GetFormValue("VistorNum"));
            if (PlanPeopleCount <= 0)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'计划人数必须大于0！'}]");
                Response.End();
            }
            DateTime[] LeaveDate = new DateTime[tmpLeaveDate.Length];

            if (!String.IsNullOrEmpty(hidTourNo))
            {
                for (int i = 0; i < tmpLeaveDate.Length; i++)
                {
                    LeaveDate[i] = Utils.GetDateTime(tmpLeaveDate[i]);
                }
            }
            //IList<EyouSoft.Model.TourStructure.TourPriceDetail> InputLists = InsertTourPriceDetail();

            if (model != null)
            {
                RouteArea = model.AreaId;
                #region 子团信息

                IList<EyouSoft.Model.TourStructure.AutoTourCodeInfo> TourCodeList = new List<EyouSoft.Model.TourStructure.AutoTourCodeInfo>();

                foreach (DateTime LeaveDateItem in LeaveDate)
                {
                    EyouSoft.Model.TourStructure.ChildrenTourInfo ChildTour = new EyouSoft.Model.TourStructure.ChildrenTourInfo();
                    ChildTour.TourCode = "";
                    ChildTour.ChildrenId = "";
                    ChildTour.LeaveDate = LeaveDateItem;
                    ChildrensTourList.Add(ChildTour);
                    ChildTour = null;
                }
                #endregion
                model.PlanPeopleCount = PlanPeopleCount;
                model.TourPriceDetail = InsertTourPriceDetail();
                model.Childrens = ChildrensTourList;
                model.ID = model.ParentTourID;
                model.CompanyID = SiteUserInfo.CompanyID;
                model.CompanyName = SiteUserInfo.CompanyName;
                int RowEffect = Ibll.AppendTemplateTourInfo(model);
                LeaveDate = null;
                model = null;
                Ibll = null;
                TourBll = null;
                ChildrensTourList = null;
                if (RowEffect > 0)
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:true,ErrorMessage:'发布成功！'}]");
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:false,ErrorMessage:'发布失败！'}]");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 团队报价信息
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.TourPriceDetail> InsertTourPriceDetail()
        {
            string[] PriceStand = Utils.GetFormValues("drpPriceRank");//价格等级编号
            string[] hidCustomerLevelID = Utils.GetFormValues("hidCustomerLevelID");//客户等级编号
            IList<EyouSoft.Model.TourStructure.TourPriceDetail> priceList = new List<EyouSoft.Model.TourStructure.TourPriceDetail>();
            
            int i = 0;
            if (PriceStand != null)
            {
                foreach (string pricestand in PriceStand)
                {
                    IList<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail> list = new List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>();
                    EyouSoft.Model.TourStructure.TourPriceDetail priceModel = new EyouSoft.Model.TourStructure.TourPriceDetail();
                    priceModel.PriceStandId = pricestand;                    
                    foreach (string customerlevelid in hidCustomerLevelID)
                    {
                        string[] PeoplePrice = Utils.GetFormValues("PeoplePrice" + customerlevelid);
                        string[] ChildPrice = Utils.GetFormValues("ChildPrice" + customerlevelid);
                        string CustomerType = Utils.GetFormValue("hidCustomerType" + customerlevelid);

                        EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail model = new EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail();
                        model.AdultPrice = Utils.GetDecimal(PeoplePrice[i]);
                        model.ChildrenPrice = Utils.GetDecimal(ChildPrice[i], 0);
                        model.CustomerLevelId = Utils.GetInt(customerlevelid);
                        list.Add(model);
                        model = null;
                    }
                    priceModel.PriceDetail = list;
                    priceList.Add(priceModel);
                    int PriceStandCount = 0;
                    PriceStandCount=priceList.Count(item => item.PriceStandId==pricestand);
                    if (PriceStandCount > 1)
                    {
                        Response.Clear();
                        Response.Write("[{isSuccess:false,ErrorMessage:'价格等级相同，保存失败！'}]");
                        Response.End();
                    }
                    list = null;
                    i++;
                }
            }
            return priceList;
        }
    }
}
