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
using System.Text;
using System.Collections.Generic;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 线路管理 团队计划列表 以及添加修改
    /// 蔡永辉 2011-12-19
    /// </summary>
    public partial class AjaxTeamLineList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        protected string PowderTourStatusStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = "";
            type = Utils.GetQueryStringValue("type");
            if (type == "GetDropdownlist")
                SetPowderTourStatus();
            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);//获取分页序号
            string operating = Utils.GetQueryStringValue("Operating");
            switch (operating)
            {
                case "Delect":
                    Del();
                    break;
                case "UpdatePowderTourStatus":
                    //UpdatePowderTourStatus();
                    break;
            }
            if (!Page.IsPostBack)
            {
                BindLineList();
            }
        }
        /// <summary>
        /// 绑定线路列表
        /// </summary>
        protected void BindLineList()
        {
            int recordCount = 0;
            IList<MPowderList> TeamLinelist = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
                PageSize,
                PageIndex,
                ref recordCount,
                Utils.GetQueryStringValue("RouteId"),
                Utils.GetQueryStringValue("TeamLineId"),
                Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate")),
                Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate")));
            if (TeamLinelist.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.UrlParams.Add("routeid", Utils.GetQueryStringValue("routeid"));
                this.ExporPageInfoSelect1.UrlParams.Add("routename", Utils.GetQueryStringValue("routename"));
                this.ExporPageInfoSelect1.UrlParams.Add("tourNo", Utils.GetQueryStringValue("tourNo"));
                this.ExporPageInfoSelect1.UrlParams.Add("startDate", Utils.GetQueryStringValue("startDate"));
                this.ExporPageInfoSelect1.UrlParams.Add("endDate", Utils.GetQueryStringValue("endDate"));
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "TeamplanManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "TeamplanManage.LoadData(this);", 0);
                this.repList.DataSource = TeamLinelist;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th>选择</th>");
                strEmptyText.Append("<th>团号</th>");
                strEmptyText.Append("<th>出团日期</th><th>报名截止</th>");
                strEmptyText.Append("<th>人数</th><th>余位</th>");
                strEmptyText.Append("<th>留位</th><th>状态</th>");
                strEmptyText.Append("<th>成人(市/结)</th><th>儿童(市/结)</th>");
                strEmptyText.Append("<th>单房差</th><th>功能</th>");
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无团队计划</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            TeamLinelist = null;
        }

        #region 删除团队计划
        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {
            string[] tourIds = Utils.GetQueryStringValue("tourIds").Split('|');
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().DeletePowder(tourIds));
            Response.End();

        }

        #endregion



        /// <summary>
        /// 设置团队状态html
        /// </summary>
        private void SetPowderTourStatus()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(PowderTourStatus));
            rpt_updateStatus.DataSource = typeli;
            rpt_updateStatus.DataBind();
            StringBuilder strbContact = new StringBuilder("{tolist:[");
            foreach (var item in typeli)
            {
                strbContact.Append("{\"Value\":\"" + item.Value + "\",\"Text\":\"" + item.Text + "\"},");
            }
            Response.Clear();
            Response.Write(strbContact.ToString().TrimEnd(',') + "]}");
            Response.End();
        }


        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }
    }
}

