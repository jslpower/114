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
    /// 嘉宾访谈信息管理
    /// </summary>
    /// 周文超 2010-07-27
    public partial class HonoredGuest : EyouSoft.Common.Control.YunYingPage
    {
        #region Attributes

        private int intPageSize = 15, CurrencyPage = 1;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ibtnDel.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
            ibtnDel.Attributes.Add("onclick", "return DelConfirm();");
            GuestInterviewMenu1.MenuIndex = 3;
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
            string strKeyWord = Utils.InputText(Request.QueryString["keyword"]);
            txtKeyWord.Value = strKeyWord;
            IList<EyouSoft.Model.CommunityStructure.HonoredGuest> List = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().GetPageList(intPageSize, CurrencyPage, ref intRecordCount, Utils.InputText(strKeyWord));
            if (List != null && List.Count > 0)
            {
                rptList.DataSource = List;
                rptList.DataBind();
                //绑定分页控件
                this.ExportPageInfo.intPageSize = intPageSize;
                this.ExportPageInfo.intRecordCount = intRecordCount;
                this.ExportPageInfo.CurrencyPage = CurrencyPage;
                this.ExportPageInfo.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo.UrlParams = Request.QueryString;
                this.ExportPageInfo.PageLinkURL = "/SupplierManage/HonoredGuest.aspx?";
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
                    ltr.Text = (intPageSize * (CurrencyPage - 1) + e.Item.ItemIndex + 1).ToString(); ;
            }
        }

        #endregion

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibtnDel_Click(object sender, ImageClickEventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_删除))
            {
                 MessageBox.Show(this.Page, "对不起，你没有该权限");
                return;
            }
            string[] strIds = Utils.GetFormValues("ckbId");
            if (strIds == null || strIds.Length <= 0)
            {
                MessageBox.ShowAndRedirect(this, "请选择要删除的项！", Request.RawUrl);
                return;
            }

            if (EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().DeleteByIds(strIds))
                MessageBox.ShowAndRedirect(this, "删除成功！", Request.RawUrl);
            else
                MessageBox.ShowAndRedirect(this, "删除失败！", Request.RawUrl);
        }
    }
}
