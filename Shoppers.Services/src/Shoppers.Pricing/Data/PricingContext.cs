using Microsoft.EntityFrameworkCore;
using Shoppers.Core.Data;
using Shoppers.Pricing.Models;

namespace Shoppers.Pricing.Data
{
    public class PricingContext : DbContext, ICoreDbContext
    {
        public DbSet<ProductPricing> Pricings { get; set; }

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
