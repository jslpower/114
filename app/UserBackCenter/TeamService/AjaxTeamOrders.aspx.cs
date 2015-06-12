using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.BLL.NewTourStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-团队订单管理
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-26
    public partial class AjaxTeamOrders : System.Web.UI.Page
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 线路来源
        /// </summary>
        protected RouteSource? routeSource = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            MTourListSearch queryModel = new MTourListSearch();
            #region 查询实体
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord"); //status
            //订单状态
            queryModel.TourOrderStatus = null;
            if (Utils.GetQueryStringValue("status").Length > 0)
            {
                queryModel.TourOrderStatus = (TourOrderStatus)Utils.GetInt(Utils.GetQueryStringValue("status"));
            }
            //出团时间
            queryModel.SLeaveDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeS"));
            //出团时间
            queryModel.ELeaveDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeE"));
            #endregion
            IList<MTourList> list = new List<MTourList>();
            if (Utils.GetQueryStringValue("routeSource").Length > 0)
            {
                routeSource = (RouteSource)Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1);
            }
            switch (routeSource)
            {
                case RouteSource.地接社添加:
                    list = BTourList.CreateInstance().GetDJList(pageSize, pageCurrent, ref recordCount, Utils.GetQueryStringValue("companyID"), queryModel);
                    break;
                case RouteSource.专线商添加:
                    list = BTourList.CreateInstance().GetZXList(pageSize, pageCurrent, ref recordCount, Utils.GetQueryStringValue("companyID"), queryModel);
                    break;
                default:
                    list = BTourList.CreateInstance().GetZTList(pageSize, pageCurrent, ref recordCount, Utils.GetQueryStringValue("companyID"), queryModel);
                    break;
            }
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                this.ExportPageInfo1.PageLinkURL = "/TeamService/TeamOrders.aspx?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.SLeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.ELeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);
                this.ExportPageInfo1.UrlParams.Add("routeSource", Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1).ToString());
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }
    }
}
