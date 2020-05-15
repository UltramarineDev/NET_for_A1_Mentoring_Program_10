using Northwind.Data.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Northwind.Data
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly DbContext _context;

        public OrderRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetMany(Expression<Func<Order, bool>> predicate)
            => _context.Set<Order>().Where<Order>(predicate);

        public IQueryable<Order> GetMany() => _context.Set<Order>();
    }
}
