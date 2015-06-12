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


namespace UserBackCenter.Tools.Memorandum
{
        
    /// <summary>
    /// 备忘录管理
    /// 袁惠   2010-08-03
    /// </summary>
    public partial class MemorandumList : EyouSoft.Common.Control.BackPage
    {
        protected int sort = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string menId = Utils.GetString(Request.QueryString["MemId"], "");
                int result = 0;                
                if (Request.QueryString["Action"]=="Delete" && menId != "")
                {
                    if (EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().Remove
(menId))
                    {
                        result = 1;
                    }
                Response.Write(result.ToString());
                Response.End();
                }

                BindData();
                this.Title = "备忘录管理";
            }
        }
        #region 绑定列表数据
        private void BindData()
        {
            string fromDate = Request.QueryString["FromDate"];
            if (StringValidate.IsDateTime(fromDate))
            {
                IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> list = EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().GetList(this.SiteUserInfo.CompanyID, Convert.ToDateTime(fromDate));
                if (list != null && list.Count > 0)
                {
                    crptMemList.DataSource = list;
                    crptMemList.DataBind();
                    list = null;
                }
                else
                {
                    crptMemList.EmptyText = "<div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无备忘录信息</div>";
                }
                hdfDateTime.Value = fromDate;
            }
            else
            {
                MessageBox.ResponseScript(this.Page, "MemorandumList.tabChange(\"/Memorandum/MemorandumCalendar.aspx\")");
                return;
            }
        }
        //获取状态
        protected string GetMemState(string state)
        {
            if (state == 
EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Cancel.ToString())
            {
                return "已取消";
            }
            else if (state == 
EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Complete.ToString())
            {
                return "已完成";
            }
            else if (state == 
EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Incomplete.ToString())
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
