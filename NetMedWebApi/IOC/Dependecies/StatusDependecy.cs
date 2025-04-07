using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Application.Services;
using NetMedWebApi.Persistence.Interfaces;
using NetMedWebApi.Persistence.Repository;

namespace NetMedWebApi.IOC.Dependencies
{
    public static class StatusDependency
    {
        public static IServiceCollection AddStatusDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IStatusRepository, StatusRepository>
                (client => { client.BaseAddress = new Uri(configuration["BaseUrl"]); });

            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddScoped<IStatusContract, StatusServices>();


            return services;
        }
    }
}
