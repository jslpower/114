using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WEB.sync
{
    /// <summary>
    /// 大平台数据同步(系统整合)处理程序
    /// </summary>
    /// Author:汪奇志 DateTime:2011-04-06
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SyncHandling : IHttpHandler
    {
        #region process request
        /// <summary>
        /// process request
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string request = context.Request.Form["request"];

            EyouSoft.OpenRelation.Model.MRequestInfo requestInfo = null;
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = EyouSoft.OpenRelation.Utils.ValidateRequest(request, out requestInfo);

            if (!responseInfo.IsSuccess)
            {
                ResponseWrite(context, responseInfo);
                return;
            }

            responseInfo = Hand(requestInfo);

            ResponseWrite(context, responseInfo);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region private memebers
        /// <summary>
        /// HttpContext Response Write
        /// </summary>
        /// <param name="context">System.Web.HttpContext</param>
        /// <param name="responseInfo">EyouSoft.OpenRelation.Model.MResponseInfo</param>
        private void ResponseWrite(HttpContext context, EyouSoft.OpenRelation.Model.MResponseInfo responseInfo)
        {
            context.Response.Clear();
            context.Response.Write(EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MResponseInfo>(responseInfo));
            context.Response.End();
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        private EyouSoft.Model.CompanyStructure.Sex GetGender(EyouSoft.OpenRelation.Model.Gender gender)
        {
            EyouSoft.Model.CompanyStructure.Sex g = EyouSoft.Model.CompanyStructure.Sex.未知;
            switch (gender)
            {
                case EyouSoft.OpenRelation.Model.Gender.G: g = EyouSoft.Model.CompanyStructure.Sex.男; break;
                case EyouSoft.OpenRelation.Model.Gender.L: g = EyouSoft.Model.CompanyStructure.Sex.女; break;
            }
            return g;
        }

        /// <summary>
        /// 分处理
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo Hand(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = null;

            switch (requestInfo.InstructionType)
            {
                case EyouSoft.OpenRelation.Model.InstructionType.CreateCompany:
                    responseInfo = CreateCompanyInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.UpdateCompany:
                    responseInfo = UpdateCompanyInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.DeleteCompany:
                    responseInfo = DeleteCompanyInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.CreateUser:
                    responseInfo = CreateUserInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.UpdateUser:
                    responseInfo = UpdateUserInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.DeleteUser:
                    responseInfo = DeleteUserInfo(requestInfo);
                    break;
            }

            return responseInfo;
        }

        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo CreateCompanyInfo(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };

            EyouSoft.OpenRelation.Model.MCUInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MCUInfo>(requestInfo.InstructionCode);
            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo companyInfo = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();
            EyouSoft.Model.CompanyStructure.UserAccount userInfo = new EyouSoft.Model.CompanyStructure.UserAccount();
            EyouSoft.Model.CompanyStructure.ContactPersonInfo contactInfo = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            EyouSoft.Model.CompanyStructure.CompanyRole roleInfo = new EyouSoft.Model.CompanyStructure.CompanyRole();

            //大平台邮箱不能为空 赋值空格
            if (string.IsNullOrEmpty(middleInfo.UserInfo.Email)) middleInfo.UserInfo.Email = " ";
            if (string.IsNullOrEmpty(middleInfo.CompanyInfo.ContactEmail)) middleInfo.CompanyInfo.ContactEmail = " ";

            int provinceId, cityId;
            this.GetProvinceAndCityId(middleInfo.CompanyInfo.ProvinceName, middleInfo.CompanyInfo.CityName, out provinceId, out cityId);

            companyInfo.CompanyName = middleInfo.CompanyInfo.CompanyName;
            companyInfo.CompanyAddress = middleInfo.CompanyInfo.Address;
            companyInfo.ProvinceId = provinceId;
            companyInfo.CityId = cityId ;

            userInfo.UserName = middleInfo.UserInfo.UserName;
            userInfo.PassWordInfo = new EyouSoft.Model.CompanyStructure.PassWord(middleInfo.UserInfo.Password);

            contactInfo.ContactName = middleInfo.CompanyInfo.ContactName;
            contactInfo.Email = middleInfo.CompanyInfo.ContactEmail;
            contactInfo.Fax = middleInfo.CompanyInfo.ContactFax;
            contactInfo.Mobile = middleInfo.CompanyInfo.ContactMobile;
            contactInfo.MSN = middleInfo.CompanyInfo.ContactMSN;
            contactInfo.QQ = middleInfo.CompanyInfo.ContactQQ;
            contactInfo.Tel = middleInfo.CompanyInfo.ContactTelephone;
            contactInfo.ContactSex = GetGender(middleInfo.CompanyInfo.ContactGender);

            if (middleInfo.CompanyInfo.PlatformCompanyType == EyouSoft.OpenRelation.Model.PlatformCompanyType.ZT)
                roleInfo.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.组团);
            else if (middleInfo.CompanyInfo.PlatformCompanyType == EyouSoft.OpenRelation.Model.PlatformCompanyType.ZX)
                roleInfo.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);

            companyInfo.AdminAccount = userInfo;
            companyInfo.CompanyRole = roleInfo;
            companyInfo.ContactInfo = contactInfo;

            try
            {
                EyouSoft.Model.ResultStructure.UserResultInfo resultInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Add(companyInfo, string.Empty);
                if (resultInfo == EyouSoft.Model.ResultStructure.UserResultInfo.Succeed)
                {
                    responseInfo.IsSuccess = true;
                    responseInfo.Desc = "创建公司成功！";
                    EyouSoft.OpenRelation.Model.MRCreateUserInfo rInfo = new EyouSoft.OpenRelation.Model.MRCreateUserInfo()
                    {
                        PlatformCompanyId = companyInfo.ID,
                        PlatformUserId = companyInfo.AdminAccount.ID
                    };
                    responseInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MRCreateUserInfo>(rInfo);

                    EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().PassRegister(companyInfo.ID);
                }
                else
                {
                    responseInfo.IsSuccess = false;
                    responseInfo.Desc = "创建公司失败，业务逻辑层返回失败信息！";
                }
            }
            catch (Exception e)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "创建公司失败，捕获到异常！";
                responseInfo.ErrorCode = e.Message + e.StackTrace;
            }

            return responseInfo;
        }

        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo UpdateCompanyInfo(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };
            if (requestInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "http request post info对象为空！";

                return responseInfo;
            }

            EyouSoft.OpenRelation.Model.MCompanyInfo MiddComp = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(requestInfo.InstructionCode);
            if (MiddComp == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "Josn对象转为中间平台对象时发生错误！";

                return responseInfo;
            }
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo ComBll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo CompanyInfo = ComBll.GetModel(MiddComp.PlatformCompanyId);
            if (CompanyInfo == null || CompanyInfo.ContactInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "在平台中未找到对应的公司或者公司对应的联系人信息！";

                return responseInfo;
            }

            //大平台邮箱不能为空 赋值空格
            if (string.IsNullOrEmpty(MiddComp.ContactEmail)) MiddComp.ContactEmail = " ";

            //公司
            if (!string.IsNullOrEmpty(MiddComp.CompanyName))
                CompanyInfo.CompanyName = MiddComp.CompanyName;
            if (!string.IsNullOrEmpty(MiddComp.Address))
                CompanyInfo.CompanyAddress = MiddComp.Address;
            if (!string.IsNullOrEmpty(MiddComp.Domain))
                CompanyInfo.WebSite = MiddComp.Domain;
            //联系人
            if (!string.IsNullOrEmpty(MiddComp.ContactName))
                CompanyInfo.ContactInfo.ContactName = MiddComp.ContactName;
            if (!string.IsNullOrEmpty(MiddComp.ContactTelephone))
                CompanyInfo.ContactInfo.Tel = MiddComp.ContactTelephone;
            if (!string.IsNullOrEmpty(MiddComp.ContactMobile))
                CompanyInfo.ContactInfo.Mobile = MiddComp.ContactMobile;
            if (!string.IsNullOrEmpty(MiddComp.ContactFax))
                CompanyInfo.ContactInfo.Fax = MiddComp.ContactFax;
            CompanyInfo.ContactInfo.Email = MiddComp.ContactEmail;
            CompanyInfo.ContactInfo.MSN = MiddComp.ContactMSN;
            CompanyInfo.ContactInfo.QQ = MiddComp.ContactQQ;
            CompanyInfo.ContactInfo.ContactSex = GetGender(MiddComp.ContactGender);

            try
            {
                responseInfo.IsSuccess = ComBll.UpdateSelf(CompanyInfo);
                responseInfo.Desc = "修改公司信息完成！";
            }
            catch (Exception e)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "修改公司信息操作执行异常！";
                responseInfo.ErrorCode = e.Message + e.StackTrace;
            }

            return responseInfo;
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo DeleteCompanyInfo(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };

            return responseInfo;

            /*
            if (requestInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "http request post info对象为空！";

                return responseInfo;
            }

            EyouSoft.OpenRelation.Model.MCompanyInfo MiddComp = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(requestInfo.InstructionCode);
            if (MiddComp == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "Josn对象转为中间平台对象时发生错误！";

                return responseInfo;
            }
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo ComBll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            try
            {
                responseInfo.IsSuccess = ComBll.Remove(MiddComp.PlatformCompanyId);
                responseInfo.Desc = "删除公司操作完成！";
            }
            catch (Exception e)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "删除公司信息操作执行异常！";
                responseInfo.ErrorCode = e.Message + e.StackTrace;
            }

            return responseInfo;*/
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo CreateUserInfo(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };
            if (requestInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "http request post info对象为空！";

                return responseInfo;
            }
            EyouSoft.OpenRelation.Model.MUserInfo MiddUser = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(requestInfo.InstructionCode);
            if (MiddUser == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "Josn对象转为中间平台对象时发生错误！";

                return responseInfo;
            }

            //大平台邮箱不能为空 赋值空格
            if (string.IsNullOrEmpty(MiddUser.Email)) MiddUser.Email = " ";

            int provinceId, cityId;
            this.GetProvinceAndCityId(MiddUser.ProvinceName, MiddUser.CityName, out provinceId, out cityId);

            EyouSoft.Model.CompanyStructure.CompanyUser UserModel = new EyouSoft.Model.CompanyStructure.CompanyUser();
            EyouSoft.IBLL.CompanyStructure.ICompanyUser UserBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            UserModel.PassWordInfo = new EyouSoft.Model.CompanyStructure.PassWord();
            UserModel.ContactInfo = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            UserModel.UserName = MiddUser.UserName.Trim();
            UserModel.PassWordInfo.NoEncryptPassword = MiddUser.Password.Trim();
            UserModel.ContactInfo.ContactName = MiddUser.RealName.Trim();
            UserModel.ContactInfo.ContactSex = GetGender(MiddUser.Gender);
            UserModel.ContactInfo.Mobile = MiddUser.Mobile.Trim();
            UserModel.ContactInfo.Tel = MiddUser.Telephone.Trim();
            UserModel.ContactInfo.Fax = MiddUser.Fax.Trim();
            UserModel.ContactInfo.Email = MiddUser.Email.Trim();
            UserModel.ContactInfo.MSN = MiddUser.MSN.Trim();
            UserModel.ContactInfo.QQ = MiddUser.QQ.Trim();

            UserModel.OpUserId = MiddUser.SystemUserId;
            UserModel.IsAdmin = false;
            UserModel.IsEnable = true;
            UserModel.CompanyID = MiddUser.PlatformCompanyId;
            UserModel.DepartId = string.Empty;
            UserModel.DepartName = string.Empty;
            UserModel.RoleID = string.Empty;
            UserModel.ProvinceId = provinceId;
            UserModel.CityId = cityId;

            try
            {
                responseInfo.IsSuccess = UserBll.Add(UserModel) == EyouSoft.Model.ResultStructure.ResultInfo.Succeed ? true : false;
                responseInfo.Desc = "新增用户操作执行完成！";

                if (responseInfo.IsSuccess)
                {
                    EyouSoft.OpenRelation.Model.MRCreateUserInfo rInfo = new EyouSoft.OpenRelation.Model.MRCreateUserInfo()
                    {
                        PlatformCompanyId = UserModel.CompanyID,
                        PlatformUserId = UserModel.ID
                    };
                    responseInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MRCreateUserInfo>(rInfo);
                    responseInfo.Desc = "新增用户操作执行完成！";
                }
                else
                {
                    responseInfo.Desc = "创建用户失败，业务逻辑层返回失败信息！";
                }

            }
            catch (Exception e)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "新增用户操作执行异常！";
                responseInfo.ErrorCode = e.Message + e.StackTrace;
            }

            return responseInfo;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo UpdateUserInfo(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };
            if (requestInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "http request post info对象为空！";

                return responseInfo;
            }
            EyouSoft.OpenRelation.Model.MUserInfo MiddUser = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(requestInfo.InstructionCode);
            if (MiddUser == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "Josn对象转为中间平台对象时发生错误！";

                return responseInfo;
            }
            EyouSoft.IBLL.CompanyStructure.ICompanyUser UserBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyUser UserModel = UserBll.GetModel(MiddUser.PlatformUserId);
            if (UserModel == null || UserModel.ContactInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "在平台中未找到对应的用户或者用户对应的联系人信息！";

                return responseInfo;
            }

            //大平台邮箱不能为空 赋值空格
            if (string.IsNullOrEmpty(MiddUser.Email)) MiddUser.Email = " ";

            UserModel.UserName = MiddUser.UserName.Trim();
            if (!string.IsNullOrEmpty(MiddUser.Password.Trim()))
                UserModel.PassWordInfo.NoEncryptPassword = MiddUser.Password.Trim();
            UserModel.ContactInfo.ContactName = MiddUser.RealName.Trim();
            UserModel.ContactInfo.ContactSex = GetGender(MiddUser.Gender);
            UserModel.ContactInfo.Mobile = MiddUser.Mobile.Trim();
            UserModel.ContactInfo.Tel = MiddUser.Telephone.Trim();
            UserModel.ContactInfo.Fax = MiddUser.Fax.Trim();
            UserModel.ContactInfo.Email = MiddUser.Email.Trim();
            UserModel.ContactInfo.MSN = MiddUser.MSN.Trim();
            UserModel.ContactInfo.QQ = MiddUser.QQ.Trim();

            try
            {
                responseInfo.IsSuccess = UserBll.UpdateChild(UserModel);
                responseInfo.Desc = "修改用户信息操作已完成！";
            }
            catch (Exception e)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "修改用户操作执行异常！";
                responseInfo.ErrorCode = e.Message + e.StackTrace;
            }

            return responseInfo;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo DeleteUserInfo(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };

            return responseInfo;

            /*
            if (requestInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "http request post info对象为空！";

                return responseInfo;
            }
            EyouSoft.OpenRelation.Model.MUserInfo MiddUser = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(requestInfo.InstructionCode);
            if (MiddUser == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "Josn对象转为中间平台对象时发生错误！";

                return responseInfo;
            }
            EyouSoft.IBLL.CompanyStructure.ICompanyUser UserBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();

            try
            {
                responseInfo.IsSuccess = UserBll.Remove(MiddUser.PlatformUserId);
                responseInfo.Desc = "删除用户操作已完成！";
            }
            catch (Exception e)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "删除用户操作执行异常！";
                responseInfo.ErrorCode = e.Message + e.StackTrace;
            }

            return responseInfo;*/
        }

        /// <summary>
        /// 获取省份城市编号
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        private void GetProvinceAndCityId(string provinceName, string cityName, out int provinceId, out int cityId)
        {
            provinceId = 0;
            cityId = 0;

            if (string.IsNullOrEmpty(cityName)) return;

            var citys = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList();
            if (citys == null || citys.Count == 0) return;

            var city = citys.First(item => item.CityName == cityName);

            if (city != null)
            {
                provinceId = city.ProvinceId;
                cityId = city.CityId;
            }
            
            /*foreach (var city in citys)
            {
                if (city.CityName == cityName)
                {
                    provinceId = city.ProvinceId;
                    cityId = city.CityId;
                    break;
                }
            }*/
        }

        #endregion
    }
}
