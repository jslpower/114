using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-国际-国内计划Ajax列表
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-23
    public partial class AjaxScatterPlan : System.Web.UI.Page
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),
                recordCount = 0;
            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数赋值
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);

            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");
            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Request.QueryString["goTimeS"] ?? (DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString());

            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"));
            //出发地Id
            queryModel.StartCityId = Utils.GetInt(Utils.GetQueryStringValue("goCityId"));
            //出发地Name
            queryModel.StartCityName = Utils.GetQueryStringValue("goCityName");
            //推荐类型
            queryModel.RecommendType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("status"), -1) >= 0)
            {
                queryModel.RecommendType = (RecommendType)Utils.GetInt(Utils.GetQueryStringValue("status"));
            }
            //出游天数
            int powderDay = Utils.GetInt(Utils.GetQueryStringValue("travelDays"), 0);
            if (powderDay > 0)
            {
                queryModel.PowderDay = (PowderDay)powderDay;
            }
            #endregion
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
                pageSize,
                pageCurrent,
                ref recordCount,
                (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType")),
                Utils.GetInt(Utils.GetQueryStringValue("uCityId")),
                queryModel);

            if (list != null && list.Count > 0)
            {

                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                this.ExportPageInfo1.PageLinkURL = "/TeamService/ScatterPlanI.aspx?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("travelDays", Utils.GetQueryStringValue("travelDays"));
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("goCityId", Utils.GetQueryStringValue("goCityId"));
                this.ExportPageInfo1.UrlParams.Add("goCityName", Utils.GetQueryStringValue("goCityName"));
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", Utils.GetQueryStringValue("goTimeS"));
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.EndLeaveDate.ToString("yyyy-MM-dd"));
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);


            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
                ExportPageInfo1.Visible = false;
            }
        }
        /// <summary>
        /// 获取预定打印按钮
        /// </summary>
        /// <returns></returns>
        protected string GetButt(string tourID)
        {
            return "<a href=\"/Order/OrderByTour.aspx?TourID=" + tourID + "\" class=\"basic_btn Order\" value=\"" + tourID + "\"><span>预订</span></a><a href=\"/PrintPage/TeamTourInfo.aspx?TeamId=" + tourID + "\" target=\"_blank\" class=\"basic_btn\"><span>打印</span></a>";
        }
    }
}
