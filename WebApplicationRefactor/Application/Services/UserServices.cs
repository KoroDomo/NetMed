using System.Security.Cryptography.X509Certificates;
using NetMed.WebApplicationRefactor.Persistence.Interfaces;
using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Users;

namespace WebApplicationRefactor.Application.Services
{
    public class UserServices : IUsersSevice
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UserServices> _logger;
        public UserServices(IUsersRepository usersRepository, ILogger<UserServices> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<OperationResult> Add(UsersApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                var user = await _usersRepository.Add(dto);
                result.Success = true;
                result.data = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                result.Success = false;
                result.message = "Error adding user";
            }
            return result;
        }



public async Task<OperationResult> Delete(UsersApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                var user = await _usersRepository.Delete(dto);
                result.Success = true;
                result.data = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                result.Success = false;
                result.message = "Error deleting user";
            }
            return result;
        }
 

        public async Task<OperationResult> GetAllData()
        {
            var result = new OperationResult();
            try
            {
                var users = await _usersRepository.GetAllData();
                result.Success = true;
                result.data = users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all users");
                result.Success = false;
                result.message = "Error fetching all users";
            }
            return result;
        }
   

        public async Task<OperationResult> GetById(int id)
        {
            var result = new OperationResult();
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID");
                result.Success = false;
                result.message = "Invalid ID";
                return result;
            }
            try
            {
                var user = await _usersRepository.GetById(id);
                if (user == null)
                {
                    _logger.LogError("User not found");
                    result.Success = false;
                    result.message = "User not found";
                    return result;
                }
                result.Success = true;
                result.data = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user by ID");
                result.Success = false;
                result.message = "Error fetching user by ID";
            }
            return result;
        }


        public async Task<OperationResult> Update(UsersApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                var user = await _usersRepository.Update(dto);
                result.Success = true;
                result.data = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                result.Success = false;
                result.message = "Error updating user";
            }
            return result;
        }
     
    }
}
