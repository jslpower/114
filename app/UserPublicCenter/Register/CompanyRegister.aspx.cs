using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter
{
    public partial class CompanyRegister : EyouSoft.Common.Control.FrontPage
    {
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].ToString();
                if (type.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(RegisterCompany());
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        protected bool RegisterCompany()
        {
            bool AddisTrue = true;

            string CompanyName = Utils.InputText(Request.Form["txtCompanyName"]);
            string UserName = Utils.InputText(Request.Form["txtUserName"]);
            string PassWord = Utils.InputText(Request.Form["txtFristPassWord"]);
            string LicenseNumber = Utils.InputText(Request.Form["txtLicenseNumber"]);
            string BrandName = Utils.InputText(Request.Form["txtBrandName"]);
            int ProvinceId = Convert.ToInt32(Request.Form["ctl00$Main$dropProvinceList"]);
            int CityId = Convert.ToInt32(Request.Form["ctl00$Main$dropCityList"]);
            string OfficeAddress = Utils.InputText(Request.Form["txtOfficeAddress"]);
            string CommendCompany = Utils.InputText(Request.Form["txtCommendCompany"]);
            string CompanyType = Utils.InputText(Request.Form["radManageArea"]);
            if (CompanyType == "")
            {
                CompanyType = Utils.InputText(Request.Form["ckCompanyType"]);
            }
            string ContactName = Utils.InputText(Request.Form["txtContactName"]);
            string ContactTel = Utils.InputText(Request.Form["txtContactTel"]);
            string ContactMobile = Utils.InputText(Request.Form["txtContactMobile"]);
            string ContactFax = Utils.InputText(Request.Form["txtContactFax"]);
            string ContactEmail = Utils.InputText(Request.Form["txtContactEmail"]);
            string ContactQQ = Utils.InputText(Request.Form["txtContactQQ"]);
            string ContactMSN = Utils.InputText(Request.Form["txtContactMSN"]);
            
            //新增公司/用户

            return AddisTrue;
        }
    }
}
