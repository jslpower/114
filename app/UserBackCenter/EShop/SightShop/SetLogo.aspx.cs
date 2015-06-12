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

namespace UserBackCenter.EShop.SightShop
{
    /// <summary>
    /// 景区网店LOGO设置
    /// </summary>
    /// 鲁功源  2010-12-09
    public partial class SetLogo : EyouSoft.Common.Control.BasePage
    {
        #region 页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        #endregion

        #region 初始化LOGO
        private void InitInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyAttachInfo model = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (model != null && model.CompanyLogo!=null && !string.IsNullOrEmpty(model.CompanyLogo.ImagePath))
            {
                hOldLogoPath.Value = model.CompanyLogo.ImagePath;
                ltOldLogo.Text = string.Format("<a href=\"{0}\" target=\"_blank\">查看原图</a>", Domain.FileSystem + model.CompanyLogo.ImagePath);
            }
            model = null;
        }
        #endregion

        #region 保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ImageLogo = Utils.GetFormValue("sfuLogo$hidFileName");
            if (ImageLogo.Length < 1 && hOldLogoPath.Value.Length == 0)
            {
                MessageBox.ShowAndRedirect(this.Page, "请上传LOGO文件!", Request.RawUrl);
            }
            EyouSoft.Model.CompanyStructure.CompanyLogo model=new EyouSoft.Model.CompanyStructure.CompanyLogo();
            model.ImagePath = ImageLogo.Length == 0 ? hOldLogoPath.Value : ImageLogo;
            bool Result= EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyLogo(SiteUserInfo.CompanyID, model);
            model = null;
            if (Result)
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            else
                MessageBox.ShowAndRedirect(this.Page, "alert(\"操作失败！\")", Request.RawUrl);
        }
        #endregion
    }
}
