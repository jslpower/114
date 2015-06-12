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
using EyouSoft.Security.Membership;
namespace UserBackCenter.SupplyManage
{
    /// <summary>
    /// 供应商后台:增加资讯
    /// 罗伏先   2010-07-22
    /// </summary>
    public partial class AddNews : EyouSoft.Common.Control.BackPage
    {
        private string CompanyID = string.Empty;
        /// <summary>
        /// 新闻ID
        /// </summary>
        protected string AfficheID = string.Empty;
        //标题
        protected string AfficheTitle = string.Empty;
        //内容
        protected string AfficheInfo = string.Empty;
        /// <summary>
        /// 公司资讯类别
        /// </summary>
        EyouSoft.Model.CompanyStructure.CompanyAfficheType ComAfficheType;
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;
            EyouSoft.Model.CompanyStructure.SupplierInfo model = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(CompanyID);
            if (!model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                Response.Clear();
                Response.Write("对不起，你没有权限操作供应商高级网店后台，如有需要请申请开通！");
                Response.End();
            }
            model = null;
            AfficheID = Utils.GetQueryStringValue("AfficheID");
            if (this.SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                switch (this.SiteUserInfo.CompanyRole.RoleItems[0])
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.车队新闻;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.购物点新闻;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.景区新闻;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.酒店新闻;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.旅游用品新闻;
                        break;
                    default:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.景区新闻;
                        break;
                }
            }
            if (Utils.GetQueryStringValue("action") == "Save")
            {
                InsertNews();
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AfficheID"]))
            {
                int AfficheID = Utils.GetInt(Request.QueryString["AfficheID"]);
                EyouSoft.Model.CompanyStructure.CompanyAffiche model = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetModel(AfficheID);
                if (model != null)
                {
                    AfficheTitle = model.AfficheTitle;
                    this.AfficheInfo = model.AfficheInfo;
                }
                model = null;
            }
        }
        /// <summary>
        /// 添加新闻
        /// </summary>
        private void InsertNews()
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,Message:'对不起，新注册用户还未审核，没有权限进入供应商网店管理！'}]");
                Response.End();
            }
            //if (!this.IsSupplyUser)
            //{
            //    Response.Clear();
            //    Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你的公司身份不是属于供应商，没有权限进入供应商网店管理！'}]");
            //    Response.End();
            //    return;
            //}
            bool isTrue = false;
            EyouSoft.Model.CompanyStructure.CompanyAffiche model = new EyouSoft.Model.CompanyStructure.CompanyAffiche();
            model.AfficheClass = ComAfficheType;
            
            string AfficheInfo = Server.UrlDecode(Request.Form["addnews_AfficheInfo"]);
            //AfficheInfo = Utils.EditInputText(Server.UrlDecode(AfficheInfo.Substring(0, AfficheInfo.Length > 4000 ? 4000 : AfficheInfo.Length)));
            model.AfficheInfo =Utils.EditInputText( AfficheInfo);

            model.AfficheTitle = Utils.GetFormValue("addnews_AfficheTitle",50);
            model.CompanyId = CompanyID;
            model.CompanyType = this.SiteUserInfo.CompanyRole.RoleItems[0];
            model.OperatorID = this.SiteUserInfo.ID;
            model.OperatorName = this.SiteUserInfo.UserName;
            model.IssueTime = Utils.GetDateTime(Request.Form["addnews_IssueTime"]);
            model.IsTop = false;
            model.IsPicNews = false;
            model.IsHot = false;
            model.Clicks = 0;
            model.PicPath = "";
            if (Request.QueryString["AfficheID"] != null && !string.IsNullOrEmpty(Request.QueryString["AfficheID"]))
            {
                model.ID = Utils.GetInt(Request.QueryString["AfficheID"]);
                isTrue = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().Update(model);
            }
            else
            {
                isTrue = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().Add(model);
            }
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:' 公司信息保存成功！'}]");
                Response.End();
            }
            model = null;
        }
    }
}
