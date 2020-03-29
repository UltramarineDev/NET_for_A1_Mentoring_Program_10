using Northwind.Data.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Northwind.Data
{
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private readonly DbContext _context;

        public OrderDetailsRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<OrderDetails> GetMany(Expression<Func<OrderDetails, bool>> predicate)
            => _context.Set<OrderDetails>().Where<OrderDetails>(predicate);
    }
}
