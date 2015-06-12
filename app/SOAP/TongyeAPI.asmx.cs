using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;

namespace SOAP
{
    /// <summary>
    /// 同业平台公共API
    /// </summary>
    [WebService(Namespace = "http://www.tongye114.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务,请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TongyeAPI : System.Web.Services.WebService
    {
        public SOAP.APISoapHeader soapHeader = new SOAP.APISoapHeader();

        #region  检测用户名是否已存在

        /// <summary>
        /// 检测用户名是否已存在
        /// </summary>
        /// <param name="UserName">待检测的用户名</param>
        /// <returns>返回是否存在</returns>
        [System.Web.Services.Protocols.SoapHeader("soapHeader")]
        [WebMethod]
        public bool IsExistsUserName(string UserName)
        {
            if (!soapHeader.IsSafeCall)
                throw new Exception("对不起，您没有权限调用此服务！");

            return EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExists(UserName);
        }

        #endregion

        #region 创建公司

        /// <summary>
        /// 创建公司
        /// </summary>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="CompanyTypes">公司类型集合</param>
        /// <param name="License">许可证号</param>
        /// <param name="CompanyBrand">品牌名称</param>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="AdminUser">管理员用户名</param>
        /// <param name="AdminPWD">管理员密码</param>
        /// <param name="AdminName">联系人</param>
        /// <param name="AdminTel">联系电话</param>
        /// <param name="AdminMobile">联系手机</param>
        /// <param name="Email">Email</param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("soapHeader")]
        [WebMethod]
        public EyouSoft.Model.ResultStructure.UserResultInfo CreatCompany(string CompanyName,
            EyouSoft.Model.CompanyStructure.CompanyType[] CompanyTypes, string License, string CompanyBrand, int ProvinceId, int CityId,
            string AdminUser, string AdminPWD, string AdminName, string AdminTel, string AdminMobile, string Email)
        {
            if (!soapHeader.IsSafeCall)
                throw new Exception("对不起，您没有权限调用此服务！");

            EyouSoft.Model.ResultStructure.UserResultInfo Result = new EyouSoft.Model.ResultStructure.UserResultInfo();
            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();

            /*公司信息*/
            model.CompanyName = Utils.InputText(CompanyName);
            model.ProvinceId = ProvinceId;
            model.CityId = CityId;
            model.CompanyBrand = Utils.InputText(CompanyBrand);
            model.License = Utils.InputText(License);


            /*用户信息*/
            EyouSoft.Model.CompanyStructure.UserAccount UserModel = new EyouSoft.Model.CompanyStructure.UserAccount();
            UserModel.UserName = Utils.InputText(AdminUser);
            UserModel.PassWordInfo.NoEncryptPassword = Utils.InputText(AdminPWD);
            model.AdminAccount = UserModel;

            /* 公司联系人信息 */
            EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactModel = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            ContactModel.ContactName = Utils.InputText(AdminName);
            ContactModel.Tel = Utils.InputText(AdminTel);
            ContactModel.Mobile = Utils.InputText(AdminMobile);
            ContactModel.Email = Utils.InputText(Email);
            model.ContactInfo = ContactModel;

            /* 公司身份 */
            EyouSoft.Model.CompanyStructure.CompanyRole RoleMode = new EyouSoft.Model.CompanyStructure.CompanyRole();
            if (CompanyTypes != null && CompanyTypes.Length > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyType tmp in CompanyTypes)
                {
                    RoleMode.SetRole(tmp);
                }
            }
            model.CompanyRole = RoleMode;

            Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Add(model, string.Empty);
            model = null;
            UserModel = null;
            ContactModel = null;
            RoleMode = null;

            return Result;
        }

        #endregion

        #region 创建子帐户

        /// <summary>
        /// 创建子帐户
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="ContactName">联系人姓名</param>
        /// <param name="ContactTel">联系人电话</param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("soapHeader")]
        [WebMethod]
        public EyouSoft.Model.ResultStructure.ResultInfo CreatChildUser(string CompanyId, string UserName, string PassWord, string ContactName
            , string ContactTel)
        {
            if (!soapHeader.IsSafeCall)
                throw new Exception("对不起，您没有权限调用此服务！");

            EyouSoft.Model.CompanyStructure.CompanyUser ModelUser = new EyouSoft.Model.CompanyStructure.CompanyUser();
            ModelUser.CompanyID = Utils.InputText(CompanyId);
            ModelUser.UserName = Utils.InputText(UserName, 20);
            ModelUser.PassWordInfo.NoEncryptPassword = PassWord;
            ModelUser.ContactInfo.ContactName = Utils.InputText(ContactName, 20);
            ModelUser.ContactInfo.Tel = Utils.InputText(ContactTel, 20);
            ModelUser.RoleID = string.Empty;
            EyouSoft.Model.ResultStructure.ResultInfo Result = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().Add(ModelUser);
            ModelUser = null;
            return Result;
        }

        #endregion

        #region 修改用户资料

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="NewPassWord">新密码，为空不修改密码</param>
        /// <param name="ContactName">姓名</param>
        /// <param name="Sex">性别，真为男，假为女</param>
        /// <param name="ContactTel">电话</param>
        /// <param name="ContactMobile">手机</param>
        /// <param name="Fax">传真</param>
        /// <param name="Email">Email</param>
        /// <param name="QQ">QQ</param>
        /// <param name="MSN">MSN</param>
        /// <returns>
        /// -2:密码修改失败，个人信息修改成功；
        /// -1：全部修改失败；
        /// 0：必填项没有赋值；
        /// 1：修改成功；
        /// 2：带密码修改成功；
        /// </returns>
        [System.Web.Services.Protocols.SoapHeader("soapHeader")]
        [WebMethod]
        public int UpdatePersonalInfo(string UserId, string NewPassWord, string ContactName, bool Sex, string ContactTel, string ContactMobile
            , string Fax, string Email, string QQ, string MSN)
        {
            if (!soapHeader.IsSafeCall)
                throw new Exception("对不起，您没有权限调用此服务！");

            //if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(ContactName) || string.IsNullOrEmpty(Email))
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(ContactName))
                return 0;

            int Resutl = 0;
            EyouSoft.Model.CompanyStructure.CompanyUser userModel = new EyouSoft.Model.CompanyStructure.CompanyUser();
            userModel = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(UserId);
            userModel.ContactInfo.ContactName = ContactName;
            userModel.ContactInfo.ContactSex = Sex ? EyouSoft.Model.CompanyStructure.Sex.男 : EyouSoft.Model.CompanyStructure.Sex.女;
            userModel.ContactInfo.Mobile = Utils.InputText(ContactMobile, 50);
            userModel.ContactInfo.Fax = Utils.InputText(Fax, 50);
            userModel.ContactInfo.Tel = Utils.InputText(ContactTel, 20);
            userModel.ContactInfo.Email = Utils.InputText(Email);
            userModel.ContactInfo.QQ = Utils.InputText(QQ, 20);
            userModel.ContactInfo.MSN = Utils.InputText(MSN, 50);

            Resutl = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().UpdatePersonal(userModel) ? 1 : -1;
            if (Resutl == 1 && !string.IsNullOrEmpty(NewPassWord))
            {
                EyouSoft.Model.CompanyStructure.PassWord pass = new EyouSoft.Model.CompanyStructure.PassWord();
                pass.NoEncryptPassword = Utils.InputText(NewPassWord);//设置新密码
                Resutl = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().UpdatePassWord(Utils.InputText(UserId), pass) ? 2 : -2;
                pass = null;
            }
            userModel = null;

            return Resutl;
        }

        #endregion

        #region 修改公司信息

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="License">许可证号</param>
        /// <param name="ContactName">总负责人</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="ContactMobile">手机</param>
        /// <param name="Fax">传真</param>
        /// <param name="QQ">QQ</param>
        /// <param name="MSN">MSN</param>
        /// <param name="CompanyAddress">公司地址</param>
        /// <param name="CompanyRemark">介绍说明</param>
        /// <returns></returns>
        [System.Web.Services.Protocols.SoapHeader("soapHeader")]
        [WebMethod]
        public bool UpdateCompanyInfo(string CompanyId, string License, string ContactName, string ContactTel, string ContactMobile
            , string Fax, string QQ, string MSN, string CompanyAddress, string CompanyRemark)
        {
            if (!soapHeader.IsSafeCall)
                throw new Exception("对不起，您没有权限调用此服务！");

            if (string.IsNullOrEmpty(CompanyId) || string.IsNullOrEmpty(ContactName))
                return false;

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo OldCompanyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(Utils.InputText(CompanyId));
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo NewCompanyModel = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();
            if (OldCompanyModel != null)
            {
                NewCompanyModel = OldCompanyModel;
            }
            NewCompanyModel.ID = Utils.InputText(CompanyId);
            NewCompanyModel.License = Utils.InputText(License);
            NewCompanyModel.ContactInfo.ContactName = Utils.InputText(ContactName);
            NewCompanyModel.ContactInfo.Tel = Utils.InputText(ContactTel);
            NewCompanyModel.ContactInfo.Mobile = Utils.InputText(ContactMobile);
            NewCompanyModel.ContactInfo.Fax = Utils.InputText(Fax);
            NewCompanyModel.ContactInfo.QQ = Utils.InputText(QQ);
            NewCompanyModel.ContactInfo.MSN = Utils.InputText(MSN);
            NewCompanyModel.CompanyAddress = Utils.InputText(CompanyAddress);
            NewCompanyModel.Remark = Utils.InputText(CompanyRemark);

            bool Result = false;
            Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().UpdateSelf(NewCompanyModel);

            NewCompanyModel = null;
            OldCompanyModel = null;

            return Result;
        }

        #endregion
    }
}
