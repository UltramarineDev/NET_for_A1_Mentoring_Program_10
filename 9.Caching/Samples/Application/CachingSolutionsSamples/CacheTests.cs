using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesMemoryCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

        [TestMethod]
        public void MemoryCache_CustomerEntity()
        {
            var entityManager = new EntitiesManager<Customer>(new EntityMemoryCache<Customer>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entityManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void MemoryCache_SupplierEntity()
        {
            var entityManager = new EntitiesManager<Supplier>(new EntityMemoryCache<Supplier>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entityManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void MemoryCache_InvalidationCache()
        {
            var command = "SELECT TerritoryID, TerritoryDescription, RegionID FROM [dbo].[Territories]";
            var entityManager = new InvalidationCacheManager<Territory>(new EntityMemoryCache<Territory>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(entityManager.GetEntities(command).Count());
                Thread.Sleep(100);
            }
        }
    }
}
