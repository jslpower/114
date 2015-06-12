using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using EyouSoft.Cache.Core;

namespace EyouSoft.Cache.Facade
{
    [ConfigurationElementTypeAttribute(typeof(CustomCacheManagerData))]
    public class DefaultCachingManager : ICacheManager, IDisposable
    {
        #region ICacheManager 成员
        ICacheStrategy cache = new DefaultCacheStrategy();

        public DefaultCachingManager(){}
        public DefaultCachingManager(System.Collections.Specialized.NameValueCollection c) { }

        public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            cache.Add(key, value, scavengingPriority, refreshAction, expirations);
        }

        public void Add(string key, object value)
        {
            cache.Add(key, value);
        }

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public int Count
        {
            get { return cache.Count; }
        }

        public void Flush()
        {
        }

        public object GetData(string key)
        {
            return cache.GetData(key);
        }

        public void Remove(string key)
        {
            if (cache.Contains(key))
                cache.Remove(key);
        }

        public object this[string key]
        {
            get { return cache[key]; }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
