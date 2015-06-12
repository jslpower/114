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
    /// 嘉宾访谈新增修改
    /// </summary>
    /// 周文超2010-07-27
    public partial class AddHonoredGuest : EyouSoft.Common.Control.YunYingPage
    {
        private string HonoredGuestId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            GuestInterviewMenu1.MenuIndex = 3;
            HonoredGuestId = Utils.InputText(Request.QueryString["ID"]);
            txtBanner.SiteModule = SiteOperationsCenterModule.供求管理;
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.嘉宾访谈_管理该栏目, true);
                return;
            }
            if (string.IsNullOrEmpty(HonoredGuestId))
            {
                ltrTitle.Text = "新增嘉宾访谈";
            }
            else
            {
                ltrTitle.Text = "修改嘉宾访谈";
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
            if (string.IsNullOrEmpty(HonoredGuestId))
                return;

            EyouSoft.Model.CommunityStructure.HonoredGuest model = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().GetModel(HonoredGuestId);
            if (model == null)
            {
                MessageBox.ShowAndRedirect(this, "没有找到要修改的信息！", "/SupplierManage/HonoredGuest.aspx");
                return;
            }

            txtTitle.Value = model.Title;
            ltrOldBanner.Text = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + model.ImgPath, model.ImgPath.Substring(model.ImgPath.LastIndexOf('/') + 1));
            ltrOldSmallImg.Text = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">{1}</a>", Domain.FileSystem + model.ImgThumb, model.ImgThumb.Substring(model.ImgThumb.LastIndexOf('/') + 1));
            hdfOldOldBanner.Value = model.ImgPath;
            hdfOldSmallImg.Value = model.ImgThumb;
            txtHonoredGuest.Value = model.Content;
            txtView1.Value = model.Opinion1;
            txtView2.Value = model.Opinion2;
            txtView3.Value = model.Opinion3;
            txtSummary.Value = model.Summary;
        }

        /// <summary>
        /// 保存嘉宾访谈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool Result = false;
            string NewImgPath = Utils.GetFormValue("txtBanner$hidFileName");
            string NewImgThumb = Utils.GetFormValue("txtSmallImg$hidFileName");
            string OldImgPath = Utils.InputText(hdfOldOldBanner.Value);
            string OldThumb = Utils.InputText(hdfOldSmallImg.Value);
            string strTitle = txtTitle.Value;
            string strContent = txtHonoredGuest.Value;
            System.Text.StringBuilder strErr = new System.Text.StringBuilder();
            if (string.IsNullOrEmpty(strTitle))
                strErr.Append("请填写标题！\\n");
            if (string.IsNullOrEmpty(strContent))
                strErr.Append("请填写嘉宾介绍！\\n");
            if (!string.IsNullOrEmpty(strErr.ToString()))
            {
                MessageBox.ShowAndReturnBack(this, strErr.ToString(), 1);
                return;
            }
            EyouSoft.Model.CommunityStructure.HonoredGuest model = new EyouSoft.Model.CommunityStructure.HonoredGuest();
            model.Title = txtTitle.Value;
            model.Content = txtHonoredGuest.Value;
            model.Opinion1 = txtView1.Value;
            model.Opinion2 = txtView2.Value;
            model.Opinion3 = txtView3.Value;
            model.Summary = txtSummary.Value;
            model.OperatorId = MasterUserInfo.ID;
            if (string.IsNullOrEmpty(HonoredGuestId))
            {
                model.ImgPath = NewImgPath;
                model.ImgThumb = NewImgThumb; 

                Result = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().Add(model);
            }
            else
            {
                model.ID = HonoredGuestId;
                if(string.IsNullOrEmpty(NewImgPath))
                    model.ImgPath = OldImgPath;
                else
                    model.ImgPath = NewImgPath;
                if (string.IsNullOrEmpty(NewImgThumb))
                    model.ImgThumb = OldThumb;
                else
                    model.ImgThumb = NewImgThumb;

                Result = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().Update(model);
            }

            if (Result)
                MessageBox.ShowAndRedirect(this, "保存成功！", "/SupplierManage/HonoredGuest.aspx");
            else
                MessageBox.ShowAndRedirect(this, "保存失败！", "/SupplierManage/HonoredGuest.aspx");
        }
    }
}
