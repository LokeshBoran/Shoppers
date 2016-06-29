using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shoppers.Core.Data
{
    public interface IRepository<T> where T : CoreEntity
    {
        Repository<T> SetContext(ICoreDbContext db);
        Task<T> Create(T newInstance);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> Find(Int64 id);
        Task<IQueryable<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<T> Delete(T entity);
    }
}
