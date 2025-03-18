using Microsoft.Extensions.DependencyInjection;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;



namespace NetMed.IOC.Dependencias
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentsRespository, AppointmentsRepository>();
            //services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            //services.AddTransient<IDoctorAvailabilityService, DoctorAvailabilityService>();
            services.AddScoped<ILoggerSystem, LoggerSystem>();
            services.AddScoped<IValidations, Validations>();           
            var messageFilePath = Path.Combine(Directory.GetCurrentDirectory(),"messages.json");
            services.AddSingleton<IMessageService>(new MessageService(messageFilePath));
            return services;
        }
    }
}
