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

namespace UserBackCenter.EShop.SightShop
{
    /// <summary>
    /// 景区网店-广告设置
    /// </summary>
    /// 鲁功源  2010-12-09
    public partial class SetAdvInfo : EyouSoft.Common.Control.BasePage
    {
        #region  页面初始化
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

        #region 初始化广告信息
        /// <summary>
        /// 初始化广告信息
        /// </summary>
        private void InitInfo()
        {
            EyouSoft.Model.CompanyStructure.SupplierInfo model=EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (model != null && model.ProductInfo != null && model.ProductInfo.Count > 0)
            {
                EyouSoft.Model.CompanyStructure.ProductInfo product=model.ProductInfo[0];
                hOldAdvPath.Value = product.ImagePath;
                txtSite.Value = product.ImageLink.Replace("http://","");
                hOldAdvId.Value = product.ID.ToString();
                ltOldAdv.Text = string.Format("<a href=\"{0}\" target=\"_blank\">查看原图</a>", Domain.FileSystem + product.ImagePath);
                product = null;
            }
            model = null;
        }
        #endregion

        #region 保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string imgpath = Utils.GetFormValue("sfuAdv$hidFileName");
            string AdvSite=Utils.GetFormValue(txtSite.UniqueID);
            if (imgpath.Length < 1 && hOldAdvPath.Value.Length==0)
                strErr += "请选择上传的广告图片！\\n";
            if (AdvSite.Trim().Length == 0)
                strErr += "请输入广告链接！";
            if (!string.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndRedirect(this.Page, strErr, Request.RawUrl);
            }

            IList<EyouSoft.Model.CompanyStructure.ProductInfo> list=new List<EyouSoft.Model.CompanyStructure.ProductInfo>();
            EyouSoft.Model.CompanyStructure.ProductInfo model = new EyouSoft.Model.CompanyStructure.ProductInfo();
            model.ID = hOldAdvId.Value != "" ? int.Parse(hOldAdvId.Value) : 0;
            model.ImageLink = txtSite.Value.Contains("http://")?txtSite.Value:"http://"+txtSite.Value;
            model.ImagePath = imgpath.Length == 0 ? hOldAdvPath.Value : imgpath;
            list.Add(model);
            bool Result = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().SetProduct(SiteUserInfo.CompanyID, string.Empty, list);
            model = null;
            list = null;
            if (Result)
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            else
                MessageBox.ShowAndRedirect(this.Page, "alert(\"操作失败！\")", Request.RawUrl);
        }
        #endregion
    }
}
