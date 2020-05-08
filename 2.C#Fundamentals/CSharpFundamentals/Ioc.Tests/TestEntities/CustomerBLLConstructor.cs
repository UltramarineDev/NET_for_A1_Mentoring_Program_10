namespace Ioc.Tests.TestEntities
{
    [ImportConstructor]
    public class CustomerBLLConstructor
    {
        public ICustomerDAL Dal { get; set; }
        public CustomerBLLConstructor(ICustomerDAL dal)
        {
            Dal = dal;
        }
    }
}
