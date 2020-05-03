using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public interface IEntityCache<T>
    {
        IEnumerable<T> Get(string forUser);
        void Set(string forUser, IEnumerable<T> entities);
        void Set(string forUser, IEnumerable<T> entities, CacheItemPolicy policy);
    }
}
