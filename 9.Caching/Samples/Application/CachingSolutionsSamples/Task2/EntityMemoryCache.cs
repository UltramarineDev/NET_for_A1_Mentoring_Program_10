using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public class EntityMemoryCache<T> : IEntityCache<T> where T : class
    {
        ObjectCache _cache = MemoryCache.Default;
        string prefix = "Cache_Entities";

        public IEnumerable<T> Get(string forUser)
        {
            return (IEnumerable<T>)_cache.Get(prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<T> entities)
        {
            _cache.Set(prefix + forUser, entities, ObjectCache.InfiniteAbsoluteExpiration);
        }

        public void Set(string forUser, IEnumerable<T> entities, CacheItemPolicy policy)
        {
            _cache.Set(prefix + forUser, entities, policy);
        }
    }
}
