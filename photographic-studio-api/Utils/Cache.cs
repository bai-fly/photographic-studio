using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{
    public class Cache
    {
        static int cacheMinutes = 10;

        private static object obj = new object();

        private static Cache instance = null;
        private static IMemoryCache cache = null;
        readonly MemoryCacheEntryOptions _options;
        private Cache()
        {
            cache = IOC.GetService<IMemoryCache>();
            _options = Cache.GetMemoryCacheEntryOptions();
        }

        public static Cache GetInstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new Cache();

                    }
                }
            }
            return instance;

        }
        public static MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
        {
            var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheMinutes)).SetSlidingExpiration(TimeSpan.FromMinutes(cacheMinutes));

            return options;
        }

        public void Set<T>(string key, T value)
        {
            cache.Set(key, value, _options);
        }
        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }
    }
}
