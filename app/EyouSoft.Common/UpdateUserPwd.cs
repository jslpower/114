using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EyouSoft.Common
{
    public class UpdateUserPwd
    {
        //定期更新用于加密SSO票据的密匙和初始化向量.
        private static string _DesKey = "12$#@!#@5trfdewsfr54321234edw%$#";
        private static string _DesIV = "!54~14874&%@+-)#";

        /// <summary>
        /// 加解密对象
        /// </summary>
        static EyouSoft.Common.DataProtection.HashCrypto _hash = new EyouSoft.Common.DataProtection.HashCrypto();

        private const string KEY = "EyouSoft.Common.UpdateUserPwd";

        static UpdateUserPwd()
        {
            _hash.Key = _DesKey;
            _hash.IV = _DesIV;
        }

        /// <summary>
        /// 根据指定的用户名生成密码重置字符串
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static string GenerateToken(string username)
        {
            //时间戳,密匙,UserName=新字符串;
            string date = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
            string token = string.Format("{0};{1};{2}", date, KEY, username);
            //SSO票据=DES加密(新字符串);
            token = _hash.DESEncrypt(token);
            return token;
        }

        /// <summary>
        ///密码重置字符串是否有效
        /// </summary>
        /// <param name="ssoToken">密码重置字符串</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static bool IsTokenValid(string token, string username,out int errorCode)
        {
            errorCode = 0;
            //字符串=DES解密(SSO票据)
            token = _hash.DeDESEncrypt(token);
            string[] arr = token.Split(';');
            if (arr.Length == 3)
            {
                DateTime date;
                string user = arr[2], key = arr[1];
                if (DateTime.TryParse(arr[0], out date)
                    && user.Equals(username, StringComparison.Ordinal)
                    && key == KEY)
                {
                    //时间戳在有效时间内。
                    if (date > DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        errorCode = 2;
                        return false;
                    }
                }
                else
                {
                    errorCode = 1;
                    return false;
                }
            }
            else
            {
                errorCode = 3;
                return false;
            }
        }
        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string EncodeToken(string token)
        {
            try
            {
                return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(token));
            }
            catch (System.Exception e)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 解码字符串 
        /// </summary>
        /// <param name="encodeedToken"></param>
        /// <returns></returns>
        public static string DecodeToken(string encodeedToken)
        {
            try
            {
                return System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(encodeedToken));
            }
            catch (System.Exception e)
            {
                return string.Empty;
            }
        }
    }
}
