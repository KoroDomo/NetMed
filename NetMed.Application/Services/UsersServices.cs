
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Infrastructure.Validations.Implementations;
using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Options;


namespace NetMed.Application.Services
{
    public class UsersServices : UsersValidations, IUsersServices
    {
        private NetMedContext _context;
        private readonly ILogger<UsersServices> _logger;
        private readonly IUsersRepository _usersRepository;
        public UsersServices(IUsersRepository usersRepository,
            NetMedContext context,
            ILogger<UsersServices> logger)
        {
            this._usersRepository = usersRepository;
            this._logger = logger;
            _context = context;

        }





        public async Task<OperationResult> Add(AddUserDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var users = new Users
                {
                    UserId = dto.UserId,
                    FirstName = dto.FirstName ?? string.Empty,
                    LastName = dto.LastName ?? string.Empty,
                    Email = dto.Email,
                    Password = dto.Password ?? string.Empty,
                    RoleID = dto.RoleID
                };

                // Perform validations
                var validationResults = new List<OperationResult>
                {
                    ValidateUserEmail(users),
                    ValidateUserFirstName(users),
                    ValidateUserLastName(users),
                    ValidateUserPassword(users),
                    ValidateUsersRoleID(users)
                };

                // Check if any validation failed
                var failedValidation = validationResults.FirstOrDefault(v => !v.Success);
                if (failedValidation != null)
                {
                    return failedValidation;
                }

                await _usersRepository.SaveEntityAsync(users);

                if (result.data == null)
                {
                    result.Success = false;
                    result.Message = "Error, datos del usuario vacios";
                }
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error agregando el usuario.";
                _logger.LogError(ex, ex.Message);
            }
            result.Success = true;
            return result;
        }

        public async Task<OperationResult> Delete(DeleteUserDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = new Users
                {
                    UserId = dto.UserId,
                    FirstName = dto.FirstName ?? string.Empty,
                    LastName = dto.LastName ?? string.Empty,
                    Email = dto.Email,
                    Password = dto.Password ?? string.Empty,

                };
                result = await _usersRepository.DeleteEntityAsync(user);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error eliminando el usuario.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }



        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = await _usersRepository.GetEntityByIdAsync(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }




        public async Task<OperationResult> GetAllData()
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _usersRepository.GetAllAsync();
                result.data = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

    
    public async Task<OperationResult> Update(UpdateUserDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = new Users
                {
                    UserId = dto.UserId,
                    FirstName = dto.FirstName ?? string.Empty,
                    LastName = dto.LastName ?? string.Empty,
                    Email = dto.Email,
              
                    Password = dto.Password ?? string.Empty,
                };

                // Perform validations
                var validationResults = new List<OperationResult>
                {
                    ValidateUserEmail(user),
                    ValidateUserFirstName(user),
                    ValidateUserLastName(user),
                    ValidateUserPassword(user),
                    ValidateUsersRoleID(user)
                };

                // Check if any validation failed
                var failedValidation = validationResults.FirstOrDefault(v => !v.Success);
                if (failedValidation != null)
                {
                    return failedValidation;
                }

                result = await _usersRepository.UpdateEntityAsync(user);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando el usuario.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

        


    }
}
