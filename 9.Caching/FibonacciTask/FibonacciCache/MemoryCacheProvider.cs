using System;
using System.Runtime.Caching;

namespace FibonacciCache
{
    public class MemoryCacheProvider : IStorage
    {
        private readonly ObjectCache _cache;

        public MemoryCacheProvider()
        {
            _cache = MemoryCache.Default;
        }

        public int? GetValueOrNull(int key)
            => (int?)_cache.Get(key.ToString());
        
        public void AddOrUpdate(int key, int value)
        {
            _cache.Set(key.ToString(), value, DateTimeOffset.UtcNow.AddSeconds(5));
        }
    }
}
