
using NetMedWebApi.Models;

namespace NetMedWebApi.Application.Base
{
    public interface IBaseContract<TModel,TSave,TUpdate,TRemove>
    {

        Task<OperationResultList<TModel>> GetAll();
        Task<OperationResult<T>> GetById<T>(int Id);
        Task<OperationResult<TSave>> Create(TSave dto);

        Task<OperationResult<TUpdate>> Update(TUpdate dto);
        Task<OperationResult<TRemove>> Delete(TRemove dto);


    }
}
