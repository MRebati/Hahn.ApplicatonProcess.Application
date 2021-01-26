using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Hahn.ApplicationProcess.December2020.Domain.Utilities
{
    public class CacheManagement : ICacheManagement
    {
        private readonly IMemoryCache _cache;
        public CacheManagement(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public T CacheCall<T>(object key, int timeoutInSeconds, T procedureMethod)
        {
            _cache.TryGetValue(key, out T cacheValue);

            if (cacheValue != null)
                return cacheValue;

            cacheValue = procedureMethod;
            SetSlidingValue(key, cacheValue, timeoutInSeconds);
            return cacheValue;
        }

        public T GetValue<T>(object key, T defaultValue = default(T))
        {
            if (_cache.TryGetValue(key, out T cacheValue))
                return cacheValue;

            return defaultValue;
        }

        public void SetValue<T>(object key, T value, int timeoutInSeconds)
        {
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(timeoutInSeconds));

            _cache.Set(key, value, cacheEntryOptions);
        }

        public void SetSlidingValue<T>(object key, T value, int timeoutInSeconds)
        {
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(timeoutInSeconds));

            _cache.Set(key, value, cacheEntryOptions);
        }

        public void Remove(object key)
        {
            _cache.Remove(key);
        }

        public void Remove(List<object> keys)
        {
            foreach (var key in keys)
                Remove(key);
        }

        public void ClearAllCaches()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            ICollection collection = field.GetValue(_cache) as ICollection;
            if (collection != null)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    string key = methodInfo.GetValue(item).ToString();
                    _cache.Remove(key);
                }
        }
    }
}
