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
namespace SiteOperationsCenter.TyProductManage
{     
    /// <summary>
        /// 页面功能：运营后台审核高级网店
        /// 开发时间：2010-07-23 开发人：袁惠
        /// </summary>
    public partial class HighShopAuit : EyouSoft.Common.Control.YunYingPage
    {
        protected string checkPeople;
        protected string checkDate;
        protected bool IsUpdateGant = true;
        protected string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
        protected string MaxDate = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断是否有该栏目查看权限
            if (!CheckMasterGrant(YuYingPermission.高级网店申请审核_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.高级网店申请审核_管理该栏目, true);
                return;
            }
            //判断是否高级网店申请审核权限
            if (!CheckMasterGrant(YuYingPermission.高级网店申请审核_管理该栏目, YuYingPermission.高级网店申请审核_审核))
            {
                IsUpdateGant = false;
            }

            string method = Utils.GetQueryStringValue("method");
            checkDate = DateTime.Now.ToString("yyyy-MM-dd");
            checkPeople = MasterUserInfo.ContactName;
            if (method == "audit")
            {
                AuditShop();
            }
        }

        #region 高级网店
        protected void AuditShop()
        {
            int applyState = Utils.GetInt(Utils.GetQueryStringValue("applystate"));
            EyouSoft.Model.SystemStructure.EshopCheckInfo shopinfo = new EyouSoft.Model.SystemStructure.EshopCheckInfo();
            DateTime StartDate = Utils.GetDateTime(Request.QueryString["enableDate"],DateTime.Now);
            DateTime EndDate = Utils.GetDateTime(Request.QueryString["expireDate"], DateTime.Now);
            shopinfo.ApplyId = Utils.GetQueryStringValue("id");
            shopinfo.EnableTime = StartDate;
            shopinfo.ExpireTime = EndDate; ;
            shopinfo.DomainName = Utils.GetString(Request.QueryString["url"], "").Replace("http://", "");
            shopinfo.TemplatePath = this.GetEShopTemplatePath((EyouSoft.Model.CompanyStructure.CompanyType)Utils.GetInt(Utils.GetQueryStringValue("cType")));
            shopinfo.GoogleMapKey = Utils.GetString(Request.QueryString["googleMapKey"], "");
            string shopCompanyId = Utils.InputText(Request.QueryString["shopCompanyId"]);     
            if (applyState != 3)
            {
                if (applyState == 1)
                {
                    if (!(StartDate <= EndDate && StartDate >= Convert.ToDateTime(NowDate)))
                    {
                        Utils.ResponseMeg(false, "有效期填写错误！");
                        return;
                    }
                }
                shopinfo.ApplyState = (EyouSoft.Model.SystemStructure.ApplyServiceState)applyState;
                shopinfo.CheckTime = DateTime.Now;
                shopinfo.OperatorId = MasterUserInfo.ID;
            }
            else
            {
                shopinfo.ApplyState = EyouSoft.Model.SystemStructure.ApplyServiceState.审核通过;
                shopinfo.CheckTime = Utils.GetDateTime(Request.QueryString["checkDate"], DateTime.Now);
                shopinfo.OperatorId = Utils.GetInt(Request.QueryString["operatorId"]);
            }
             switch(EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().EshopChecked(shopinfo))
             {
                 case 1:
                     int row = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().UpdateAdv(shopCompanyId, shopinfo.DomainName);
                     Utils.ResponseMeg(true, "操作成功！");
                   
                     break;
                 case 0:
                     Utils.ResponseMeg(false, "操作失败");
                    break;
                 case 2:
                     Utils.ResponseMeg(false,"域名重复！");
                     break;
                 case 3:
                     Utils.ResponseMeg(false, "有效期限时间值错误！");
                     break;
            }
             shopinfo = null;
        }
        #endregion

        /// <summary>
        /// 获取高级网店模板地址
        /// </summary>
        /// <param name="cType">单位类型</param>
        /// <returns></returns>
        private string GetEShopTemplatePath(EyouSoft.Model.CompanyStructure.CompanyType cType)
        {
            string s = string.Empty;

            switch (cType)
            {
                case EyouSoft.Model.CompanyStructure.CompanyType.景区: s = "/scenicspots/t1/default.aspx"; break;
                case EyouSoft.Model.CompanyStructure.CompanyType.专线: s = "/seniorshop/default.aspx"; break;
            }

            return s;
        }
    }    
}