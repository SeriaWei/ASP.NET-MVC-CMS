using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace Easy.Web.Cache
{
    public class ExMemoryCache
    {
        public delegate void RemovedEventHandler(CacheEntryRemovedArguments arguments);
        public event RemovedEventHandler RemovedEvent;
        private static MemoryCache cache;
        CacheItemPriority priority;
        TimeSpan time;
        static ExMemoryCache()
        {
            cache = new MemoryCache("ExMemoryCache");
        }
        public ExMemoryCache()
        {
            priority = CacheItemPriority.Default;
            time = new TimeSpan(0, 30, 0);
        }

        public ExMemoryCache(CacheItemPriority priority)
        {
            this.priority = priority;
            time = new TimeSpan(0, 30, 0);
        }

        public ExMemoryCache(CacheItemPriority priority, TimeSpan time)
        {
            this.priority = priority;
            this.time = time;
        }

        public void Add(string key, object obj)
        {
            var policy = new CacheItemPolicy();
            policy.Priority = priority;
            policy.SlidingExpiration = time;
            policy.RemovedCallback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback);
            cache.Set(key, obj, policy);
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        private void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            if (this.RemovedEvent != null)
            {
                this.RemovedEvent(arguments);
            }
        }
    }

    public class ExMemoryCache<T> : ExMemoryCache where T : class
    {
        public ExMemoryCache()
            : base() { }
        public ExMemoryCache(CacheItemPriority priority)
            : base(priority) { }
        public ExMemoryCache(CacheItemPriority priority, TimeSpan time)
            : base(priority, time) { }
        public void Add(T obj)
        {
            base.Add(typeof(T).FullName, obj);
        }
        public void Add(string key, T obj)
        {
            base.Add(typeof(T).FullName + "_" + key, obj);
        }
        public T Get()
        {
            return base.Get(typeof(T).FullName) as T;
        }
        public new T Get(string key)
        {
            return base.Get(typeof(T).FullName + "_" + key) as T;
        }
    }

}
