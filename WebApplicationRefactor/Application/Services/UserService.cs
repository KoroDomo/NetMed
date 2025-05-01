using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Users;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;
using WebApplicationRefactor.Services.Interface;
using WebApplicationRefactor.Application.BaseApp;

namespace WebApplicationRefactor.Services.Service
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<UsersApiModel> _userRepository;
        private readonly ILogger<UsersService> _logger;


        public UsersService(IRepository<UsersApiModel> userRepository, ILogger<UsersService> logger, IErrorMessageService errorMessageService)
        {
            _userRepository = userRepository;
            _logger = logger;
      
        }

        public async Task<OperationResult> GetAllData()
        {
     
        var result = new OperationResult();
            try
            {
                var users = await _userRepository.GetAllAsync();
                result.success = true;
                result.data = users;
            }
            catch (Exception ex)
            {
               
                result.success = false;
                result.message = "Operations, GenericError";
            }
            return result;
        }

        public async Task<OperationResult> GetById(int id)
        {
            var result = new OperationResult();
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided: {Id}", id); 
                result.success = false;
                result.message = "EntityBase, InvalidID";
                return result;
            }
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    result.success = false;
                    result.message = "Entity NotFound";
                    return result;
                }
                result.success = true;
                result.data = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user by ID: {Id}", id); 
                result.success = false;
                result.message = "Operations GenericError";
            }
            return result;
        }

        public async Task<OperationResult> Add(UsersApiModel dto)
        {
            var validationResult = ValidateUser(dto);
            if (!validationResult.success)
            {
                return await Task.FromResult(validationResult);
            }

            var result = new OperationResult();
            try
            {
                await _userRepository.AddAsync(dto);
                result.success = true;
                result.message = "Operations SaveSuccess";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operations, SaveFailed");
                result.success = false;
                result.message = "Operations " + " SaveFailed";
            }
            return result;
        }

        public async Task<OperationResult> Update(UsersApiModel dto)
        {
            var validationResult = ValidateUser(dto);
            if (!validationResult.success)
            {
                return validationResult;
            }

            var result = new OperationResult();
            try
            {
                await _userRepository.UpdateAsync(dto);
                result.success = true;
                result.message = ("Operations" +  " UpdateSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ("Operations"+"UpdateFailed"));
                result.success = false;
                result.message = "Operations"+ "UpdateFailed";
            }
            return result;
        }

        public async Task<OperationResult> Delete(UsersApiModel dto)
        {
       
        var result = new OperationResult();
            try
            {
                await _userRepository.DeleteAsync(dto.Id);
                result.success = true;
                result.message = "Operations" + "DeleteSuccess";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,("Operations" + " DeleteFailed"));
                result.success = false;
                result.message = ("Operations" +  " DeleteFailed");
            }
            return result;
        }

        private OperationResult ValidateUser(dynamic user)
        {
            if (user == null)
            {
                return new OperationResult
                {
                    success = false,
                    message = "User " + 
                    "NullUser"
                };
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return new OperationResult
                {
                    success = false,
                    message = "User" + "EmptyName"
                };
            }

            if (user.Name.Length > 50)
            {
                return new OperationResult
                {
                    success = false,
                    message = "User"+"NameTooLong"
                };
            }

            return new OperationResult { success = true };
        }

   

     

      
    }
}
