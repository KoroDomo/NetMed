using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Interfaces;
using NetMed.Application.Services;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependecies.Medical.AvailabilityModes
{
    public static class AvailabilityModesDependency
    {
        public static void AddAvailabilityModesDependency(this IServiceCollection services)
        {
            services.AddTransient<IAvailabilityModesService, AvailabilityModesService>();
            services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
        }
    }
}
