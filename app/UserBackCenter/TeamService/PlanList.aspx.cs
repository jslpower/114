using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-线路库计划列表
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
        /// 线路ID
        /// </summary>
        protected string routeId = string.Empty, areaId = string.Empty, type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Key = "TeamServicePlanList" + Guid.NewGuid().ToString();
            areaId = Utils.GetQueryStringValue("areaId");
            type = Utils.GetQueryStringValue("type");
            InitPage();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            //分页参数
            int pageSize = 15,
                pageCurrent =  Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            routeId = Utils.GetQueryStringValue("routeId");
            string goTimeS = Utils.GetQueryStringValue("goTimeS"),
                    goTimeE = Utils.GetQueryStringValue("goTimeE");
            txt_goTimeS.Value = goTimeS;
            txt_goTimeE.Value = goTimeE;
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
                pageSize,
                pageCurrent,
                ref recordCount,
                routeId,
                null,
                Utils.GetDateTimeNullable(goTimeS),
                Utils.GetDateTimeNullable(goTimeE)
                );

            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("routeId", routeId);
                //this.ExportPageInfo1.UrlParams.Add("showPrice", Utils.GetQueryStringValue("showPrice"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", goTimeS);
                this.ExportPageInfo1.UrlParams.Add("goTimeE", goTimeE);

            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }

        }

        /// <summary>
        /// 获取预定打印按钮
        /// </summary>
        /// <returns></returns>
        protected string GetButt(string tourID)
        {
            return "<a href=\"/Order/OrderByTour.aspx?TourID=" + tourID + "\" class=\"basic_btn Order\" value=\"" + tourID + "\"><span>预订</span></a><a href='/PrintPage/TeamTourInfo.aspx?TeamId="+tourID+"' class=\"basic_btn\" target=\"_blank\" value=\"" + tourID + "\"><span>打印</span></a>";
        }
    }
}
