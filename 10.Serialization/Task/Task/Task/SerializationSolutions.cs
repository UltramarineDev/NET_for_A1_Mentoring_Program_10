using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using Task.Surrogates;
using System.Data.Entity.Infrastructure;

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
            var serializer = new NetDataContractSerializer(new StreamingContext(
                StreamingContextStates.All, dbContext as IObjectContextAdapter));

            var categories = dbContext.Categories.ToList();

            using (FileStream fs = File.Open("test4" + typeof(IEnumerable<Category>).Name + ".xml", FileMode.Create))
            {
                Console.WriteLine("Testing for type: {0}", typeof(IEnumerable<Category>));
                serializer.WriteObject(fs, categories);
            }
            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(serializer, true);

			var c = categories.First();

			tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;
            var serializer = new NetDataContractSerializer(
                new StreamingContext(StreamingContextStates.All, dbContext as IObjectContextAdapter));
            var products = dbContext.Products.ToList();

            using (FileStream fs = File.Open("test3" + typeof(IEnumerable<Product>).Name + ".xml", FileMode.Create))
            {
                serializer.WriteObject(fs, products);
            }

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(serializer, true);
			tester.SerializeAndDeserialize(products);
		}

		[TestMethod]
		public void ISerializationSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

            var serializer = new NetDataContractSerializer()
            {
                SurrogateSelector = new OrderDetailSurrogateSelector(dbContext)
            };

            var orderDetails = dbContext.Order_Details.ToList();

            //using (FileStream fs = File.Open("test2" + typeof(IEnumerable<Order_Detail>).Name + ".xml", FileMode.Create))
            //{
            //    Console.WriteLine("Testing for type: {0}", typeof(IEnumerable<Order_Detail>));
            //    serializer.WriteObject(fs, orderDetails);
            //}

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);

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

            var orders = dbContext.Orders.ToList();

            //using (FileStream fs = File.Open("test" + typeof(IEnumerable<Order>).Name + ".xml", FileMode.Create))
            //{
            //    Console.WriteLine("Testing for type: {0}", typeof(IEnumerable<Order>));
            //    serializer.WriteObject(fs, orders);
            //}

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);

			tester.SerializeAndDeserialize(orders);
		}
	}
}
