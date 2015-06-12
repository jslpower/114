using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Data;
using System.Text;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 专线商数据列表
    /// 功能：专线商查询数据列表
    /// 创建人：戴银柱
    /// 创建时间： 2010-09-17  
    /// </summary>
    public partial class LineCompanyList : EyouSoft.Common.Control.YunYingPage
    {
        #region 分页变量
        protected int pageSize = 20;  //分页大小
        public int pageIndex = 1;     //当前页
        protected int recordCount;    //数据行数
        #endregion 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断权限
                if (!CheckMasterGrant(YuYingPermission.统计分析_专线商行为分析))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_专线商行为分析, true);
                    return;
                }

                #region Request的参数
                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);

                //获得单位名称
                string companyName = Utils.InputText(Server.UrlDecode(Utils.GetQueryStringValue("companyName")));

                //获得省份Id
                int cityId = Utils.GetInt(Utils.GetQueryStringValue("cityId"));

                //获得排序值
                int orderIndex = Utils.GetInt(Utils.GetQueryStringValue("orderIndex"));

                //获得区域Id
                int areaId = Utils.GetInt(Utils.GetQueryStringValue("areaId"));

                //获得开始时间
                DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startTime"), null);

                //获得结束时间
                DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endTime"), null);
                #endregion

                //页面数据初始化
                DataListInit(companyName, cityId, orderIndex, areaId, startTime, endTime);
            }
        }

        #region 页面数据初始化
        protected void DataListInit(string companyName, int cityId, int orderIndex, int areaId, DateTime? startTime, DateTime? endTime)
        {
            IList<EyouSoft.Model.TourStructure.WholesalersStatistics> list = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetWholesalersStatistics(pageSize, pageIndex, ref recordCount, orderIndex, companyName, MasterUserInfo.AreaId, cityId, areaId, startTime, endTime);

            if (list != null && list.Count > 0)
            {
                this.rpt_List.DataSource = list;
                this.rpt_List.DataBind();
                BindPage();
                this.lblMsg.Text = "";
            }
            else
            {
                this.ExportPageInfo1.Visible = false;
                this.lblMsg.Text = "没有数据";
            }
            list = null;
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
            this.ExportPageInfo1.PageLinkURL = "#/Statistics/LineCompanyAnalysis.aspx" + "?";
        }
        #endregion

        #region 获得线路区域
        public string GetAreaByList(Object list, string companyId)
        {
            IList<EyouSoft.Model.TourStructure.AreaStatInfo> areaList = (IList<EyouSoft.Model.TourStructure.AreaStatInfo>)list;

            StringBuilder sb = new StringBuilder();
            if (areaList != null && areaList.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.AreaStatInfo model in areaList)
                {

                    sb.Append("<a href=\"javascript:void(0)\" onclick=\"OpenProPage('ProductList.aspx','产品列表','" + companyId + "','" + model.AreaId + "')\" style=\"cursor:pointer\" >【");
                    sb.Append(model.AreaName);
                    sb.Append(" (" + model.Number + ")");
                    sb.Append("】</a><br />");
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}
