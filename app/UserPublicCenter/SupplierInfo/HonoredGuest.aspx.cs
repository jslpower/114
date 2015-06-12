using System;
using System.Collections.Generic;
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
namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-30
    /// 描述：供求信息-嘉宾访谈
    /// </summary>
    public partial class HonoredGuest : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 嘉宾访谈详细页地址
        /// </summary>
        protected string HonoredGuestInfoUrl = string.Empty;
        EyouSoft.IBLL.CommunityStructure.IExchangeComment Ibll = EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 3;

            #region 同业交流专区
            CommonTopic5.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.同业交流专区;
            CommonTopic5.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.文字描述;
            #endregion

            #region 绑定最新一期的嘉宾访谈及回复
            EyouSoft.IBLL.CommunityStructure.IHonoredGuest GuestBll = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance();
            EyouSoft.Model.CommunityStructure.HonoredGuest GuestModel= GuestBll.GetNewInfo();
            if (GuestModel != null)
            {
                ltrMoreComment.Text = string.Format("<a href=\"/SupplierInfo/HonoredGuestComment.aspx?Id={0}\" target=\"_blank\" class=\"heise12\">更多&gt;&gt;</a>", GuestModel.ID);
                HonoredGuestInfoUrl = "/SupplierInfo/HonoredGuestInfo.aspx?Id=" + GuestModel.ID;
                lbTitle.Text = string.Format("<a href=\"/SupplierInfo/HonoredGuestInfo.aspx?Id={0}\">{1}</a>", GuestModel.ID, GuestModel.Title);//Utils.GetText(GuestModel.Title, 10));
                lbContent.Text = GuestModel.Content;//Utils.GetText(GuestModel.Content, 50, true);
                OpinionContent1.Text = GuestModel.Opinion1;//Utils.GetText(GuestModel.Opinion1, 50, true);
                OpinionContent2.Text = GuestModel.Opinion2;//Utils.GetText(GuestModel.Opinion2, 50, true);
                OpinionContent3.Text = GuestModel.Opinion3;//Utils.GetText(GuestModel.Opinion3, 50, true);
                Summary.Text = GuestModel.Summary;//Utils.GetText(GuestModel.Summary, 50, true);
                hGuestId.Value = GuestModel.ID;
                #region 绑定访谈回复
                IList<EyouSoft.Model.CommunityStructure.ExchangeComment> CommentList= Ibll.GetGuestInterview(6, hGuestId.Value);
                if (CommentList != null && CommentList.Count > 0)
                {
                    RpComment.DataSource = CommentList;
                    RpComment.DataBind();
                }
                CommentList = null;
                #endregion
            }
            GuestModel = null;
            GuestBll = null;
            #endregion

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.CommunityStructure.ExchangeComment model = new EyouSoft.Model.CommunityStructure.ExchangeComment();
            model.CommentId = string.Empty;
            model.CommentIP = StringValidate.GetRemoteIP();
            model.CommentText = Utils.InputText(Utils.GetFormValue("txt_content"));
            model.CompanyId = base.SiteUserInfo.CompanyID;
            model.CompanyName = base.SiteUserInfo.CompanyName;
            model.IsAnonymous = IsAnonymous.Checked;
            model.IsCheck=false;
            model.IsDeleted=false;
            model.IsHasNextLevel=false;
            model.IssueTime=DateTime.Now;
            model.OperatorId=base.SiteUserInfo.ID;
            model.OperatorMQ=base.SiteUserInfo.ContactInfo.MQ;
            model.OperatorName=base.SiteUserInfo.ContactInfo.ContactName;
            model.TopicType=EyouSoft.Model.CommunityStructure.TopicType.嘉宾;
            model.TopicId=hGuestId.Value;
            int Result= Ibll.AddExchangeComment(model);
            Ibll = null;
            model = null;
            if (Result>0)
            {
                Utils.ShowAndRedirect("发表成功！",Request.RawUrl);
            }
            else
            {
                Utils.ShowAndRedirect("发表失败！",Request.RawUrl);
            }
        }
    }
}
