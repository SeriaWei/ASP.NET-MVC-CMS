using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Easy.Extend;

namespace Easy.Cache
{
    public class StaticCache
    {
        public class CacheObject
        {
            public bool AutoRemove { get; set; }
            public DateTime LastVisit { get; set; }
            readonly object _obj;
            public CacheObject(object obj, bool autoRemove)
            {
                this._obj = obj;
                LastVisit = DateTime.Now;
                this.AutoRemove = autoRemove;
            }
            public object Get()
            {
                LastVisit = DateTime.Now;
                if (this._obj is ICloneable)
                {
                    return (this._obj as ICloneable).Clone();
                }
                return this._obj;
            }
        }

        internal static Dictionary<string, CacheObject> Cache;
        private static Task _timer;

        static StaticCache()
        {
            Cache = new Dictionary<string, CacheObject>();
            _timer = new Task(() =>
            {
                while (true)
                {
                    Thread.Sleep(new TimeSpan(0, 20, 1));
                    lock (Cache)
                    {
                        var needRemove = new List<string>();
                        Cache.Each(item =>
                        {
                            if (item.Value.AutoRemove && (DateTime.Now - item.Value.LastVisit).TotalMinutes > 20)
                            {
                                needRemove.Add(item.Key);
                            }
                        });
                        needRemove.Each(item => Cache.Remove(item));
                    }
                }
            });
            _timer.Start();
        }


        public T Get<T>(string key, Func<Signal, T> source)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(key))
                {
                    return (T)Cache[key].Get();
                }
                else
                {
                    var signal = new Signal(key);
                    T result = source.Invoke(signal);
                    Cache.Add(key, new CacheObject(result, signal.AutoRemove));
                    return result;
                }
            }
        }

        public void Remove(string key)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(key))
                {
                    Cache[key] = null;
                    Cache.Remove(key);
                }
            }
        }

        public static int Count
        {
            get { return Cache.Keys.Count; }
        }
        public void Clear()
        {
            lock (Cache)
            {
                Cache.Clear();
            }
        }
    }

    public class Signal
    {
        private static readonly Dictionary<string, List<string>> SignalRela;
        public string CacheKey { get; private set; }

        public Signal()
        {

        }

        public Signal(string cacheKey)
        {
            this.CacheKey = cacheKey;
        }

        static Signal()
        {
            SignalRela = new Dictionary<string, List<string>>();
        }
        public bool AutoRemove { get; set; }
        public void When(string signal)
        {
            lock (SignalRela)
            {
                if (SignalRela.ContainsKey(signal))
                {
                    List<string> cacheKeys = SignalRela[signal];
                    if (!cacheKeys.Contains(CacheKey))
                    {
                        cacheKeys.Add(CacheKey);
                    }
                }
                else
                {
                    SignalRela.Add(signal, new List<string> { CacheKey });
                }
            }
        }

        public void Trigger(string signal)
        {
            lock (SignalRela)
            {
                if (SignalRela.ContainsKey(signal))
                {
                    lock (StaticCache.Cache)
                    {
                        List<string> cacheKeys = SignalRela[signal];
                        cacheKeys.Each(m =>
                        {
                            if (StaticCache.Cache.ContainsKey(m))
                            {
                                StaticCache.Cache[m] = null;
                                StaticCache.Cache.Remove(m);
                            }

                        });
                    }
                }
            }
        }
    }
}
