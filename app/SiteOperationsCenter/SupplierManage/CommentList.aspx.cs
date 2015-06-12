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
    /// 评论列表页
    /// </summary>
    /// 周文超 2010-07-28
    public partial class CommentList : EyouSoft.Common.Control.YunYingPage
    {
        #region Attributes

        private int intPageSize = 50, CurrencyPage = 1;
        private EyouSoft.Model.CommunityStructure.TopicType TopicType = EyouSoft.Model.CommunityStructure.TopicType.未知;
        private string GuestId = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TopicType = (EyouSoft.Model.CommunityStructure.TopicType)Utils.GetInt(Request.QueryString["Type"]);
            GuestId = Utils.InputText(Request.QueryString["ID"]);
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
            IList<EyouSoft.Model.CommunityStructure.ExchangeComment> List = null;
            if(TopicType==EyouSoft.Model.CommunityStructure.TopicType.供求)
                List=EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().GetSupplyComment(intPageSize, CurrencyPage, ref intRecordCount, GuestId);
            if (TopicType == EyouSoft.Model.CommunityStructure.TopicType.嘉宾)
                List = EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().GetGuestInterview(intPageSize, CurrencyPage, ref intRecordCount, GuestId);
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
                this.ExportPageInfo.PageLinkURL = "/SupplierManage/CommentList.aspx?";
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

        /// <summary>
        /// 命令行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CommandName) || string.IsNullOrEmpty(e.CommandArgument.ToString()))
                return;

            bool Result = false;
            switch (e.CommandName.ToLower())
            {
                case "ckd":
                    Result = EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().SetIsCheck(true, e.CommandArgument.ToString());
                    break;
                case "nckd":
                    Result = EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().SetIsCheck(false, e.CommandArgument.ToString());
                    break;
                case "del":
                    Result = EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().Delete(e.CommandArgument.ToString());
                    break;
            }
            if (Result)
                MessageBox.ShowAndRedirect(this, "操作成功！", Request.RawUrl);
            else
                MessageBox.ShowAndRedirect(this, "操作失败！", Request.RawUrl);
        }

        #endregion
    }
}
