using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories.Interfaces;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace NetMed.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        private readonly NetMedContext _context;
        public UsersRepository(NetMedContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users> GetEmailAsync(string email)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.FindAsync(email);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Users.FindAsync(email) ?? throw new InvalidOperationException("User not found");

        }

        public async Task<List<Users>> GetActiveUsersAsync(bool isActive = true)
        {
            var result = new OperationResult();
            List<Users> data = new List<Users>();
            try
            {
                data = await _context.Users.Where(x => x.IsActive == isActive).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<Users> GetByRoleAsync(int roleID)
        {
            var result = new OperationResult();

            try
            {
                var data = await _context.Users.Where(x => x.RoleID == roleID).FirstOrDefaultAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return await _context.Users.FindAsync(roleID) ?? throw new InvalidOperationException("User not found");
        }

        public async Task<Users> SearchByNameAsync(string firstName)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.Where(x => x.FirstName.Contains(firstName)).FirstOrDefaultAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return await _context.Users.FindAsync(firstName) ?? throw new InvalidOperationException("User not found");
        }

        public async Task<Users> GetUsersRegisteredInRangeAsync(DateTime startDate, DateTime endDate)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.Where(x => x.RegisterDate >= startDate && x.RegisterDate <= endDate).FirstOrDefaultAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return await _context.Users.FindAsync(startDate, endDate) ?? throw new InvalidOperationException("User not found");
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string passwordHash)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.Where(x => x.Email == email && x.PasswordHash == passwordHash).FirstOrDefaultAsync();
                result.Result = data;
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
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.FindAsync(userId);
                if (data == null)
                {
                    throw new InvalidOperationException("User not found");
                }
                data.PasswordHash = newPasswordHash;
                await _context.SaveChangesAsync();
                result.Result = data;
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
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.FindAsync(userId);
                if (data == null)
                {
                    throw new InvalidOperationException("User not found");
                }
                data.IsActive = false;
                await _context.SaveChangesAsync();
                result.Result = data;
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
            var result = new OperationResult();
            try
            {
                var data = await _context.Users.FindAsync(userId);
                if (data == null)
                {
                    throw new InvalidOperationException("User not found");
                }
                data.RoleID = roleId;
                await _context.SaveChangesAsync();
                result.Result = data;
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