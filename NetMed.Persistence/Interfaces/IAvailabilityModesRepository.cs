using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Interfaces
{
    public interface IAvailabilityModesRepository : IBaseRepository<AvailabilityModes>
    {
        Task<OperationResult> GetByNameAsync(string availabilityModeName);
    }
}

