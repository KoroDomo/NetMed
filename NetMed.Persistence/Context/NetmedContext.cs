
using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Entities;

namespace NetMed.Persistence.Context
{
    public class NetmedContext : DbContext
    {

        public NetmedContext(DbContextOptions<NetmedContext> options) : base(options)
        {

        }
        public DbSet<Status> statuses { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Roles> Roles { get; set; }
    }
}
