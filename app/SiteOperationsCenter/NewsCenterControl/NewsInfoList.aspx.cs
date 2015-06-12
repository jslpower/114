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
using System.Text;
using EyouSoft.Common.Function;
using System.Collections.Generic;
using EyouSoft.Common;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 页面功能：平台管理——新闻中心列表页
    /// 开发人：杜桂云      开发时间：2010-07-22
    /// </summary>
    public partial class NewsInfoList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        private int CurrencyPage = 1;   //当前页
        private int intPageSize = 20;   //每页显示的记录数
        protected bool EditFlag = false; //修改权限
        protected bool DeleteFlag = false; //删除权限
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] editParms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_修改 };
            EditFlag = CheckMasterGrant(editParms);
            YuYingPermission[] deleteParms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_删除 };
            DeleteFlag = CheckMasterGrant(deleteParms);
            if(!EditFlag && !DeleteFlag)
            {
                Utils.ResponseNoPermit();
                return;
            }
            //根据权限判断批量删除的按钮的显示和隐藏
            if (DeleteFlag)
            {
                this.hyDelItem.Attributes.Add("onclick", "return NewsInfoPage.OnDeleteSubmit();");
                this.hyDelItem.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
            }
            else
            {
                this.hyDelItem.Visible = false;
            }
            if (!this.IsPostBack)
            {
                //页面请求删除操作
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["DeleteID"])))
                {
                    EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().Delete(Utils.GetInt(Request.QueryString["DeleteID"]));
                    MessageBox.ShowAndRedirect(this, "删除成功", "NewsInfoList.aspx");
                }
                //绑定列表数据
                InitListInfo();
            }
        }
        #endregion

        #region 绑定列表数据
        /// <summary>
        ///绑定列表数据
        /// </summary>
        private void InitListInfo()
        {
            int intRecordCount = 0;
            //获取数据集
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetList(intPageSize,CurrencyPage,ref intRecordCount,null);
            if (ModelList != null && ModelList.Count > 0)
            {
                //绑定数据源
                this.rpt_NewsList.DataSource = ModelList;
                this.rpt_NewsList.DataBind();
                //绑定分页控件
                this.ExportPageInfo2.intPageSize = intPageSize;//每页显示记录数
                this.ExportPageInfo2.intRecordCount = intRecordCount;
                this.ExportPageInfo2.CurrencyPage = CurrencyPage;
                this.ExportPageInfo2.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo2.LinkType = 3;
            }
            else
            {
                this.NoData.Visible = true;
            }
            //释放资源
            ModelList = null;
        }
        #endregion
        #region 转换时间显示格式
        protected string GetTime(DateTime times) 
        {
            return times.ToString("MM/dd hh:mm");
        }
        #endregion

        #region 新闻标题连接
        protected string GetTitle(string TitleName, int NewsID)
        {
            return string.Format("<a href=\"OperatorNewsPage.aspx?EditID={0}\">{1}</a>", NewsID, TitleName);
        }
        #endregion

        #region 操作权限
        protected string CreateOperation(int NewsID) 
        {
            string returnVal = string.Format("<a href='OperatorNewsPage.aspx?EditID={0}'>修改</a>", NewsID);
            if (DeleteFlag)
            {
                returnVal = string.Format("<a href='OperatorNewsPage.aspx?EditID={0}'>修改</a><strong>/</strong><a href='javascript:;' onclick='NewsInfoPage.OnItemDeleteSubmit({0});return false;'>删除</a>", NewsID);
            }
            return returnVal;
        }
        #endregion

        #region 批量删除
        protected void hyDelItem_Click(object sender, ImageClickEventArgs e)
        {
            foreach (RepeaterItem item in this.rpt_NewsList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("cbListId");
                if (cb.Checked)
                {
                    //获取要删除的列表项的主键编号
                    Label lblItemID = (Label)item.FindControl("lblItemID");
                    int UserID = int.Parse(lblItemID.Text);
                    EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().Delete(UserID);
                }
            }
            MessageBox.ShowAndRedirect(this, "删除成功", "NewsInfoList.aspx");
        }
        #endregion

        #region 项绑定事件
        protected void rpt_NewsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                //------------添加序号-------------------------
                //取得当前项的索引值加1,因为项的索引值是从0开始的.
                int itemNum = e.Item.ItemIndex + 1;
                Label lblItemID = (Label)e.Item.FindControl("lblItemID");
                CheckBox cbListId = (CheckBox)e.Item.FindControl("cbListId");
                cbListId.Text = Convert.ToString((CurrencyPage - 1) * intPageSize + itemNum);
                cbListId.Attributes.Add("InnerValue", lblItemID.Text);
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //操作状态
                    LinkButton linkState = (LinkButton)e.Item.FindControl("linkState");
                    Label lblIsEnable = (Label)e.Item.FindControl("lblIsEnable");
                    if (lblIsEnable.Text == "False")
                    {
                        linkState.Text = "置顶";
                    }
                    else if (lblIsEnable.Text == "True")
                    {
                        linkState.Text = "<span class='RedFnt'>取消置顶</span>";
                    }
                }
            }
        }
        #endregion

        #region 处理项事件
        protected void rpt_NewsList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //设置置顶
            if (e.CommandName == "Lock")
            {
                int userId = Int32.Parse(e.CommandArgument.ToString());
                LinkButton linkBtn = e.CommandSource as LinkButton;

                if (EditFlag)
                {
                    if (linkBtn.Text == "置顶")
                    {
                        //修改取消置顶
                        bool flag = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().SetTop(userId, true);
                        if (flag == true)
                            MessageBox.ShowAndRedirect(this, "操作成功！", "NewsInfoList.aspx");
                        else
                            MessageBox.ShowAndRedirect(this, "操作失败！", "NewsInfoList.aspx");
                    }
                    else if (linkBtn.Text == "<span class='RedFnt'>取消置顶</span>")
                    {
                        //取消设置

                        bool flag = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().SetTop(userId, false);
                        if (flag == true)
                            MessageBox.ShowAndRedirect(this, "操作成功！", "NewsInfoList.aspx");
                        else
                            MessageBox.ShowAndRedirect(this, "操作失败！", "NewsInfoList.aspx");
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(this, "对不起，您没有修改权限！", "NewsInfoList.aspx");
                }
            }
        }
        #endregion
    }
}
