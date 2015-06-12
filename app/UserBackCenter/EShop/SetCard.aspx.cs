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
    /// <summary>
    /// 上传名片
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    public partial class SetCard :EyouSoft.Common.Control.BasePage
    {
        private string CompanyId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            CompanyId = this.SiteUserInfo.CompanyID;
            
            if (!IsPostBack)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId); //公司详细信息
                if (compDetail != null)
                {
                    string imgPath = compDetail.AttachInfo.CompanyCard.ImagePath;
                    hdfAgoImgPath.Value = imgPath;
                    if (!String.IsNullOrEmpty(imgPath))
                    {
                        lblCardPath.Text = string.Format("<a href={0}  target='_blank'  title='点击查看' >查看原图</a>",Domain.FileSystem +imgPath); //卡片
                    }
                }
                compDetail = null;
            }
        }

        //提交 
        protected void btnSubmcard_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.CompanyStructure.CardInfo card = new EyouSoft.Model.CompanyStructure.CardInfo();
            string imgpath = Utils.GetFormValue("sfucard$hidFileName");
            if (imgpath.Length>0)
            {
                card.ImagePath = imgpath;
                card.ImageLink = imgpath;
                card.ThumbPath = "";
            }
            else
            {
                MessageBox.ResponseScript(this.Page, "alert(\"请选择文件上传！\");");
                return;
            }
            if (EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyCard(CompanyId, card))
            {
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "alert('操作失败！');",Request.Url.ToString());
            }
        }
    }
}
