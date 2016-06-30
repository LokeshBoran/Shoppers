using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shoppers.Core.Data;
using Shoppers.Core.Rest.Catalogue;
using Shoppers.Pricing.Models;

namespace Shoppers.Pricing.Controllers
{
    [Route("api/[controller]")]
    public class PriceController : Controller
    {

        public PriceController(UnitOfWork db, ICatalogueWebRepository catalogueWebRepository)
        {
            this.db = db;
            _productPrices = db.Repository<ProductPricing>();
            _catalogueWebRepository = catalogueWebRepository;
        }

        private readonly UnitOfWork db;
        private readonly IRepository<ProductPricing> _productPrices;
        private readonly ICatalogueWebRepository _catalogueWebRepository;

         
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (db)
            {
                return Ok((await _productPrices.FindAll(null)).Take(10).ToArray());
            }
        }

        [HttpGet("product/{Id}")]
        public async Task<IActionResult> GetProductPricing(Int64 Id)
        {
            using (db)
            {
                var product = await _catalogueWebRepository.GetById(Id);
                if(product == null){
                    return NotFound();
                }

                var productPrices = (await _productPrices.FindAll(p => p.ProductId == Id)).Take(10).ToArray();
                if(!productPrices.Any()){
                    return NotFound();                    
                }



        
                return Ok(productPrices.Select(_ => new { 
                    Title = product.Title, 
                    ProductType = product.ProductType, 
                    Price = _.Price,
                    Provider = _.Provider 
                    }));
            }
        }
        

         
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (db)
            {
                var product = await _productPrices.Find(p => p.Id == id);
                return product == null ? NotFound(string.Format("Product with Id = {0} not found", id)) as IActionResult : Ok(product) as IActionResult;
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductPricing value)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var productPrice = await _productPrices.Create(value);

                    return CreatedAtAction("Get", "Price", new { Id = productPrice.Id }, productPrice );
 
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        {
            using (db)
            {
                var product = await _productPrices.Find(id);

                if (product != null)
                {
                    await  _productPrices.Delete(product);
                    return Ok(product);
                }
                else
                {
                    return NotFound(string.Format("Product with Id = {0} not found", id));
                }

            }
        }

      
    }
}
