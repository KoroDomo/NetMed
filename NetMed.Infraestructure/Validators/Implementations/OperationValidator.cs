using NetMed.Domain.Base;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators.Interfaces;
using System.Text.RegularExpressions;

namespace NetMed.Infraestructure.Validators.Implementations
{
    public class OperationValidator : BaseValidator, IOperationValidator
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled
        );
        private static readonly Regex PhoneRegex = new Regex(
            @"^\d{10}$", // 10 dígitos exactos
            RegexOptions.Compiled
        );
        public OperationValidator()
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
            result.Result = entity;
            result.Success = true;
            return result;
        }
        public OperationResult isNull<TEntity>(TEntity entity, ICustomLogger customLogger)
        {
            var result = new OperationResult();

            if (entity is null)
            {

                result.Success = false;
                result.Message = $"{entity.ToString} es nulo.";
                customLogger.LogWarning("No se encontraron registros.");

                return result;
            }
            result.Result = entity;
            result.Success = true;
            return result;
        }

        public OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity, string entityName)
        {
            var result = new OperationResult();

            if (entity is string str && string.IsNullOrWhiteSpace(str))
            {

                result.Success = false;
                result.Message = $"{entityName} no puede ser nulo ni estar en blanco.";
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
                    result.Message = $"{entityName} debe ser mayor que cero.";
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
                result.Message = "La fecha debe ser mayor a la fecha actual.";
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
                result.Message = $"{entityName} ya existe en el sistema.";
                return result;
            }
            result.Success = true;
            return result;
        }
        public OperationResult ValidateStringLength(string entityName, int maxLength)
        {
            var result = new OperationResult();

            if (entityName.Length > maxLength)
            {
                result.Success = false;
                result.Message = $"{entityName} no puede tener más de {maxLength} caracteres.";
                return result;
            }
            result.Success = true;
            return result;
        }

        public OperationResult IsValidEmail(string email)
        {
            var result = new OperationResult();
            if (EmailRegex.IsMatch(email) == false)
            {
                result.Success = false;
                result.Message = "El correo electronico no es valido.";
                return result;
            }
            result.Success = true;
            return result;
        }

        public OperationResult IsValidPhoneNumber(string phoneNumber)
        {
            var result = new OperationResult();
            if (PhoneRegex.IsMatch(phoneNumber) == false)
            {
                result.Success = false;
                result.Message = "El numero de telefono no es valido.";
                return result;
            }
            result.Success = true;
            return result;

        }

    }
}