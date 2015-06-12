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
    /// 添加友情链接
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// 
    public partial class SetFriendLinkManage : EyouSoft.Common.Control.BasePage
    {
        protected string CompanyId =string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            } 
            if (!IsPostBack)
            {
                CompanyId = this.SiteUserInfo.CompanyID;
                string action = Utils.GetString(Request.QueryString["action"], "");
                if (action== "delete")
                {
                    Delete();
                    return;
                }
                if (action == "update")
                {
                    this.UpdateFriendLink();
                    return;
                }
                if (action == "addFriendLink")
                {
                    AddFriendLink();
                    return;
                }
            }
        }
       
         //<summary>
         //添加友情链接
         //</summary>
        private void AddFriendLink()
        {
            string LinkAddress =Utils.InputText(Request.QueryString["LinkAddress"]);
            LinkAddress = LinkAddress.ToLower().Contains("http://") ? LinkAddress : "http://" + LinkAddress;
            string errorMsg = string.Empty;
            string LinkName =Server.HtmlDecode(Utils.InputText(Request.QueryString["LinkName"]));
            if (string.IsNullOrEmpty(LinkName))
            {
                errorMsg = "链接名称为空或输入错误！";
            }
            if (string.IsNullOrEmpty(LinkAddress))
            {
                errorMsg = errorMsg == "" ? "链接地址不能为空！" : errorMsg + "，链接地址不能为空！";
            }
            else
            {
                if (!EyouSoft.Common.Function.StringValidate.IsUrl(LinkAddress))
                {
                    errorMsg = errorMsg == "" ? "链接地址输入错误！" : errorMsg + "，链接地址输入错误！";
                }
            }
            if (errorMsg == "")
            {  
                EyouSoft.Model.ShopStructure.HighShopFriendLink link = new EyouSoft.Model.ShopStructure.HighShopFriendLink();
                link.CompanyID = CompanyId;
                link.LinkAddress = LinkAddress;
                link.OperatorID =this.SiteUserInfo.ID;
                link.LinkName = StringValidate.SafeRequest(LinkName);
                link.SortID = Utils.GetInt(Request.QueryString["SortId"], 0);
                if (EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().Add(link))
                {
                    link = null;
                    Response.Clear();
                    Response.Write("1");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("{errorMsg:\"" + errorMsg + "\"}");
                Response.End();
            }

        }
        /// <summary>
        /// 修改友情链接
        /// </summary>
        private void UpdateFriendLink()
        {
            string LinkAddress =Utils.InputText(Request.QueryString["LinkAddress"]);
            LinkAddress = LinkAddress.ToLower().Contains("http://") ? LinkAddress : "http://" + LinkAddress;
            string LinkName=Server.HtmlDecode(Utils.InputText(Request.QueryString["LinkName"]));
            string errorMsg = string.Empty;
            if (string.IsNullOrEmpty(LinkName))
            {
                errorMsg = "链接名称为空或输入错误！";
            }
            if (string.IsNullOrEmpty(LinkAddress))
            {
                errorMsg = errorMsg == "" ? "链接地址不能为空！" : errorMsg+"，链接地址不能为空！";
            }
            else
            {
                if (!StringValidate.IsUrl(LinkAddress))
                {
                    errorMsg = errorMsg == "" ? "链接地址输入错误！" : errorMsg + "，链接地址输入错误！";                    
                }
            }
            if (errorMsg == "")
            {
                EyouSoft.Model.ShopStructure.HighShopFriendLink link = new EyouSoft.Model.ShopStructure.HighShopFriendLink();
                link.ID = StringValidate.SafeRequest(Request.QueryString["FriendLinkId"]);
                link.CompanyID = CompanyId;
                link.LinkAddress = LinkAddress;
                link.OperatorID = this.SiteUserInfo.ID;
                link.LinkName = StringValidate.SafeRequest(LinkName);
                link.SortID = Utils.GetInt(Request.QueryString["SortId"], 0);
                if (EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().Update(link))
                {
                    link = null;
                    Response.Clear();
                    Response.Write("1");
                    Response.End();
                }                
            }
            else
            {
                Response.Clear();
                Response.Write("{errorMsg:\"" + errorMsg + "\"}");
                Response.End();
            }
        }
        /// <summary>
        /// 删除高级网店友情链接
        /// </summary>
        private void Delete()
        {
            string FriendLinkId = StringValidate.SafeRequest(Request.QueryString["FriendLinkId"]);
            if (FriendLinkId.Length>0)
            {
                if (EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().Delete(FriendLinkId))
                {
                    Response.Clear();
                    Response.Write("1");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("0");
                Response.End();
            }
        }
    }
}

