
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Context.Interfaces
{
    public interface IStatusRepository : IBaseRepository<Status> 
    {

            Task<OperationResult> GetStatusByIdAsync(int statusId);

            Task <OperationResult>CreateStatusAsync(Status status);

            Task <OperationResult>UpdateStatusAsync(Status status);
    }
}
