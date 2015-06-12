using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace EyouSoft.Cache.Core
{
    /// <summary>
    /// 默认缓存策略类

    /// </summary>
    public class DefaultCacheStrategy : ICacheStrategy
    {
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;

        #region ICacheManager 成员
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        public virtual void Add(string key, object value, Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority scavengingPriority, Microsoft.Practices.EnterpriseLibrary.Caching.ICacheItemRefreshAction refreshAction, params Microsoft.Practices.EnterpriseLibrary.Caching.ICacheItemExpiration[] expirations)
        {            
            if (expirations[0] is Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime)
            {
                DateTime expire = ((Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime)expirations[0]).AbsoluteExpirationTime.ToLocalTime();                
                webCache.Insert(key, value, null, expire, System.Web.Caching.Cache.NoSlidingExpiration);
            }
            if (expirations[0] is Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.SlidingTime)
            {
                TimeSpan expire = ((Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.SlidingTime)expirations[0]).ItemSlidingExpiration;
                webCache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expire);
            }                        
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        public virtual void Add(string key, object value)
        {
            if (key == null || key.Length == 0 || value == null)
            {
                return;
            } 
            webCache.Insert(key, value);
        }
        /// <summary>
        /// 缓存对象是否存在
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <returns></returns>
        public virtual bool Contains(string key)
        {            
            return webCache.Get(key) != null;
        }

        public virtual int Count
        {
            get { return webCache.Count; }
        }

        public virtual void Flush()
        {
            return;
        }
        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="key">对象的关键字</param>
        /// <returns>对象</returns>
        public virtual object GetData(string key)
        {
            if (key == null || key.Length == 0)
            {
                return null;
            }
            return webCache.Get(key);
        }
        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="key">对象的关键字</param>
        public virtual void Remove(string key)
        {
            if (key == null || key.Length == 0)
            {
                return;
            }
            webCache.Remove(key);
        }

        public virtual object this[string key]
        {
            get { return webCache[key]; }
        }

        #endregion
    }
}
