
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Interfaces
{
    public interface IAvailabilityModesRepository : IBaseRepository<AvailabilityModes>
    {

        Task<OperationResult> AvailabilityModeName(string AvailabilityModeName);
    }
}
