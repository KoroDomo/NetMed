
using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
    public static class StatusDependency
    {


        public static void AddStatusDependency(this IServiceCollection service)
        {

            service.AddScoped<IStatusRepository, StatusRepository>();
            service.AddTransient<IStatusContract, StatusServices>();




        }

    }
}
