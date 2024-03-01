using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Skapiec.Entities
{
    public class SkapiecDBcontext : DbContext
    {
        
        public SkapiecDBcontext(DbContextOptions<SkapiecDBcontext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
