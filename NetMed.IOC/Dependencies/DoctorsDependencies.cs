

using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
   public static class DoctorsDependencies
    {
        public static void AddDoctorsDependencies(IServiceCollection services)
        {
            services.AddScoped<IDoctorsServices, DoctorsServices>();
            services.AddScoped<IDoctorsRepository, DoctorsRepository>();
            services.AddScoped<IRepErrorMapper, RepErrorMapper>();
        }
    }
}
