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
using EyouSoft.Common.Function;

namespace UserBackCenter.Tools.Memorandum
{
    /// <summary>
    /// 新增备忘录
    /// 袁惠   2010-08-03
    /// </summary>
    public partial class AddMemorandum : EyouSoft.Common.Control.BackPage
    {
        protected string tbMemId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Utils.GetFormValue("method");

            tbMemId = "AddMemorandum" + Guid.NewGuid().ToString();
            if (method == "save")
            {
                SaveMem();
                return;
            }
            if (!IsPostBack)
            {
                string memId = Utils.GetString(Request.QueryString["MemId"], "");
                hdeUppage.Value =Request.QueryString["UpPage"];
                DataBindMemStateUrgle();
                if (memId.Length > 0)   //修改
                {
                    ShowMemoInfo(memId);
                }
                else
                {
                    DateTime defaultdate = Utils.GetDateTime(Request.QueryString["DefaultDay"], DateTime.MinValue);
                    DatePicker1.Value = defaultdate ==DateTime.MinValue ? "" : defaultdate.ToString("yyyy-MM-dd");
                }
                this.AddJavaScriptInclude(JsManage.GetJsFilePath("validatorform"), true, false);
            }
        }

        #region 绑定下拉列表
        private void DataBindMemStateUrgle()
        {
            ddlUrgentType.Items.Add(new ListItem("-请选择-", ""));
            string[] urgents = Enum.GetNames(typeof(EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType));
            string urgentName = "";
            foreach (string item in urgents)
            {
                if (item == EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType.Urgent.ToString())
                {
                    urgentName = "紧急";
                }
                if (item == EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType.Normal.ToString())
                {
                    urgentName = "一般";
                }
                ddlUrgentType.Items.Add(new ListItem(urgentName, item));
            }
            urgents = null;
            urgentName = null;
            ddlMemState.Items.Add(new ListItem("-请选择-", ""));
            urgents = Enum.GetNames(typeof(EyouSoft.Model.ToolStructure.MemoDetailType.MemoState));
            foreach (string item in urgents)
            {
                if (item == EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Cancel.ToString())
                {
                    urgentName = "已取消";
                }else if (item == EyouSoft.Model.ToolStructure.MemoDetailType.MemoState.Complete.ToString())
                {
                    urgentName = "已完成";
                }else 
                {
                    urgentName = "未完成";
                }
                ddlMemState.Items.Add(new ListItem(urgentName, item));
            }
            urgents = null;
        }
        #endregion
        protected void ShowMemoInfo(string memId)
        {
            EyouSoft.Model.ToolStructure.CompanyDayMemo dayMemo = EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().GetModel(memId);
            if (dayMemo != null)
            {
               DatePicker1.Value = dayMemo.MemoTime.ToString("yyyy-MM-dd");
                txtTitle.Value = dayMemo.MemoTitle;
                txtDetialInfo.Value = dayMemo.MemoText;
                ddlUrgentType.SelectedValue = dayMemo.UrgentType.ToString();
                ddlMemState.SelectedValue = dayMemo.MemoState.ToString();
                dayMemo = null;
            }
            hdfMemID.Value = memId;
        }
        #region 保存
        private void SaveMem()
        {
            string title= Utils.GetFormValue(txtTitle.UniqueID);
            string datetime=Utils.GetFormValue(DatePicker1.UniqueID);
            string urgent=Utils.GetFormValue(ddlUrgentType.UniqueID);           
            string detailinfo = Utils.GetFormValue(txtDetialInfo.UniqueID);
            string memostate = Utils.GetFormValue(ddlMemState.UniqueID);
            if(title.Length<=0)
            {
                Response.Write("请填写事件标题!");
                Response.End();
                return;
            }
            if(!StringValidate.IsDateTime(datetime))
            {
                Response.Write("请填写正确的时间!");
                Response.End();
                return;
            }
            if(urgent.Length<=0)
            {
                Response.Write("请选择事件紧急程度!");
                Response.End();
                return;
            }
            if (memostate.Length<=0)
            {
                Response.Write("请选择事件状态!");
                Response.End();
                return;
            }
            EyouSoft.Model.ToolStructure.CompanyDayMemo dayMemo = new EyouSoft.Model.ToolStructure.CompanyDayMemo();
            dayMemo.MemoText = detailinfo;
            dayMemo.MemoTitle = title;
            dayMemo.OperatorId = this.SiteUserInfo.ID;
            dayMemo.OperatorName = this.SiteUserInfo.UserName;
            dayMemo.UrgentType = (EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType)Enum.Parse(typeof(EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType),urgent);
            dayMemo.MemoTime = Convert.ToDateTime(datetime);
            dayMemo.CompanyId = this.SiteUserInfo.CompanyID;
            dayMemo.MemoState = (EyouSoft.Model.ToolStructure.MemoDetailType.MemoState)Enum.Parse(typeof(EyouSoft.Model.ToolStructure.MemoDetailType.MemoState),memostate);
            bool result = false;
            string memId = Utils.GetFormValue(hdfMemID.UniqueID);
            if (!string.IsNullOrEmpty(memId))
            {
                //修改
                dayMemo.ID=memId;
                result = EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().Update(dayMemo);
            }
            else
            {
                //新增
                dayMemo.IssueTime = DateTime.Now;
                result = EyouSoft.BLL.ToolStructure.CompanyDayMemo.CreateInstance().Add(dayMemo);
            }
            if (result)
            {
                Response.Write("操作成功!");
                Response.End();
            }
            else
            {
                Response.Write("操作失败!");
                Response.End();
            }
        }
        #endregion
    }
}
