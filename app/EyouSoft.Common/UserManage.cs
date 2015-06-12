//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using EyouSoft.Common.DataProtection;


//namespace EyouSoft.Common
//{
//    /// <summary>
//    /// 用户身份验证(使用cookie实现单点登录)
//    /// </summary>
//    public class UserManage
//    {
//        private static object synchObject = new object();
//        //定期更新用于加密SSO票据的密匙和初始化向量.
//        private static string _DesKey = "12$#@!#@5trfdewsfr54321234edw%$#";
//        private static string _DesIV = "!54~14874&%@+-)#";
//        //private static string DesKey
//        //{
//        //    get
//        //    {
//        //        return null;   
//        //    }
//        //}
//        //private static string DesIV
//        //{
//        //    get
//        //    {
//        //        return null;
//        //    }
//        //}
//        private static HashCrypto createHashCrypto()
//        {
//            HashCrypto _hash = new HashCrypto();
//            _hash.Key = _DesKey;
//            _hash.IV = _DesIV;
//            return _hash;
//        }

//        //生成SSO票据
//        public static string GenerateSSOToken(string username)
//        {
//            //时间戳,密匙,UserName=新字符串;
//            string date = DateTime.Now.AddMinutes(ExpiresMinutes).ToString("yyyy-MM-dd HH:mm:ss");
//            string token = string.Format("{0};{1};{2}",date,KEY,username);
//            //SSO票据=DES加密(新字符串);
//            token = createHashCrypto().DESEncrypt(token);
//            return token;
//        }

//        //SSO票据是否有效
//        public static bool IsSSOTokenValid(string ssoToken, string username)
//        {
//            //字符串=DES解密(SSO票据)
//            string token = createHashCrypto().DeDESEncrypt(ssoToken);
//            string[] arr = token.Split(';');
//            if (arr.Length == 3)
//            {
//                DateTime date;
//                string user=arr[2], key=arr[1];
//                if (DateTime.TryParse(arr[0],out date)
//                    && user.Equals(username,StringComparison.Ordinal)
//                    && key==KEY)
//                {
//                    //时间戳在有效时间内。
//                    if (date > DateTime.Now)
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                return false;
//            }
//        }
        

//        /// <summary>
//        /// 用户登录COOKIES NAME
//        /// </summary>
//        public const string LoginCookie_SSO = "TYPT_SSO";
//        public const string LoginCookie_UserName = "TYPE_U";
//        /// <summary>
//        /// 登录key有效时间量
//        /// </summary>
//        private const int ExpiresMinutes = 500;
//        /// <summary>
//        /// 登录页面URL
//        /// </summary>
//        public static string Url_Login
//        {
//            get
//            {
//                return Domain.UserPublicCenter + "/Register/Login.aspx";
//            }
//        }
//        private const string KEY = "EyouSoft.Common.UserManage";

//        #region COOKIES
//        /// <summary>
//        /// 设置用户登录COOKIES
//        /// </summary>
//        /// <param name="model"></param>
//        public static void GenerateUserLoginCookies(string ssotoken, string username)
//        {
//            ssotoken = HttpUtility.UrlEncode(ssotoken, System.Text.Encoding.UTF8);
//            string domainSuffix = Utils.GetDomainSuffix(Utils.AbsoluteWebRoot);

//            HttpResponse response = HttpContext.Current.Response;

//            //移除COOKIES
//            response.Cookies.Remove(LoginCookie_SSO);
//            response.Cookies.Remove(LoginCookie_UserName);
//            response.Cookies[LoginCookie_SSO].Expires = DateTime.Now.AddDays(-1);
//            if (!String.IsNullOrEmpty(domainSuffix))
//            {
//                response.Cookies[LoginCookie_SSO].Domain = domainSuffix;
//            }
//            response.Cookies[LoginCookie_UserName].Expires = DateTime.Now.AddDays(-1);
//            if (!String.IsNullOrEmpty(domainSuffix))
//            {
//                response.Cookies[LoginCookie_UserName].Domain = domainSuffix;
//            }

//            DateTime expiresDate = DateTime.Now.AddMinutes(ExpiresMinutes);
//            //添加新的Cookies
//            System.Web.HttpCookie cookies = new HttpCookie(LoginCookie_SSO);
//            cookies.Value = ssotoken;
//            if (!String.IsNullOrEmpty(domainSuffix))
//            {
//                cookies.Domain = domainSuffix;
//            }
//            cookies.HttpOnly = true;
//            cookies.Expires = expiresDate;
//            HttpContext.Current.Response.AppendCookie(cookies);

