using NetMed.ApiConsummer.Core.Base;

namespace NetMed.ApiConsummer.Infraestructure.Validator.Interfaces
{
    public interface IOperationValidator
    {
        OperationResult<T> CheckNull<T>(T model, string operationType);
        OperationResult<T> CheckId<T>(int id, string operationType);

    }
}
