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
    /// 页面功能：平台管理——战略合作伙伴
    /// 开发人：杜桂云      开发时间：2010-06-24
    /// </summary>
    public partial class PartnersInfo : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        private int CurrencyPage = 1;   //当前页
        private int intPageSize =20;   //每页显示的记录数
        protected string imgUrl="";
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_战略合作伙伴 };
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_战略合作伙伴, true);
                return;
              //  this.hyDelItem.Visible = false;
            }
            else 
            {
                this.hyDelItem.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
                this.hyDelItem.Attributes.Add("onclick", "return OnDeleteSubmit();");
            }
            imgUrl = Domain.FileSystem;

            if (!this.IsPostBack)
            {
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
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> LinkList = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(intPageSize,CurrencyPage,ref intRecordCount,1,EyouSoft.Model.SystemStructure.FriendLinkType.战略合作);

            //填充分页数据
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            if (LinkList != null && LinkList.Count > 0)
            {
                //绑定数据源
                this.rpt_PartList.DataSource = LinkList;
                this.rpt_PartList.DataBind();
                //绑定分页控件
                this.ExportPageInfo2.intPageSize = intPageSize;//每页显示记录数
                this.ExportPageInfo2.intRecordCount = intRecordCount;
                this.ExportPageInfo2.CurrencyPage = CurrencyPage;
                this.ExportPageInfo2.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo2.UrlParams = Request.QueryString;
                this.ExportPageInfo2.PageLinkURL = "PartnersInfo.aspx?";
                this.ExportPageInfo2.LinkType = 3;

            }
            else
            {
                this.NoData.Visible = true;
            }
        }
        #endregion

        #region 项绑定事件
        /// <summary>
        /// 项绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_PartList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            foreach (RepeaterItem item in this.rpt_PartList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("cbListId");
                if (cb.Checked)
                {
                    //获取要删除的列表项的主键编号
                    Label lblItemID = (Label)item.FindControl("lblItemID");
                    int UserID = int.Parse(lblItemID.Text);
                    bool flag=EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().DeleteSysFriendLink(UserID);
                }
            }
            MessageBox.ShowAndRedirect(this, "删除成功", "PartnersInfo.aspx");
        }
        #endregion
    }
}
