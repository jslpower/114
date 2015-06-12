using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.SupplierManage
{
    /// <summary>
    /// 顾问团队管理
    /// </summary>
    /// 周文超 2010-07-27
    public partial class CommunityAdvisor : EyouSoft.Common.Control.YunYingPage
    {
        #region Attributes

        private int intPageSize = 15, CurrencyPage = 1;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GuestInterviewMenu1.MenuIndex = 2;
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.嘉宾访谈_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPageData()
        {
            int intRecordCount = 0;
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);

            IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> List = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().GetCommunityAdvisorList(intPageSize, CurrencyPage, ref intRecordCount, null);

            if (!List.Equals(null) && List.Count > 0)
            {
                rptList.DataSource = List;
                rptList.DataBind();
                //绑定分页控件
                this.ExportPageInfo.intPageSize = intPageSize;
                this.ExportPageInfo.intRecordCount = intRecordCount;
                this.ExportPageInfo.CurrencyPage = CurrencyPage;
                this.ExportPageInfo.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo.UrlParams = Request.QueryString;
                this.ExportPageInfo.PageLinkURL = "/SupplierManage/CommunityAdvisor.aspx?";
                this.ExportPageInfo.LinkType = 3;
            }
            else
            {
                trNoData.Visible = true;
            }
            if (List != null) List.Clear();
            List = null;
        }

        #region 前台函数

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (!ltr.Equals(null))
                    ltr.Text = (intPageSize * (CurrencyPage - 1) + e.Item.ItemIndex + 1).ToString();
                LinkButton lkbD = (LinkButton)e.Item.FindControl("lkbDel");
                LinkButton lkbCancel = (LinkButton)e.Item.FindControl("lkbCancel");
                if (!lkbD.Equals(null))
                {
                    if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_顾问团队审核))
                    {
                        lkbD.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                    else
                    {
                        lkbD.Attributes.Add("onclick", "return confirm('确定要进行删除操作吗？');");
                    }
                }
                if (lkbCancel != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_顾问团队审核))
                    {
                        lkbCancel.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                }
            }
        }

        /// <summary>
        /// 命令行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CommandName) || string.IsNullOrEmpty(e.CommandArgument.ToString()))
                return;
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_顾问团队审核))
            {
                 MessageBox.Show(this.Page, "对不起，你没有该权限");
                return;
            }
            switch (e.CommandName.ToLower())
            {
                case "show":
                    string[] strTmp = e.CommandArgument.ToString().Trim().Split(',');
                    bool IsShow = false;
                    if (strTmp == null || strTmp.Length != 2 || string.IsNullOrEmpty(strTmp[0]) || StringValidate.IsInteger(strTmp[0]) == false)
                        break;
                    if (strTmp[1].ToLower() == "true" || strTmp[1].ToLower() == "1")
                        IsShow = true;
                    if (EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().SetIsShow(IsShow ? false : true, MasterUserInfo.ID, int.Parse(strTmp[0])))
                        MessageBox.ShowAndRedirect(this, "操作成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, "操作失败！", Request.RawUrl);
                    break;
                case "del":
                    if (StringValidate.IsInteger(e.CommandArgument.ToString()) == false)
                        break;
                    if (EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().DeleteCommunityAdvisor(int.Parse(e.CommandArgument.ToString())))
                        MessageBox.ShowAndRedirect(this, "删除成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, "删除失败！", Request.RawUrl);
                    break;
            }
        }

        #endregion
    }
}
