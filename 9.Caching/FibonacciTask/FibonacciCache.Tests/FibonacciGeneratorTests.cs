using NUnit.Framework;

namespace FibonacciCache.Tests
{
    public class FibonacciGeneratorTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(8, 21)]
        public void Generate_RuntimeCaching(int position, int expected)
        {
            var generator = new FibonacciGenerator(new MemoryCacheProvider());
            var actual = generator.Generate(position);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(8, 21)]
        public void Generate_RedisCache(int position, int expected)
        {
            var generator = new FibonacciGenerator(new RedisCacheProvider());
            var actual = generator.Generate(position);
            Assert.AreEqual(expected, actual);
        }
    }
}
