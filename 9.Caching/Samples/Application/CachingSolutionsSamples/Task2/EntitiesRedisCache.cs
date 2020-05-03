using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public class EntitiesRedisCache<T> : IEntityCache<T>
    {
        private ConnectionMultiplexer redisConnection;
        string prefix = "Cache_Entities";
        DataContractSerializer serializer = new DataContractSerializer(typeof(IEnumerable<T>));

        public EntitiesRedisCache(string hostName)
        {
            redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<T> Get(string forUser)
        {
            var db = redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<T>)serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<T> entities)
        {
            var db = redisConnection.GetDatabase();
            var key = prefix + forUser;

            if (entities == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, entities);
                db.StringSet(key, stream.ToArray());
            }
        }

        public void Set(string forUser, IEnumerable<T> entities, CacheItemPolicy policy)
        {
            throw new NotImplementedException();
        }
    }
}
