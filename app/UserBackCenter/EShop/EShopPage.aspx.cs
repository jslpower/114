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
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店管理
    /// 创建者：袁惠 创建时间：2010-6-25
    /// </summary>
    /// 2010-11-10 汪奇志 增加模板4 
    public partial class EShopPage : BackPage
    {
        #region Attributes
        /// <summary>
        /// 高级网店URL
        /// </summary>
        protected string EShopUrl = "";
        /// <summary>
        /// 网店管理首页URL
        /// </summary>
        protected string EShopMUrl = string.Empty;
        //公司身份标识
        protected string role = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断是否开通高级网店       当前登录的公司拥有的身份         
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    role = "1";
                    bool isOpenShop = false;
                    isOpenShop = Utils.IsOpenHighShop(this.SiteUserInfo.CompanyID);
                    if (!isOpenShop)
                    {
                        Utils.ShowError("公司未开通高级网店!", "SeniorShop");
                    }
                }
                else
                {
                    role = "0";
                }
                                       
                if (!string.IsNullOrEmpty(Request.QueryString["StyleIndex"]))//修改模板
                {
                    this.UpdateTemplate();
                }
                else
                {
                    this.InitEShopTemplate();
                }
            }

        }

        /// <summary>
        /// 初始化网店模板信息
        /// </summary>
        private void InitEShopTemplate()
        {
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo shopinfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID);
            
            int tempId = 1;

            if (shopinfo != null)
            {
                tempId = shopinfo.TemplateId;
                if (tempId < 1)
                {
                    tempId = 1;
                }
                if (tempId > 4)
                {
                    tempId = 1;
                }
                shopinfo = null;
            }

            this.EShopUrl = Utils.GetEShopUrl(this.SiteUserInfo.CompanyID);
            this.EShopMUrl = Utils.GetEshopMTemplatePath(tempId);

            this.Page.ClientScript.RegisterStartupScript(GetType(), "js1", "initSelectRadio('" + tempId + "')", true);
        }

        /// <summary>
        /// 修改网店模板
        /// </summary>
        private void UpdateTemplate()
        {
            int templateId = Utils.GetInt(Request.QueryString["StyleIndex"], 1);
            bool setTemplateResult = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().SetTemplate(this.SiteUserInfo.CompanyID, templateId, Utils.GetEShopTemplatePath(templateId));

            //json.isSuccess:是否成功 json.msg:提示信息 json.sMUrl:成功后管理首页的url json.sUrl:成功后网店链接
            string responseText = "{{isSuccess:{0},msg:'{1}',sMUrl:'{2}',sUrl:'{3}'}}";
            if (setTemplateResult)
            {
                responseText = string.Format(responseText, "true", "修改成功！", Utils.GetEshopMTemplatePath(templateId), Utils.GetEShopUrl(this.SiteUserInfo.CompanyID));
            }
            else
            {
                responseText = string.Format(responseText, "false", "修改失败！", string.Empty, string.Empty);
            }

            Response.Write(responseText);            
            Response.End();
        }
    }
}
