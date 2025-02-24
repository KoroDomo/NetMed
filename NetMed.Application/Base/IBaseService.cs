

using NetMed.Domain.Base;

namespace NetMed.Application.Base
{
    public interface IBaseService<TDtoAdd, TDtoUpdate, TDtoRemove>
    {
        //Agregar otros metodos que sean propios de las entidades
        Task<OperationResult> GetAll();
        Task<OperationResult> GetById(int Id);
        Task<OperationResult> Update(TDtoUpdate dto);
        Task<OperationResult> Remove(TDtoRemove dto);
        Task<OperationResult> Save(TDtoAdd dto);
    }
}
