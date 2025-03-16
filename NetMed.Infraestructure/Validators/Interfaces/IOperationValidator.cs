using NetMed.Domain.Base;
using NetMed.Infraestructure.Logger;

namespace NetMed.Infraestructure.Validators.Interfaces
{
    public interface IOperationValidator : IBaseValidator
    {
        public OperationResult isNull<TEntity>(TEntity entity);
        public OperationResult isNull<TEntity>(TEntity entity, ICustomLogger customLogger);
        public OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity, string entityName);
        public OperationResult IsInt<TEntity>(TEntity entity, int entityName);
        public OperationResult ValidateStringLength(string entityName, int maxLength);
        public OperationResult IsValidEmail(string email);
        public OperationResult IsValidPhoneNumber(string phoneNumber);
    }
}
