using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NorthwindLibrary;


namespace CachingSolutionsSamples
{
    public class EntitiesManager<T> where T: class
    {
        private readonly IEntityCache<T> _entityCache;

        public EntitiesManager(IEntityCache<T> entityCache)
        {
            _entityCache = entityCache;
        }

        public IEnumerable<T> GetEntities()
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = _entityCache.Get(user);

            if (entities == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                    _entityCache.Set(user, entities);
                }
            }

            return entities;
        }
    }
}
