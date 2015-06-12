using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 嘉宾访谈评论回复页
    /// </summary>
    /// 周文超 2010-08-05
    public partial class HonoredGuestComment : EyouSoft.Common.Control.FrontPage
    {
        #region 页面级变量(私有)

        /// <summary>
        /// 嘉宾访谈ID
        /// </summary>
        protected string HonoredGuestId = string.Empty;
        /// <summary>
        /// 嘉宾访谈列表页
        /// </summary>
        private string HonoredGuestListPageUrl = "/SupplierInfo/ExchangeList.aspx";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsLogin)
            {
                panSaveComment.Visible = true;
                ltrUserName.Text = SiteUserInfo.UserName;
                ibtnSave.ImageUrl = Domain.ServerComponents + "/images/UserPublicCenter/20090716tijiao.gif";
            }
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 3;

            HonoredGuestId = Utils.InputText(Request.QueryString["Id"]);

            if (string.IsNullOrEmpty(HonoredGuestId))
            {
                MessageBox.ShowAndRedirect(this, "未找到您要查看的信息！", HonoredGuestListPageUrl);
                return;
            }
        }

        /// <summary>
        /// 提交回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibtnSave_Click(object sender, ImageClickEventArgs e)
        {
            string strInfo = Utils.InputText(txtCommentInfo.Value.Trim());
            string strTopicId = HonoredGuestId;
            string strCommentId = Utils.GetFormValue("hidCommentId");
            if (string.IsNullOrEmpty(strInfo))
            {
                MessageBox.ShowAndReturnBack(this, "回复内容不能为空！", 1);
                return;
            }
            if (strInfo.Length > 500)
            {
                MessageBox.ShowAndReturnBack(this, "回复内容应限制在500个字符以内！", 1);
                return;
            }

            EyouSoft.Model.CommunityStructure.ExchangeComment model = new EyouSoft.Model.CommunityStructure.ExchangeComment();
            model.CommentId = strCommentId;
            model.CommentIP = StringValidate.GetRemoteIP();
            model.CommentText = strInfo;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.IsAnonymous = ckbIsAnonymous.Checked;
            model.IsCheck = false;
            model.IsDeleted = false;
            model.IsHasNextLevel = false;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = SiteUserInfo.ContactInfo == null ? "0" : SiteUserInfo.ContactInfo.MQ;
            model.OperatorName = SiteUserInfo.ContactInfo == null ? string.Empty : SiteUserInfo.ContactInfo.ContactName;
            model.TopicId = strTopicId;
            model.TopicType = EyouSoft.Model.CommunityStructure.TopicType.嘉宾;

            if (EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().AddExchangeComment(model) == 1)
            {
                MessageBox.ShowAndRedirect(this, "提交评论成功！", Request.RawUrl);
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "提交评论失败！", Request.RawUrl);
                return;
            }
        }
    }
}
