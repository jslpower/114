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
    public partial class SetLeftImage :EyouSoft.Common.Control.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            if (!IsPostBack)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo deinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID);
                if (deinfo != null)
                {
                    if (!string.IsNullOrEmpty(deinfo.AttachInfo.CompanyImg.ImagePath))
                    {
                        lbl_showImg.Text = string.Format("<a href='{0}' target='_blank' title='点击查看'>查看原图</a> ", Domain.FileSystem + deinfo.AttachInfo.CompanyImg.ImagePath); //图片路径
                    }                   
                }
                deinfo = null;
            }
        }

        protected void btnSubmtImg_Click(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                //Response.Clear();
                //Response.Write("对不起，您还未开通审核，不能进行此操作！");
                //Response.End();
                MessageBox.ResponseScript(this.Page, "alert('对不起，您还未开通审核，不能进行此操作！')");
                return;
            }
            string imgpath = Utils.GetFormValue("sfucard$hidFileName");
            if (imgpath.Length<1)
            {
                MessageBox.ShowAndRedirect(this.Page, "请选择上传的文件！", "SetLeftImage.aspx");
                return;
            }
            EyouSoft.Model.CompanyStructure.CompanyImg img=new EyouSoft.Model.CompanyStructure.CompanyImg();
            img.ImagePath = imgpath;
            //img.ThumbPath = "";

            if (EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyImage(this.SiteUserInfo.CompanyID, img))
            {
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作失败！;",Request.Url.ToString());
            }
            

        }
    }
}
