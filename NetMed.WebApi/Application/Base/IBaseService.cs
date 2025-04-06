using NetMed.ApiConsummer.Core.Base;

namespace NetMed.ApiConsummer.Application.Base
{
    public interface IBaseService<TModelGet, TModelAdd, TModelUpdate, TModelRemove>
    {
        Task<OperationResult> GetAll();
        Task<OperationResult> GetById(int id);
        Task<OperationResult> Save(TModelAdd dto);
        Task<OperationResult> Update(TModelUpdate dto);
        Task<OperationResult> Remove(TModelRemove dto);
    }
}
