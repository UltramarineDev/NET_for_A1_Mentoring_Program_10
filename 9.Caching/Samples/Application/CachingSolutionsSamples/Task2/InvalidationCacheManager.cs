using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public class InvalidationCacheManager<T> where T : class
    {
        private readonly IEntityCache<T> _entityCache;

        public InvalidationCacheManager(IEntityCache<T> entityCache)
        {
            _entityCache = entityCache;
        }

        public IEnumerable<T> GetEntities(string command)
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = _entityCache.Get(user);
            if (entities == null)
            {
                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                    var connectionString = dbContext.Database.Connection.ConnectionString;

                    _entityCache.Set(user, entities, GetPolicy(command, connectionString));
                }
            }

            return entities;
        }

        private CacheItemPolicy GetPolicy(string command, string connectionString)
        {
            var policy = new CacheItemPolicy();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand(command, connection))
                {
                    SqlDependency.Start(connectionString);
                    var dependency = new SqlDependency();
                    dependency.AddCommandDependency(sqlCommand);
                    connection.Open();

                    var monitor = new SqlChangeMonitor(dependency);
                    sqlCommand.ExecuteNonQuery();
                    policy.ChangeMonitors.Add(monitor);
                    return policy;
                }
            }
        }
    }
}
