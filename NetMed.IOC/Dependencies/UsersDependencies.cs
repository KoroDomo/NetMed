using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
    public static class UsersDependencies
    {
        public static void AddUsersDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsersServices, UsersService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }
    }
}
