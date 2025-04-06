using NetMed.WebApplicationRefactor.Persistence.Interfaces;
using NetMed.WebApplicationRefactor.Persistence.Repositories;
using WebApplicationRefactor.Persistence.Config;

namespace WebApplicationRefactor.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration
            services.Configure<UrlSet>(configuration.GetSection("UrlSet"));

            // Register HttpClient without BaseAddress
            services.AddHttpClient<DoctorsRepository>();

            // Register Repositories
            services.AddHttpClient<PatientsRepository>();

            // Register Services
            services.AddHttpClient<UsersRepository>();


            return services;
        }
    }
}
