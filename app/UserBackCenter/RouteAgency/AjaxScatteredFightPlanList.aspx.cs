using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 我的散拼计划Ajax列表获取
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-20
    public partial class AjaxScatteredFightPlanList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数赋值
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");

            ////专线类型
            //queryModel.AreaType = null;
            //if (Utils.GetIntSign(Utils.GetQueryStringValue("lineType"), -1) >= 0)
            //{
            //    queryModel.AreaType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType"));
            //}
            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeS"));
         
            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"));
           
            #endregion
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
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
                this.ExportPageInfo1.PageLinkURL = "/RouteAgency/ScatteredFightPlan.aspx?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDate.ToString("yyyy-MM-dd"));
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.EndLeaveDate.ToString("yyyy-MM-dd"));
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