//            cookies = new HttpCookie(LoginCookie_UserName);
//            cookies.Value = username;
//            if (!String.IsNullOrEmpty(domainSuffix))
//            {
//                cookies.Domain = domainSuffix;
//            }
//            cookies.HttpOnly = true;
//            cookies.Expires = expiresDate;
//            HttpContext.Current.Response.AppendCookie(cookies);
//        }
//        public static string GetCookie_SSO()
//        {
//            HttpRequest request = HttpContext.Current.Request;
//            if (request.Cookies[LoginCookie_SSO] == null)
//            {
//                return string.Empty;
//            }
//            else
//            {
//                return HttpUtility.UrlDecode(request.Cookies[LoginCookie_SSO].Value, System.Text.Encoding.UTF8);
//            }
//        }
//        public static string GetCookie_U()
//        {
//            HttpRequest request = HttpContext.Current.Request;
//            if (request.Cookies[LoginCookie_UserName] == null)
//            {
//                return string.Empty;
//            }
//            else
//            {
//                return request.Cookies[LoginCookie_UserName].Value;
//            }
//        }
//        #endregion COOKIES

//        #region 判断用户是否登录
//        /// <summary>
//        /// 判断用户是否登录
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsUserLogin()
//        {
//            HttpRequest request = HttpContext.Current.Request;
//            if (request.Cookies[LoginCookie_SSO] == null || request.Cookies[LoginCookie_UserName] == null)
//            {
//                return false;
//            }
//            string sso = request.Cookies[LoginCookie_SSO].Value;
//            string username = request.Cookies[LoginCookie_UserName].Value;

//            if (String.IsNullOrEmpty(sso) || String.IsNullOrEmpty(username))
//            {
//                return false;
//            }

//            sso = HttpUtility.UrlDecode(sso, System.Text.Encoding.UTF8);

//            bool isValid = false;
//            try
//            {
//                isValid = IsSSOTokenValid(sso, username);
//            }
//            catch (System.Exception e)
//            {
//                isValid = false;
//            }

//            return isValid;
//        }
//        #endregion 判断用户是否登录

//        #region 用户登录
//        /// <summary>
//        /// 用户登录 
//        /// </summary>
//        /// <param name="userName">用户名</param>
//        /// <param name="password">用户密码</param>
//        /// <returns>1：成功，0：用户不存在或密码错误,2:用户被后台管理员锁定</returns>
//        //public static int UserLogin(string userName, string password)
//        //{
//        //    UserCarBLL.WebUser bll = new UserCarBLL.WebUser();
//        //    UserCarModel.WebUser model = new UserCarModel.WebUser();

//        //    model = bll.GetModel(userName, password);

//        //    if (model != null && model.Status == (int)WebUserStatus.正常)
//        //    {
//        //        if (SiteMemberLogin.ToLower() == "session")
//        //        {
//        //            //设置用户登录SESSION
//        //            SetUserLoginSession(model);
//        //        }
//        //        else
//        //        {
//        //            SetUserLoginCookies(model);
//        //        }

//        //        model = null;
//        //        bll = null;

//        //        return 1;
//        //    }
//        //    else if (model != null && model.Status == (int)WebUserStatus.锁定)
//        //    {
//        //        return 2;
//        //    }
//        //    else
//        //    {
//        //        return 0;
//        //    }

//        //}
//        #endregion 用户登录

//        #region 用户退出
//        /// <summary>
//        /// 退出当前用户，并跳转到登录页面
//        /// </summary>
//        public static void Logout()
//        {
//            Logout(string.Empty);
//        }
//        /// <summary>
//        /// 退出当前用户，并跳转到指定的页面。
//        /// </summary>
//        /// <param name="url">指定退出后要跳转的页面地址</param>
//        public static void Logout(string url)
//        {
//            string domainSuffix = Utils.GetDomainSuffix(Utils.AbsoluteWebRoot);

//            System.Web.HttpCookie cookies = new HttpCookie(LoginCookie_SSO);
//            if (!String.IsNullOrEmpty(domainSuffix))
//            {
//                cookies.Domain = domainSuffix;
//            }
//            cookies.HttpOnly = true;
//            cookies.Expires = DateTime.Now.AddDays(-1) ;
//            HttpContext.Current.Response.AppendCookie(cookies);

//            if (string.IsNullOrEmpty(url))
//            {
//                RedirectLogin();
//            }
//            else
//            {
//                HttpContext.Current.Response.Redirect(url, true);
//            }
//        }
//        #endregion 用户退出

