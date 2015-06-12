using System;
using System.Collections;
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
using EyouSoft.Model.TourStructure;
using System.Collections.Generic;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 查看 专线商被查看次数明细的页面
    /// 张新兵，20100920
    /// </summary>
    public partial class ViewedCompanyList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int PageSize = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        private int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int Count = 0;

        /// <summary>
        /// 公司ID
        /// </summary>
        protected string CompanyId = string.Empty;
        /// <summary>
        /// 线路区域
        /// </summary>
        protected string RouteArea = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindList();
            }
        }

        /// <summary>
        /// 列表数据绑定
        /// </summary>
        protected void BindList()
        {
            int recordCount = 0;
            PageIndex = Utils.GetInt(Request.QueryString["Page"],1);
            CompanyId = Utils.GetQueryStringValue("CompanyId");
            int? areaid = Utils.GetIntNull(Request.QueryString["RouteArea"]);
            if(areaid.HasValue){
                RouteArea = areaid.ToString();
            }
            string CompanyName = Utils.InputText(Server.UrlDecode(Request.QueryString["CompanyName"]));
            DateTime? StartDate = Utils.GetDateTimeNullable(Request.QueryString["BeginTime"]);
            DateTime? EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndTime"]);
            string routeName = Utils.InputText(Server.UrlDecode(Request.QueryString["RouteName"]));

            //初始化日期段控件
            StartAndEndDate1.SetStartDate = StartDate.HasValue ? StartDate.Value.ToString("yyyy-MM-dd") : "";
            StartAndEndDate1.SetEndDate = EndDate.HasValue ? EndDate.Value.ToString("yyyy-MM-dd") : "";
            
            IList<TourVisitInfo> list = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourVistedHistorysByCompany(
               PageSize,PageIndex,ref recordCount,CompanyId,CompanyName,routeName,StartDate,EndDate,areaid);
            if (recordCount > 0)
            {
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = "ViewedCompanyList.aspx?";
                this.rptCompanyList.DataSource = list;
                this.rptCompanyList.DataBind();

                this.ExportPageInfo1.Visible = true;
            }
            else
            {
                this.rptCompanyList.EmptyText = "<table><tr><td height='100px' align='center'>暂无记录</td></tr></table>";
                this.ExportPageInfo1.Visible = false;
            }
            list = null;
        }

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++Count + (PageIndex - 1) * PageSize;
        }
    }
}
