using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;
using NetMed.Persistence.Validators;

public class OperationValidator : BaseValidator
{

    public OperationValidator(IConfiguration configuration): base(configuration)
    {
    
    }

    public OperationResult isNull<TEntity>(TEntity entity)
    {
        var result = new OperationResult();

        if (entity is null)
        {

            result.Success = false;
            result.Message = $"{entity.ToString} es nulo.";

            return result;
        }
        result.Success = true;
        return result;
    }

    public OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity, string entityName)
    {
        var result = new OperationResult();

        if (entity is string str && string.IsNullOrWhiteSpace(str))
        {
            
            result.Success = false;
            result.Message = $"{entityName} no puede ser nulo ni estar en blanco";
            return result;
        }
        result.Success = true;
        return result;
    }
    public OperationResult IsInt<TEntity>(TEntity entity, int entityName)
    {
        var result = new OperationResult();

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
    public OperationResult CheckDate<TEntity>(TEntity entity, string entityName)
    {
        var result = new OperationResult();

        if (entity is DateTime date && date >= DateTime.Now)
        {
            result.Success = false;
            result.Message = "La fecha debe ser mayor a la fecha actual";
            return result;
        }
        result.Success = true;
        return result;
    }
    public OperationResult Exists<TEntity>(TEntity entity, string entityName, Func<TEntity, bool> searchFunction)
    {
        var result = new OperationResult();

        if (searchFunction(entity))
        {
            result.Success = false;
            result.Message = $"{entityName} ya existe en el sistema";
            return result;
        }
        result.Success = true;
        return result;
    }
    public OperationResult ValidateStringLength(string entityName, int maxLength)
    {
        var result = new OperationResult();

        if (entityName==null && entityName.Length > maxLength)
        {
            result.Success = false;
            result.Message = $"{entityName} no puede tener más de {maxLength} caracteres";
            return result;
        }
        result.Success = true;
        return result;
    }
}