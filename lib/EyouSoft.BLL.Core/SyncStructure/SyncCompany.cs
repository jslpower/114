using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SyncStructure
{
    /// <summary>
    /// 数据同步公司操作类
    /// </summary>
    /// 周文超   2011-04-12
    public class SyncCompany
    {
        /// <summary>
        /// 同步修改其他平台公司信息  zwc  2011-04-07
        /// </summary>
        /// <param name="PlamCompanyId">公司Id</param>
        /// <param name="PlamUserId">用户Id，不传值不修改</param>
        public static void SyncUpdateCompany(string PlamCompanyId, string PlamUserId)
        {
            if (string.IsNullOrEmpty(PlamCompanyId))
                return;

            EyouSoft.Model.CompanyStructure.CompanyInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(PlamCompanyId);
            if (model == null)
                return;

            #region 同步修改其他平台公司信息  zwc  2011-04-07

            if (EyouSoft.OpenRelation.Utils.GetIsSync())
            {
                //同步修改其他平台公司信息
                EyouSoft.OpenRelation.Utils.CreateRequest(
                    new EyouSoft.OpenRelation.Model.MRequestInfo()
                    {
                        AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                        RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                        RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                        InstructionType = EyouSoft.OpenRelation.Model.InstructionType.UpdateCompany,
                        InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(new EyouSoft.OpenRelation.Model.MCompanyInfo()
                        {
                            CompanyName = model.CompanyName,
                            ContactName = model.ContactInfo.ContactName,
                            ContactTelephone = model.ContactInfo.Tel,
                            ContactMobile = model.ContactInfo.Mobile,
                            ContactFax = model.ContactInfo.Fax,
                            Address = model.CompanyAddress,
                            Domain = model.WebSite,
                            PlatformCompanyId = model.ID,
                            ContactEmail = model.ContactInfo.Email,
                            ContactMSN = model.ContactInfo.MSN,
                            ContactQQ = model.ContactInfo.QQ,
                            ContactGender = model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.男 ? EyouSoft.OpenRelation.Model.Gender.G : EyouSoft.OpenRelation.Model.Gender.L,
                            SystemType = EyouSoft.OpenRelation.Model.SystemType.Platform
                            //SystemCompanyType = EyouSoft.OpenRelation.Model.SystemCompanyType.
                        })
                    });

                #region 同步修改管理员信息

                if (!string.IsNullOrEmpty(PlamUserId))
                {
                    //同步修改其他平台用户信息
                    EyouSoft.Model.CompanyStructure.CompanyUser UserModel = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(PlamUserId);
                    if (UserModel != null)
                    {
                        EyouSoft.OpenRelation.Utils.CreateRequest(
                            new EyouSoft.OpenRelation.Model.MRequestInfo()
                            {
                                AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                                RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                                RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                                InstructionType = EyouSoft.OpenRelation.Model.InstructionType.UpdateUser,
                                InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(new EyouSoft.OpenRelation.Model.MUserInfo()
                                {
                                    UserName = UserModel.UserName.Trim(),
                                    Password = UserModel.PassWordInfo.NoEncryptPassword.Trim(),
                                    RealName = UserModel.ContactInfo.ContactName,
                                    Gender = UserModel.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.男 ? EyouSoft.OpenRelation.Model.Gender.G : EyouSoft.OpenRelation.Model.Gender.L,
                                    Mobile = UserModel.ContactInfo.Mobile,
                                    Telephone = UserModel.ContactInfo.Tel,
                                    Fax = UserModel.ContactInfo.Fax,
                                    Email = UserModel.ContactInfo.Email,
                                    MSN = UserModel.ContactInfo.MSN,
                                    QQ = UserModel.ContactInfo.QQ,
                                    PlatformCompanyId = UserModel.CompanyID,
                                    PlatformUserId = UserModel.ID,
                                    SystemType = EyouSoft.OpenRelation.Model.SystemType.Platform
                                })
                            });
                    }
                }

                #endregion
            }

            #endregion
        }

        /// <summary>
        /// 同步删除其他平台公司
        /// </summary>
        /// <param name="companyIdList">大平台公司Id集合</param>
        public static void SyncDeleteCompany(params string[] companyIdList)
        {
            if (companyIdList == null || companyIdList.Length == 0)
                return;

            #region 同步删除其他平台公司信息 zwc  2011-04-07

            if (EyouSoft.OpenRelation.Utils.GetIsSync())
            {
                foreach (string t in companyIdList)
                {
                    if (string.IsNullOrEmpty(t))
                        continue;

                    //同步删除其他平台公司信息
                    EyouSoft.OpenRelation.Utils.CreateRequest(
                        new EyouSoft.OpenRelation.Model.MRequestInfo()
                        {
                            AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                            RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                            RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                            InstructionType = EyouSoft.OpenRelation.Model.InstructionType.DeleteCompany,
                            InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(new EyouSoft.OpenRelation.Model.MCompanyInfo()
                            {
                                PlatformCompanyId = t.Trim(),
                                SystemType = EyouSoft.OpenRelation.Model.SystemType.Platform
                            })
                        });
                }
            }

            #endregion
        }
    }
}
