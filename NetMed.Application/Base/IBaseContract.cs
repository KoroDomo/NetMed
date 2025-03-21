using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMed.Application.Base
{
    public interface IBaseContract<TDtoSave, TDtoUpdate, TDtoDelete>
    {

        Task<OperationResult> GetAllDto();
        Task<OperationResult> GetDtoById(Notification notification);
        Task<OperationResult> UpdateDto(TDtoUpdate dtoUpdate);
        Task<OperationResult> SaveDto(TDtoSave dtoSave);
        Task<OperationResult> DeleteDto(TDtoDelete dtoDelete);


    }
}
