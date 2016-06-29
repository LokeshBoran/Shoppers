using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Shoppers.Core.Data
{
    public interface ICoreDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : CoreEntity;
    }
}
