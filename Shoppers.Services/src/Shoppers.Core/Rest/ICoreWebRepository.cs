using System;
using System.Threading.Tasks;

namespace Shoppers.Core.Rest.Catalogue
{
    public interface ICoreWebRepository<T>
    {
        Task<T> GetById(Int64 Id);
        Task<T[]> Get();
        Task<T> Create(T t);
        Task<T> Delete(Int64 Id);        
    }    
}