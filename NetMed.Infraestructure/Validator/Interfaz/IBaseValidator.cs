using NetMed.Domain.Base;

namespace NetMed.Infrastructure.Validator.Interfaces
{
    public interface IBaseValidator<T> where T : class
    {
        OperationResult ValidateIsEntityIsNull(T entity, string errorMessageKey);

        OperationResult ValidateNumberEntityIsNegative(int ID, string errorMessageKey);

       OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity);
       OperationResult ValidateStringLength(string entityName, int maxLength);
       OperationResult ValideteIdIsNotNull(int ID);

    }
}