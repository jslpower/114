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
    public partial class ApplicationEShop : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divApplSuss.Visible = false;
                if (StringValidate.SafeRequest(Request.QueryString["AddEShop"])=="1")
                {
                    AddEShop();
                }
                InitCompanyInfo();
                this.AddJavaScriptInclude(JsManage.GetJsFilePath("validatorform"), true, false);
                    
            }
                
        }
        //获取公司信息
        private void InitCompanyInfo()
        {
                 //判断是否已提交过申请
            if(EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().IsApply(this.SiteUserInfo.CompanyID,EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                EyouSoft.Model.SystemStructure.SysApplyServiceInfo applinfo = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetApplyInfo(this.SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.SysService.HighShop);
                if (applinfo != null)
                {
                    appleshop_appleperson.Value = applinfo.ContactName;
                    appleshop_companyName.Value = applinfo.CompanyName;
                    appleshop_mod.Value = applinfo.ContactMobile;
                    appleshop_tel.Value = applinfo.ContactTel;
                    appleshop_address.Value = applinfo.ApplyText;
                    appleshop_address.Disabled = true;
                    appleshop_appleperson.Disabled = true;
                    appleshop_mod.Disabled = true;
                    appleshop_tel.Disabled = true;
                    appleshop_companyName.Disabled = true;
                    btnaddEshop.Disabled = true;
                    btnaddEshop.Visible = false;
                    divApplSuss.Visible = true;
                }
            }
            else
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID); //公司详细信息
                if (compDetail != null)
                {
                    appleshop_appleperson.Value = compDetail.ContactInfo.ContactName;
                    appleshop_companyName.Value = compDetail.CompanyName;
                    appleshop_mod.Value = compDetail.ContactInfo.Mobile;
                    appleshop_tel.Value = compDetail.ContactInfo.Tel;               
                }
                compDetail = null;
            }
        }

        //申请开通高级网店
        protected void AddEShop()
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                return;
            }
            int result = 0;
            string ContactName=Utils.InputText(Server.HtmlDecode(Request.QueryString["p"]));
            string Tel = Utils.InputText(Server.HtmlDecode(Request.QueryString["t"]));
            string Mobile = Utils.InputText(Server.HtmlDecode(Request.QueryString["m"]));
            string Net = Utils.InputText(Request.QueryString["n"]);
            string CompanyName = Utils.InputText(Server.HtmlDecode(Request.QueryString["c"]));
            if (ContactName.Length > 0 && Tel.Length > 0 && Mobile.Length > 0 && Net.Length > 0 && CompanyName.Length>0)
            {
                if (Utils.IsMobile(Mobile) && Utils.IsPhone(Tel) && StringValidate.IsUrl(Net) && CompanyName==this.SiteUserInfo.CompanyName)
                {
                    EyouSoft.Model.SystemStructure.SysApplyServiceInfo model = new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();
                    model.ContactMobile = Mobile;
                    model.ContactMQ = this.SiteUserInfo.ContactInfo.MQ;
                    model.ContactName = ContactName;
                    model.ContactQQ = this.SiteUserInfo.ContactInfo.QQ;
                    model.ContactTel = Tel;
                    model.ApplyTime = DateTime.Now;
                    model.UserId = this.SiteUserInfo.ID;
                    model.CityId = this.SiteUserInfo.CityId;
                    model.CityName = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(this.SiteUserInfo.CityId).CityName;
                    model.CompanyId = this.SiteUserInfo.CompanyID;
                    model.CompanyName = this.SiteUserInfo.CompanyName;
                    model.ProvinceId = this.SiteUserInfo.ProvinceId;
                    model.ProvinceName = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(this.SiteUserInfo.ProvinceId).ProvinceName;
                    model.ApplyServiceType = EyouSoft.Model.CompanyStructure.SysService.HighShop;
                    model.ApplyText = Net;
                    if (EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().Apply(model))
                    {
                        result = 1;
                    }
                    model = null;
                }
                else
                {
                    result = 4;  //数据输入错误
                }
            }
            else
            {
                result = 3;     //数据填写不完整
            }
            Response.Write(result.ToString());
            Response.End();
            return;
        }
    }
}
