using NetMed.Domain.Base;

namespace NetMed.Persistence.Validators
{
    public static class EntityValidator
    {
        public static OperationResult Validator<TEntity>(TEntity entity, string entityName)
        {
            var result = new OperationResult();

            if (entity is null)
            {
                result.Success = false;
                result.Message = $"{entityName} no puede ser nulo";
                return result; 
            }
            if (entity is int intValue) 
            {
                if (intValue <= 0)
                {
                    result.Success = false;
                    result.Message = $"{entityName} debe ser mayor que cero";
                    return result; 
                }
            }
            result.Success = true;
            return result;
        }
    }
}