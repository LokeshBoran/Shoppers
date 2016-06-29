using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shoppers.Catalogue.Data;
using Shoppers.Catalogue.Models;

namespace Shoppers.Catalogue.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ValuesController(ProductCatalogueContext db)
        {
            this.db = db;
        }

        private ProductCatalogueContext db { get; set; }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            using (db)
            {
                return Ok(db.Products.Take(10).ToArray());
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (db)
            {
                var product = db.Products.SingleOrDefault(p => p.Id == id);
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
                    return Ok(await db.Products.Add(value).Context.SaveChangesAsync());
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (db)
            {
                var product = db.Products.SingleOrDefault(p => p.Id == id);

                if (product != null)
                {
                    await db.Remove(product).Context.SaveChangesAsync();

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
