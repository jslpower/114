using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EyouSoft.SSOComponent.Entity;

namespace EyouSoft.ControlCommon.Control
{
    public class BaseAshx
    {
        protected HttpRequest request;
        protected HttpResponse response;

        private bool isLogin = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return isLogin;
            }
        }

        private bool _IsCompanyCheck = false;
        /// <summary>
        /// 公司是否通过审核
        /// </summary>
        public bool IsCompanyCheck
        {
            get
            {
                return _IsCompanyCheck;
            }
        }

        private UserInfo _userInfo = null;

        public UserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
        }

        #region IHttpHandler 成员

        protected void BaseInit(HttpContext context)
        {
            request = context.Request;
            response = context.Response;

            UserInfo userInfo = null;
            isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out userInfo);
            if (userInfo != null)
            {
                _userInfo = userInfo;
                _IsCompanyCheck = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(_userInfo.CompanyID).IsCheck;
            }
        }

        #endregion
    }
}
