using NetMed.ApiConsummer.Application.Contracts;
using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;
using NetMed.ApiConsummer.Persistence.Repositories;
using NetMed.ApiConsummer.Services;

namespace NetMed.ApiConsummer.IOC.Dependencies
{
    public static class NetworkTypeDependency
    {

        public static IServiceCollection AddNetworkTypeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration
            services.AddHttpClient<INetworkTypeRepository, NetworkTypeRepository>(client =>
            {
                client.BaseAddress = new Uri(configuration["ApiBaseUrl"]);
            });

            // Register Repositories
            services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();

            // Register Services
            services.AddScoped<INetworkTypeService, NetworkTypeService>();


            return services;
        }
    }
}
