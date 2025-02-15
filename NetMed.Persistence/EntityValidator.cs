
using NetMed.Domain.Base;

namespace NetMed.Persistence
{
    public static class EntityValidator
    {
        public static OperationResult ValidateNotNull<T>(T entity, string entityName)
        {
            var result = new OperationResult();
            if (entity == null)
            {
                result.Success = false;
                result.Message = $"{entityName} No puede ser nulo";
            }
            else
            {
                result.Success = true;
            }
            return result;
        }
    }
}

