using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;

using NetMed.Infrastructure.Validations.Interfaces;


namespace NetMed.Infraestructure.Validation.Interfaces
{
    public interface IOperationValidator : IBaseValidations
    {
        public OperationResult isNull<TEntity>(TEntity entity);
        public OperationResult isNull<TEntity>(TEntity entity, ILogger logger);
        public OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity, string entityName);
        public OperationResult IsInt<TEntity>(TEntity entity, int entityName);
        public OperationResult ValidateStringLength(string entityName, int maxLength);
        public OperationResult IsValidEmail(string email);
        public OperationResult IsValidPhoneNumber(string phoneNumber);
    }
}