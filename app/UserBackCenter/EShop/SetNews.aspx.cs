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
    /// 添加修改最新动态
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    public partial class SetNews : EyouSoft.Common.Control.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            setnews_txtDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");    
            if (!IsPostBack)
            {
                string NewsId = Request.QueryString["NewsId"];
                if (!string.IsNullOrEmpty(NewsId))
                {
                    hdfOpeaType.Value = "Update";
                    ShowNewsById(NewsId);
                }
                else
                {
                    hdfOpeaType.Value = "Insert";
                }
            }            
        }
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="NewsId">动态Id</param>
        private void ShowNewsById(string NewsId)
        {
            EyouSoft.Model.ShopStructure.HighShopNews info = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetModel(NewsId);
            if (info != null)
            {
                setnews_txtTitle.Value = info.Title;
                editnews.Value = info.ContentText;
                setnews_txtDate.Value= info.IssueTime.ToString("yyyy-MM-dd HH:mm");
            }
            info = null;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(setnews_txtTitle.Value.Trim()) && setnews_txtTitle.Value.Trim().Length>30)
            {
                MessageBox.ResponseScript(this.Page, "alert('标题不能为空并且不能大于30个字符！');");
                return;
            }
            if (string.IsNullOrEmpty(editnews.Value))
            {
                MessageBox.ResponseScript(this.Page, "alert('内容不能为空！');");
                return;
            }
            bool result = true;  
            //获取用户编号
            EyouSoft.Model.ShopStructure.HighShopNews info = new EyouSoft.Model.ShopStructure.HighShopNews();
            info.Title = Utils.InputText(setnews_txtTitle.Value);
            info.ContentText = Utils.EditInputText(editnews.Value);
            info.CompanyID = this.SiteUserInfo.CompanyID;
            info.TypeID =1;
            info.OperatorID = this.SiteUserInfo.ID;           
            info.IssueTime= System.DateTime.Now;
            info.ID = Guid.NewGuid().ToString();
            
            if (hdfOpeaType.Value == "Insert")
                result = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().Add(info);
            else
            {
                info.ID = Request.QueryString["NewsId"];
                result = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().Update(info);
            }
            info = null;
            if (result)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作成功", "SetNewsList.aspx");
            }
            else
            {
                MessageBox.Show(this.Page,"操作失败");
                ShowNewsById(Request.QueryString["NewsId"]);  
            }         
        }
    }
}
