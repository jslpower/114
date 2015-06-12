using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-散拼订单统计ajax页面
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-22
    public partial class AjaxOrderStatistics : System.Web.UI.Page
    {
        /// <summary>
        /// 是否详细列表
        /// </summary>
        protected bool isDetail = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //分页参数
            int intRecordCount = 0,//记录条数
             CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),//页码
             intPageSize = 15;//每页条数
            MOrderStaticSearch queryModel = new MOrderStaticSearch();
            #region 查询参数
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"));
            isDetail = queryModel.AreaId > 0;
            //是否详细列表
            queryModel.IsDetail = isDetail;
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeS"));
            //出团时间结束
            queryModel.LeaveDateE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeE"));
            //公司编号
            queryModel.CompanyId = Utils.GetQueryStringValue("companyID");
            //公司类型
            queryModel.CompanyTyp = CompanyType.组团;
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
                this.ExportPageInfo1.PageLinkURL = "/TeamService/OrderStatistics.aspx?";


                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
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
    }
}
