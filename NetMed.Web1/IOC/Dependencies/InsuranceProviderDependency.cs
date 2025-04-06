using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Repositories;
using NetMed.ApiConsummer.Application.Contracts;
using NetMed.ApiConsummer.Services;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.IOC.Dependencies
{
    public static class InsuranceProviderDependency
    {
        public static IServiceCollection AddInsuranceProviderDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration
            services.AddHttpClient<IInsuranceProviderRepository, InsuranceProviderRepository>(client =>
            {
                client.BaseAddress = new Uri(configuration["ApiBaseUrl"]);
            });

            // Register Repositories
            services.AddScoped<IInsuranceProviderRepository, InsuranceProviderRepository>();

            // Register Services
            services.AddScoped<IInsuranceProviderService, InsuranceProviderService>();


            return services;
        }
    }
}