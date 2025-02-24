using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Interfaces;
using NetMed.Application.Services;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependecies.Medical.Specialties
{
    public static class SpecialtiesDependency
    {
        public static void AddSpecialtiesDependency(this IServiceCollection services)
        {
            services.AddTransient<ISpecialtiesService, SpecialtiesService>();
            services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
        }
    }
}
