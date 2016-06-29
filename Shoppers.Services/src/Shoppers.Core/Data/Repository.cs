using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shoppers.Core.Data
{
    public class Repository<T> : IRepository<T> where T : CoreEntity
    {
        public Repository(ICoreDbContext db)
        {
            this.db = db;
        }
        
        private ICoreDbContext db { get; set; }
        public DbSet<T> Collection
        {
            get
            {
                return db.Set<T>();
            }
        }

        public async Task<T> Create(T newInstance)
        {
           return await Collection.Add(newInstance).Context.SaveChangesAsync() > 0 ? newInstance : null ;
        }

        public async Task<T> Delete(T entity)
        {
           return await Collection.Remove(entity).Context.SaveChangesAsync() > 0 ? entity : null ;
        }

        public async Task<IQueryable<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
           
            return predicate == null ? Collection : await Task.Run( () => Collection.Where(predicate));
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await Collection.SingleOrDefaultAsync(predicate);
        }

        public async Task<T> Find(Int64 id)
        {
            return await Task.Run(() => Collection.FirstOrDefault(m => m.Id == id));
        }

    }
}
