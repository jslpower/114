using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// URL重写 实用程序，张新兵，2010-12-14
    /// </summary>
    public class UrlReWriteUtils
    {
        #region 处理分页的Url 重写
        //判断是否是查询以后的URL
        public static bool IsReWriteUrl(System.Web.HttpRequest request)
        {
            if (request.RawUrl.ToUpper().IndexOf("&PAGE=") < 0 && request.RawUrl.ToUpper().IndexOf("ASPX") < 0)
            {
                return true;
            }
            return false;
        }
        //处理分页的URL 
        public static string GetUrlForPage(System.Web.HttpRequest request)
        {
            //如果是分页以后的链接
            if (request.RawUrl.ToUpper().IndexOf("ASPX") < 0)
            {
                //判断是否是查询以后的链接
                if (request.Url.ToString().ToUpper().IndexOf("PAGE=") >= 0)
                {
                    string newUrl = request.RawUrl;
                    //如果是分页以后的，那么进行截取
                    newUrl = newUrl.Substring(0, newUrl.LastIndexOf('_'));
                    return newUrl;
                }
                else
                {
                    return request.RawUrl;
                }
            }
            //分页之前的链接
            else
            {
                return request.Url.ToString();
            }
        }
        #endregion
    }
}
