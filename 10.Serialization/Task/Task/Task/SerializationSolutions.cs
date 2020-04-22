using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		Northwind dbContext;

		[TestInitialize]
		public void Initialize()
		{
			dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
			var categories = dbContext.Categories.ToList();

			var c = categories.First();

			tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
			var products = dbContext.Products.ToList();

			tester.SerializeAndDeserialize(products);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
			var orderDetails = dbContext.Order_Details.ToList();

			tester.SerializeAndDeserialize(orderDetails);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;
			dbContext.Configuration.LazyLoadingEnabled = true;

            var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), new DataContractSerializerSettings
            {
                DataContractSurrogate = new DataContractSurrogate()
            });

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
			var orders = dbContext.Orders.ToList();

			tester.SerializeAndDeserialize(orders);
		}
	}
}
