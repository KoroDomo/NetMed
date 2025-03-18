using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.IOC.Dependencies;

namespace NetMed.IOC
{
    public static class DependencyInjection
    {

        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            
            DoctorsDependencies.AddDoctorsDependencies(serviceCollection);
            PatientsDependecies.AddPatientsDependecies(serviceCollection);
            UsersDependencies.AddUsersDependencies(serviceCollection);

        }

    }

}