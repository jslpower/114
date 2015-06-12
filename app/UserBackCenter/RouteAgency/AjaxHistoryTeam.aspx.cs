using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 专线-我的历史团队-ajax页面
    /// </summary>
    public partial class AjaxHistoryTeam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void InitPage()
        {
            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数赋值
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);


            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");

            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeS"));

            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"));

            #endregion
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetHistoryList(
                pageSize,
                pageCurrent,
                ref recordCount,
                Utils.GetQueryStringValue("companyID"),
                queryModel);

            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                this.ExportPageInfo1.PageLinkURL = "/RouteAgency/HistoryTeam.aspx?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.EndLeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }

        }
    }
}
