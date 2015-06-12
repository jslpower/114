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
    public partial class SetCopyRight : EyouSoft.Common.Control.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }

            if (!IsPostBack)
            {
                EyouSoft.Model.ShopStructure.HighShopCompanyInfo info = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID);
                if (info != null)
                {
                    editcopy.Value = info.ShopCopyRight;
                }
                info = null;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string context = Utils.EditInputText(editcopy.Value);
            if (!string.IsNullOrEmpty(context))
            {
                if (EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().SetCopyRight(this.SiteUserInfo.CompanyID, context))
                {
                    MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
                }
                else
                {
                    MessageBox.ResponseScript(this.Page, "alert('操作失败！');");
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "请填写版权内容！", "SetCopyRight.aspx");
            }
        }
    }
}
