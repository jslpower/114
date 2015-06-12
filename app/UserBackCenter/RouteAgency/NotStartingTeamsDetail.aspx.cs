using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common;
using Adpost.Common;
namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 未出发团队-子团列表
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class NotStartingTeamsDetail : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        protected string ServerUrl = string.Empty;
        protected DateTime? BeginDate = null;
        protected DateTime? EndDate = null;
        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;

        protected string TourNumber = string.Empty;
        protected string RouteArea = string.Empty;
        private int intPageSize = 15;
        protected int intPageIndex = 1;
        private string TourID = string.Empty;
        /// <summary>
        /// 模板团编号
        /// </summary>
        protected string TemplateTourID = string.Empty;

        protected bool IsGrantUpdate = false;
        protected bool IsGrantDelete = false;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路管理))
            {
                Response.Clear();
                Response.Write("对不起，你目前登录的帐号没有该区域管理！");
                Response.End();
            }
            IsGrantUpdate = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.专线_未出团计划_修改);
            IsGrantDelete = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.专线_未出团计划_删除);
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            TemplateTourID = Utils.GetQueryStringValue("TemplateTourID");
            if (Request.QueryString["action"] != null && !string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                switch (Utils.GetQueryStringValue("action"))
                {
                    case "TemplateTourDelete"://模板团删除
                        Delete();
                        break;
                    case "tourDelete"://组团删除                        
                        Delete();
                        break;
                    case "changeState"://状态设置：客满，停收，正常
                        ChangeState();
                        break;
                    case "setTourMarkerNote"://团队类型设置
                        SetTourMarkerNote();
                        break;
                    case "setTourRemnantNumber":
                        SetTourRemnantNumber();
                        break;
                }
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
            int intRecordCount = 0;
            EyouSoft.Model.TourStructure.SearchTourState SearchState = new EyouSoft.Model.TourStructure.SearchTourState();
            dplSearchTourState.DataSource = Enum.GetNames(typeof(EyouSoft.Model.TourStructure.SearchTourState));
            dplSearchTourState.DataBind();
            #region 初始化查询条件
            BeginDate = Utils.GetDateTimeNullable(Request.QueryString["BeginDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["BeginDate"]))
            {
                ShowBeginDate = Utils.GetDateTime(Request.QueryString["BeginDate"]).ToShortDateString();
            }
            EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);
            if (!string.IsNullOrEmpty(Request.QueryString["EndDate"]))
            {
                ShowEndDate = Utils.GetDateTime(Request.QueryString["EndDate"]).ToShortDateString();
            }
            TourNumber = Utils.GetQueryStringValue("TourNumber");
            string TourSate = Utils.GetQueryStringValue("TourState");
            if (!string.IsNullOrEmpty(TourSate))
            {
                SearchState = (EyouSoft.Model.TourStructure.SearchTourState)Enum.Parse(typeof(EyouSoft.Model.TourStructure.SearchTourState), TourSate);
                dplSearchTourState.Items.FindByText(TourSate).Selected = true;
            }
            else
            {
                dplSearchTourState.Items.FindByText(EyouSoft.Model.TourStructure.SearchTourState.全部.ToString()).Selected = true;
                SearchState = EyouSoft.Model.TourStructure.SearchTourState.全部;
            }
            #endregion
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> TourList = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();
            TourList = Ibll.GetNotStartingTours(intPageSize, intPageIndex, ref intRecordCount, TemplateTourID, TourNumber, SearchState, BeginDate, EndDate);

            this.rpt_NotStartingTeamsDetail.DataSource = TourList;
            this.rpt_NotStartingTeamsDetail.DataBind();
            //是否有订单信息
            if (rpt_NotStartingTeamsDetail.Items.Count <= 0)
            {
                this.NoData.Visible = true;
            }
            Ibll = null;
            TourList = null;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 设置剩余人数
        /// </summary>
        private void SetTourRemnantNumber()
        {
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            string ItemTourID = Utils.GetQueryStringValue("SingleTourID");
            int RemnantNumber = Utils.GetInt(Utils.GetQueryStringValue("RemnantNumber"));
            bool isTrue = Ibll.SetTourRemnantNumber(ItemTourID, RemnantNumber);
            Ibll = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'操作成功！'}]");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'操作失败！'}]");
                Response.End();
            }
        }
        protected string ReturnCss(string TourSate)
        {
            string Result = string.Empty;
            if (TourSate.Trim().Contains("停收"))
            {
                Result = "tingsbj";
            }
            else if (TourSate.Trim().Contains("客满"))
            {
                Result = "kemanbj";
            }
            return Result;
        }

        /// <summary>
        /// 获取报价等级列表
        /// </summary>
        /// <param name="PriceLists"></param>
        /// <returns></returns>
        private List<CompanyPriceDetail> getPriceInfo(string TourID)
        {
            #region 价格等级信息
            IList<EyouSoft.Model.TourStructure.TourPriceDetail> PriceLists = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourPriceDetail(TourID);
            List<CompanyPriceDetail> newLists = new List<CompanyPriceDetail>();
            CompanyPriceDetail cpModel = null;
            if (PriceLists != null && PriceLists.Count > 0)
            {
                ((List<EyouSoft.Model.TourStructure.TourPriceDetail>)PriceLists).ForEach(item =>
                {
                    cpModel = new CompanyPriceDetail();
                    cpModel.PriceStandName = item.PriceStandName;
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
            }
            PriceLists = null;
            #endregion
            return newLists;
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_NotStartingTeamsDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(intPageSize * (intPageIndex - 1) + (e.Item.ItemIndex + 1));
                EyouSoft.Model.TourStructure.TourBasicInfo model = (EyouSoft.Model.TourStructure.TourBasicInfo)e.Item.DataItem;
                #region 价格等级
                Repeater rptPriceInfo = (Repeater)e.Item.FindControl("rptPriceInfo");
                List<CompanyPriceDetail> priceList = getPriceInfo(model.ID);
                if (rptPriceInfo != null)
                {
                    rptPriceInfo.DataSource = priceList;
                    rptPriceInfo.DataBind();
                }
                priceList = null;
                #endregion

                Literal ltrOrderDetail = (Literal)e.Item.FindControl("ltrOrderDetail");
                if (ltrOrderDetail != null)
                {
                    int HasIn = model.RealRemnantNumber;
                    if (HasIn > 0)
                    {
                        ltrOrderDetail.Text = "预订";
                    }
                }
                //团队状态，推广状态名
                Literal ltrSateName = (Literal)e.Item.FindControl("ltrSateName");
                if (ltrSateName != null)
                {
                    int state = (int)model.TourState;
                    if (state != 1)
                        switch (state)
                        {
                            case 0:
                                ltrSateName.Text = "<a class=\"tings\" href=\"javascript:void(0)\">停收</a>";
                                break;
                            case 2:
                                ltrSateName.Text = "<a class=\"keman\" href=\"javascript:void(0)\">客满</a>";
                                break;
                            case 3:
                                ltrSateName.Text = "<a class=\"tings\" href=\"javascript:void(0)\">停收</a>";
                                break;
                            case 4:
                                ltrSateName.Text = "<a class=\"keman\" href=\"javascript:void(0)\">客满</a>";
                                break;
                        }
                    else//收客时
                    {
                        ltrSateName.Text = model.TourSpreadStateName;
                    }
                }
            }
        }

        /// <summary>
        /// 设置团队类型,即推广状态
        /// </summary>
        private void SetTourMarkerNote()
        {
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            EyouSoft.Model.TourStructure.TourSpreadState SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.None;
            switch (Utils.GetFormValue("TourMarkerNote"))
            {
                case "1"://推荐精品 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.推荐精品;
                    break;
                case "2"://促销 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.促销;
                    break;
                case "3"://最新 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.最新;
                    break;
                case "4"://品质 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.品质;
                    break;
                case "5"://纯玩  
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.纯玩;
                    break;
                default://none
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.None;
                    break;
            }
            string[] TourIDs = Utils.GetFormValue("TourID").Trim(',').Split(',');
            //推广说明
            string Description = Utils.GetFormValue("Description");
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            bool isTrue = Ibll.SetTourSpreadState(SpreadState, Description, TourIDs);
            Ibll = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'操作成功！'}]");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'操作失败！'}]");
                Response.End();
            }
        }
        /// <summary>
        /// 设置收客状态
        /// </summary>
        private void ChangeState()
        {
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            EyouSoft.Model.TourStructure.TourState TourState = new EyouSoft.Model.TourStructure.TourState();
            switch (Utils.GetQueryStringValue("TourState"))
            {
                case "1":
                    TourState = EyouSoft.Model.TourStructure.TourState.手动客满;
                    break;
                case "2":
                    TourState = EyouSoft.Model.TourStructure.TourState.手动停收;
                    break;
                default:
                    TourState = EyouSoft.Model.TourStructure.TourState.收客;
                    break;
            }
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            string[] TourIDs = Utils.GetQueryStringValue("TourID").Trim(',').Split(',');
            bool isTrue = Ibll.SetTourState(TourState, TourIDs);
            Ibll = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'操作成功！'}]");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'操作失败！'}]");
                Response.End();
            }
        }
        /// <summary>
        /// 删除团队
        /// </summary>
        private void Delete()
        {
            if (!IsGrantDelete)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            string TourID = Utils.GetQueryStringValue("TourID").Trim(',');
            if (!string.IsNullOrEmpty(TourID))
            {
                EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
                bool isTrue = Ibll.DeleteByVirtual(TourID);
                Ibll = null;
                if (isTrue)
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:true,ErrorMessage:'操作成功！'}]");
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:false,ErrorMessage:'操作失败！'}]");
                    Response.End();
                }
            }
        }
    }
    /// <summary>
    /// 转换价格等级数据，方便绑定浮动价格等级
    /// </summary>
    public class CompanyPriceDetail
    {
        /// <summary>
        /// 价格等级编号
        /// </summary>
        public string PriceStandId { get; set; }
        /// <summary>
        /// 价格等级名称
        /// </summary>
        public string PriceStandName { get; set; }
        /// <summary>
        /// 同行-成人价
        /// </summary>
        public decimal AdultPrice1 { get; set; }
        /// <summary>
        /// 同行-儿童价
        /// </summary>
        public decimal ChildrenPrice1 { get; set; }
        /// <summary>
        /// 同行-单房差[结算价]
        /// </summary>
        public decimal SingleRoom1 { get; set; }

        /// <summary>
        /// 门市-成人价
        /// </summary>
        public decimal AdultPrice2 { get; set; }
        /// <summary>
        /// 门市-儿童价
        /// </summary>
        public decimal ChildrenPrice2 { get; set; }
        /// <summary>
        /// 门市-单房差
        /// </summary>
        public decimal SingleRoom2 { get; set; }

    }
}
