using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Application.Services;
using NetMedWebApi.Persistence.Interfaces;
using NetMedWebApi.Persistence.Repository;

namespace NetMedWebApi.IOC.Dependencies
{
    public static class RolesDependency
    {
        public static IServiceCollection AddRolesDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IRolesRepository, RolesRepository>
                (client => { client.BaseAddress = new Uri(configuration["BaseUrl"]); });

            services.AddScoped<IRolesRepository, IRolesRepository>();

            services.AddScoped<IRolesContract, RolesServices>();


            return services;
        }
    }
}
