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
namespace UserBackCenter.SystemSet
{
    public partial class ApplyMQ : BackPage
    {
        protected string isApply = "";
        protected string contantName = "";
        protected string moible = "";
        protected string tel = "";
        protected string adress = "";
        protected string companyName = "";
        protected string mq_message = @"对不起，您还不是企业MQ会员，<br />
          暂不能使用该功能，请先申请开通企业MQ会员！";
        EyouSoft.IBLL.SystemStructure.ISysApplyService applyBll;
        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!CheckGrant(TravelPermission.系统设置_权限管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            applyBll=EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance();
            string isCustomer = Utils.GetQueryStringValue("iscustomer");
            if (isCustomer == "yes")
            {
                sznb1.Visible = false;
            }
            companyName = SiteUserInfo.CompanyName;
           string method = Utils.GetFormValue("method");
           if(method=="applyMQ")
           {
               Apply_MQ();
               return;
           }
           if (applyBll.IsApply(SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.SysService.MQ))
           {
               EyouSoft.Model.SystemStructure.SysApplyServiceInfo serviceInfo = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetApplyInfo(SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.SysService.MQ);
               if (serviceInfo != null)
               {
                   am_txtAddress.Visible = false;
                   am_txtContact.Visible = false;
                   am_txtMoible.Visible = false;
                   am_txtTel.Visible = false;
                   contantName = "<span>" + serviceInfo.ContactName + "</span>";
                   moible = "<span>" + serviceInfo.ContactMobile + "</span>";
                   tel = "<span>" + serviceInfo.ContactTel + "</span>";
                   adress = "<span>" + serviceInfo.ContactAddress + "</span>";
                   mq_message = "你已经申请开通MQ会员，目前处于审核当中……";
               }

               isApply = "style='display:none'";
           }
           else
           {
               am_txtContact.Value = SiteUserInfo.ContactInfo.ContactName;
               am_txtMoible.Value = SiteUserInfo.ContactInfo.Mobile;
               am_txtTel.Value = SiteUserInfo.ContactInfo.Tel;
               EyouSoft.Model.CompanyStructure.CompanyInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
               if (companyModel != null)
               {
                   am_txtAddress.Value = companyModel.CompanyAddress;
               }
           }
        }
        #region 申请MQ
        protected void Apply_MQ()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string companyId = SiteUserInfo.CompanyID;
            string applyContact = Utils.GetFormValue("contact");
            string tel = Utils.GetFormValue("tel");
            string mobile = Utils.GetFormValue("mobile");
            string address = Utils.GetFormValue("address");
            EyouSoft.Model.SystemStructure.SysApplyServiceInfo serviceInfo=new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();
            serviceInfo.ApplyServiceType=EyouSoft.Model.CompanyStructure.SysService.MQ;
            serviceInfo.ApplyTime=DateTime.Now;
            serviceInfo.CityId=SiteUserInfo.CityId;
            EyouSoft.Model.SystemStructure.SysCity cityModel=EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(SiteUserInfo.CityId);
            if(cityModel!=null)
            {
              serviceInfo.CityName=cityModel.CityName;
            }
            serviceInfo.CompanyId=SiteUserInfo.CompanyID;
            serviceInfo.CompanyName=SiteUserInfo.CompanyName;
            serviceInfo.ContactAddress=address;
            serviceInfo.ContactMobile=mobile;
            serviceInfo.ContactMQ=SiteUserInfo.ContactInfo.MQ;
            serviceInfo.ContactName=applyContact;
            serviceInfo.ContactQQ=SiteUserInfo.ContactInfo.QQ;
            serviceInfo.ContactTel = tel;
            serviceInfo.UserId=SiteUserInfo.ID;
            EyouSoft.Model.SystemStructure.SysProvince provinceModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(SiteUserInfo.ProvinceId);
            if (provinceModel != null)
            {
                serviceInfo.ProvinceName = provinceModel.ProvinceName;
            }
            serviceInfo.ProvinceId = SiteUserInfo.ProvinceId;
            if (!applyBll.IsApply(SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.SysService.MQ))
            {
                if (applyBll.Apply(serviceInfo))
                {
                    Utils.ResponseMegSuccess();
                }
                else
                {
                    Utils.ResponseMeg(false, "申请失败!");
                }
            }
            else
            {
                Utils.ResponseMeg(false, "你已经提交过申请!");
            }
        }
        #endregion
    }
}
