using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace EyouSoft.Component.Factory
{
    /// <summary>
    /// DataAccess 的摘要说明。

    /// </summary>
    public sealed class DataAccess
    {        
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string path, string CacheKey)
        {
            object objType = GetCache(CacheKey);//从缓存读取

            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(CacheKey);//反射创建
                    SetCache(CacheKey, objType);// 写入缓存
                }
                catch
                { }
            }
            return objType;
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        private static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        private static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }
    }
}
