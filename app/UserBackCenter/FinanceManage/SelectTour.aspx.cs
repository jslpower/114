using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.FinanceManage
{
    public partial class SelectTour : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        private int intPageSize = 12;
        protected int intPageIndex = 1;
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
            btnSearch.ImageUrl = this.ImageServerUrl + "/images/chaxun.gif";
            if (this.IsLogin)
            {
                if (!IsPostBack)
                {
                    BindArea();
                    InitPage();
                }
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            int intRecordCount = 0;
            #region 初始化查询条件
            if (Request.Form[dplArea.UniqueID] == null)
            {
                intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            }
            CompanyID = this.SiteUserInfo.CompanyID;
            string StrBeginDate = Request.Form[txtBeginDate.UniqueID] == null ? Request.QueryString["BeginDate"] : Request.Form[txtBeginDate.UniqueID];
            string StrEndDate = Request.Form[txtEndDate.UniqueID] == null ? Request.QueryString["EndDate"] : Request.Form[txtEndDate.UniqueID];
            BeginDate = Utils.GetDateTimeNullable(StrBeginDate);
            EndDate = Utils.GetDateTimeNullable(StrEndDate);
            TourNumber = Utils.InputText(Request.Form[txtTourNumber.UniqueID] == null ? Request.QueryString["TourNumber"] : Request.Form[txtTourNumber.UniqueID]);
            RouteName = Utils.InputText(Request.Form[txtRouteName.UniqueID] == null ? Request.QueryString["RouteName"] : Request.Form[txtRouteName.UniqueID]);
            int? AreaId = Utils.GetIntNull(Request.Form[dplArea.UniqueID] == null ? Request.QueryString["AreaId"] : Request.Form[dplArea.UniqueID]);
            txtBeginDate.Value = StrBeginDate;
            txtEndDate.Value = StrEndDate;
            txtRouteName.Value = RouteName;
            this.txtTourNumber.Value = TourNumber;
            if (AreaId != null)
            {
                dplArea.Items.FindByValue(AreaId.ToString()).Selected = true;
            }
            #endregion
            IList<EyouSoft.Model.TourStructure.TourInfo> TourList = new List<EyouSoft.Model.TourStructure.TourInfo>();
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            TourList = Ibll.GetTours(intPageSize, intPageIndex, ref intRecordCount, CompanyID, TourNumber, RouteName, AreaId, BeginDate, EndDate);
            rpt_SelectTour.DataSource = TourList;
            rpt_SelectTour.DataBind();
            //是否有出发团队信息信息
            if (TourList == null || TourList.Count == 0)
            {
                this.NoData.Visible = true;
            }
            Ibll = null;
            TourList = null;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.UrlParams.Add("BeginDate", StrBeginDate);
            this.ExportPageInfo1.UrlParams.Add("EndDate", StrEndDate);
            this.ExportPageInfo1.UrlParams.Add("TourNumber", TourNumber);
            this.ExportPageInfo1.UrlParams.Add("RouteName", RouteName);
            this.ExportPageInfo1.UrlParams.Add("AreaId", AreaId.ToString());
            this.ExportPageInfo1.UrlParams.Add("NeedId", Request.QueryString["NeedId"]);
            this.ExportPageInfo1.UrlParams.Add("iframeId", Request.QueryString["iframeId"]);
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindArea()
        {
            int[] AreaId = this.SiteUserInfo.AreaId;
            List<EyouSoft.Model.SystemStructure.SysArea> AreaList = (List<EyouSoft.Model.SystemStructure.SysArea>)EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(AreaId);
            dplArea.DataValueField = "AreaId";
            dplArea.DataTextField = "AreaName";
            dplArea.DataSource = AreaList;
            dplArea.DataBind();
            AreaList = null;
            dplArea.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// repeater绑定数据时，赋值序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_SelectTour_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(intPageSize * (intPageIndex - 1) + (e.Item.ItemIndex + 1));
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindArea();
            InitPage();
        }
    }
}
