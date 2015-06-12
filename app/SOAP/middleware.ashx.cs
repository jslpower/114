using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SOAP
{
    /// <summary>
    /// 系统整合中间处理程序
    /// </summary>
    /// Author:汪奇志 DateTime:2011-04-01
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]    
    public class middleware : IHttpHandler
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

        /// <summary>
        /// IsReusable
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region private members
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
        /// 分发处理
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo Hand(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = null;

            switch (requestInfo.InstructionType)
            {
                case EyouSoft.OpenRelation.Model.InstructionType.None:
                    responseInfo = NoneInstruction();
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.CreateCompany:
                    responseInfo = CreateCompanyInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.CreateUser: 
                    responseInfo = CreateUserInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.UpdateCompany:
                    responseInfo = UpdateCompanyInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.UpdateUser:
                    responseInfo = UpdateUserInfo(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.DeleteCompany:
                    //responseInfo = DeleteCompanyInfo(requestInfo);
                    responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo() { IsSuccess = true };
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.DeleteUser:
                    //responseInfo = DeleteUserInfo(requestInfo);
                    responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo() { IsSuccess = true };
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.ExistsUser:
                    responseInfo = ExistsUser(requestInfo);
                    break;
                case EyouSoft.OpenRelation.Model.InstructionType.UserPermission:
                    responseInfo = GetUserPermission(requestInfo);
                    break;
            }

            responseInfo.InstructionType = requestInfo.InstructionType;
            return responseInfo;
        }

        /// <summary>
        /// 空指令
        /// </summary>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo NoneInstruction()
        {
            return new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true,
                Desc = "空白指令处理成功!"
            };
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

            if (requestInfo.RequestSystemType == EyouSoft.OpenRelation.Model.SystemType.Platform)
            {
                return responseInfo;
            }

            EyouSoft.OpenRelation.Model.MCUInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MCUInfo>(requestInfo.InstructionCode);

            var items = EyouSoft.BLL.OpenStructure.BCompany.CreateInstance().GetCompanyList(middleInfo.CompanyInfo.SystemCompanyId, (int)middleInfo.CompanyInfo.SystemType, null, (int)middleInfo.CompanyInfo.SystemCompanyType);

            if (items != null && items.Count > 0)//已存在公司对应关系，进行新增用户操作
            {
                requestInfo.InstructionType = EyouSoft.OpenRelation.Model.InstructionType.CreateUser;
                requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(middleInfo.UserInfo);

                return CreateUserInfo(requestInfo);
            }

            requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.Platform);
            responseInfo = HandOut(requestInfo);            

            if (responseInfo.IsSuccess)//创建成功，写对应关系
            {
                EyouSoft.OpenRelation.Model.MRCreateUserInfo rInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MRCreateUserInfo>(responseInfo.InstructionCode);

                EyouSoft.BLL.OpenStructure.BCompany.CreateInstance().AddMCompany(new EyouSoft.Model.OpenStructure.MCompanyInfo()
                {
                    PlatformCompanyId = rInfo.PlatformCompanyId,
                    SystemCompanyId = middleInfo.CompanyInfo.SystemCompanyId,
                    SystemCompanyType = (int)middleInfo.CompanyInfo.SystemCompanyType,
                    SystemType = (int)middleInfo.CompanyInfo.SystemType
                });

                EyouSoft.BLL.OpenStructure.BUser.CreateInstance().AddMUser(new EyouSoft.Model.OpenStructure.MUserInfo()
                {
                    PlatformCompanyId = rInfo.PlatformCompanyId,
                    PlatformUserId = rInfo.PlatformUserId,
                    SystemType = (int)middleInfo.UserInfo.SystemType,
                    SystemUserId = middleInfo.UserInfo.SystemUserId
                });
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
            var ibll = EyouSoft.BLL.OpenStructure.BCompany.CreateInstance();
            EyouSoft.OpenRelation.Model.MCompanyInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(requestInfo.InstructionCode);
            IList<EyouSoft.Model.OpenStructure.MCompanyInfo> items = new List<EyouSoft.Model.OpenStructure.MCompanyInfo>();
            switch (requestInfo.RequestSystemType)
            {
                case EyouSoft.OpenRelation.Model.SystemType.Platform:
                    items = ibll.GetCompanyList(0, (int)requestInfo.RequestSystemType, middleInfo.PlatformCompanyId);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.TYT:
                    items = ibll.GetCompanyList(middleInfo.SystemCompanyId, (int)requestInfo.RequestSystemType, null, (int)middleInfo.SystemCompanyType);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.YYT:
                    items = ibll.GetCompanyList(middleInfo.SystemCompanyId, (int)requestInfo.RequestSystemType, null, (int)middleInfo.SystemCompanyType);
                    break;
            }
            ibll = null;

            if (items == null || items.Count < 1)
            {
                responseInfo.IsSuccess = true;
                responseInfo.Desc = "中间处理程序未找到公司对应关系，未处理任何数据！";
                return responseInfo;
            }

            foreach (var item in items)
            {
                middleInfo.PlatformCompanyId = item.PlatformCompanyId;
                middleInfo.SystemCompanyId = item.SystemCompanyId;
                middleInfo.SystemCompanyType = (EyouSoft.OpenRelation.Model.SystemCompanyType)item.SystemCompanyType;
                middleInfo.SystemType = (EyouSoft.OpenRelation.Model.SystemType)item.SystemType;

                if (requestInfo.RequestSystemType == EyouSoft.OpenRelation.Model.SystemType.Platform)
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(middleInfo.SystemType);
                }
                else
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.Platform);
                }

                requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(middleInfo);

                HandOut(requestInfo);
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
            var ibll = EyouSoft.BLL.OpenStructure.BCompany.CreateInstance();
            EyouSoft.OpenRelation.Model.MCompanyInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(requestInfo.InstructionCode);
            IList<EyouSoft.Model.OpenStructure.MCompanyInfo> items = new List<EyouSoft.Model.OpenStructure.MCompanyInfo>();

            switch (requestInfo.RequestSystemType)
            {
                case EyouSoft.OpenRelation.Model.SystemType.Platform:
                    items = ibll.GetCompanyList(0, (int)requestInfo.RequestSystemType, middleInfo.PlatformCompanyId);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.TYT:
                    items = ibll.GetCompanyList(middleInfo.SystemCompanyId, (int)requestInfo.RequestSystemType, null, (int)middleInfo.SystemCompanyType);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.YYT:
                    items = ibll.GetCompanyList(middleInfo.SystemCompanyId, (int)requestInfo.RequestSystemType, null, (int)middleInfo.SystemCompanyType);
                    break;
            }

            ibll = null;

            if (items == null || items.Count < 1)
            {
                responseInfo.IsSuccess = true;
                responseInfo.Desc = "中间处理程序未找到公司对应关系，未处理任何数据！";
                return responseInfo;
            }

            foreach (var item in items)
            {
                middleInfo.PlatformCompanyId = item.PlatformCompanyId;
                middleInfo.SystemCompanyId = item.SystemCompanyId;
                middleInfo.SystemType = (EyouSoft.OpenRelation.Model.SystemType)item.SystemType;
                middleInfo.SystemCompanyType =( EyouSoft.OpenRelation.Model.SystemCompanyType) item.SystemCompanyType;

                if (requestInfo.RequestSystemType == EyouSoft.OpenRelation.Model.SystemType.Platform)
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(middleInfo.SystemType);
                }
                else
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.Platform);
                }

                requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MCompanyInfo>(middleInfo);

                HandOut(requestInfo);
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
            if (requestInfo.RequestSystemType == EyouSoft.OpenRelation.Model.SystemType.Platform)
            {
                return responseInfo;
            }

            EyouSoft.OpenRelation.Model.MUserInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(requestInfo.InstructionCode);
            var items=EyouSoft.BLL.OpenStructure.BCompany.CreateInstance().GetCompanyList(middleInfo.SystemCompanyId, (int)middleInfo.SystemType, null, (int)middleInfo.SystemCompanyType);
            if (items == null || items.Count < 1)
            {
                responseInfo.IsSuccess = true;
                responseInfo.Desc = "中间处理程序未找到用户与公司对应关系，未处理任何数据！";
                return responseInfo;
            }
            middleInfo.SystemCompanyId = items[0].SystemCompanyId;
            middleInfo.SystemCompanyType = (EyouSoft.OpenRelation.Model.SystemCompanyType)items[0].SystemCompanyType;
            middleInfo.SystemType = (EyouSoft.OpenRelation.Model.SystemType)items[0].SystemType;
            middleInfo.PlatformCompanyId = items[0].PlatformCompanyId;

            requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.Platform);
            requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(middleInfo);
            responseInfo = HandOut(requestInfo);

            if (responseInfo.IsSuccess)//创建成功，写对应关系
            {
                EyouSoft.OpenRelation.Model.MRCreateUserInfo rInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MRCreateUserInfo>(responseInfo.InstructionCode);

                EyouSoft.BLL.OpenStructure.BUser.CreateInstance().AddMUser(new EyouSoft.Model.OpenStructure.MUserInfo()
                {
                    PlatformCompanyId = rInfo.PlatformCompanyId,
                    PlatformUserId = rInfo.PlatformUserId,
                    SystemType = (int)middleInfo.SystemType,
                    SystemUserId = middleInfo.SystemUserId
                });
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
            var ibll = EyouSoft.BLL.OpenStructure.BUser.CreateInstance();
            EyouSoft.OpenRelation.Model.MUserInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(requestInfo.InstructionCode);
            IList<EyouSoft.Model.OpenStructure.MUserInfo> items = new List<EyouSoft.Model.OpenStructure.MUserInfo>();

            switch (requestInfo.RequestSystemType)
            {
                case EyouSoft.OpenRelation.Model.SystemType.Platform:
                    items = ibll.GetMUserList(0, (int)requestInfo.RequestSystemType, middleInfo.PlatformUserId);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.TYT:
                    items = ibll.GetMUserList(middleInfo.SystemUserId, (int)requestInfo.RequestSystemType, null);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.YYT:
                    items = ibll.GetMUserList(middleInfo.SystemUserId, (int)requestInfo.RequestSystemType, null);
                    break;
            }

            ibll = null;

            if (items == null || items.Count < 1)
            {
                responseInfo.IsSuccess = true;
                responseInfo.Desc = "中间处理程序未找到用户对应关系，未处理任何数据！";
                return responseInfo;
            }

            foreach (var item in items)
            {
                middleInfo.PlatformCompanyId = item.PlatformCompanyId;
                middleInfo.PlatformUserId = item.PlatformUserId;
                middleInfo.SystemType =( EyouSoft.OpenRelation.Model.SystemType) item.SystemType;
                middleInfo.SystemUserId = item.SystemUserId;

                if (requestInfo.RequestSystemType == EyouSoft.OpenRelation.Model.SystemType.Platform)
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(middleInfo.SystemType);
                }
                else
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.Platform);
                }

                requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(middleInfo);

                HandOut(requestInfo);
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
            var ibll = EyouSoft.BLL.OpenStructure.BUser.CreateInstance();
            EyouSoft.OpenRelation.Model.MUserInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(requestInfo.InstructionCode);
            IList<EyouSoft.Model.OpenStructure.MUserInfo> items = new List<EyouSoft.Model.OpenStructure.MUserInfo>();

            switch (requestInfo.RequestSystemType)
            {
                case EyouSoft.OpenRelation.Model.SystemType.Platform:
                    items = ibll.GetMUserList(0, (int)requestInfo.RequestSystemType, middleInfo.PlatformUserId);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.TYT:
                    items = ibll.GetMUserList(middleInfo.SystemUserId, (int)requestInfo.RequestSystemType, null);
                    break;
                case EyouSoft.OpenRelation.Model.SystemType.YYT:
                    items = ibll.GetMUserList(middleInfo.SystemUserId, (int)requestInfo.RequestSystemType, null);
                    break;
            }

            ibll = null;

            if (items == null || items.Count < 1)
            {
                responseInfo.IsSuccess = true;
                responseInfo.Desc = "中间处理程序未找到用户对应关系，未处理任何数据！";
                return responseInfo;
            }

            foreach (var item in items)
            {
                middleInfo.PlatformCompanyId = item.PlatformCompanyId;
                middleInfo.PlatformUserId = item.PlatformUserId;
                middleInfo.SystemType = (EyouSoft.OpenRelation.Model.SystemType)item.SystemType;
                middleInfo.SystemUserId = item.SystemUserId;

                if (requestInfo.RequestSystemType == EyouSoft.OpenRelation.Model.SystemType.Platform)
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(middleInfo.SystemType);
                }
                else
                {
                    requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.Platform);
                }

                requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(middleInfo);

                HandOut(requestInfo);
            }

            return responseInfo;*/
        }

        /// <summary>
        /// 发送分发处理请求
        /// </summary>
        /// <param name="requestInfo"></param>
        private EyouSoft.OpenRelation.Model.MResponseInfo HandOut(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = EyouSoft.OpenRelation.Utils.CreateRequest(requestInfo);

            if (!responseInfo.IsSuccess)//写异常日志
            {
                var ibll = EyouSoft.BLL.OpenStructure.BException.CreateInstance();
                EyouSoft.Model.OpenStructure.MExceptionInfo exceptionInfo = new EyouSoft.Model.OpenStructure.MExceptionInfo()
                {
                    ExceptionCode = responseInfo.ErrorCode,
                    ExceptionDesc = responseInfo.Desc,
                    ExecptionId = Guid.NewGuid().ToString(),
                    ExceptionTime = DateTime.Now,
                    InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MRequestInfo>(requestInfo),
                    SystemType = (int)requestInfo.RequestSystemType
                };
                ibll.AddMException(exceptionInfo);
                ibll = null;
            }

            return responseInfo;
        }

        /// <summary>
        /// 用户是否存在验证
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        /// <remarks>
        /// 按配置文件指定的方式进行验证，用户名仅在新增用户时进行验证
        /// </remarks>
        private EyouSoft.OpenRelation.Model.MResponseInfo ExistsUser(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            bool isExists = false;
            string desc = string.Empty;
            var iubll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            string platformUserId = string.Empty;
            var ibll = EyouSoft.BLL.OpenStructure.BUser.CreateInstance(); 
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };
            EyouSoft.OpenRelation.Model.MExistsUserInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MExistsUserInfo>(requestInfo.InstructionCode);
            var items = ibll.GetMUserList(middleInfo.SystemUserId, (int)middleInfo.SystemType, null);
            
            if (items != null && items.Count > 0)
            {
                platformUserId = items[0].PlatformUserId;
            }
            items = null;
            ibll = null;

            middleInfo.ExistsUserType = EyouSoft.OpenRelation.Utils.GetExistsUserType();

            #region 修改时的验证
            if (middleInfo.SystemUserId > 0 && !string.IsNullOrEmpty(platformUserId))////若有用户对应关系视为修改操作，修改操作时若配置含邮箱验证，仅对邮箱进行验证，不对用户名进行验证
            {
                if (middleInfo.ExistsUserType == EyouSoft.OpenRelation.Model.ExistsUserType.Email || middleInfo.ExistsUserType == EyouSoft.OpenRelation.Model.ExistsUserType.Both)
                {
                    isExists = iubll.IsExistsEmail(middleInfo.Email, platformUserId);
                    if (isExists) desc = "邮箱已存在";

                    responseInfo.Desc = desc;
                    responseInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<bool>(isExists);
                }

                return responseInfo;
            }
            #endregion

            #region 新增时的验证
            //新增时的验证
            switch (middleInfo.ExistsUserType)
            {
                case EyouSoft.OpenRelation.Model.ExistsUserType.Both:
                    if (string.IsNullOrEmpty(middleInfo.Email))
                    {
                        isExists = true;
                        desc = "邮箱为空";
                    }
                    if (string.IsNullOrEmpty(middleInfo.UserName))
                    {
                        isExists = true;
                        desc = "用户名为空";
                    }
                    isExists = iubll.IsExistsEmail(middleInfo.Email) || iubll.IsExists(middleInfo.UserName);
                    if (isExists) desc = "邮箱或用户名已存在";
                    break;
                case EyouSoft.OpenRelation.Model.ExistsUserType.Email:
                    if (string.IsNullOrEmpty(middleInfo.Email))
                    {
                        isExists = true;
                        desc = "邮箱为空";
                    }
                    isExists = iubll.IsExistsEmail(middleInfo.Email);
                    if (isExists) desc = "邮箱已存在";                    
                    break;
                case EyouSoft.OpenRelation.Model.ExistsUserType.UserName:
                    if (string.IsNullOrEmpty(middleInfo.UserName))
                    {
                        isExists = true;
                        desc = "用户名为空";
                    }
                    isExists = iubll.IsExists(middleInfo.UserName);
                    if (isExists) desc = "用户名已存在";
                    break;
            }

            responseInfo.Desc = desc;
            responseInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<bool>(isExists);
            #endregion

            return responseInfo;
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private EyouSoft.OpenRelation.Model.MResponseInfo GetUserPermission(EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo()
            {
                IsSuccess = true
            };

            EyouSoft.OpenRelation.Model.MUserBaseInfo middleInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MUserBaseInfo>(requestInfo.InstructionCode);
            if (middleInfo != null)
            {
                requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetURI(middleInfo.SystemType);
                responseInfo = HandOut(requestInfo);
            }
            return responseInfo;
        }
        #endregion
    }
}
