using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Shoppers.Core.Rest.Catalogue
{
    public class CoreWebRepository<T> : ICoreWebRepository<T>
    {

        public CoreWebRepository(string service, string suffix, IConfigurationRoot config){
            _client.BaseAddress = new Uri(config.GetSection("Services")[service + "EndPoint"]);
             _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            _suffix = suffix;
        }

        protected HttpClient _client = new HttpClient();
        protected string _suffix = string.Empty;
        public Task<T> Create(T t)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<T[]> Get()
        {
            using(_client)
            {
                
              var response = await _client.GetAsync( _suffix );
              if(response.IsSuccessStatusCode){
                 return await response.Content.ReadAsAsync<T[]>();
              } 
            }

            return null;
        }

        public async Task<T> GetById(long Id)
        {
            using(_client)
            {
              Console.WriteLine("getting By Id: " + _suffix + "/" + Id.ToString());
              var response = await _client.GetAsync(_suffix + "/" + Id.ToString());
              if(response.IsSuccessStatusCode){
                 return await response.Content.ReadAsAsync<T>();
              } 
            }

            return default(T);

        }
    }
}