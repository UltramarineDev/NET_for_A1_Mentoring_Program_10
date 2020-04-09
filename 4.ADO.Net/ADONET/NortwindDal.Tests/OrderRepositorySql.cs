using NorthwindDAL;

namespace NortwindDal.Tests
{
    public class OrderRepositorySql: OrderRepository
    {
        public OrderRepositorySql(string connectionString, string provider) : base(connectionString, provider) { }
    }
}
