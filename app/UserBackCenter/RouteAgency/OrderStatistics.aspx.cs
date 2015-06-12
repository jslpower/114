using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 专线商-专线订单统计
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-21
    public partial class OrderStatistics : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            Key = "OrderStatistics" + Guid.NewGuid().ToString();
            InitPage();
        }
        /// <summary>
        /// 绑定专线下拉
        /// </summary>
        private void BindZX()
        {
            ICompanyUser companyUserBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = companyUserBLL.GetModel(SiteUserInfo.ID);
            if (companyUserModel != null && companyUserModel.Area != null && companyUserModel.Area.Count > 0)
            {
                ddl_ZX.AppendDataBoundItems = true;
                ddl_ZX.DataTextField = "AreaName";
                ddl_ZX.DataValueField = "AreaId";
                ddl_ZX.DataSource = companyUserModel.Area;
                ddl_ZX.DataBind();
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindZX();
            //分页参数
            int intRecordCount = 0,//记录条数
             CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),//页码
             intPageSize = 15;//每页条数
            MOrderStaticSearch queryModel = new MOrderStaticSearch();
            #region 查询参数
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"));
            ddl_ZX.SelectedValue = Utils.GetQueryStringValue("lineId");
            //是否详细列表
            queryModel.IsDetail = false;
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeS"));
            txt_goTimeS.Value = Utils.GetQueryStringValue("goTimeS");
            //出团时间结束
            queryModel.LeaveDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeE"));
            txt_goTimeE.Value = Utils.GetQueryStringValue("goTimeE");
            //公司编号
            queryModel.CompanyId = SiteUserInfo.CompanyID;
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
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";

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
            queryModel.CompanyId = SiteUserInfo.CompanyID;
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
                sb.Append("<tr bgcolor=\"#f1f1f1\"><th style=\"width: 450px\">线路名</th><th>订单量</th><th>总人数</th><th>成人</th><th>儿童</th><th>销售总额</th><th>结算总额</th></tr>");
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
