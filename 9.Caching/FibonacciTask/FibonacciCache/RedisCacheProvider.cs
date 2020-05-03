using System.IO;
using StackExchange.Redis;
using System.Runtime.Serialization;

namespace FibonacciCache
{
    public class RedisCacheProvider : IStorage
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly DataContractSerializer _serializer;

        public RedisCacheProvider()
        {
            ConfigurationOptions cfg = new ConfigurationOptions()
            {
                EndPoints = { "localhost" },
                AbortOnConnectFail = false
            };

            _connection = ConnectionMultiplexer.Connect(cfg);
            _serializer = new DataContractSerializer(typeof(int));
        }

        public void AddOrUpdate(int key, int value)
        {
            var database = _connection.GetDatabase();
            var stream = new MemoryStream();
            _serializer.WriteObject(stream, value);
            database.StringSet(key.ToString(), stream.ToArray());
        }

        public int? GetValueOrNull(int key)
        {
            var database = _connection.GetDatabase();
            byte[] redisValue = database.StringGet(key.ToString());
            if (redisValue == null)
            {
                return null;
            }

            return (int?)_serializer.ReadObject(new MemoryStream(redisValue));
        }
    }
}
