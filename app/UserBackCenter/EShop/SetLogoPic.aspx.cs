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
using System.IO;

namespace UserBackCenter.EShop
{
    public partial class SetLogoPic : EyouSoft.Common.Control.BasePage
    {
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

        //初始化信息
        private void InitInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo deinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID);
            string strType = Utils.GetQueryStringValue("Type");
            if (string.IsNullOrEmpty(strType))
                InitOldImg(deinfo);
            else if (strType.ToLower() == "t4")
                InitNewImg(deinfo);

            deinfo = null;
        }

        private void InitOldImg(EyouSoft.Model.CompanyStructure.CompanyDetailInfo deinfo)
        {
            divOldTemplate.Visible = true;
            divNewTemplate.Visible = false;
            if (deinfo != null)
            {
                string imgpath = deinfo.AttachInfo.CompanyShopBanner.ImagePath;   //高级网店头部图片路径
                hdfAgoImgPath.Value = imgpath;
                if (!string.IsNullOrEmpty(imgpath))
                {
                    ltr_logo.Text = string.Format("<a href={0}  target='_blank'  title=\"点击查看\" >查看原图</a>", Domain.FileSystem + imgpath);
                }
            }
        }

        protected void btnsubmitlogo_Click(object sender, EventArgs e)
        {
            string imgpath = Utils.GetFormValue("sfulogo$hidFileName");
            if (imgpath.Length < 1)
            {
                MessageBox.ShowAndRedirect(this.Page, "请选择上传的文件！", Request.Url.ToString());
                return;
            }
            EyouSoft.Model.CompanyStructure.CompanyShopBanner banner = new EyouSoft.Model.CompanyStructure.CompanyShopBanner();
            banner.ImagePath = imgpath;

            if (EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyShopBanner(this.SiteUserInfo.CompanyID, banner))
            {
                this.form1.InnerHtml = string.Empty;
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            }
            else
            {
                this.form1.InnerHtml = string.Empty;
                MessageBox.ShowAndRedirect(this.Page, "操作失败", "SetLogoPic.aspx");
            }
            banner = null;
        }

        #region 保存模版四的log图片

        /// <summary>
        /// 初始化模版四的log图片
        /// </summary>
        /// <param name="deinfo">公司信息实体</param>
        private void InitNewImg(EyouSoft.Model.CompanyStructure.CompanyDetailInfo deinfo)
        {
            divOldTemplate.Visible = false;
            divNewTemplate.Visible = true;
            if (deinfo != null)
            {
                switch (deinfo.AttachInfo.CompanyShopBanner.BannerType)
                {
                    case EyouSoft.Model.CompanyStructure.ShopBannerType.Default:
                        ckbDefault.Checked = true;
                        MessageBox.ResponseScript(this, "ShowFileUp('d')");
                        break;
                    case EyouSoft.Model.CompanyStructure.ShopBannerType.Personalize:
                        ckbCustom.Checked = true;
                        MessageBox.ResponseScript(this, "ShowFileUp('c')");
                        break;
                    default:
                        ckbDefault.Checked = true;
                        MessageBox.ResponseScript(this, "ShowFileUp('d')");
                        break;
                }

                //公司log
                string strLogo = deinfo.AttachInfo.CompanyShopBanner.CompanyLogo;
                //背景
                string strBackImg = deinfo.AttachInfo.CompanyShopBanner.BannerBackground;
                //个性化定制图片
                string strCustomImg = deinfo.AttachInfo.CompanyShopBanner.ImagePath;
                hidCompanyLog.Value = strLogo;
                hidBackImg.Value = strBackImg;
                hidCustomImg.Value = strCustomImg;
                if (!string.IsNullOrEmpty(strLogo))
                    ltrCompanyLog.Text = string.Format("<a href='{0}'  target='_blank'  title=\"点击查看\" >查看原图</a>", Domain.FileSystem + strLogo);
                if (!string.IsNullOrEmpty(strBackImg))
                    ltrBackImg.Text = string.Format("<a href='{0}'  target='_blank'  title=\"点击查看\" >查看原图</a>", Domain.FileSystem + strBackImg);
                if (!string.IsNullOrEmpty(strCustomImg))
                    ltrCustomImg.Text = string.Format("<a href='{0}'  target='_blank'  title=\"点击查看\" >查看原图</a>", Domain.FileSystem + strCustomImg);
            }
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            //公司log
            string strLogo = Utils.GetFormValue("sfuCompanyLog$hidFileName");
            //背景
            string strBackImg = Utils.GetFormValue("sfuBackImg$hidFileName");
            //个性化定制图片
            string strCustomImg = Utils.GetFormValue("sfuCustomImg$hidFileName");

            string strOldLogo = hidCompanyLog.Value;
            string strOldBackImg = hidBackImg.Value;
            string strOldCustomImg = hidCustomImg.Value;
            strLogo = string.IsNullOrEmpty(strLogo) ? strOldLogo : strLogo;
            strBackImg = string.IsNullOrEmpty(strBackImg) ? strOldBackImg : strBackImg;
            strCustomImg = string.IsNullOrEmpty(strCustomImg) ? strOldCustomImg : strCustomImg;

            EyouSoft.Model.CompanyStructure.ShopBannerType BannerType = EyouSoft.Model.CompanyStructure.ShopBannerType.Personalize;
            if (ckbDefault.Checked && (string.IsNullOrEmpty(strLogo) || string.IsNullOrEmpty(strBackImg)))
            {
                MessageBox.ShowAndRedirect(this.Page, "请选择上传的文件！", Request.Url.ToString());
                return;
            }
            else if (ckbCustom.Checked && string.IsNullOrEmpty(strCustomImg))
            {
                MessageBox.ShowAndRedirect(this.Page, "请选择上传的文件！", Request.Url.ToString());
                return;
            }
            if(ckbDefault.Checked)
                BannerType = EyouSoft.Model.CompanyStructure.ShopBannerType.Default;
            if(ckbCustom.Checked)
                BannerType = EyouSoft.Model.CompanyStructure.ShopBannerType.Personalize;

            EyouSoft.Model.CompanyStructure.CompanyShopBanner banner = new EyouSoft.Model.CompanyStructure.CompanyShopBanner();
            banner.ImagePath = strCustomImg;
            banner.BannerBackground = strBackImg;
            banner.CompanyLogo = strLogo;
            banner.BannerType = BannerType;

            if (EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyShopBanner(this.SiteUserInfo.CompanyID, banner))
            {
                this.form1.InnerHtml = string.Empty;
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            }
            else
            {
                this.form1.InnerHtml = string.Empty;
                MessageBox.ShowAndRedirect(this.Page, "操作失败", "SetLogoPic.aspx");
            }
            banner = null;
        }

        #endregion
    }
}
