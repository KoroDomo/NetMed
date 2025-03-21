
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Services
{
   public class UsersServices : IUsersServices
    {
        private readonly IUsersRepository _usersRepository;
        public UsersServices(IUsersRepository usersRepository,
            ILogger<UsersServices> logger)
        {
            this._usersRepository = usersRepository;
        }


        public async Task<OperationResult> Add(AddUserDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = new Users
                {
                    FirstName = "Cris",
                    LastName = "Perez",
                    Email = "fsf@rfrg",
                    Password = "3203",
                    RoleID = 2,
                        
                };
                result = await _usersRepository.SaveEntityAsync(user);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error agregando el usuario.";
            }
            return result;
        }

        public async Task<OperationResult> Delete(DeleteUserDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = new Users
                {
                    UserId = 4,
                    FirstName = "Cris",
                    LastName = "Perez",
                    Email = "fsf@rfrg",
                    Password = "3203",
                    RoleID = 2,
                };
                result = await _usersRepository.DeleteEntityAsync(user);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error eliminando el usuario.";
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
            }
            return result;
        }




        public async Task<OperationResult> GetAllData()
        {
           OperationResult result = new OperationResult();
            try
            {
                result = await _usersRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
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
                   UserId = 3,
                    FirstName = "Cris",
                    LastName = "Perez",
                    Email = "fsf@rfrg",
                    Password = "3203",
                    RoleID = 2,
                };
                result = await _usersRepository.UpdateEntityAsync(user);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando el usuario.";
            }
            return result;
        }

    
    }
}
