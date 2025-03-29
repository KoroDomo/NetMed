

using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Base;
using NetMed.Infraestructure.Validator.Interfaz;

namespace NetMed.Infraestructure.Validator.Implementations
{
    public class RolesValidator : BaseValidator<Roles>, IRolesValidator
    {
        private readonly JsonMessage _jsonMessage;
        private readonly ILoggerCustom _loggerCustom;

        public RolesValidator(ILoggerCustom loggerCustom,
               JsonMessage messageMapper) : base(loggerCustom, messageMapper)
        {
            _loggerCustom = loggerCustom;
            _jsonMessage = messageMapper;
        }

        public OperationResult ValidateRoleIsNotNull(Roles roles, string errorMessage)
        {
            if (roles == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NullEntity"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }
        }


        public OperationResult ValidateRoleIsActive(Roles roles, string errorMessage)
        {
            OperationResult result = new OperationResult();

            if (roles.IsActive == false)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["RoleIsNotActive"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }

        }

        public OperationResult ValidateIsEntityIsNull(Roles entity, string errorMessage)
        {
            if (entity == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages[""] };

            }
            else
            {
                return new OperationResult { Success = true };
            }
        }

        public OperationResult ValidateRolesIdIsNegative(int roleId, string errorMessage)
        {
            OperationResult result = new OperationResult();


            if (roleId < 0)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidId"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }


        }

        public OperationResult ValidateRoleNameLength(Roles roles, string errorMessage)
        {
            if (roles.RoleName.Length > 20)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["RolesMessage"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }
        }

        public OperationResult ValidateRoleNameCharacters(Roles roles, string errorMessage)
        {
            string regexPattern = @"^[a-zA-Z0-9_]+$";

            return ValidateWithRegex(roles.RoleName, regexPattern, errorMessage);
        }

        public OperationResult ValidateRoleNameIsNotNull(Roles roles, string erroMessage)
        {
            if (roles.RoleName == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["RolesNull"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }
        }
    }
}
