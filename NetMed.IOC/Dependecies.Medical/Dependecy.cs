using Microsoft.Extensions.DependencyInjection;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
            services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
            services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
            // Agregar demas repositorios aqui
            return services;
        }
    }
}