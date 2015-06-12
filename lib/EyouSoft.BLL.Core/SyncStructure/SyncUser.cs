using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SyncStructure
{
    /// <summary>
    /// 数据同步用户操作类
    /// </summary>
    /// 周文超   2011-04-12
    public class SyncUser
    {
        /// <summary>
        /// 同步添加其他平台用户信息   zwc  2011-04-07
        /// </summary>
        /// <param name="model">大平台用户信息实体</param>
        public static void SyncAddUserInfo(EyouSoft.Model.CompanyStructure.CompanyUser model)
        {
            if (model == null)
                return;

            if (EyouSoft.OpenRelation.Utils.GetIsSync())
            {
                //同步添加其他平台用户信息
                EyouSoft.OpenRelation.Utils.CreateRequest(
                    new EyouSoft.OpenRelation.Model.MRequestInfo()
                    {
                        AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                        RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                        RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                        InstructionType = EyouSoft.OpenRelation.Model.InstructionType.CreateUser,
                        InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(new EyouSoft.OpenRelation.Model.MUserInfo()
                        {
                            UserName = model.UserName.Trim(),
                            Password = model.PassWordInfo.NoEncryptPassword.Trim(),
                            RealName = model.ContactInfo.ContactName,
                            Gender = model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.男 ? EyouSoft.OpenRelation.Model.Gender.G : EyouSoft.OpenRelation.Model.Gender.L,
                            Mobile = model.ContactInfo.Mobile,
                            Telephone = model.ContactInfo.Tel,
                            Fax = model.ContactInfo.Fax,
                            Email = model.ContactInfo.Email,
                            MSN = model.ContactInfo.MSN,
                            QQ = model.ContactInfo.QQ,
                            PlatformCompanyId = model.CompanyID,
                            PlatformUserId = model.ID,
                            SystemType = EyouSoft.OpenRelation.Model.SystemType.Platform
                        })
                    });
            }
        }

        /// <summary>
        /// 同步修改其他平台用户信息   zwc  2011-04-07
        /// </summary>
        /// <param name="PlamUserId">用户Id</param>
        public static void SyncUpdateUserInfo(string PlamUserId)
        {
            if (string.IsNullOrEmpty(PlamUserId))
                return;

            EyouSoft.Model.CompanyStructure.CompanyUser model = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(PlamUserId);
            if (model == null)
                return;

            #region 同步修改其他平台用户信息   zwc  2011-04-07

            if (EyouSoft.OpenRelation.Utils.GetIsSync())
            {
                //同步修改其他平台用户信息
                EyouSoft.OpenRelation.Utils.CreateRequest(
                    new EyouSoft.OpenRelation.Model.MRequestInfo()
                    {
                        AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                        RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                        RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                        InstructionType = EyouSoft.OpenRelation.Model.InstructionType.UpdateUser,
                        InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(new EyouSoft.OpenRelation.Model.MUserInfo()
                        {
                            UserName = model.UserName.Trim(),
                            Password = model.PassWordInfo.NoEncryptPassword.Trim(),
                            RealName = model.ContactInfo.ContactName,
                            Gender = model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.男 ? EyouSoft.OpenRelation.Model.Gender.G : EyouSoft.OpenRelation.Model.Gender.L,
                            Mobile = model.ContactInfo.Mobile,
                            Telephone = model.ContactInfo.Tel,
                            Fax = model.ContactInfo.Fax,
                            Email = model.ContactInfo.Email,
                            MSN = model.ContactInfo.MSN,
                            QQ = model.ContactInfo.QQ,
                            PlatformCompanyId = model.CompanyID,
                            PlatformUserId = model.ID,
                            SystemType = EyouSoft.OpenRelation.Model.SystemType.Platform
                        })
                    });
            }

            #endregion
        }

        /// <summary>
        /// 同步删除其他平台用户信息
        /// </summary>
        /// <param name="userIdList">大平台用户Id集合</param>
        public static void SyncDeleteUserInfo(params string[] userIdList)
        {
            if (userIdList == null || userIdList.Length == 0)
                return;

            #region 同步删除其他平台用户信息   zwc  2011-04-07

            if (EyouSoft.OpenRelation.Utils.GetIsSync())
            {
                foreach (string t in userIdList)
                {
                    if (string.IsNullOrEmpty(t))
                        continue;

                    //同步删除其他平台用户信息
                    EyouSoft.OpenRelation.Utils.CreateRequest(
                        new EyouSoft.OpenRelation.Model.MRequestInfo()
                        {
                            AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                            RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                            RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                            InstructionType = EyouSoft.OpenRelation.Model.InstructionType.DeleteUser,
                            InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(new EyouSoft.OpenRelation.Model.MUserInfo()
                            {
                                PlatformUserId = t.Trim(),
                                SystemType = EyouSoft.OpenRelation.Model.SystemType.Platform
                            })
                        });
                }
            }

            #endregion
        }



        /// <summary>
        /// 获取用户权限所属系统权限
        /// </summary>
        /// <param name="SystemType">所属系统类型</param>
        /// <param name="SystemUserId">系统用户编号</param>
        /// <returns></returns>
        public static string SyncGetUserPermission(EyouSoft.OpenRelation.Model.SystemType SystemType, int SystemUserId)
        {
            if (SystemUserId < 1)
                return string.Empty;
            
            EyouSoft.OpenRelation.Model.MResponseInfo response = EyouSoft.OpenRelation.Utils.CreateRequest(
                new EyouSoft.OpenRelation.Model.MRequestInfo()
                {
                    AppKey = EyouSoft.OpenRelation.Utils.GetAppKey(),
                    RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI(),
                    RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.Platform,
                    InstructionType = EyouSoft.OpenRelation.Model.InstructionType.UserPermission,
                    InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserBaseInfo>(new EyouSoft.OpenRelation.Model.MUserBaseInfo()
                    {
                        SystemUserId = SystemUserId,
                        SystemType = SystemType
                    })
                });
            return response.InstructionCode;
        }
    }
}
