

using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
   public static class DoctorsDependencies
    {
        public static void AddDoctorsDependencies(IServiceCollection services)
        {
            services.AddScoped<IDoctorsServices, DoctorsService>();
            services.AddScoped<IDoctorsRepository, DoctorsRepository>();
        }
    }
}
