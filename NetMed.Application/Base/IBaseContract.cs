using NetMed.Domain.Base;

namespace NetMed.Application.Base
{
    public interface IBaseContract<TDtoSave, TDtoUpdate, TDtoDelete>
    {

        Task<List<OperationResult>> GetAllDto();
        Task<OperationResult> GetDtoById(int id);
        Task<OperationResult> UpdateDto(TDtoUpdate dtoUpdate);
        Task<OperationResult> SaveDto(TDtoSave dtoSave);
        Task<OperationResult> DeleteDto(TDtoDelete dtoDelete);


    }
}
