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

namespace UserBackCenter.SupplyManage
{
    /// <summary>
    /// 供应商高级网店申请
    /// luofx 2010-8-17
    /// </summary>
    public partial class HighApplication : EyouSoft.Common.Control.BackPage
    {
        protected string ContactName = string.Empty;
        protected string Tel = string.Empty;
        protected string Mobile = string.Empty;
        protected string Address = string.Empty;
        protected string CompanyName = string.Empty;
        protected bool IsApply = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "save")
            {
                AddEShop();
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        private void InitPage()
        {
            if (EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().IsApply(this.SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                IsApply = true;
                EyouSoft.Model.SystemStructure.SysApplyServiceInfo applinfo = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetApplyInfo(this.SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.SysService.HighShop);
                ContactName = applinfo.ContactName;
                Mobile = applinfo.ContactMobile;
                Tel = applinfo.ContactTel;
                CompanyName = applinfo.CompanyName;
                Address = applinfo.ContactAddress;
                applinfo = null;
            }
            else {
                CompanyName = this.SiteUserInfo.CompanyName;
                ContactName = this.SiteUserInfo.ContactInfo.ContactName;
                Mobile = this.SiteUserInfo.ContactInfo.Mobile;
                Tel = this.SiteUserInfo.ContactInfo.Tel;
            }
        }
        //申请开通高级网店
        private void AddEShop()
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你的公司目前还未审核！'}]");
                Response.End();
                return;
            }
            ContactName = Utils.GetFormValue("HighApplication_AppName");
            Tel = Utils.GetFormValue("HighApplication_Tel");
            Mobile = Utils.GetFormValue("HighApplication_Mobile");
            Address = Utils.GetFormValue("HighApplication_Address");
            CompanyName = Utils.GetFormValue("HighApplication_CompanyName");
            if (!Utils.IsMobile(Mobile))
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'手机号码填写错误！'}]");
                Response.End();
                return;
            }
            if (!Utils.IsPhone(Tel))
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'电话号码填写错误！'}]");
                Response.End();
                return;
            }
            EyouSoft.Model.SystemStructure.SysApplyServiceInfo model = new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();
            model.ContactMobile = Mobile;
            model.UserId = this.SiteUserInfo.ID;
            model.CityId = this.SiteUserInfo.CityId;
            model.ContactMQ = this.SiteUserInfo.ContactInfo.MQ;
            model.ContactQQ = this.SiteUserInfo.ContactInfo.QQ;
            model.CompanyId = this.SiteUserInfo.CompanyID;
            model.CompanyName = this.SiteUserInfo.CompanyName;
            model.ProvinceId = this.SiteUserInfo.ProvinceId;
            model.ContactName = ContactName;
            model.ContactTel = Tel;
            model.ApplyText = string.Empty;
            model.ContactAddress = Address;
            model.ApplyTime = DateTime.Now;
            model.CityName = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(this.SiteUserInfo.CityId).CityName;
            model.ProvinceName = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(this.SiteUserInfo.ProvinceId).ProvinceName;
            model.ApplyServiceType = EyouSoft.Model.CompanyStructure.SysService.HighShop;

            if (EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().Apply(model))
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'高级网店申请资料发送成功，请等待进一步审核！'}]");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'申请失败，请重新申请！'}]");
                Response.End();
            }
            model = null;
        }
    }
}
