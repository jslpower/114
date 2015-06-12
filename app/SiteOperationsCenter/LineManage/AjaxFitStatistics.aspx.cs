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
using System.Text;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散客订单统计列表查询
    /// 蔡永辉 2011-12-22
    /// </summary>
    public partial class AjaxFitStatistics : EyouSoft.Common.Control.YunYingPage
    {
        protected string type = "";
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
        protected int currentPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);//获取分页序号
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            type = Utils.GetQueryStringValue("Type");
            if (!IsPostBack)
            {
                BindCompanyList();
            }

            #region ajax请求
            //if (type == "aaaaaa")
            //{
            //    string html = "<tr id=\"tr_Line\" bgcolor=\"#F3F7FF\" onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\""
            //    + "style=\"display: none;\">"
            //    + "<td colspan=\"9\" align=\"left\">"
            //        + "<table width=\"96%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#9dc4dc\""
            //            + "style=\"margin-top: 5px; margin-bottom: 5px;\">"
            //            + "<tr class=\"list_basicbg\">"
            //                + "<th class=\"no_cuti\">线路名</th>"
            //                + "<th class=\"no_cuti\">订单量</th>"
            //                + "<th class=\"no_cuti\">总人数</th>"
            //                + "<th class=\"no_cuti\">成人</th>"
            //                + "<th class=\"no_cuti\">儿童</th>"
            //                + "<th class=\"no_cuti\">销售总额</th>"
            //                + "<th class=\"no_cuti\">结算总额</th>"
            //            + "</tr>"
            //            + "<tr>"
            //                + "<td bgcolor=\"#FFFFFF\">普吉岛6日豪华</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">12</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">26</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">24</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">2</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">16000元</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">13000元</td>"
            //            + "</tr>"
            //            + "<tr>"
            //                + "<td bgcolor=\"#FFFFFF\">普吉岛5日豪华</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">2</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">5</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">3</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">2</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">8000元</td>"
            //                + "<td align=\"center\" bgcolor=\"#FFFFFF\">6000元</td>"
            //            + "</tr>"
            //         + "</table>"
            //       + "</td>"
            //    + "</tr>";

            //    Response.Clear();
            //    Response.Write(html);
            //    Response.End();
            //}
            #endregion
        }




        /// <summary>
        /// 绑定散客订单列表
        /// </summary>
        protected void BindCompanyList()
        {
            int recordCount = 0;
            int Line1 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line1"]);//专线国内国外周边
            int Line2 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line2"]);//专线区域
            string BusinessLine = Utils.GetQueryStringValue("BusinessLine");//专线商
            string StartDate = Utils.InputText(Request.QueryString["StartDate"]);//出发时间
            string EndDate = Utils.InputText(Request.QueryString["EndDate"]);//返回时间
            MOrderStaticSearch SearchModel = new MOrderStaticSearch();
            if (Line1 > -1)
                SearchModel.AreaType = (AreaType)Line1;
            if (Line2 > 0)
                SearchModel.AreaId = Line2;
            if (BusinessLine != "0")
                SearchModel.CompanyId = BusinessLine;
            if (StartDate != "")
                SearchModel.LeaveDateS = Convert.ToDateTime(StartDate);
            if (EndDate != "")
                SearchModel.LeaveDateE = Convert.ToDateTime(EndDate);
            IList<MOrderStatic> listScenicArea = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderStaticLst(PageSize, PageIndex, ref recordCount, SearchModel);


            if (listScenicArea.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "FitStatistics.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "FitStatistics.LoadData(this);", 0);
                this.ExporPageInfoSelect1.UrlParams = Request.Params;
                this.repList.DataSource = listScenicArea;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th>时间</th>");
                strEmptyText.Append("<th>专线</th>");
                strEmptyText.Append("<th>订单量</th><th>总人数</th>");
                strEmptyText.Append("<th>成人</th><th>儿童</th>");
                strEmptyText.Append("<th>销售总额</th><th>订单总额</th>");
                strEmptyText.Append("<th>功能</th>");
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无订单信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            listScenicArea = null;
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
