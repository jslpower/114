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
    /// 顾问团队申请
    /// </summary>
    /// 周文超 2010-08-02
    public partial class ApplicationTeam : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 3;
            btnSave.Style.Add("background-image", ImageServerUrl + "/images/UserPublicCenter/subban.gif");

            txtPhoto.IsNeedLogin = false;

            if (!IsPostBack)
            {

                #region 访谈介绍

                CommonTopic1.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.访谈介绍;
                CommonTopic1.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.文字描述;

                #endregion

                #region 顾问团队

                CommonTopic2.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.顾问团队;
                CommonTopic2.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;

                #endregion

                #region 近期访谈回顾

                CommonTopic3.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.嘉宾访谈;
                CommonTopic3.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;

                #endregion

                #region 最新行业资讯

                CommonTopic4.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.嘉宾访谈下资讯;
                CommonTopic4.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
                CommonTopic4.TopicArea = null;
                CommonTopic4.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;

                #endregion
            }
        }

        /// <summary>
        /// 保存顾问团队信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strName = Utils.InputText(txtName.Value.Trim());
            string strMobile = Utils.InputText(txtMobile.Value.Trim());
            string NewImgPath = Utils.GetFormValue("ctl00$ctl00$Main$SupplierMain$txtPhoto$hidFileName");
            System.Text.StringBuilder strErr = new System.Text.StringBuilder();
            if (string.IsNullOrEmpty(strName))
                strErr.Append("请填写姓名！\\n");
            if (string.IsNullOrEmpty(strMobile) || Utils.IsMobilePhone(strMobile) == false)
                strErr.Append("请填写正确的手机号码！\\n");

            if (!string.IsNullOrEmpty(strErr.ToString()))
            {
                MessageBox.ShowAndReturnBack(this, strErr.ToString(), 1);
                return;
            }

            EyouSoft.Model.CommunityStructure.CommunityAdvisor model = new EyouSoft.Model.CommunityStructure.CommunityAdvisor();
            model.ContactName = strName;
            if (raSex.SelectedValue == "1")
                model.Sex = true;
            else
                model.Sex = false;
            model.CompanyName = Utils.InputText(txtCompany.Value.Trim());
            model.ContactTel = strMobile;
            model.QQ = Utils.InputText(txtQQ.Value.Trim());
            model.Job = Utils.InputText(txtJob.Value.Trim());
            model.Achieve = Utils.InputText(txtAchieve.Value.Trim());
            model.ImgPath = NewImgPath;

            model.IsCheck = false;
            model.IsShow = false;
            model.OperatorId = SiteUserInfo == null ? string.Empty : SiteUserInfo.ID;
            model.SysOperatorId = 0;

            int Result = 0;
            Result = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().AddCommunityAdvisor(model);

            if (Result > 0)
                MessageBox.ShowAndRedirect(this, "您的申请已经提交，请等待审核！", "/SupplierInfo/HonoredGuest.aspx");
            else
                MessageBox.ShowAndRedirect(this, "申请提交失败！", "/SupplierInfo/HonoredGuest.aspx");
        }
    }
}
