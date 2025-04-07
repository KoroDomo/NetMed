using NetMedWebApi.Models;

namespace NetMedWebApi.Infrastructure.Validator
{
    public interface IValidateGeneral
    {
        OperationResult<T> CheckIfEntityIsNull<T>(T model, string operationType);

        OperationResult<T> CheckIfId<T>(int id, string operationType);

    }
}
