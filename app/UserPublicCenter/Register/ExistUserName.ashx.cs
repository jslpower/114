using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace UserPublicCenter.Register
{
    /// <summary>
    /// 注册时检查用户名(邮箱)是否存在
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ExistUserName : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strUserName = EyouSoft.Common.Utils.InputText(context.Request.QueryString["UserName"]);
            string strEmail = EyouSoft.Common.Utils.InputText(context.Request.QueryString["Email"]);
            if (!string.IsNullOrEmpty(context.Request.QueryString["isEmail"]))
            {
                context.Response.Write(IsExistEmail(strEmail));
                context.Response.End();
            }
            else
            {
                context.Response.Write(IsExist(strUserName));
                context.Response.End();
            }

        }
        /// <summary>
        /// 用户名是否存在
        /// </summary>
        private bool IsExist(string UserName)
        {
            bool isTrue =false;
            isTrue = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExists(UserName);
            return isTrue;
        }
        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        private bool IsExistEmail(string Email)
        {
            bool isTrue = false;
            isTrue = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExistsEmail(Email);
            return isTrue;
        }

        #region IHttpHandler 成员
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion IHttpHandler 成员
    }
}
