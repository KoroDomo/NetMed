using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

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
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
            
                result.data = await _context.Users.FindAsync(email);
                result.Success = true;
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
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                
                result.data = await _context.Users.Where(x => x.IsActive == isActive).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetByRoleAsync(int roleID)
        {
            OperationResult result = new OperationResult();

            try
            {
                if(result.data == null)
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
                result.data = await _context.Users.Where(x => x.RegisterDate >= startDate && x.RegisterDate <= endDate).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }
        public async Task<bool> ValidateCredentialsAsync(string email, string passwordHash)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.Email == email && x.PasswordHash == passwordHash).FirstOrDefaultAsync();
        
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return await _context.Users.FindAsync(email, passwordHash) != null;
        }

        public async Task UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            OperationResult result = new OperationResult();
            try
            {
             result.data = await _context.Users.FindAsync(userId);
                if (result.data == null)
                {
                    throw new InvalidOperationException("User not found");
                }
                result.data.PasswordHash = newPasswordHash;
                result.data = await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
            }
            
        }

        public async Task DeactivateUserAsync(int userId)
        {
            OperationResult result = new OperationResult();
            try
            {
            result.data = await _context.Users.FindAsync(userId);
                if (result.data == null)
                {
                    throw new InvalidOperationException("User not found");
                }
                result.data.IsActive = false;
                result.data = await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
            }
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            OperationResult result = new OperationResult();
            try
            {
              result.data = await _context.Users.FindAsync(userId);
                if (result.data == null)
                {
                    throw new InvalidOperationException("User not found");
                }
                result.data.RoleID = roleId;
                
                result.data = await _context.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
            }
        }

    }

}