using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
namespace NetMed.Persistence.Interfaces
{
    public interface IPatientsRepository : IBaseRepository<Patients>
    {
        Task<OperationResult> GetPatientsAsyncWithoutInsuranceAsync();

        Task<OperationResult> GetByBloodTypeAsync(string bloodType);

        Task<OperationResult> GetByInsuranceProviderAsync(int providerId);

        Task<OperationResult> SearchByAddressAsync(string addressFragment);

        Task<OperationResult> GetPatientsByAgeRangeAsync(int minAge, int maxAge);

        Task<OperationResult> GetByEmergencyContactAsync(string contactInfo);

        Task<OperationResult> GetPatientsWithAllergiesAsync(string? allergy = null);

        Task<OperationResult> GetPatientsByGenderAsync(string gender);

    }
}
