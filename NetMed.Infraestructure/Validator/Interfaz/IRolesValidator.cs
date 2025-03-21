

using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Validator.Interfaces;

namespace NetMed.Infraestructure.Validator.Interfaz
{
    public interface IRolesValidator : IBaseValidator<Roles>
    {
       OperationResult ValidateRoleIsNotNull(Roles roles, string erroMessage);

        OperationResult ValidateRoleNameIsNotNull(Roles roles, string erroMessage);

        OperationResult ValidateRoleNameLength(Roles roles,  string errorMessage);
       OperationResult ValidateRoleNameCharacters(Roles roles, string errorMessage);
       OperationResult ValidateRoleIsActive(Roles roles, string errorMessage);
        OperationResult ValidateRolesIdIsNegative(int roleId, string errorMessage);
    }
}
