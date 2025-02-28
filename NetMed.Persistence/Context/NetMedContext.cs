
using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Entities;


namespace NetMed.Persistence.Context
{
    public  class NetMedContext: DbContext
    {
        public NetMedContext(DbContextOptions options) : base(options)
        { }
         public   DbSet<Doctors> Doctors { get; set; }

         public   DbSet<Patients> Patients { get; set; }

         public   DbSet<UsersModel> Users { get; set; }

     }         
}