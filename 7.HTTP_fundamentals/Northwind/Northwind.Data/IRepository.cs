using System;
using System.Linq;
using System.Linq.Expressions;

namespace Northwind.Data
{
    public interface IRepository<TEntity> where TEntity: class
    {
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate);
    }
}
