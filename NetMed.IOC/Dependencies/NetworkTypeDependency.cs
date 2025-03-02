using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Interfaces;

namespace NetMed.IOC.Dependencies
{
    public static class NetworkTypeDependency
    {
        public static void AddNetworkTypeDependency(this IServiceCollection services)
        {
            services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();
            services.AddTransient<INetworkTypeService, NetworktypeService>();
        }
    }
}
