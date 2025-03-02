

using NetMed.Domain.Base;

namespace NetMed.Application.Base
{
   public interface IBaseService<TDtoAdd,TDtoUpdate,TDtoDelete>
    {
        Task<OperationResult> GetAllData();

        Task<OperationResult> GetById(int id);

        Task<OperationResult> Add(TDtoAdd dto);

        Task<OperationResult> Update(TDtoUpdate dto);

        Task<OperationResult> Delete(TDtoDelete dto);
    }
}
