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

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 关于我们设置
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// 
    public partial class SetAboutUs :EyouSoft.Common.Control.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //加载数据
          
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }            
            
            if (!IsPostBack)
            {
                EyouSoft.Model.ShopStructure.HighShopCompanyInfo info = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID);
                if (info != null)
                {
                    editabout.Value = info.CompanyInfo;
                }
                info = null;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string context =Utils.EditInputText(editabout.Value);
            if (!string.IsNullOrEmpty(context))
            {
                if (EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().SetAboutUs(this.SiteUserInfo.CompanyID, context))
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
                MessageBox.ShowAndRedirect(this.Page, "内容不能为空！", "SetAboutUs.aspx");
            }
        }       
    }
}
