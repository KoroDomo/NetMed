

using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
    public static class RolesDependency
    {

        public static void AddRolesDependency(this IServiceCollection service)
        {

            service.AddScoped<IRolesRepository, RolesRepository>();
            service.AddTransient<IRolesContract, RolesService>();




        }
    }
}
