using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
    public static class UsersDependencies
    {
        public static void AddUsersDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersServices>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddSingleton<IRepErrorMapper, RepErrorMapper>();

        }
    }
}
