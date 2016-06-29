using Microsoft.EntityFrameworkCore;
using Shoppers.Catalogue.Models;
using Shoppers.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppers.Catalogue.Data
{
    public class ProductCatalogueContext : DbContext, ICoreDbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : CoreEntity
        {
            return base.Set<TEntity>();
        }
    }
}
