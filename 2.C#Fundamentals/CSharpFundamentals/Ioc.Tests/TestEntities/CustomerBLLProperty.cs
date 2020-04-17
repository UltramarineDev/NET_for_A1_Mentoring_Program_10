namespace Ioc.Tests.TestEntities
{
    public class CustomerBLLProperty
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }

        [Import]
        public Logger Logger { get; set; }
    }
}
