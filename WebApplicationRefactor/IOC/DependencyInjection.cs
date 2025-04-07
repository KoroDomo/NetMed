using WebApplicationRefactor.Models.Doctors;
using WebApplicationRefactor.Models.Patients;
using WebApplicationRefactor.Models.Users;


using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;
using WebApplicationRefactor.Persistence.Config;
using NetMed.WebApplicationRefactor.Persistence.Repositories;
using WebApplicationRefactor.Application.Services;
using NetMed.Application.Services;

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
            services.AddScoped<IDoctorServices, DoctorService>();
            services.AddScoped<IPatientsService, PatientsService>();
            services.AddScoped<IUsersService, UsersServices>();
            // Register Error Message Service
                   services.AddSingleton<IErrorMessageService, ErrorMessageService>();

                  services.AddScoped(typeof(ILoggerManger), typeof(LoggerManger<>));


            // Add other dependencies as needed

            return services;
        }
    }
}