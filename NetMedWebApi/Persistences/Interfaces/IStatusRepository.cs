using NetMedWebApi.Models;
using NetMedWebApi.Models.Status;

namespace NetMedWebApi.Persistence.Interfaces
{
    public interface IStatusRepository 
    {
        Task<OperationResultList<StatusApiModel>> GetAllStatusAsync();

        Task<OperationResult<T>> GetStatusByIdAsync<T>(int Id);

        Task<OperationResult<SaveStatusModel>> CreateStatusAsync(SaveStatusModel model);

        Task<OperationResult<UpdateStatusModel>> UpdateStatusAsync(UpdateStatusModel model);

        Task<OperationResult<DeleteStatusModel>> DeleteStatusAsync(DeleteStatusModel model);
    }

}
