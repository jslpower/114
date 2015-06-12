using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter
{
    public partial class TicketRegister : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnSubmit.Style.Add("background", "url(" + ImageServerPath + "/Images/new2011/images/yxregbg.gif) no-repeat scroll 0 -144px transparent");
            }

            if (Utils.GetQueryStringValue("type") == "submit")
            {
               
                Response.Clear();
                Response.Write(FormSubmit());
                Response.End();
            }
        }

        protected string FormSubmit()
        {
            //获得表单值
            //公司名称
            string companyName = Utils.GetFormValue("txtCompanyName");
            //用户名
            string userName = Utils.GetFormValue("u");
            //用户密码
            string userPwd = Utils.GetFormValue("p");
            //确认密码
            string userPwdTwo = Utils.GetFormValue("txtPwdTwo");
            //真实姓名
            string trueName = Utils.GetFormValue("txtTrueName");
            //手机号码   
            string userMobile = Utils.GetFormValue("txtPhone");
            //邮箱
            string userEmail = Utils.GetFormValue("txtEmail");
            //用户QQ
            string userQQ = Utils.GetFormValue("txtQQ");
            //省份ID
            int provinceID = Utils.GetInt(Utils.GetFormValue("ProvinceAndCityList1$ddl_ProvinceList"));
            //城市ID
            int cityID = Utils.GetInt(Utils.GetFormValue("ProvinceAndCityList1$ddl_CityList"));

            #region 验证
            if (companyName.Trim() == "")
            {
                return "用户名不能为空";
            }

            if (userPwd.Trim() == "")
            {
                return "密码不能为空";
            }
            if (userPwd.Trim() != userPwdTwo.Trim())
            {
                return "两次密码不一致";
            }
            if (trueName.Trim() == "")
            {
                return "真实姓名不能为空";
            }
            if (userMobile.Trim() == "")
            {
                return "手机号码不能为空";
            }
            if (userEmail.Trim() == "")
            {
                return "不能为空";
            }

            if (userQQ.Trim() == "")
            {
                return "QQ不能为空";
            }
            if (provinceID <= 0)
            {
                return "请选择一个省份";
            }
            if (cityID <= 0)
            {
                return "请选择一个城市";
            }
            #endregion


            /*公司信息*/
            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();
            /*用户信息*/
            EyouSoft.Model.CompanyStructure.UserAccount UserModel = new EyouSoft.Model.CompanyStructure.UserAccount();
            /* 公司联系人信息 */
            EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactModel = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            string OtherSaleCity = "";



            UserModel.UserName = userName; /*用户信息*/

            UserModel.PassWordInfo.NoEncryptPassword = userPwd;
            ContactModel.ContactName = trueName;
            ContactModel.Mobile = userMobile;
            ContactModel.Email = userEmail;

            /*公司信息*/
            model.CompanyName = companyName;
            model.ProvinceId = provinceID;
            model.CityId = CityId;

            /* 公司身份 */
            EyouSoft.Model.CompanyStructure.CompanyRole RoleMode = new EyouSoft.Model.CompanyStructure.CompanyRole();
            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.组团;


            RoleMode.SetRole(TypeEmnu);
            model.CompanyRole = RoleMode;


            model.AdminAccount = UserModel;
            model.ContactInfo = ContactModel;


            EyouSoft.Model.ResultStructure.UserResultInfo Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Add(model, OtherSaleCity);

            if (Result == EyouSoft.Model.ResultStructure.UserResultInfo.Succeed)
            {
                return "OK";
            }
            else
            {
                return "Error";
            }




        }
    }
}
