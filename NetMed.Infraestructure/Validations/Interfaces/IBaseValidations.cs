using NetMed.Domain.Base;

namespace NetMed.Infrastructure.Validations.Interfaces
{
   public interface IBaseValidations
    {
        public OperationResult SuccessResult(dynamic result, string className, string methodName);
        public OperationResult HandleException(string className, string methodName);
        public string GetErrorMessage(string className, string methodName);
        public string GetSuccesMessage(string className, string methodName);
    }
}
