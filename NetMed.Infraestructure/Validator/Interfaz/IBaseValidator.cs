using NetMed.Domain.Base;

namespace NetMed.Infrastructure.Validator.Interfaces
{
    public interface IBaseValidator<T> where T : class
    {
        OperationResult ValidateIsEntityIsNull(T entity, string errorMessageKey);

        OperationResult ValidateNumberEntityIsNegative(int ID, string errorMessageKey);
    }
}