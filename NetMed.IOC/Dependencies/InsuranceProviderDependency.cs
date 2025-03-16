using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Validators;

namespace NetMed.IOC.Dependencies
{
    public static class InsuranceProviderDependency
    {
        public static void AddInsuranceProviderDependency(this IServiceCollection services)
        {

            services.AddScoped<IInsuranceProviderRepository, InsuranceProviderRepository>();
            services.AddTransient<IInsuranceProviderService, InsuranceProviderService>();

            
        }
    }
}
