using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社线路库Ajax页面
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-11-19
    public partial class AjaxLineLibraryList : System.Web.UI.Page
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 专线ID
        /// </summary>
        protected string areaId = string.Empty, type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            //记录条数
            int intRecordCount = 0;
            //页码
            int CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            int intPageSize = 15;
            //IRoute bll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            //查询实体
            MRouteSearch queryModel = new MRouteSearch();
            #region 查询参数赋值
            //线路来源
            queryModel.RouteSource = null;
            //专线Id
            areaId = Utils.GetQueryStringValue("lineId");
            queryModel.AreaId = Utils.GetInt(areaId, 0);
            //专线类型
            queryModel.RouteType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("lineType"), -1) >= 0)
            {
                type = Utils.GetQueryStringValue("lineType");
                queryModel.RouteType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType"));
            }
            //出发城市
            queryModel.StartCity = Utils.GetInt(Utils.GetQueryStringValue("goCityId"), 0);
            queryModel.StartCityName = Utils.GetQueryStringValue("goCity");
            //出团月份
            string goTime = Utils.GetQueryStringValue("goTime");
            //出团月份 0:全部,1:无出团计划 3：月份 默认0：全部 值在3的情况下,要给Year,Month属性赋值
            queryModel.LeaveMonth = goTime == "-1" ? 0 : 3;
            if (queryModel.LeaveMonth == 3)
            {
                queryModel.Month = Utils.GetInt(Utils.GetDateTime(goTime).ToString("MM"));
                queryModel.Year = Utils.GetInt(Utils.GetDateTime(goTime).ToString("yyyy"));
            }
            //关键字
            queryModel.RouteKey = Utils.GetQueryStringValue("keyWord") == "线路名称" ? string.Empty : Utils.GetQueryStringValue("keyWord");
            #endregion
            //线路库列表
            IList<MRoute> list = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetList(
                intPageSize,
                CurrencyPage,
                ref intRecordCount,
                queryModel);
            if (list != null && list.Count > 0)
            {
                //存在列表数据
                rpt_List.DataSource = list;
                rpt_List.DataBind();

                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = "/TeamService/LineLibraryList.aspx?";

                this.ExportPageInfo1.UrlParams.Add("lineId", Utils.GetQueryStringValue("lineId"));
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goCityId", queryModel.StartCity.ToString());
                this.ExportPageInfo1.UrlParams.Add("goCity", queryModel.StartCityName.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.RouteKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }
    }
}
