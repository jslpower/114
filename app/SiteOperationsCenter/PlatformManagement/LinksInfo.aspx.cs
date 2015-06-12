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

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——友情链接
    /// 开发人：杜桂云      开发时间：2010-06-24
    /// </summary>
    public partial class LinksInfo : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        private int CurrencyPage = 1;   //当前页
        private int intPageSize = 20;   //每页显示的记录数
        protected int LinkType = 0; //链接类型
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["linkType"] != null)
                LinkType = Convert.ToInt32(Request.QueryString["linkType"]);
            if (!this.IsPostBack)
            {
                if (LinkType == (int)EyouSoft.Model.SystemStructure.FriendLinkType.资讯首页)//如果点击是从咨询链接那边过来
                {
                    //权限判断
                    YuYingPermission[] parms1 = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.新闻友情链接管理_管理该栏目 };
                    if (!CheckMasterGrant(parms1))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.新闻友情链接管理_管理该栏目, true);
                        return;
                    }
                    else
                    {
                        this.hyDelItem.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
                        this.hyDelItem.Attributes.Add("onclick", "return OnDeleteSubmit();");
                    }
                }
                else
                {
                    //权限判断
                    YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_友情链接 };
                    if (!CheckMasterGrant(parms))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.平台管理_友情链接, true);
                        return;
                        //this.hyDelItem.Visible = false;
                    }
                    else
                    {
                        this.hyDelItem.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
                        this.hyDelItem.Attributes.Add("onclick", "return OnDeleteSubmit();");
                    }
                }
                //绑定列表数据
                InitListInfo(LinkType);
            }
        }
        #endregion

        #region 绑定列表数据
        /// <summary>
        ///绑定列表数据
        /// </summary>
        private void InitListInfo(int linkType)
        {
            int intRecordCount = 0;
            //获取数据集
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> LinkList = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(intPageSize, CurrencyPage, ref intRecordCount, 1, (EyouSoft.Model.SystemStructure.FriendLinkType)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.FriendLinkType), linkType.ToString()));
            if (LinkList != null && LinkList.Count > 0)
            {
                //绑定数据源
                this.rpt_LinkList.DataSource = LinkList;
                this.rpt_LinkList.DataBind();
                //绑定分页控件
                this.ExportPageInfo2.intPageSize = intPageSize;//每页显示记录数
                this.ExportPageInfo2.intRecordCount = intRecordCount;
                this.ExportPageInfo2.CurrencyPage = CurrencyPage;
                this.ExportPageInfo2.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo2.UrlParams = Request.QueryString;
                this.ExportPageInfo2.PageLinkURL = "LinksInfo.aspx?";
                this.ExportPageInfo2.LinkType = 3;
            }
            else
            {
                this.NoData.Visible = true;
            }
            //释放资源
            LinkList = null;
        }
        #endregion

        #region 项绑定事件
        /// <summary>
        /// 项绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_LinkList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            }
        }
        #endregion

        #region 列表项的删除操作
        /// <summary>
        /// 列表项的删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hyDelItem_Click(object sender, ImageClickEventArgs e)
        {
            foreach (RepeaterItem item in this.rpt_LinkList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("cbListId");
                if (cb.Checked)
                {
                    //获取要删除的列表项的主键编号
                    Label lblItemID = (Label)item.FindControl("lblItemID");
                    int UserID = int.Parse(lblItemID.Text);
                    bool flag = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().DeleteSysFriendLink(UserID);

                }
            }
            MessageBox.ShowAndRedirect(this, "删除成功", string.Format("LinksInfo.aspx?linkType={0}", LinkType));
        }
        #endregion
    }
}
