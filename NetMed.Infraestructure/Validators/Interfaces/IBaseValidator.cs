
using NetMed.Domain.Base;

namespace NetMed.Infraestructure.Validators.Interfaces
{
    public interface IBaseValidator
    {
        public OperationResult SuccessResult(dynamic result, string className, string methodName);
        public OperationResult HandleException(string className, string methodName);
        public string GetErrorMessage(string className, string methodName);
        public string GetSuccesMessage(string className, string methodName);
    }
}
