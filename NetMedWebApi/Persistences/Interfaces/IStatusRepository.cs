using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMedWebApi.Persistence.Interfaces
{
    public interface IStatusRepository 
    {
        Task<OperationResult> GetStatusByIdAsync(int statusId);
        Task<OperationResult> CreateStatusAsync(Status status);

        Task<OperationResult> UpdateStatusAsync(Status status);

        Task<OperationResult> DeleteStatusAsync(int statusId);
    }

}
