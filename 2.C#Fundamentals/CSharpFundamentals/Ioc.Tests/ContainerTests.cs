using NUnit.Framework;
using FluentAssertions;
using System.Reflection;
using Ioc.Tests.TestEntities;

namespace Ioc.Tests
{
    [TestFixture]
    public class Container_GetInstance : ContainerTestBase
    {

        [Test]
        public void CreateaAnInstanceWithNoParams()
        {
            var subject = (A)Container.Resolve(typeof(A));
            subject.Should().BeOfType<A>();
        }

        [Test]
        public void CreateaAnInstanceWithParams()
        {
            var subject = (B)Container.Resolve(typeof(B));
            subject.A.Should().BeOfType<A>();
        }

        [Test]
        public void ItAllowsParameterlessConstuctor()
        {
            var subject = (C)Container.Resolve(typeof(C));
            subject.Invoked.Should().BeTrue();
        }

        [Test]
        public void ItAllowsGenericInitialization()
        {
            var subject = Container.Resolve<A>();
            subject.Should().BeOfType<A>();
        }

        public class A { }

        public class B
        {
            public A A { get; }

            public B(A a)
            {
                A = a;
            }
        }

        public class C
        {
            public bool? Invoked { get; set; }

            public C()
            {
                Invoked = true;
            }
        }
    }

    [TestFixture]
    public class Container_Register : ContainerTestBase
    {
        [Test]
        public void AddAssembly()
        {
            Container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBLLConstructor = Container.Resolve<CustomerBLLConstructor>();
            var customerBLLProperty = Container.Resolve<CustomerBLLProperty>();
            customerBLLConstructor.Dal.Should().BeOfType<CustomerDAL>();
            customerBLLProperty.CustomerDAL.Should().BeOfType<CustomerDAL>();
            customerBLLProperty.Logger.Should().BeOfType<Logger>();
        }

        [Test]
        public void RegisterATypeFromAnInterface()
        {
            Container.Bind<IMaterial, Plastic>();
            var subject = Container.Resolve<IMaterial>();
            subject.Should().BeOfType<Plastic>();
        }

        [Test]
        public void InitializeObjectWithDependancies()
        {
            Container.Bind<IMaterial, Toy>();
            Container.Bind<ITemp, Temp>();
            var subject = (Toy)Container.Resolve<IMaterial>();
            subject.Temp.Should().BeOfType<Temp>();
        }

        interface IMaterial
        {
            int Weight { get; }
        }

        class Plastic : IMaterial
        {
            public int Weight => 42;
        }

        class Metal : IMaterial
        {
            public int Weight => 84;
        }

        class Toy : IMaterial
        {
            public int Weight => 100;
            public ITemp Temp;

            public Toy(ITemp temp)
            {
                Temp = temp;
            }
        }

        interface ITemp { }
        class Temp : ITemp { }
    }

    [TestFixture]
    public class Container_RegisterSingleClass: ContainerTestBase
    {
        [Test]
        public void ItReturnsASingleInstance()
        {
            var petFirst = new Pet();
            Container.Bind(petFirst);
            var subject = Container.Resolve<Pet>();
            subject.Should().Be(petFirst);
        }

        class Pet
        {
            public Pet()
            {
            }
        }
    }
}
