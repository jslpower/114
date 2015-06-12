using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 专线商-散拼计划-计划列表and线路库-班次列表
    /// </summary>
    public partial class PlanList : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 线路ID,线路名称
        /// </summary>
        protected string routeId = string.Empty, routename = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Key = "RouteAgencyPlanList" + Guid.NewGuid().ToString();
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void InitPage()
        {
            BindRecommendType();
            BindPowderTourStatus();
            #region 查询条件
            //线路ID
            routeId = Utils.GetQueryStringValue("routeId");
            //出发时间开始
            string goDateS = Utils.GetQueryStringValue("goTimeS");
            txt_goTimeS.Value = goDateS;
            //出发时间结束
            string goDateE = Utils.GetQueryStringValue("goTimeE");
            txt_goTimeE.Value = goDateE;
            #endregion
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
                pageSize,
                pageCurrent,
                ref recordCount,
                routeId,//线路编号
                null,//团号
                Utils.GetDateTimeNullable(goDateS),//出发时间开始
                Utils.GetDateTimeNullable(goDateE));//出发时间结束
            if (list != null && list.Count > 0)
            {
                routename = list[0].RouteName;
                //lbl_routeName.Text = routename;
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("goTimeS", Utils.GetQueryStringValue("goTimeS"));
                this.ExportPageInfo1.UrlParams.Add("goTimeE", Utils.GetQueryStringValue("goTimeE"));
                this.ExportPageInfo1.UrlParams.Add("routeId", Utils.GetQueryStringValue("routeId"));
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }
        /// <summary>
        /// 绑定推荐类型
        /// </summary>
        private void BindRecommendType()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(RecommendType));
            //去除无这个状态
            typeli.RemoveAt(0);
            rpt_type.DataSource = typeli.Where(obj => obj.Value != ((int)RecommendType.推荐).ToString()).ToList();
            rpt_type.DataBind();
        }
        /// <summary>
        /// 绑定团队状态
        /// </summary>
        private void BindPowderTourStatus()
        {
            //获取团队状态
            rpt_powderTourStatus.DataSource = EnumObj.GetList(typeof(PowderTourStatus)); ;
            rpt_powderTourStatus.DataBind();

        }
    }
}
