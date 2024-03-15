using Skapiec.Entities;
//not used
namespace Skapiec.Services
{
    public class DbConnectionService
    {
        private readonly SkapiecDBcontext dBcontext;

        public DbConnectionService(SkapiecDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }
    }
}
