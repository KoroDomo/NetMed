using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Interfaces;
using NetMed.Application.Services;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencias
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentsRespository, AppointmentsRepository>();
            services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            services.AddTransient<IDoctorAvailabilityService, DoctorAvailabilityService>();
            // Agregar demas dependencias aqui
            return services;
        }
    }
}
