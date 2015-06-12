using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 公司登录次数列表
    /// luofx 2010-9-17
    /// </summary>
    public partial class LoginRecord : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        private int PageSize = 15;
        /// <summary>
        /// 当前页
        /// </summary>
        private int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrEmpty(Page))
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Page);
            }
            int recordCount = 0;
            string CompanyId =Utils.GetQueryStringValue("CompanyId");
            //&BeginTime=&EndTime=
            DateTime? BeginTime = Utils.GetDateTimeNullable(Request.QueryString["BeginTime"]);
            DateTime? EndTime = Utils.GetDateTimeNullable(Request.QueryString["EndTime"]);
            if (!string.IsNullOrEmpty(CompanyId))
            {
                IList<EyouSoft.Model.CompanyStructure.LogUserLogin> LoginList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetLoginList(PageSize, PageIndex, ref recordCount, CompanyId, BeginTime, EndTime);               
                if (LoginList != null && LoginList.Count > 0)
                {
                    NoData.Visible = false;
                    this.ExportPageInfo1.intPageSize = PageSize;
                    this.ExportPageInfo1.intRecordCount = recordCount;
                    this.ExportPageInfo1.CurrencyPage = PageIndex;
                    this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExportPageInfo1.UrlParams = Request.QueryString;
                    this.rpt_LoginInfo.DataSource = LoginList;
                    this.rpt_LoginInfo.DataBind();
                }
                LoginList = null;
            }           
        }
        /// <summary>
        /// 绑定数据时给控件(ltrXH)赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_LoginInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(PageSize * (PageIndex - 1) + (e.Item.ItemIndex + 1));
            }
        }
    }
}
