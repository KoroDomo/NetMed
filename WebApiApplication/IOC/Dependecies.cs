
using WebApiApplication.Application.Interfaces;
using WebApiApplication.Infraestructura.Messages;
using WebApiApplication.Persistence.Interfaces;
using WebApiApplication.Persistence.Repository;
using WebApiApplication.Infraestructura.Logger;
using WebApiApplication.Application.Services;
using WebApiApplication.Infraestructura.Validators;
using WebApiApplication.UrlConfi;

namespace WebApiApplication.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Register HttpClient 
            services.AddHttpClient<IAppointmentsRepository, AppointmentRepository>();
            services.AddHttpClient<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();

            // Register Repository
            services.AddScoped<IAppointmentsRepository, AppointmentRepository>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();

            //Register Services 
            services.AddTransient<IAppointmentsService, AppointmentService>();
            services.AddTransient<IDoctorAvailabilityService, DoctorAvailabilityService>();
            services.AddScoped<ILoggerSystem, LoggerSystem>();         
            var messageFilePath = Path.Combine(Directory.GetCurrentDirectory(),"messages.json");
            services.AddSingleton<IMessageService>(new MessageService(messageFilePath));
            return services;
        }
    }
}
