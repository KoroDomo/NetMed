using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Interfaces;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Application.Services;

namespace NetMed.IOC.Dependecies.Medical.MedicalRecords
{
    public static class MedicalRecordsDependency
    {
        public static void AddMedicalRecordsDependency(this IServiceCollection services)
        {
            services.AddTransient<IMedicalRecordsService, MedicalRecordsService>();
            services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
        }
    }
}
