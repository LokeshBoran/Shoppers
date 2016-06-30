using Microsoft.EntityFrameworkCore;
using System;

namespace Shoppers.Core.Data
{
    public interface ICoreDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : CoreEntity;
    }
}
