using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Skapiec.Entities
{
    public class SkapiecDBcontext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public SkapiecDBcontext(DbContextOptions<SkapiecDBcontext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sknerus;Trusted_Connection=True;");
        }

        //jedna z możliwości podłączenia się do bazy
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString);
                //.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sknerus;Trusted_Connection=True");
                //SQL albo wbudowana (?)
        }
        */
    }
}
