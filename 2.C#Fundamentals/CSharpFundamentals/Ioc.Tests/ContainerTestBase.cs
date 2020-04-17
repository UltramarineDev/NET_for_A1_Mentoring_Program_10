using NUnit.Framework;

namespace Ioc.Tests
{
    public class ContainerTestBase
    {
        protected Container Container;

        [SetUp]
        public void Before()
        {
            var defaultInstanceCreator = new DefaultInstanceCreator();
            this.Container = new Container(defaultInstanceCreator);
        }

        [TearDown]
        public void After()
        {
            Container = null;
        }
    }
}
