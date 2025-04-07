using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Application.Services;
using NetMedWebApi.Persistence.Interfaces;
using NetMedWebApi.Persistence.Repository;

namespace NetMedWebApi.IOC.Dependencies
{
    public static class NotificationDependency
    {
        public static IServiceCollection AddNotificationDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<INotificationRepository, NotificationRepository>
                (client =>{client.BaseAddress = new Uri(configuration["BaseUrl"]);});

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddScoped<INotificationContract, NotificationServices>();


            return services;
        }
    }
}

