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
using System.IO;

namespace UserBackCenter.EShop
{
    /// <summary>
    ///添加修改旅游资源推荐
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// history: zhangzy   2010-11-10  整合"旅游资源推荐"到"出游指南"栏目中
    public partial class SetResources : EyouSoft.Common.Control.BasePage
    {
        protected string img_Path = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            setreso_txtDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");    
            if (!IsPostBack)
            {
                string ResoId = Request.QueryString["Reso_Id"];
                if (!string.IsNullOrEmpty(ResoId))
                {
                    InitResoInfo(ResoId);
                }
            }
          
        }

        /// <summary>
        /// 修改，加载数据
        /// </summary>
        /// <param name="companyId">id</param>
        private void InitResoInfo(string resoId)
        {
            EyouSoft.Model.ShopStructure.HighShopResource reso = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().GetModel(Request.QueryString["Reso_Id"]);
            if (reso != null)
            {
                setreso_txttitle.Value = reso.Title;
                editreso.Value = reso.ContentText;
                setreso_txtDate.Value = reso.IssueTime.ToString("yyyy-MM-dd HH:mm");
                if(!string.IsNullOrEmpty(reso.ImagePath))
                {
                    img_Path = string.Format("<a href=\"{0}\"target='_blank'  title=\"点击查看\">查看原图</a>", Domain.FileSystem+ reso.ImagePath);
                    hdfAgoImgPath.Value = reso.ImagePath;
                }
            }
            reso = null;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(setreso_txttitle.Value.Trim()) && setreso_txttitle.Value.Trim().Length > 30)
            {
                MessageBox.ResponseScript(this.Page, "alert('标题不能为空并且不能大于30个字符！');");
                return;
            }
            if (string.IsNullOrEmpty(editreso.Value.Trim()))
            {
                MessageBox.ResponseScript(this.Page, "alert('内容不能为空！');");
                return;
            }
            EyouSoft.Model.ShopStructure.HighShopResource reso = new EyouSoft.Model.ShopStructure.HighShopResource();
            reso.Title =StringValidate.SafeRequest(setreso_txttitle.Value);
            reso.ContentText = Utils.EditInputText(editreso.Value);
           
            reso.OperatorID = this.SiteUserInfo.ID;
            reso.CompanyID = this.SiteUserInfo.CompanyID;
            string imgpath = Utils.GetFormValue("sfuResoImg$hidFileName");
            if (imgpath.Length > 0)
            {
                reso.ImagePath =imgpath;                
            }
            else
            {
                reso.ImagePath = Utils.GetFormValue(hdfAgoImgPath.UniqueID);
            }

            bool result = false;
            if (!string.IsNullOrEmpty(Request.QueryString["Reso_Id"]))
            {
                reso.ID = Request.QueryString["Reso_Id"];
                result = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().Update(reso);
            }
            else
            {
                reso.IssueTime = DateTime.Now;
                result = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().Add(reso);
            }
            if (result)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作成功", "SetResourcesList.aspx");
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作失败",Request.Url.ToString());
            }
            reso = null;
        }
      
    }
}
