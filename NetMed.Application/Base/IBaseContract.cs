using NetMed.Domain.Base;

namespace NetMed.Application.Base
{
    public interface IBaseContract<TDtoSave, TDtoUpdate, TDtoDelete>
    {

        Task<OperationResult> GetAllDto();
        Task<OperationResult> GetDtoById(int notification);
        Task<OperationResult> UpdateDto(TDtoUpdate dtoUpdate);
        Task<OperationResult> SaveDto(TDtoSave dtoSave);
        Task<OperationResult> DeleteDto(int dtoDelete);


    }
}
