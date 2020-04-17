namespace Ioc.Tests.TestEntities
{
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {
    }
}
