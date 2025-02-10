
using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Entities;


namespace NetMed.Persistence.Context
{
    public partial class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {


        }

        // MI CONTEXTO BASE DE DATOS
        DbSet<Doctors> doctors { get; set; }

        DbSet<Patients> patients { get; set; }

        DbSet<Users> users { get; set; }

    }
}