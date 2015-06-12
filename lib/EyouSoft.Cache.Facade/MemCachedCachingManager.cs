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
    public class MemCachedCachingManager:ICacheManager,IDisposable
    {
        ICacheStrategy memCached = new MemCachedStrategy();

        public MemCachedCachingManager(){}
        public MemCachedCachingManager(System.Collections.Specialized.NameValueCollection c){}
        #region ICacheManager 成员

        public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            memCached.Add(key, value, scavengingPriority, refreshAction, expirations);
        }

        public void Add(string key, object value)
        {
            memCached.Add(key, value);
        }

        public bool Contains(string key)
        {
            return memCached.Contains(key);
        }

        public int Count
        {
            get { return memCached.Count; }
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public object GetData(string key)
        {
            return memCached.GetData(key);
        }

        public void Remove(string key)
        {
            if (memCached.Contains(key))
                memCached.Remove(key);
        }

        public object this[string key]
        {
            get { return memCached[key]; }
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
