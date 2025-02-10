using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Entities;


namespace NetMed.Persistence.Context
{
    public class NetMedContext : DbContext
    {
        public NetMedContext(DbContextOptions<NetMedContext> options) : base(options) 
        {

        
        }

        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }

        public DbSet<NetworkType> NetworkTypes { get; set; }


    }
}
