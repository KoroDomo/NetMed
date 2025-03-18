using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validation.Interfaces;

namespace NetMed.Infrastructure.Validations.Interfaces
{
    public interface IUserValidations  : IOperationValidator
    {
        public OperationResult ValidateUserEmail(Users users);
        public OperationResult ValidateUserPassword(Users users);

        public OperationResult ValidateUserFirstName(Users users);

        public OperationResult ValidateUserLastName(Users users);

        public OperationResult ValidateUsersRoleID(Users users);


    }
}
