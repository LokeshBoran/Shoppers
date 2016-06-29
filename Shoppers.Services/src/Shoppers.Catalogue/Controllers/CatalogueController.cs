using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shoppers.Catalogue.Data;
using Shoppers.Catalogue.Models;
using Shoppers.Core.Data;

namespace Shoppers.Catalogue.Controllers
{
    [Route("api/[controller]")]
    public class CatalogueController : Controller
    {
        public CatalogueController(UnitOfWork db)
        {
            this.db = db;
            Products = db.Repository<Product>();
        }

        private UnitOfWork db { get; set; }
        private readonly IRepository<Product> Products;

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (db)
            {
                return Ok((await Products.FindAll(null)).Take(10).ToArray());
            }
        }

        // GET api/values/5
        [HttpGet("type/{productType}")]
        public async Task<IActionResult> GetByType(string productType)
        {
            using (db)
            {
                var products = await Products.FindAll(p => p.ProductType.ToLower().Contains(productType.ToLower()));
                return products == null ? NotFound(string.Format("Products with ProductType having {0} not found", productType)) as IActionResult : Ok(products.ToArray()) as IActionResult;
            }
        }

        [HttpGet("title/{productTitle}")]
        public async Task<IActionResult> GetByTitle(string productTitle)
        {
            using (db)
            {
                var products = await Products.FindAll(p => p.Title.ToLower().Contains(productTitle.ToLower()));
                return products == null ? NotFound(string.Format("Products with Title having {0} not found", productTitle)) as IActionResult : Ok(products.ToArray()) as IActionResult;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (db)
            {
                var product = Products.Find(p => p.Id == id);
                return product == null ? NotFound(string.Format("Product with Id = {0} not found", id)) as IActionResult : Ok(product) as IActionResult;
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product value)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    return Ok(await Products.Create(value));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        {
            using (db)
            {
                var product = await Products.Find(id);

                if (product != null)
                {
                    await  Products.Delete(product);
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
