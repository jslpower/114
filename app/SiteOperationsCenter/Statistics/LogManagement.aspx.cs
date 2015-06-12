using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 日志管理页面
    /// 创建日期：2011-12-16 方琪
    /// </summary>
    public partial class LogManagement : YunYingPage
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        protected string txtOperator = string.Empty;
        protected string txtOrderNo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtOperator = Utils.GetQueryStringValue("Operator");
            txtOrderNo = Utils.GetQueryStringValue("OrderNo");
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            if (!Page.IsPostBack)
            {
                initPage();
            }
        }

        protected void initPage()
        {
            //绑定数据列表 分页控件
            int intRecordCount = 0;
            EyouSoft.Model.NewTourStructure.MOrderHandleLogSearch LogSearchModel =
                new EyouSoft.Model.NewTourStructure.MOrderHandleLogSearch();
            LogSearchModel.OperatorName = txtOperator;
            this.Operator.Text = txtOperator;
            this.OrderNo.Text = txtOrderNo;
            LogSearchModel.OrderNo = txtOrderNo;
            IList<EyouSoft.Model.NewTourStructure.MOrderHandleLog> list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderHandleLogLst(PageSize, PageIndex, ref intRecordCount, LogSearchModel);
            this.rptLogList.DataSource = list;
            this.rptLogList.DataBind();
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
            this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?Operator=" + txtOperator + "&OrderNo=" + txtOrderNo+"&";
        }

        /// <summary>
        /// 序号（根据当前页码 和页大小计算出序号）
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }
    }
}
