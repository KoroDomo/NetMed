using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Entities;

namespace NetMed.Persistence.Context
{
    public class NetMedContext : DbContext
    {
        public NetMedContext(DbContextOptions<NetMedContext> options) : base(options)
        {
         
        }

        public DbSet<AvailabilityModes> AvailabilityMode { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
    }
}
