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
namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 未出发团队：预订操作
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class Booking : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 线路名称
        /// </summary>
        protected string RouteName = string.Empty;
        /// <summary>
        /// 出发时间
        /// </summary>
        protected string LeaveDate = string.Empty;
        /// <summary>
        /// 出发时：周几
        /// </summary>
        protected string leaveDateDay = string.Empty;
        /// <summary>
        /// 剩余人数
        /// </summary>
        protected int RemnantNumber = 0;
        protected string TourID = string.Empty;
        /// <summary>
        /// 模板团ID
        /// </summary>
        private string ParentTourID = string.Empty;

        protected string FromCompanyID = string.Empty;
        protected string PriceStandId = string.Empty;

        protected string Traffic = string.Empty;
        protected string SpecialContent = string.Empty;
        protected bool IsGrantUpdate = false;
        private string SeatList = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_管理栏目))
            //{
            //    Response.Clear();
            //    Response.Write("对不起，你正在使用的帐号没有操作改页面的权限！");
            //    Response.End();
            //}
            IsGrantUpdate = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.专线_未出团计划_修改);

            if (Request.QueryString["TourID"] != null && !string.IsNullOrEmpty(Request.QueryString["TourID"]))
            {
                TourID = Utils.InputText(Request.QueryString["TourID"]);
            }
            ParentTourID = Utils.GetQueryStringValue("ParentTourID");
            if (Utils.GetQueryStringValue("action") == "TourBook")
            {
                LeaveBook();
                return;
            }
            if (Utils.GetQueryStringValue("action") == "LeaveBook")
            {
                LeaveBook();
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
            BindingPriceCustomerLeaveDetail();
            #region 预订
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo Model = new EyouSoft.Model.TourStructure.TourInfo();
            Model = Ibll.GetTourInfo(TourID);
            if (Model != null)
            {
                FromCompanyID = Model.CompanyID;
                RouteName = Model.RouteName;
                LeaveDate = Model.LeaveDate.ToShortDateString();
                leaveDateDay = Utils.ConvertWeekDayToChinese(Model.LeaveDate);
                RemnantNumber = Model.RealRemnantNumber;
                Traffic = Model.LeaveTraffic;
                if (Model.ServiceStandard != null)
                {
                    SpecialContent = Model.ServiceStandard.SpeciallyNotice;
                }
                int HasIn = Model.CollectAdultNumber + Model.CollectChildrenNumber;
                if (HasIn >= Model.PlanPeopleCount)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('实收人数已经等于或者大于实际计划人数！');closeWin()");
                    Ibll = null;
                    Model = null;
                    return;
                }
            }
            Ibll = null;
            Model = null;
            #endregion
        }
        /// <summary>
        /// 绑定价格等级
        /// </summary>
        private void BindingPriceCustomerLeaveDetail()
        {
            #region 价格等级信息
            IList<EyouSoft.Model.TourStructure.TourPriceDetail> PriceLists = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourPriceDetail(TourID);
            List<CompanyPriceDetail> newLists = new List<CompanyPriceDetail>();
            CompanyPriceDetail cpModel = null;
            if (PriceLists != null && PriceLists.Count > 0)
            {
                PriceStandId = PriceLists[0].PriceStandId;
                ((List<EyouSoft.Model.TourStructure.TourPriceDetail>)PriceLists).ForEach(item =>
                {
                    cpModel = new CompanyPriceDetail();
                    cpModel.PriceStandName = item.PriceStandName;
                    cpModel.PriceStandId = item.PriceStandId;
                    ((List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>)item.PriceDetail).ForEach(childItem =>
                    {
                        switch (childItem.CustomerLevelType)
                        {
                            case EyouSoft.Model.CompanyStructure.CustomerLevelType.同行:
                                cpModel.AdultPrice1 = childItem.AdultPrice;
                                cpModel.ChildrenPrice1 = childItem.ChildrenPrice;
                                break;
                            case EyouSoft.Model.CompanyStructure.CustomerLevelType.门市:
                                cpModel.AdultPrice2 = childItem.AdultPrice;
                                cpModel.ChildrenPrice2 = childItem.ChildrenPrice;
                                break;
                            case EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差:
                                cpModel.SingleRoom1 = childItem.AdultPrice;
                                cpModel.SingleRoom2 = childItem.ChildrenPrice;
                                break;
                        }
                    });
                    newLists.Add(cpModel);
                    cpModel = null;
                });
                this.rptPrice.DataSource = newLists;
                this.rptPrice.DataBind();
                newLists = null;
            }
            PriceLists = null;
            #endregion
        }
        /// <summary>
        /// 绑定价格明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //    Repeater rptChild = new Repeater();
                //    rptChild = (Repeater)e.Item.FindControl("rptPriceCustomerLeaveDetail");
                //    EyouSoft.Model.TourStructure.TourPriceDetail model = (EyouSoft.Model.TourStructure.TourPriceDetail)e.Item.DataItem;
                //    List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail> TourPricelists = (List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>)model.PriceDetail;//
                //    //查找同行价格项
                //    TourPricelists.Select(delegate(EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail item)
                //    {
                //        return item.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.同行;
                //    });
                //    if (rptChild != null)
                //    {
                //        rptChild.DataSource = TourPricelists;
                //        rptChild.DataBind();
                //    }
            }
        }
        /// <summary>
        /// 代客预订
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeaveBook()
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的账户还未审核，没有权限执行该操作！'}]");
                Response.End();
            }
            string ReturnString = string.Empty;
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo tourModel = new EyouSoft.Model.TourStructure.TourInfo();
            tourModel = Ibll.GetTourInfo(TourID);
            PriceStandId = Utils.GetFormValue("hidPriceStandId");
            EyouSoft.IBLL.TourStructure.ITourOrder Orderbll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            EyouSoft.Model.TourStructure.TourOrder orderModel = new EyouSoft.Model.TourStructure.TourOrder();
            if (Utils.GetQueryStringValue("action") == "LeaveBook")
            {
                orderModel.OrderState = EyouSoft.Model.TourStructure.OrderState.已留位;
                orderModel.SaveSeatDate = Utils.GetDateTime(Utils.GetFormValue("txtEndTime$dateTextBox"));
                if (Utils.GetDateTime(Utils.GetFormValue("txtEndTime$dateTextBox")) < DateTime.Now)
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:false,ErrorMessage:'留位时间不能小于当前时间！'}]");
                    Response.End();
                }
            }
            else
            {
                orderModel.SaveSeatDate = DateTime.Now;
                orderModel.OrderState = EyouSoft.Model.TourStructure.OrderState.未处理;
            }
            orderModel.AreaId = tourModel.AreaId;
            orderModel.SeatList = SeatList.Trim(',');
            orderModel.LeaveTraffic = Utils.GetFormValue("Traffic");
            orderModel.AdultNumber = Utils.GetInt(Utils.GetFormValue(PriceStandId + "AdultNumber"));
            orderModel.ChildNumber = Utils.GetInt(Utils.GetFormValue(PriceStandId + "ChildNumber"));
            orderModel.ChildPrice = decimal.Parse(Utils.GetFormValue(PriceStandId + "ChildrenPrice1"));
            orderModel.MarketPrice = Utils.GetDecimal(Utils.GetFormValue(PriceStandId + "MarketPrice"));
            orderModel.BuyCompanyID = Utils.GetFormValue("BuyCompanyID");
            orderModel.BuyCompanyName = Utils.GetFormValue("BuyCompanyName");
            orderModel.CompanyID = SiteUserInfo.CompanyID;
            orderModel.ContactFax = Utils.GetFormValue("BuyCompanyFax");
            orderModel.ContactMQ = Utils.GetFormValue("BuyCompanyMQ");
            orderModel.ContactName = Utils.GetFormValue("BuyCompanyContactName");
            orderModel.ContactQQ = Utils.GetFormValue("BuyCompanyQQ");
            orderModel.ContactTel = Utils.GetFormValue("BuyCompanyTel");
            orderModel.LastOperatorID = SiteUserInfo.ID;
            orderModel.MarketNumber = Utils.GetInt(Utils.GetFormValue(PriceStandId + "MarketNumber"));
            orderModel.OperatorID = SiteUserInfo.ID;
            orderModel.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            orderModel.OrderType = 1;
            orderModel.OtherPrice = Utils.GetDecimal(Utils.GetFormValue(PriceStandId + "OtherPrice"));
            orderModel.PersonalPrice = Utils.GetDecimal(Utils.GetFormValue(PriceStandId + "PersonalPrice"));
            orderModel.SumPrice = orderModel.AdultNumber * orderModel.PersonalPrice + orderModel.ChildNumber * orderModel.ChildPrice + orderModel.MarketNumber * orderModel.MarketPrice + orderModel.OtherPrice;
            orderModel.PeopleNumber = orderModel.ChildNumber + orderModel.AdultNumber;
            orderModel.PriceStandId = PriceStandId;
            orderModel.RouteName = Utils.GetFormValue("RouteName");
            orderModel.LeaveDate = Utils.GetDateTime(Utils.GetFormValue("LeaveDate"));
            orderModel.SpecialContent = Utils.GetFormValue("SpecialContent");
            orderModel.TourCompanyId = SiteUserInfo.CompanyID;
            orderModel.TourCompanyName = SiteUserInfo.CompanyName;
            orderModel.TourDays = tourModel.TourDays;
            orderModel.TourId = Utils.GetFormValue("TourId");
            orderModel.TourNo = tourModel.TourNo;
            orderModel.TourType = EyouSoft.Model.TourStructure.TourType.组团散拼;
            orderModel.IssueTime = DateTime.Now;
            orderModel.OrderSource = EyouSoft.Model.TourStructure.TourOrderOperateType.代客预定;
            orderModel.TourOrderCustomer = TourOrderCustomer();
            if (orderModel.TourOrderCustomer == null || orderModel.TourOrderCustomer.Count == 0)
            {
                ReturnString = "[{isSuccess:false,ErrorMessage:'游客总人数必须大于0，否则将不能保存，请填写正确的游客人数及信息！'}]";
                Response.Clear();
                Response.Write(ReturnString);
                Response.End();
            }
            if (orderModel.PeopleNumber > Utils.GetInt(Utils.GetFormValue("RemnantNumber")))
            {
                ReturnString = "[{isSuccess:false,ErrorMessage:'游客总人数大于剩余人数，不能保存请重新填写游客资料！'}]";
                Response.Clear();
                Response.Write(ReturnString);
                Response.End();
            }
            int ReturnValue = Orderbll.AddTourOrder(orderModel);
            Ibll = null;
            tourModel = null;
            Orderbll = null;
            orderModel = null;
            switch (ReturnValue)
            {
                case -1:
                    ReturnString = "[{isSuccess:false,ErrorMessage:'写入订单信息失败！'}]";
                    break;
                case 0:
                    ReturnString = "[{isSuccess:false,ErrorMessage:'订单实体为空！'}]";
                    break;
                default:
                    ReturnString = "[{isSuccess:true,ErrorMessage:'订单代客预订成功！'}]";
                    break;
            }
            Response.Clear();
            Response.Write(ReturnString);
            Response.End();

        }
        /// <summary>
        /// 获取游客实体集合
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.TourOrderCustomer> TourOrderCustomer()
        {
            IList<EyouSoft.Model.TourStructure.TourOrderCustomer> lists = new List<EyouSoft.Model.TourStructure.TourOrderCustomer>();
            EyouSoft.Model.TourStructure.TourOrderCustomer model = null;
            int len = Utils.GetFormValues("VisitorName").Length;
            SeatList = "";
            for (int i = 0; i < len; i++)
            {
                model = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                model.VisitorName = Utils.GetFormValues("VisitorName")[i];
                int cType = (int)Utils.GetInt(Utils.GetFormValues("CradType")[i]);
                switch (cType)
                {
                    case 0:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.请选择证件;
                        break;
                    case 1:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.身份证;
                        break;
                    case 2:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.户口本;
                        break;
                    case 3:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.军官证;
                        break;
                    case 4:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.护照;
                        break;
                    case 5:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.边境通行证;
                        break;
                    case 6:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.其他;
                        break;
                }
                model.SiteNo = Utils.GetFormValues("SeatNumber")[i];
                model.CradNumber = Utils.GetFormValues("CradNumber")[i];
                model.ContactTel = Utils.GetFormValues("ContactTel")[i];
                model.Sex = Utils.GetFormValues("Sex")[i] == "0" ? false : true;
                model.VisitorType = Utils.GetFormValues("VisitorType")[i] == "0" ? false : true;
                model.Remark = Utils.GetFormValues("Remark")[i];
                lists.Add(model);
                model = null;
            }
            return lists;
        }
    }
}
