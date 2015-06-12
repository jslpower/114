using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.AdvertiseTicket
{
    /// <summary>
    /// 修改机票咨询信息
    /// 开发人：刘玉灵  时间：2010-10-12
    /// </summary>
    public partial class EditTicketMessage : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 修改咨询ID
        /// </summary>
        private string EditApplyId = "";
        /// <summary>
        /// 返回URL
        /// </summary>
        protected string strReturnUrl = "/AdvertiseTicket/Default.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.广告机票_机票查询_管理该栏目))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]))
            {
                strReturnUrl = Utils.InputText( Request.QueryString["returnUrl"]);
            }
            EditApplyId =Utils.InputText( Request.QueryString["EditId"]);
            this.hidEditId.Value = EditApplyId;
            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                string type = Request.QueryString["Type"].ToString();
                if (type.Equals("Update", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Write(this.UpdateApply());
                    Response.End();
                }
            }
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["EditId"]))
                {
                    Response.Redirect(strReturnUrl);
                    Response.End();
                    return;
                }
                else
                {
                    this.GetTicketApplyInfo();
                }
            }
        }
        /// <summary>
        /// 初始化信息
        /// </summary>
        private void GetTicketApplyInfo()
        {
            EyouSoft.Model.TicketStructure.TicketApply  ApplyModel= EyouSoft.BLL.TicketStructure.TicketApply.CreateInstance().GetModel(EditApplyId);
            if (ApplyModel != null)
            {
                this.literTicketArticleTitle.Text =Utils.InputText(  ApplyModel.TicketArticleTitle);
                this.literVoyageSet.Text = ApplyModel.TicketFlight.VoyageSet.ToString();
                this.leterSumPeople.Text = ApplyModel.TicketFlight.PeopleNumber.ToString()+"人";
                this.literPeopleCountryType.Text = ApplyModel.TicketFlight.PeopleCountryType == EyouSoft.Model.TicketStructure.PeopleCountryType.Foreign ? "外宾 " : "内宾 "; 

                this.txtTakeOffDate.Text = ApplyModel.TicketFlight.TakeOffDate.Value.ToShortDateString();
                if (ApplyModel.TicketFlight.VoyageSet != EyouSoft.Model.TicketStructure.VoyageType.单程)
                {
                    this.txtReturnDate.Text = ApplyModel.TicketFlight.ReturnDate.Value.ToShortDateString();
                }
                this.txtRemark.Value =ApplyModel.Remark;
                
                //采购商资料
                this.txtCompanyName.Text = ApplyModel.CompanyName;
                this.txtContactName.Text=ApplyModel.ContactName;
                this.txtContactTel.Text = ApplyModel.CompanyTel;
                this.txtContactAddress.Text = ApplyModel.CompanyAddress;
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page,"暂无该信息记录!", strReturnUrl);
                return;
            }
            ApplyModel=null;
        }

        /// <summary>
        /// 修改
        /// </summary>
        protected bool UpdateApply()
        {
            bool Result = false;
            EyouSoft.Model.TicketStructure.TicketApply UpdateModel = new EyouSoft.Model.TicketStructure.TicketApply();
            UpdateModel.ApplyId = Utils.InputText( Request.Form["hidEditId"]);
            UpdateModel.TicketFlight.TakeOffDate = Utils.GetDateTimeNullable(Request.Form["txtTakeOffDate$dateTextBox"]);
            UpdateModel.TicketFlight.ReturnDate = Utils.GetDateTimeNullable(Request.Form["txtReturnDate$dateTextBox"]);
            UpdateModel.Remark = Utils.InputText(Request.Form["txtRemark"]);
            UpdateModel.CompanyName = Utils.InputText(Request.Form["txtCompanyName"]);
            UpdateModel.ContactName = Utils.InputText(Request.Form["txtContactName"]);
            UpdateModel.CompanyTel = Utils.InputText(Request.Form["txtContactTel"]);
            UpdateModel.CompanyAddress = Utils.InputText(Request.Form["txtContactAddress"]);
            Result= EyouSoft.BLL.TicketStructure.TicketApply.CreateInstance().Update(UpdateModel);
            UpdateModel = null;
            return Result;
        }
    }
}
