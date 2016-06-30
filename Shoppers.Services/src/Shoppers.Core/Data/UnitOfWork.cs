using System;
using System.Collections.Generic;

namespace Shoppers.Core.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly ICoreDbContext context;
        private readonly IServiceProvider provider;
        public UnitOfWork(ICoreDbContext context, IServiceProvider provider)
        {
            this.context = context;
            this.provider = provider;
            cache = new Dictionary<Type, object>();
        }
        private readonly IDictionary<Type, object> cache;

        public IRepository<T> Repository<T>() where T : CoreEntity
        {
            if (!cache.ContainsKey(typeof(T)))
            {
                cache.Add(typeof(T), (provider.GetService(typeof(IRepository<T>)) as IRepository<T>).SetContext(context));
            }
            
            return cache[typeof(T)] as IRepository<T>; ;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    cache.Clear();
                    context.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
