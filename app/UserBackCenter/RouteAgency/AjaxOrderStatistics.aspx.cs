using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.CompanyStructure;
using System.Text;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 专线商-专线订单统计Ajax
    /// </summary>
    public partial class AjaxOrderStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            //分页参数
            int intRecordCount = 0,//记录条数
             CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),//页码
             intPageSize = 15;//每页条数
            MOrderStaticSearch queryModel = new MOrderStaticSearch();
            #region 查询参数
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"));
            
            //是否详细列表
            queryModel.IsDetail = false;
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeS"));
            
            //出团时间结束
            queryModel.LeaveDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeE"));
            
            //公司编号
            queryModel.CompanyId = Utils.GetQueryStringValue("companyId");
            //公司类型
            queryModel.CompanyTyp = CompanyType.专线;
            #endregion
            IList<MOrderStatic> list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderStaticLst(
                  intPageSize,
                  CurrencyPage,
                  ref intRecordCount,
                  queryModel);

            if (list != null && list.Count > 0)
            {
                rpt_parentList.DataSource = list;
                rpt_parentList.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = "/RouteAgency/OrderStatistics.aspx?";

                this.ExportPageInfo1.UrlParams.Add("area", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDateS.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.LeaveDateE.ToString());
                //this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.OrderKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        protected string GetList(int lineId)
        {
            //分页参数
            int intRecordCount = 0,//记录条数
             CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),//页码
             intPageSize = 15;//每页条数
            MOrderStaticSearch queryModel = new MOrderStaticSearch();
            #region 查询参数
            //专线Id
            queryModel.AreaId = lineId;

            //是否详细列表
            queryModel.IsDetail = true;
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeS"));

            //出团时间结束
            queryModel.LeaveDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeE"));

            //公司编号
            queryModel.CompanyId = Utils.GetQueryStringValue("companyId");
            //公司类型
            queryModel.CompanyTyp = CompanyType.专线;
            #endregion
            IList<MOrderStatic> list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderStaticLst(
                  intPageSize,
                  CurrencyPage,
                  ref intRecordCount,
                  queryModel);

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<tr id=\"div_Detailed_" + lineId + "\" style=\"display: none\" bgcolor=\"#FFFFFF\">");
                sb.Append("<td colspan=\"9\" align=\"left\">");
                sb.Append("<table width=\"96%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#cccccc\"style=\"margin-top: 5px; margin-bottom: 5px;\">");
                sb.Append("<tr bgcolor=\"#f1f1f1\"><th>线路名</th><th>订单量</th><th>总人数</th><th>成人</th><th>儿童</th><th>销售总额</th><th>结算总额</th></tr>");
                foreach (var item in list)
                {
                    sb.Append("<tr>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + item.RouteName + "</td>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + item.TotalOrder + "</td>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + item.TotalPeople + "</td>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + item.TotalAdult + "</td>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + item.TotalChild + "</td>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + Utils.FilterEndOfTheZeroDecimal(item.TotalSale) + "元</td>");
                    sb.Append("<td bgcolor=\"#FFFFFF\">" + Utils.FilterEndOfTheZeroDecimal(item.TotalSettle) + "元</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table></div></td></tr></div></td></tr>");
                return sb.ToString();
            }
            else
            {
                string str = "<tr bgcolor=\"#FFFFFF\"><td colspan=\"9\" align=\"left\"><div id=\"div_Detailed_1\" style=\"display: none\">";
                str += "暂无数据！";
                str += "</div></td></tr>";
                return str;
            }
        }
    }
}
