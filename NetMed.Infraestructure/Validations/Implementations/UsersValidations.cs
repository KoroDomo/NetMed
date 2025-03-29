
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validations.Implementations;
using NetMed.Infrastructure.Validations.Interfaces;

namespace NetMed.Infrastructure.Validations.Implementations
{
 public class UsersValidations : OperationValidator, IUserValidations
    {
        public OperationResult ValidateUserEmail(Users users)
        {
          OperationResult result = new OperationResult();

            if (string.IsNullOrEmpty(users.Email) ||(string.IsNullOrWhiteSpace(users.Email)))
            {
                result.Success = false;
                result.Message = "Invalid Email";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUserFirstName(Users users)
        {
            OperationResult result = new OperationResult();
            if (string.IsNullOrEmpty(users.FirstName) || (string.IsNullOrWhiteSpace(users.FirstName)))
            {
                result.Success = false;
                result.Message = "Invalid First Name";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUserLastName(Users users)
        {

            OperationResult result = new OperationResult();
            if (string.IsNullOrEmpty(users.LastName) || (string.IsNullOrWhiteSpace(users.LastName)))
            {
                result.Success = false;
                result.Message = "Invalid Last Name";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUserPassword(Users users)
        {
            OperationResult result = new OperationResult();
            if (string.IsNullOrEmpty(users.Password) || (string.IsNullOrWhiteSpace(users.Password)) && (users.Password.Length < 4))
            {
                result.Success = false;
                result.Message = "Invalid Password";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUsersRoleID(Users users)
        {
            OperationResult result = new OperationResult();
            if (users.RoleID > 1 && users.RoleID > 3)
            {
                result.Success = false;
                result.Message = "Invalid RoleID";
                return result;
            }
            return result;
        }
    }
}
