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
    /// 新增修改顾问团队
    /// </summary>
    /// 周文超 2010-07-27
    public partial class AddCommunityAdvisor : EyouSoft.Common.Control.YunYingPage
    {
        private int CommunityAdvisorId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Style.Add("background-image", ImageServerUrl + "/images/UserPublicCenter/subban.gif");
            CommunityAdvisorId = Utils.GetInt(Request.QueryString["ID"]);
            GuestInterviewMenu1.MenuIndex = 2;
            txtPhoto.SiteModule = SiteOperationsCenterModule.供求管理;
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.嘉宾访谈_管理该栏目, true);
                return;
            }
            if (CommunityAdvisorId > 0)
            {
                ltrTitle.Text = "修改顾问团队";
                ltrSmallTitle.Text = "修改顾问团队";
            }
            else
            {
                ltrTitle.Text = "新增顾问团队";
                ltrSmallTitle.Text = "新增顾问团队";
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
            if (CommunityAdvisorId <= 0)
            {
                raSex.SelectedValue = "1";
                return;
            }

            EyouSoft.Model.CommunityStructure.CommunityAdvisor model = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().GetCommunityAdvisor(CommunityAdvisorId);
            if (model.Equals(null))
            {
                MessageBox.ShowAndRedirect(this, "没有找到要修改的信息！", "/SupplierManage/CommunityAdvisor.aspx");
                return;
            }

            txtName.Value = model.ContactName;
            raSex.SelectedValue = model.Sex ? "1" : "0";
            txtCompany.Value = model.CompanyName;
            txtMobile.Value = model.ContactTel;
            txtQQ.Value = model.QQ;
            txtJob.Value = model.Job;
            ltrOldImg.Text = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + model.ImgPath, model.ImgPath.Substring(model.ImgPath.LastIndexOf('/') + 1));
            hdfOldImgPath.Value = model.ImgPath;
            txtAchieve.Value = model.Achieve;
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
            string NewImgPath = Utils.GetFormValue("txtPhoto$hidFileName");
            string OldImgPath = Utils.InputText(hdfOldImgPath.Value);
            System.Text.StringBuilder strErr = new System.Text.StringBuilder();
            if (string.IsNullOrEmpty(strName))
                strErr.Append("请填写姓名！\\n");
            if(string.IsNullOrEmpty(strMobile) || Utils.IsMobilePhone(strMobile) == false)
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
            if (CommunityAdvisorId > 0)
            {
                if (string.IsNullOrEmpty(NewImgPath))
                    model.ImgPath = OldImgPath;
                else
                    model.ImgPath = NewImgPath;
            }
            else if (CommunityAdvisorId <= 0)
                model.ImgPath = NewImgPath;

            model.IsCheck = true;
            model.IsShow = true;
            model.OperatorId = MasterUserInfo.ID.ToString();
            model.SysOperatorId = MasterUserInfo.ID;

            int Result = 0;
            if (CommunityAdvisorId > 0)
            {
                model.ID = CommunityAdvisorId;
                Result = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().UpdateCommunityAdvisor(model);
            }
            else
                Result = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance().AddCommunityAdvisor(model);

            if (Result > 0)
                MessageBox.ShowAndRedirect(this, "保存成功！", "/SupplierManage/CommunityAdvisor.aspx");
            else
                MessageBox.ShowAndRedirect(this, "保存失败！", "/SupplierManage/CommunityAdvisor.aspx");
        }
    }
}
