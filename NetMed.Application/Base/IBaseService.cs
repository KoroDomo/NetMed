
using NetMed.Domain.Base;

namespace NetMed.Application.Base
{
    public interface IBaseService<TDtoSave,TDtoUpdate, TDtoRemove>
    {
        Task<OperationResult> GetAll();
        Task<OperationResult> GetById(int Id);
        Task<OperationResult> Save(TDtoSave TDto);
        Task<OperationResult> Update(TDtoUpdate TDto);
        Task<OperationResult> Remove(int TDto);
    }
}
