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
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 上传公司信息
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// 
    public partial class CompanyProfile : EyouSoft.Common.Control.BasePage
    {      
        private string CompanyId =string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            CompanyId=this.SiteUserInfo.CompanyID;
            if (!IsPostBack)
            {
                InitPage();
            }
        }
            #region 初始化
            /// <summary>
            /// 初始化页面
            /// </summary>
            private void InitPage()
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId); //公司详细信息
                if (compDetail != null)
                {
                    txtCompanyBrand.Value =Utils.InputText( compDetail.CompanyBrand);                  
                    txtOfficeAddress.Value =Utils.InputText(compDetail.CompanyAddress);
                    txtContactFax.Value =Utils.InputText(compDetail.ContactInfo.Fax);
                    txtContactName.Value =Utils.InputText(compDetail.ContactInfo.ContactName);
                    txtContactTel.Value =Utils.InputText(compDetail.ContactInfo.Tel);
                    txtContactMobile.Value =Utils.InputText(compDetail.ContactInfo.Mobile);
                    txtMQ.Value =Utils.InputText(compDetail.ContactInfo.MQ); 
                }
                compDetail = null;                             
            }
            #endregion
        #region 保存
        /// <summary>
            /// 保存
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void btnSave_Click(object sender, EventArgs e)
            {
                if(string.IsNullOrEmpty(txtCompanyBrand.Value.Trim()))
                {
                    MessageBox.ResponseScript(this.Page,"alert('请填写品牌名称！');");
                    return;
                } 
                if (string.IsNullOrEmpty(txtContactName.Value.Trim()))
                {
                    MessageBox.ResponseScript(this.Page, "alert('请填写联系人！');");
                    return;
                }
                if (!string.IsNullOrEmpty(txtContactMobile.Value.Trim()) && !Utils.IsMobile(txtContactMobile.Value.Trim()))
                {
                    MessageBox.ResponseScript(this.Page, "alert('手机号码填写错误！');");
                    return;
                }
                EyouSoft.Model.CompanyStructure.CompanyArchiveInfo info = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();
                info.CompanyAddress = Utils.InputText(txtOfficeAddress.Value);
                info.ContactInfo.Tel = StringValidate.SafeRequest(txtContactTel.Value);
                info.ContactInfo.ContactName = Utils.InputText(txtContactName.Value);
                info.ContactInfo.Fax = Utils.InputText(txtContactFax.Value);
                info.ContactInfo.Mobile = Utils.InputText(txtContactMobile.Value);
                info.CompanyBrand = Utils.InputText(this.txtCompanyBrand.Value);
                info.ContactInfo.MQ = Utils.InputText(this.txtMQ.Value);
                info.ID = CompanyId;
                if (EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().UpdateArchive(info))
                {
                    MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作失败",Request.Url.ToString());
                }
            }
            #endregion
    }
}
