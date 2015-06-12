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
    /// 同业学堂资讯列表
    /// </summary>
    /// 周文超 2010-07-29
    public partial class SchoolList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量

        private int CurrencyPage = 1;   //当前页
        private int intPageSize = 20;   //每页显示的记录数
        private EyouSoft.Model.CommunityStructure.TopicClass?[] TopicClass = new EyouSoft.Model.CommunityStructure.TopicClass?[] { EyouSoft.Model.CommunityStructure.TopicClass.案例分析, EyouSoft.Model.CommunityStructure.TopicClass.导游之家, EyouSoft.Model.CommunityStructure.TopicClass.计调指南, EyouSoft.Model.CommunityStructure.TopicClass.经验交流 };

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            hyDelItem.Attributes.Add("onclick", "return NewsInfoPage.DeleteAll();");
            hyDelItem.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
            if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.同业学堂_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                BindDropDownList();
                InitPageData();
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindDropDownList()
        {
            ddlTopicClass.Items.Clear();
            ddlTopicClass.DataSource = EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.TopicClass));
            ddlTopicClass.DataTextField = "text";
            ddlTopicClass.DataValueField = "value";
            ddlTopicClass.DataBind();
            ddlTopicClass.Items.Insert(0, new ListItem("请选择", "0"));

            ddlTopicArea.Items.Clear();
            ddlTopicArea.DataSource = EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.TopicAreas));
            ddlTopicArea.DataTextField = "text";
            ddlTopicArea.DataValueField = "value";
            ddlTopicArea.DataBind();
            ddlTopicArea.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitPageData()
        {
            int intRecordCount = 0;
            bool? IsPic = null;
            EyouSoft.Model.CommunityStructure.TopicClass? TopicType = null;
            EyouSoft.Model.CommunityStructure.TopicAreas? TopicArea = null;
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            int Type = Utils.GetInt(Request.QueryString["type"], -1);
            int Area = Utils.GetInt(Request.QueryString["area"], -1);
            int ispic = Utils.GetInt(Request.QueryString["ispic"], -1);
            string strkw = Utils.InputText(Request.QueryString["keyword"]);
            string strtag = Utils.InputText(Request.QueryString["tag"]);

            ddlIsPic.SelectedValue = ispic.ToString();
            ddlTopicArea.SelectedValue = Area.ToString();
            ddlTopicClass.SelectedValue = Type.ToString();
            txtKeyWord.Value = strkw;
            txtTag.Value = strtag;

            if (ispic == 1)
                IsPic = true;
            else if (ispic == 0)
                IsPic = false;
            if (Type >= 1)
                TopicType = (EyouSoft.Model.CommunityStructure.TopicClass)Type;
            if (Area >= 0)
                TopicArea = (EyouSoft.Model.CommunityStructure.TopicAreas)Area;
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> List = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetPageList(intPageSize, CurrencyPage, ref intRecordCount, TopicType, TopicArea, IsPic, strkw, strtag);

            if (!List.Equals(null) && List.Count > 0)
            { 
                rpt_NewsList.DataSource = List;
                rpt_NewsList.DataBind();
                //绑定分页控件
                this.ExportPageInfo.intPageSize = intPageSize;
                this.ExportPageInfo.intRecordCount = intRecordCount;
                this.ExportPageInfo.CurrencyPage = CurrencyPage;
                this.ExportPageInfo.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo.UrlParams = Request.QueryString;
                this.ExportPageInfo.PageLinkURL = "/SupplierManage/SchoolList.aspx?";
                this.ExportPageInfo.LinkType = 3;
            }
            else
            {
                NoData.Visible = true;
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
        protected void rpt_NewsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                LinkButton lbdel = (LinkButton)e.Item.FindControl("lkbDel");
                LinkButton lbtop = (LinkButton)e.Item.FindControl("lkbSetIsTop");
                LinkButton lbfront = (LinkButton)e.Item.FindControl("lkbSetFrontPage");
                if (ltr != null)
                    ltr.Text = ((CurrencyPage - 1) * intPageSize + e.Item.ItemIndex + 1).ToString();
                if (lbdel != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_删除))
                    {
                        lbdel.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                    else
                    {
                        lbdel.Attributes.Add("onclick", "return confirm('确定要执行删除操作吗？');");
                    }               
                }
                if (lbtop != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_修改))
                    {
                        lbtop.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                }
                if (lbfront != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_修改))
                    {
                        lbfront.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                }
            }
        }

        /// <summary>
        /// 命令行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_NewsList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CommandName) || string.IsNullOrEmpty(e.CommandArgument.ToString()))
                return;

            switch (e.CommandName.ToLower())
            {
                case "top":
                    if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_修改))
                    {
                        MessageBox.Show(this.Page, "对不起，你没有该权限");
                        return;
                    }
                    string[] strTmp = e.CommandArgument.ToString().Trim().Split(',');
                    bool IsTop = false;
                    if (strTmp == null || strTmp.Length != 2 || string.IsNullOrEmpty(strTmp[0]))
                        break;
                    if (strTmp[1].ToLower() == "true" || strTmp[1].ToLower() == "1")
                        IsTop = true;
                    if (EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().SetTop(strTmp[0], IsTop ? false : true))
                        MessageBox.ShowAndRedirect(this, "操作成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, "操作失败！", Request.RawUrl);
                    break;
                case "frontpage":
                    if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_修改))
                    {
                        MessageBox.Show(this.Page, "对不起，你没有该权限");
                        return;
                    }
                    string[] strTmp1 = e.CommandArgument.ToString().Trim().Split(',');
                    bool Isfrontpage = false;
                    if (strTmp1 == null || strTmp1.Length != 2 || string.IsNullOrEmpty(strTmp1[0]))
                        break;
                    if (strTmp1[1].ToLower() == "true" || strTmp1[1].ToLower() == "1")
                        Isfrontpage = true;
                    if (EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().SetFrontPage(strTmp1[0], Isfrontpage ? false : true))
                        MessageBox.ShowAndRedirect(this, "操作成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, "操作失败！", Request.RawUrl);
                    break;
                case "del":
                    if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_删除))
                    {
                        MessageBox.Show(this.Page, "对不起，你没有该权限");
                        return;
                    }
                    if (EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().Delete(e.CommandArgument.ToString()))
                        MessageBox.ShowAndRedirect(this, "删除成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, "删除失败！", Request.RawUrl);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 多条删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hyDelItem_Click(object sender, ImageClickEventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_删除))
            {
                MessageBox.Show(this.Page, "对不起，你没有该权限");
                return;
            }
            string[] strIds = Utils.GetFormValues("ckbId");
            if (strIds == null || strIds.Length <= 0)
                return;
            
            if (EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().DeleteByIds(strIds))
                MessageBox.ShowAndRedirect(this, "删除成功！", Request.RawUrl);
            else
                MessageBox.ShowAndRedirect(this, "删除失败！", Request.RawUrl);
        }
    }
}
