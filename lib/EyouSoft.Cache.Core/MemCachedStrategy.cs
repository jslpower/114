using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Cache.Core
{
    // <summary>
    /// MemCached缓存策略类
    /// </summary>
    public class MemCachedStrategy : ICacheStrategy
    {
        #region MemCachedStrategy 成员
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        public void Add(string key, object value, Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority scavengingPriority, Microsoft.Practices.EnterpriseLibrary.Caching.ICacheItemRefreshAction refreshAction, params Microsoft.Practices.EnterpriseLibrary.Caching.ICacheItemExpiration[] expirations)
        {
            if (expirations[0] is Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime)
            {
                DateTime expire = ((Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime)expirations[0]).AbsoluteExpirationTime.ToLocalTime();
                MemCachedManager.CacheClient.Set(key, value, expire);
            } 
        }
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            MemCachedManager.CacheClient.Set(key, value);
        }
        /// <summary>
        /// 缓存对象是否存在
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return MemCachedManager.CacheClient.KeyExists(key);
        }

        public int Count
        {
            get { return -1; }
        }

        public void Flush()
        {
            return;
        }
        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            return MemCachedManager.CacheClient.Get(key);
        }
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        public void Remove(string key)
        {
            if (MemCachedManager.CacheClient.KeyExists(key))
                MemCachedManager.CacheClient.Delete(key);
        }

        public object this[string key]
        {
            get { return MemCachedManager.CacheClient.Get(key); }
        }

        #endregion
    }
}
