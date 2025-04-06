using NetMed.ApiConsummer.Core.Base;

namespace NetMed.ApiConsummer.Application.Base
{
    public interface IBaseService<TModelGet, TModelAdd, TModelUpdate, TModelRemove>
    {
        Task<ListOperationResult<TModelGet>> GetAll();
        Task<OperationResult<TEntity>> GetById<TEntity>(int id);
        Task<OperationResult<TModelAdd>> Save(TModelAdd dto);
        Task<OperationResult<TModelUpdate>> Update(TModelUpdate dto);
        Task<OperationResult<TModelRemove>> Remove(TModelRemove dto);
    }
}
