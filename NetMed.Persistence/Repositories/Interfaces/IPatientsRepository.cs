

using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
namespace NetMed.Persistence.Repositories.Interfaces
{
    public interface IPatientsRepository : IBaseRepository<Patients>
    {
        Task<Patients> GetPatientsAsyncWithoutInsuranceAsync();

        Task<Patients> GetByBloodTypeAsync(string bloodType);

        Task<Patients> GetByInsuranceProviderAsync(int providerId);

        Task<Patients> SearchByAddressAsync(string addressFragment);

        Task<Patients> GetPatientsByAgeRangeAsync(int minAge, int maxAge);

        Task<Patients> GetByEmergencyContactAsync(string contactInfo);

        Task<Patients> GetPatientsWithAllergiesAsync(string? allergy = null);

        Task<Patients> GetPatientsByGenderAsync(string gender);

    }
}
