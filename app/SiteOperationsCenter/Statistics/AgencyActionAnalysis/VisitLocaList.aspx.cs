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
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.Function;
namespace SiteOperationsCenter.Statistics.AgencyActionAnalysis
{
    /// <summary>
    /// 组团行为分析-查看访问轨迹
    /// 创建时间：2010-9-19   袁惠
    /// </summary>
    public partial class VisitLocaList :EyouSoft.Common.Control.YunYingPage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!this.CheckMasterGrant(YuYingPermission.统计分析_管理该栏目))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_管理该栏目, true);
                    return;
                }
                if (!this.CheckMasterGrant(YuYingPermission.统计分析_组团社行为分析))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, true);
                    return;
                }

                InitList(true);
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <param name="IsOneLoad">判断是否为初次加载</param>
        private void InitList(bool IsOneLoad)
        {
            int recordCount=0;
            string CompanyId = "";
            string CompanyName = null;
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            pageIndex = Utils.GetInt(Request.QueryString["Page"]);
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            CompanyId = Utils.InputText(Request.QueryString["CompanyId"]);
            string date1 = Request.QueryString["BeginTime"];
            string date2 = Request.QueryString["EndTime"];
            CompanyName = Utils.InputText(Server.HtmlDecode(Request.QueryString["CompanyName"]));
            if (StringValidate.IsDateTime(date1))
            {
                StartDate = Convert.ToDateTime(date1);
            }
            if (StringValidate.IsDateTime(date2))
            {
                EndDate = Convert.ToDateTime(date2);
            }
            StartAndEndDate1.SetStartDate = date1;
            StartAndEndDate1.SetEndDate =date2;
            txtcompanyName.Value = CompanyName;
            hidCompanyId.Value = CompanyId;
            

            IList<EyouSoft.Model.TourStructure.TourVisitInfo> list = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetVisitedHistorysByCompany(pageSize, pageIndex, ref recordCount, CompanyId, CompanyName,StartDate,EndDate);

            if (list.Count > 0 && list != null)
            {
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.CurrencyPage = pageIndex;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = "VisitLocaList.aspx?"; 
                crptinfoList.DataSource = list;
                crptinfoList.DataBind();
                list = null;
            }
            else
            {
                crptinfoList.EmptyText = "<tr><td colspan=\"4\" align=\"center\"><div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无数据！</span></div></td></tr>";
                ExportPageInfo1.Visible = false;
            }
        }
        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            InitList(false);
        }
    }
}
