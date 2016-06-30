using System.Threading.Tasks;

namespace Shoppers.Core.Rest.Catalogue
{
    public interface ICatalogueWebRepository : ICoreWebRepository<Product>
    {
                Task<Product[]> GetByTitle(string title);
                Task<Product[]> GetByType(string productType);
    }    
}