using WebApplicationRefactor.Models.Doctors;
using WebApplicationRefactor.Models.Patients;
using WebApplicationRefactor.Models.Users;


using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;
using WebApplicationRefactor.Application.Services;
using NetMed.Application.Services;
using WebApplicationRefactor.Persisten.Config;
using WebApplicationRefactor.Persisten.Repository;
using WebApplicationRefactor.ServicesApi.Interface;
using WebApplicationRefactor.Services.Interface;
using WebApplicationRefactor.Application.IBaseApp;
using WebApplicationRefactor.Services.Service;

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
            services.AddHttpClient<PatientsRepository>();
            services.AddHttpClient<UsersRepository>();

            // Register Repositories
            services.AddScoped<IRepository<DoctorsApiModel>, DoctorsRepository>();
            services.AddScoped<IRepository<PatientsApiModel>, PatientsRepository>();
            services.AddScoped<IRepository<UsersApiModel>, UsersRepository>();

            // Register Services
            services.AddScoped<IPatientsService, PatientsService>();
            services.AddScoped<IDoctorServices, DoctorService>();

            // Fix for CS0311: Ensure UsersServices implements IUsersService
            services.AddScoped<IUsersService, UsersService>();

            // Register Error Message Service

            services.AddScoped<ILoggerManager, ILoggerManager>();


            // Add other dependencies as needed

            return services;
        }
    }
}