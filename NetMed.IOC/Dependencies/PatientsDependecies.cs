

using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Persistence.Base;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.IOC.Dependencies
{
    public static class PatientsDependecies
    {
        public static void AddPatientsDependecies(IServiceCollection services)
        {
            services.AddScoped<IPatientsServices, PatientsServices>();
            services.AddScoped<IPatientsRepository, PatientsRepository>();  
        }
    }
}
