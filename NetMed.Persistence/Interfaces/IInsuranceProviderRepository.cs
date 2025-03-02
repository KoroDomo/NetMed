using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface IInsuranceProviderRepository : IBaseRepository<InsuranceProviders>
    {
        Task<OperationResult> RemoveInsuranceProviderAsync(int id);
        Task<OperationResult> GetPreferredInsuranceProvidersAsync();
        Task<OperationResult> GetActiveInsuranceProvidersAsync();
        Task<OperationResult> GetInsuranceProvidersByRegionAsync(string region);
        Task<OperationResult> GetInsuranceProvidersByMaxCoverageAsync(decimal maxCoverage);
        
    }
}
