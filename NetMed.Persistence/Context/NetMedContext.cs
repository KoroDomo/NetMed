using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Entities;

namespace NetMed.Persistence.Context
{
    public class NetMedContext : DbContext
    {
        public NetMedContext(DbContextOptions<NetMedContext> options) : base(options) { }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
    }
}
