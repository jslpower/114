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
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.Function;
namespace UserBackCenter.Memorandum
{
    public partial class AjaxMemorList : EyouSoft.Common.Control.BackPage
    {
        protected int PageSize = 20;
        protected int CurrentPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        #region 绑定列表数据
        private void BindData()
        {
            int intRecordCount = 0;
            string fromDate = Request.QueryString["FromDate"];
            CurrentPage = Utils.GetInt(Request.QueryString["Page"], 1);
            if (StringValidate.IsDateTime(fromDate))
            {
                IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> list = EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().GetList(this.SiteUserInfo.CompanyID, Convert.ToDateTime(fromDate));
                if (list != null && list.Count > 0)
                {
                    crptMemList.DataSource = list;
                    crptMemList.DataBind();
                    this.ExportPageInfo1.intPageSize = PageSize;
                    this.ExportPageInfo1.CurrencyPage = CurrentPage;
                    this.ExportPageInfo1.intRecordCount = intRecordCount;
                    this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    list = null;
                }
                else
                {
                    ExportPageInfo1.Visible = false;
                    crptMemList.EmptyText = "<div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无备忘录信息</div>";
                }
            }
            else
            {
                //页面跳转到备忘录首页
            }
        }
        //获取状态
        protected string GetMemState(string state)
        {
            if (state == EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Cancel.ToString())
            {
                return "已取消";
            }
            else if (state == EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Complete.ToString())
            {
                return "已完成";
            }
            else if (state == EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Incomplete.ToString())
            {
                return "未完成";
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
