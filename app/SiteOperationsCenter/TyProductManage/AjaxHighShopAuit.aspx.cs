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
using EyouSoft.Common.Control;
using System.Collections.Generic;

namespace SiteOperationsCenter.TyProductManage
{
    public partial class AjaxHighShopAuit :EyouSoft.Common.Control.YunYingPage
    {
        protected int pageIndex;
        protected int pageSize = 15;
        protected int recordCount;
        protected int itemIndex = 1;
        protected EyouSoft.IBLL.SystemStructure.ISystemUser userBll;

        /// <summary>
        /// Signup Google Map Key URL
        /// </summary>
        private const string GoogleMapKeySignupPath = "http://code.google.com/intl/zh-CN/apis/maps/signup.html";
        /// <summary>
        /// Signup Google Map Key Tip
        /// </summary>
        private const string GoogleMapKeySignupTip = "signup key";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.高级网店申请审核_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.高级网店申请审核_审核, true);
                return;
            }
            userBll = EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            itemIndex = (pageIndex - 1) * pageSize + 1;
            int pId = Utils.GetInt(Utils.GetQueryStringValue("province"));
            int cId = Utils.GetInt(Utils.GetQueryStringValue("city"));
            int? provinceId = pId == 0 ? new Nullable<int>() : pId;
            int? cityId = cId == 0 ? new Nullable<int>() : cId;
            string companyName = Utils.GetQueryStringValue("companyname");
            string state = Utils.GetQueryStringValue("state");
            DateTime? applyStartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("applysdate"));
            DateTime? applyFinishDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("applyfdate"));
            EyouSoft.Model.SystemStructure.ApplyServiceState? applyState = state == "" ? new Nullable<EyouSoft.Model.SystemStructure.ApplyServiceState>() : (EyouSoft.Model.SystemStructure.ApplyServiceState)(Utils.GetInt(state));
            IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> serviceList = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetApplys(pageSize, pageIndex, ref recordCount, EyouSoft.Model.CompanyStructure.SysService.HighShop, provinceId, cityId, companyName, applyStartDate, applyFinishDate, applyState);
            if (serviceList != null && serviceList.Count > 0)
            {
                ama_rpt_applyList.DataSource = serviceList;
                ama_rpt_applyList.DataBind();
                BindPage();
            }
            else
            {
                ama_rpt_applyList.EmptyText = "<div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无高级网店审核信息</span></div>";
                ExporPageInfoSelect1.Visible = false;
            }
            
        }
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
            this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "ShopAudit.loadData(this);", 1);
            this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "ShopAudit.loadData(this);", 0);
        }
        protected string GetSysUserName(int id)
        {
            string userName = MasterUserInfo.ContactName;
            if (id != 0)
            {
                EyouSoft.Model.SystemStructure.SystemUser userModel = userBll.GetSystemUserModel(id);
                if (userModel != null)
                {
                    userName = userModel.ContactName;
                }
                userModel = null;
            }
            return userName;
        }
        protected string GetDate(string dateTime)
        {
            if (dateTime == "0001-01-01")
            {
                dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            }
            return dateTime;
        }

        /// <summary>
        /// ama_rpt_applyList_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ama_rpt_applyList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex < 0) return;
            //高级网店申请信息
            var aInfo = (EyouSoft.Model.SystemStructure.SysApplyServiceInfo)e.Item.DataItem;
            //申请高级网店公司信息
            var cInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(aInfo.CompanyId);
            //item control
            var ltrBusinessProperties = (Literal)e.Item.FindControl("ltrBusinessProperties");
            var ltrDomainName = (Literal)e.Item.FindControl("ltrDomainName");
            var ltrGoogleMapKey = (Literal)e.Item.FindControl("ltrGoogleMapKey");
            //company type
            var cType = Utils.GetCompanyType(cInfo);

            //专线、景区网店域名信息
            if (cType == EyouSoft.Model.CompanyStructure.CompanyType.专线 || cType == EyouSoft.Model.CompanyStructure.CompanyType.景区)
            {
                string s = "<tr name=\"trDomainUrl\" ><td align=\"left\"> 域名：<input name=\"shop_url\" type=\"text\" value=\"{0}\" class=\"textfield\" domaintext=\"{0}\" /><input type=\"hidden\" name=\"txtCType\" value=\"{1}\"></td></tr>";
                
                if (aInfo.ApplyState == EyouSoft.Model.SystemStructure.ApplyServiceState.未审核)
                {
                    ltrDomainName.Text = string.Format(s, aInfo.ApplyText, (int)cType);
                }
                else
                {
                    ltrDomainName.Text = string.Format(s, aInfo.CheckText, (int)cType);
                }
            }

            //景区网店google map key信息
            if (cType == EyouSoft.Model.CompanyStructure.CompanyType.景区)
            {
                var eInfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(aInfo.CompanyId);

                if (eInfo != null)
                {
                    ltrGoogleMapKey.Text = string.Format("<tr name=\"trGoogleMap\" ><td align=\"left\">Map Key：<input name=\"txtGoogleMapKey\" type=\"text\" value=\"{0}\" class=\"textfield\" id=\"txtGoogleMapKey\" />&nbsp;&nbsp;<a href=\"{1}\" target=\"_blank\">{2}</a></td></tr>", eInfo.GoogleMapKey, GoogleMapKeySignupPath, GoogleMapKeySignupTip);
                    eInfo = null;
                }
                else
                {
                    ltrGoogleMapKey.Text = string.Format(
                        "<tr name=\"trGoogleMap\" ><td align=\"left\">Map Key：<input name=\"txtGoogleMapKey\" type=\"text\" value=\"{0}\" class=\"textfield\" id=\"txtGoogleMapKey\" />&nbsp;&nbsp;<a href=\"{1}\" target=\"_blank\">{2}</a></td></tr>",
                        "", GoogleMapKeySignupPath, GoogleMapKeySignupTip);
                }
            }
            
            //企业性质
            if (cInfo != null)
            {
                ltrBusinessProperties.Text = cInfo.BusinessProperties.ToString();
            }

        }
    }
}
