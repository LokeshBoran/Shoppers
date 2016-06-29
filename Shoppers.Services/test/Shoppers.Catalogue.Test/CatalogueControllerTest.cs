using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

using Shoppers.Catalogue.Controllers;
using Shoppers.Catalogue.Data;
using Shoppers.Catalogue.Models;
using Shoppers.Core.Data;
using Xunit;

namespace Shoppers.Catalogue.Test
{
     public class CatalogueControllerTest
    {
        private ServiceCollection _service = new ServiceCollection();
        private CatalogueController _controller = null;
            private IServiceProvider provider = null;

        public CatalogueControllerTest(){



           /* _service.AddDbContext<ProductCatalogueContext>()
            .AddEntityFrameworkInMemoryDatabase(); */ 
            
            _service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            _service.AddTransient(typeof(ICoreDbContext), _ => {

                var context = new  ProductCatalogueContext();

            if(!context.Products.Any()){
                context.Products.Add(new Product() { Title = "Sample 1", ProductType = "Other" } );
                context.Products.Add(new Product() { Title = "Sample 2", ProductType = "Other" } );
                context.Products.Add(new Product() { Title = "Sample 3", ProductType = "Other" } );
                context.Products.Add(new Product() { Title = "Sample 4", ProductType = "Other" } );

                context.Products.Add(new Product() {  Title = "Sample Book 1", ProductType = "Book" } );
                context.Products.Add(new Product() { Title = "Sample Book 2", ProductType = "Book" } );
                context.Products.Add(new Product() {  Title = "Sample Book 3", ProductType = "Book" } );

                context.Products.Add(new Product() {  Title = "Sample Car 1", ProductType = "Car" } );
                context.Products.Add(new Product() { Title = "Sample Car 2", ProductType = "Car" } );
                            context.SaveChanges();

            }                

                return context;

            });
            _service.AddTransient<UnitOfWork>();

            provider = _service.BuildServiceProvider();            
    
        }

        private void ProvisonNewController(){
            _controller = new CatalogueController(provider.GetService(typeof(UnitOfWork)) as UnitOfWork);    
        }

  
        [Fact]
        public async void GetsAllProductsReturns10Products()
        {
          ProvisonNewController();            
          var abc = await _controller.Get();

         Assert.Equal(9, ((dynamic) abc).Value.Length); 
          
        }

        [Fact]
        public async void GetsProductsByTypeReturnsCorrespondingProducts()
        {

          ProvisonNewController();            
          var abc = await _controller.GetByType("other");
          Assert.Equal(4, ((dynamic) abc).Value.Length); 

          ProvisonNewController();
          abc = await _controller.GetByType("car");

          Assert.Equal(2, ((dynamic) abc).Value.Length); 

          ProvisonNewController();          
          abc = await _controller.GetByType("book");

          Assert.Equal(3, ((dynamic) abc).Value.Length); 
        }

        [Fact]
        public async void GetsProductsByTitleReturnsCorrespondingProducts()
        {

          ProvisonNewController();            
          var abc = await _controller.GetByTitle("Sample");
          Assert.Equal(9, ((dynamic) abc).Value.Length); 

          ProvisonNewController();
          abc = await _controller.GetByTitle("car");

          Assert.Equal(2, ((dynamic) abc).Value.Length); 

          ProvisonNewController();          
          abc = await _controller.GetByTitle("book");

          Assert.Equal(3, ((dynamic) abc).Value.Length); 
        }

        [Fact]
        public async void AddProduct()
        {
          ProvisonNewController();            
          await _controller.Post(new Product() { Title = "From Test", ProductType = "Test" });
        
          ProvisonNewController();                    
          var abc = await _controller.GetByType("Test");        
          Assert.Equal(1, ((dynamic) abc).Value.Length); 
        }

        [Fact]
        public async void RemoveProduct()
        {
          ProvisonNewController();            
          await _controller.Post(new Product() { Title = "From RemoveProduct Test", ProductType = "RemoveProductTest" });
        
          ProvisonNewController();                    
          var abc = await _controller.GetByType("RemoveProductTest");        
          Assert.Equal(1, ((dynamic) abc).Value.Length); 

          ProvisonNewController();                    
          await _controller.Delete((((dynamic) abc).Value[0] as Product).Id);        
          
          ProvisonNewController();                    
          abc = await _controller.GetByType("RemoveProductTest");        
          Assert.Equal(0, ((dynamic) abc).Value.Length);
        }
    }
}
