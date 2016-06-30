using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Shoppers.Core.Rest.Catalogue
{
    public class CatalogueWebRepository : CoreWebRepository<Product>, ICatalogueWebRepository
    {
        public CatalogueWebRepository(IConfigurationRoot configProvider) : base("Catalogue", "api/catalogue", configProvider) { }
        public async Task<Product[]> GetByTitle(string title)
        {
            using(_client)
            {
              var response = await _client.GetAsync(_suffix + "/title/" + title);
              if(response.IsSuccessStatusCode){
                 return await response.Content.ReadAsAsync<Product[]>();
              } 
            }

            return null;
        }

        public async Task<Product[]> GetByType(string productType)
        {
            using(_client)
            {
              var response = await _client.GetAsync("type/" + productType);
              if(response.IsSuccessStatusCode){
                 return await response.Content.ReadAsAsync<Product[]>();
              } 
            }

            return null;
        }
    }
}