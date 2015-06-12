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
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace UserBackCenter.GeneralShop
{
    public partial class SetCompanyInfo :EyouSoft.Common.Control.BasePage
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }        
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
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID); //公司详细信息
                if (compDetail != null)
                {
                    txtCompanyBrand.Value =Utils.InputText(compDetail.CompanyBrand);
                    txtOfficeAddress.Value = Utils.InputText(compDetail.CompanyAddress);
                    txtContactFax.Value = Utils.InputText(compDetail.ContactInfo.Fax);
                    txtContactName.Value = Utils.InputText(compDetail.ContactInfo.ContactName);
                    txtContactTel.Value = Utils.InputText(compDetail.ContactInfo.Tel);
                    txtContactMobile.Value = Utils.InputText(compDetail.ContactInfo.Mobile);
                    txtMQ.Value = Utils.InputText(compDetail.ContactInfo.MQ);      
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
                if (!IsCompanyCheck)
                {
                    //Response.Clear();
                    //Response.Write("对不起，您还未开通审核，不能进行此操作！");
                    //Response.End();
                    MessageBox.ResponseScript(this.Page, "alert('对不起，您还未开通审核，不能进行此操作！')");
                    return;
                }
                if (string.IsNullOrEmpty(txtCompanyBrand.Value.Trim()))
                {
                    MessageBox.ResponseScript(this.Page, "alert('请填写品牌名称！');");
                    return;
                }
                if (string.IsNullOrEmpty(txtContactName.Value.Trim()))
                {
                    MessageBox.ResponseScript(this.Page, "alert('请填写联系人！');");
                    return;
                }            
                EyouSoft.Model.CompanyStructure.CompanyArchiveInfo arch=new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();
                arch.CompanyAddress =  Utils.InputText(txtOfficeAddress.Value);
                arch.ContactInfo.Tel =StringValidate.SafeRequest(txtContactTel.Value);
                arch.ContactInfo.ContactName =Utils.InputText(txtContactName.Value);
                arch.ContactInfo.Fax = Utils.InputText(txtContactFax.Value);
                arch.ContactInfo.Mobile =Utils.InputText(txtContactMobile.Value);
                arch.CompanyBrand = Utils.InputText(this.txtCompanyBrand.Value);
                arch.ContactInfo.MQ=Utils.InputText(this.txtMQ.Value);
                arch.ID = this.SiteUserInfo.CompanyID;
                if (EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().UpdateSelf(arch))
                    MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
                else
                {
                    MessageBox.ShowAndRedirect(this.Page,"操作失败",Request.Url.ToString());
                }
            }
            #endregion
    }
}
