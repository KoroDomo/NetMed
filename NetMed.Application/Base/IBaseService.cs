
using NetMed.Domain.Base;

namespace NetMed.Application.Base
{
    public interface IBaseService<TDtoAdd, TDtoUpdate, TDtoRemove>
    {
        Task<OperationResult> GetAll();
        Task<OperationResult> GetById(int id);
        Task<OperationResult> Save(TDtoAdd dto);
        Task<OperationResult> Update(TDtoUpdate dto);
        Task<OperationResult> Remove(TDtoRemove dto);
    }
}
