using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.CommunityStructure;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.TyProductManage
{
    /// <summary>
    /// 页面功能：运营后台--同业114产品管理--提醒（登录下面的提醒）列表管理
    /// CrateTime:2011-05-12
    /// </summary>
    /// Author:刘咏梅
    public partial class LoginedRemindList : EyouSoft.Common.Control.YunYingPage
    {

        #region 初始化页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.提醒_登录下面的提醒))
                {
                    MessageBox.Show(this.Page, "对不起，你没有该权限");
                    return;
                }
               
                //初始化登录提醒数据
                InitLoginedRemindList();
            }
        }
        #endregion

        #region 项绑定事件 绑定下拉框里的类型
        protected void crp_LoginRemindList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
            {
                DropDownList ddlType = e.Item.FindControl("ddlType") as DropDownList;
                if (ddlType != null)
                {
                    foreach (var str in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.TitleTypes)))
                    {
                        ListItem item = new ListItem();
                        item.Text = str;
                        item.Value = ((int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.TitleTypes), str)).ToString();
                        ddlType.Items.Add(item);
                    }
                    ddlType.Items.Insert(0, new ListItem("请选择", "-1"));
                }
            }
        }
        #endregion

        #region 保存提醒
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<Remind> list = new List<Remind>();
            string[] SortNos = Utils.GetFormValues("txtSerialNo");
            string[] RemindTypes = Utils.GetFormValues("hType");
            string[] EventDates = Utils.GetFormValues("txtEventTime");
            string[] OperatorNames = Utils.GetFormValues("hOperatorName");
            string[] IsShows = Utils.GetFormValues("cbIsShow");
            for (int i = 0; i < IsShows.Length; i++)
            {
                if (!string.IsNullOrEmpty(IsShows[i]) && IsShows[i] == "1"
                    && !string.IsNullOrEmpty(RemindTypes[i]) && RemindTypes[i] != "-1")
                {
                    Remind model = new Remind();
                    model.Sort = string.IsNullOrEmpty(SortNos[i]) ? 0 : int.Parse(SortNos[i]);
                    model.EventTime = string.IsNullOrEmpty(EventDates[i]) ? DateTime.Now : DateTime.Parse(EventDates[i]);
                    model.IsDisplay = string.IsNullOrEmpty(IsShows[i]) ? false : (IsShows[i] == "1" ? true : false);
                    model.TilteType = (EyouSoft.Model.CommunityStructure.TitleTypes)int.Parse(RemindTypes[i]);
                    model.Operator = OperatorNames[i];
                    list.Add(model);
                    model = null;
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show(this, "请选择您要操作的行！");
                return;
            }
            bool result = EyouSoft.BLL.CommunityStructure.Remind.CreateInstance().AddRemind(list);
            if (result)
                MessageBox.Show(this, "添加成功！");
            else
                MessageBox.Show(this, "添加失败！");
            InitLoginedRemindList();
        }
        #endregion


        #region 绑定提醒消息列表
        private void InitLoginedRemindList()
        {
            //获取提醒消息列表
            IList<Remind> list = EyouSoft.BLL.CommunityStructure.Remind.CreateInstance().GetList();
            if (list != null && list.Count > 0)
            {
                this.crp_LoginRemindList.DataSource = list;
                this.crp_LoginRemindList.DataBind();
            }
            else
                this.crp_LoginRemindList.EmptyText = "暂时没有数据";
        }
        #endregion
    }
}
