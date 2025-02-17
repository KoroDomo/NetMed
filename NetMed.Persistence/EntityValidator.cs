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
                result.Message = $"{entityName} es nulo";
                return result;
            }
            if (entity is string str && string.IsNullOrWhiteSpace(str))
            {
                result.Success = false;
                result.Message = $"{entityName} esta vacio";
                return result;
            }
            result.Success = true;
            return result;
        }
    }
}