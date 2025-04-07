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
        private readonly ILoggerManger<UsersService> _logger;
        private readonly IErrorMessageService _errorMessageService;

        public UsersService(IRepository<UsersApiModel> userRepository, ILoggerManger<UsersService> logger, IErrorMessageService errorMessageService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _errorMessageService = errorMessageService;
        }

        public async Task<OperationResult> GetAll()
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
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "GenericError"));
                result.success = false;
                result.message = _errorMessageService.GetErrorMessage("Operations", "GenericError");
            }
            return result;
        }

        public async Task<OperationResult> GetById(int id)
        {
            var result = new OperationResult();
            if (id <= 0)
            {
                _logger.LogWarning(_errorMessageService.GetErrorMessage("EntityBase", "InvalidID"));
                result.success = false;
                result.message = _errorMessageService.GetErrorMessage("EntityBase", "InvalidID");
                return result;
            }
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError(new Exception(_errorMessageService.GetErrorMessage("Entity", "NotFound")), _errorMessageService.GetErrorMessage("Entity", "NotFound"));
                    result.success = false;
                    result.message = _errorMessageService.GetErrorMessage("Entity", "NotFound");
                    return result;
                }
                result.success = true;
                result.data = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "GenericError"));
                result.success = false;
                result.message = _errorMessageService.GetErrorMessage("Operations", "GenericError");
            }
            return result;
        }

        public async Task<OperationResult> Save(UsersApiModel dto)
        {
            var validationResult = ValidateUser(dto);
            if (!validationResult.success)
            {
                return validationResult;
            }

            var result = new OperationResult();
            try
            {
                await _userRepository.AddAsync(dto);
                result.success = true;
                result.message = _errorMessageService.GetErrorMessage("Operations", "SaveSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "SaveFailed"));
                result.success = false;
                result.message = _errorMessageService.GetErrorMessage("Operations", "SaveFailed");
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
                result.message = _errorMessageService.GetErrorMessage("Operations", "UpdateSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "UpdateFailed"));
                result.success = false;
                result.message = _errorMessageService.GetErrorMessage("Operations", "UpdateFailed");
            }
            return result;
        }

        public async Task<OperationResult> Remove(UsersApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                await _userRepository.DeleteAsync(dto.Id);
                result.success = true;
                result.message = _errorMessageService.GetErrorMessage("Operations", "DeleteSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "DeleteFailed"));
                result.success = false;
                result.message = _errorMessageService.GetErrorMessage("Operations", "DeleteFailed");
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
                    message = _errorMessageService.GetErrorMessage("User", "NullUser")
                };
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return new OperationResult
                {
                    success = false,
                    message = _errorMessageService.GetErrorMessage("User", "EmptyName")
                };
            }

            if (user.Name.Length > 50)
            {
                return new OperationResult
                {
                    success = false,
                    message = _errorMessageService.GetErrorMessage("User", "NameTooLong")
                };
            }

            return new OperationResult { success = true };
        }

    }
}
