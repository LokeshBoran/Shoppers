using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shoppers.Core.Data
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Collection { get; }
        Task<T> Create(T newInstance);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<T> Delete(T entity);
    }
}
