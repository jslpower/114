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
namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 已出发团队
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class AlreadyStartingTeams : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        private int intPageSize = 12;
        protected int intPageIndex = 1;
        protected int? Days = null;
        private DateTime? BeginDate = null;
        private DateTime? EndDate = null;

        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;

        protected string TourNumber = string.Empty;
        protected string RouteName = string.Empty;
        private string CompanyID = string.Empty;

        protected bool isGrant = false;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_管理栏目))
            //{
            //    Response.Clear();
            //    Response.Write("对不起，你正在使用的帐号没有操作改页面的权限！");
            //    Response.End();
            //}
            isGrant = true; // this.CheckGrant(EyouSoft.Common.TravelPermission.专线_已出团计划);
            CompanyID = this.SiteUserInfo.CompanyID;
            if (Request["action"] != null && !string.IsNullOrEmpty(Request["action"]) && Request["action"] == "tourDelete")
            {
                Delete();
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
            #region 初始化查询条件
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
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
            TourNumber = Utils.InputText(Request.QueryString["TourNumber"]);
            RouteName = Utils.InputText(Request.QueryString["RouteName"]);
            RouteName = Server.UrlDecode(RouteName).Trim();
            Days = Utils.GetIntNull(Request.QueryString["Days"]);
            #endregion
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> TourList = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            TourList = Ibll.GetStartingTours(intPageSize, intPageIndex, ref intRecordCount, CompanyID, TourNumber, RouteName, Days, BeginDate, EndDate);
            rpt_AlreadyStartingTeams.DataSource = TourList;
            rpt_AlreadyStartingTeams.DataBind();
            Ibll = null;
            TourList = null;
            //是否有出发团队信息信息
            if (rpt_AlreadyStartingTeams.Items.Count <= 0)
            {
                this.NoData.Visible = true;
            }
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
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
        protected void rpt_AlreadyStartingTeams_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                //团队状态，推广状态名
                Literal ltrSateName = (Literal)e.Item.FindControl("ltrSateName");
                if (ltrSateName != null)
                {
                    ltrSateName.Text = model.TourSpreadStateName;
                }
            }
        }
        /// <summary>
        /// 出发子团队删除
        /// </summary>
        private void Delete()
        {
            //出发团队删除
            if (Request.QueryString["TourID"] != null && !string.IsNullOrEmpty(Request.QueryString["TourID"]))
            {
                string TourID = Utils.InputText(Request.QueryString["TourID"]);
                EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
                bool isTrue = Ibll.DeleteByVirtual(TourID);
                Ibll = null;
                if (isTrue)
                {
                    Response.Clear();
                    Response.Write("1");
                    Response.End();
                }
            }
        }
    }
}
