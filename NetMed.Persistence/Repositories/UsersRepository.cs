using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NetMed.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<UsersRepository> _logger;
        public UsersRepository(NetMedContext context,
         ILogger<UsersRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> GetEmailAsync(string email)
        {
            OperationResult result = new OperationResult();
            try
            {
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                    if (user == null)
                    {
                        result.Message = "No se encontraron datos";
                        result.Success = false;
                    }
                    else
                    {
                        result.data = user;
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> GetActiveUsersAsync(bool isActive = true)
        {
            OperationResult result = new OperationResult();

            try
            {
                var users = await _context.Users.Where(x => x.IsActive == isActive).ToListAsync();
                if (users == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                else
                {
                    result.data = users;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetByRoleByIDAsync(int roleID)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.RoleID == roleID).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> SearchByNameAsync(string firstName)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.FirstName.Contains(firstName)).FirstOrDefaultAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }
        public async Task<OperationResult> GetUsersRegisteredInRangeAsync(DateTime startDate, DateTime endDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetPhoneNumberAsync(string phoneNumber)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> GetAddressAsync(string address)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.Address == address).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> GetUserPassword(string password)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.Password == password).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Users, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Users.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;

        }

        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.FindAsync(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public override async Task<bool> ExistsAsync(Expression<Func<Users, bool>> filter)
        {
            return await _context.Users.AnyAsync(filter);
        }

        public override async Task<OperationResult> GetAllAsync()
        {

            OperationResult result = new OperationResult();
            try
            {

                var consult = await _context.Users.ToListAsync();

                result.data = consult;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError("Error obteniendo los datos" + ex.Message.ToString());
            }

            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(Users users)
        {
            OperationResult result = new OperationResult();
            try

            {
                if (users == null)
                {
                    result.Success = false;
                    result.Message = "User data is null.";
                    return result;
                }
                _context.Users.Add(users);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
                result.Success = false;
                _logger.LogError(ex, " error while saving user. ");
            }

            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Users entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
                result.Success = false;
            }
            return result;
        }

    }
}