//        #region 跳转到登录页面
//        /// <summary>
//        /// 跳转到登录页面
//        /// </summary>
//        public static void RedirectLogin()
//        {
//            HttpContext.Current.Response.Redirect(Url_Login, true);
//            //string script = "<script>window.top.location.href ='{0}';</script> ";
//            //HttpContext.Current.Response.Write(string.Format(script, LoginUrl));
//            //HttpContext.Current.Server.Transfer(Url_Login, false);
//        }
//        /// <summary>
//        /// 跳转到登录页面，并指定登录成功后要跳转的页面地址，及在登录页面要显示的信息
//        /// </summary>
//        /// <param name="url"></param>
//        /// <param name="message"></param>
//        public static void RedirectLogin(string url, string message)
//        {
//            if (!string.IsNullOrEmpty(message))
//            {
//                HttpContext.Current.Response.Redirect(Url_Login + "?returnurl=" + HttpContext.Current.Server.UrlEncode(url) +
//                    "&nlm=" + HttpContext.Current.Server.UrlEncode(message));
//            }
//            else
//            {
//                RedirectLogin(url);
//            }
//        }
//        /// <summary>
//        /// 跳转到登录页面，并指定登录成功后要跳转的页面地址
//        /// </summary>
//        /// <param name="url">指定登录成功后要跳转的页面地址</param>
//        public static void RedirectLogin(string url)
//        {
//            HttpContext.Current.Response.Redirect(Url_Login + "?returnurl=" + HttpContext.Current.Server.UrlEncode(url));
//        }
//        /// <summary>
//        /// 跳转到登录页面，并指定要显示的信息。
//        /// </summary>
//        /// <param name="message"></param>
//        public static void RedirectLoginAndShowMsg(string message)
//        {
//            if (!string.IsNullOrEmpty(message))
//            {
//                HttpContext.Current.Response.Redirect(Url_Login + "?nlm=" + HttpContext.Current.Server.UrlEncode(message));
//            }
//            else
//            {
//                RedirectLogin();
//            }
//        }

       
//        /// <summary>
//        /// 跳转登录页面，在最顶端页面显示登录页
//        /// </summary>
//        public static void RedirectLoginOpenTopPage()
//        {
//            string script = "<script>window.top.location.href ='{0}';</script> ";
//            HttpContext.Current.Response.Write(string.Format(script, Url_Login));            
//        }
//         /// <summary>
//        /// 跳转到登录页面，在最顶端页面显示登录页并指定登录成功后要跳转的页面地址
//        /// </summary>
//        /// <param name="url">指定登录成功后要跳转的页面地址</param>
//        public static void RedirectLoginOpenTopPage(string url)
//        {
//            string script = "<script>window.top.location.href ='{0}';</script> ";
//            HttpContext.Current.Response.Write(string.Format(script, Url_Login + "?returnurl=" + HttpContext.Current.Server.UrlEncode(url)));            
//        }
//        /// <summary>
//        /// 跳转到登录页面，在最顶端页面显示登录页并指定登录成功后要跳转的页面地址，及在登录页面要显示的信息
//        /// </summary>
//        /// <param name="url"></param>
//        /// <param name="message"></param>
//        public static void RedirectLoginOpenTopPage(string url, string message)
//        {
//            string script = "<script>window.top.location.href ='{0}';</script> ";
//            if (!string.IsNullOrEmpty(message))
//            {
//                HttpContext.Current.Response.Write(Url_Login + "?returnurl=" + HttpContext.Current.Server.UrlEncode(url) +
//                    "&nlm=" + HttpContext.Current.Server.UrlEncode(message));
//            }
//            else
//            {
//                RedirectLoginOpenTopPage(url);
//            }
//        }
//        #endregion 跳转到登录页面

//        #region 取当前登录用户信息
//        /// <summary>
//        /// 取当前登录用户信息 
//        /// </summary>
//        /// <returns></returns>
//        //public static UserCarModel.WebUser GetLoginUserInfo()
//        //{
//        //    if (SiteMemberLogin.ToLower() == "session")
//        //    {
//        //        //获取用户登录SESSION
//        //        return GetUserLoginSession();
//        //    }
//        //    else
//        //    {
//        //        //获取用户登录COOKIES
//        //        UserCarModel.WebUser model = GetUserLoginCookies();
//        //        if (model != null)
//        //        {
//        //            model = new UserCarBLL.WebUser().GetModel(model.ID);
//        //        }
//        //        return model;
//        //    }
//        //}
//        #endregion
//    }
//}
