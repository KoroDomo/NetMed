using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
namespace NetMed.Persistence.Interfaces
{
    public interface IPatientsRepository : IBaseRepository<Patients>
    {
      
        Task<OperationResult> GetByBloodTypeAsync(char bloodType);

        Task<OperationResult> GetByInsuranceProviderAsync(int providerId);

        Task<OperationResult> SearchByAddressAsync(string address);

        Task<OperationResult> GetByEmergencyContactAsync(string EcontactInfo);

        Task<OperationResult> GetPatientsWithAllergiesAsync(string? allergy = null);

        Task<OperationResult> GetPatientsByGenderAsync(char gender);

    }
}
