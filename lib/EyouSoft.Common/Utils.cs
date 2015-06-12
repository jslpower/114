using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace EyouSoft.Common
{
    using System.Text.RegularExpressions;

    public class Utility
    {
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        /// <summary>
        /// 取得客户的IP数据
        /// </summary>
        /// <returns>客户的IP</returns>
        public static string GetRemoteIP()
        {
            string Remote_IP = "";
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    Remote_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    Remote_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                } 
            }
            catch
            {
            }
            return Remote_IP;
        }
        /// <summary>
        /// 获取当前页面地址
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl()
        {
            string RequestUrl = "";
            try
            {
                if (HttpContext.Current.Request.Url != null)
                {
                    RequestUrl = HttpContext.Current.Request.Url.ToString();
                }
            }
            catch
            {
            }
            return RequestUrl;
        }
        /// <summary>
        /// 将127.0.0.1 形式的IP地址转换成10进制整数，这里没有进行任何错误处理
        /// </summary>
        /// <param name="strIP">IP地址转换</param>
        /// <returns>返回0进制整数</returns>
        public static long IpToLong(string strIP)
        {
            if (string.IsNullOrEmpty(strIP))
                return 0;

            string[] strIPs = strIP.Trim().Split('.');
            if (strIPs.Length != 4)
                return 0;

            long[] ip = new long[4];
            for (int i = 0; i < strIPs.Length; i++)
            {
                ip[i] = long.Parse(strIPs[i]);
            }

            return ip[0] * 256 * 256 * 256 + ip[1] * 256 * 256 + ip[2] * 256 + ip[3];
        }

        /// <summary>
        /// 替换XML敏感字符
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns></returns>
        public static string ReplaceXmlSpecialCharacter(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
            }

            return s;
        }

        /// <summary>
        /// 将字符串转化为数字 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            int result = 0;
            bool b = Int32.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为数字 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        /// <summary>
        /// 将字符串转化为decimal 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            decimal result = 0;
            bool b = decimal.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为decimal 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            return GetDecimal(key, 0);
        }

        /// <summary>
        /// 将字符串转化为double 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(string key, double defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            double result = 0;
            bool b = double.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为double 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(string key)
        {
            return GetDouble(key, 0);
        }

        /// <summary>
        /// 将格式为yyyyMMdd(如：2010-12-05)的字符串转换成日期格式 若不能转换成日期将返回defaultValue
        /// </summary>
        /// <param name="s">要转换的字符串 格式(yyyyMMdd 如：20101205)</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime112(string s, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            DateTime result = defaultValue;
            bool b = DateTime.TryParse(s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2), out result);

            return result;
        }

        /// <summary>
        /// 将格式为yyyyMMdd(如：2010-12-05)的字符串转换成日期格式 若不能转换成日期将返回DateTime.MinValue
        /// </summary>
        /// <param name="s">要转换的字符串 格式(yyyyMMdd 如：20101205)</param>
        /// <returns></returns>
        public static DateTime GetDateTime112(string s)
        {
            return GetDateTime112(s, DateTime.MinValue);
        }

        /// <summary>
        /// 将字符串转换成日期格式 若不能转换成日期将返回defaultValue
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            DateTime result = defaultValue;
            bool b = DateTime.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转换成日期格式 若不能转换成日期将返回DateTime.MinValue
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s)
        {
            return GetDateTime(s, DateTime.MinValue);
        }

        /// <summary>
        /// 传的like key参数经过toSqlLike格式化
        /// </summary>
        /// <param name="s">匹配字符串</param>
        /// <returns>格式化字符串</returns>
        public static string ToSqlLike(string s)
        {
            return string.IsNullOrEmpty(s) ? s : ((new Regex(@"(\[|\]|\*|_|%)")).Replace(s, "[$1]").Replace("'", "''"));
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="s">号码</param>
        /// <returns></returns>
        public static bool IsMobile(string s)
        {
            return new System.Text.RegularExpressions.Regex(@"^(13[0-9]|14[5,7]|15[0-9]|18[0|2|3|5|6|7|8|9])\d{8}$").IsMatch(s);
        }


        #region XElement
        /// <summary>
        /// Get XAttribute Value
        /// </summary>
        /// <param name="XAttribute">xAttribute</param>
        /// <returns>Value</returns>
        public static string GetXAttributeValue(XAttribute xAttribute)
        {
            if (xAttribute == null)
                return string.Empty;

            return xAttribute.Value;
        }

        /// <summary>
        /// Get XAttribute Value
        /// </summary>
        /// <param name="xElement">XElement</param>
        /// <param name="attributeName">Attribute Name</param>
        /// <returns></returns>
        public static string GetXAttributeValue(XElement xElement, string attributeName)
        {
            return GetXAttributeValue(xElement.Attribute(attributeName));
        }

        /// <summary>
        /// Get XElement
        /// </summary>
        /// <param name="xElement">parent xElement</param>
        /// <param name="xName">xName</param>
        /// <returns>XElement</returns>
        public static XElement GetXElement(XElement xElement, string xName)
        {
            XElement x = xElement.Element(xName);

            if (x != null) return x;

            return new XElement(xName);
        }

        /// <summary>
        /// Get XElements
        /// </summary>
        /// <param name="xElement">parent xElement</param>
        /// <param name="xName">xName</param>
        /// <returns>XElements</returns>
        public static IEnumerable<XElement> GetXElements(XElement xElement, string xName)
        {
            var x = xElement.Elements(xName);
            if (x == null)
                return new List<XElement>();

            return x;
        }

        /// <summary>
        /// 单字符0,1转换成bool
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool GetStringToBool(string val)
        {
            bool result = false;
            if (val.Equals("0"))
                result = false;
            else if(val.Equals("1"))
                result = true;
            return result;
        }
        /// <summary>
        /// bool转换成字符串0,1
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetBoolToString(bool result)
        {
            if (result)
                return "1";
            else
                return "0";
        }
        #endregion

    }
}
