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
using Microsoft.Extensions.Configuration;
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
using System.Linq;


namespace NetMed.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<UsersRepository> _logger;
        private readonly IRepErrorMapper _repErrorMapper;


        public UsersRepository(NetMedContext context,
         ILogger<UsersRepository> logger, IRepErrorMapper repErrorMapper) : base(context)
        {
            _context = context;
            _logger = logger;
            _repErrorMapper = repErrorMapper;
        }


        public async Task<OperationResult> GetEmailAsync(string email)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    result.Success = true;
                    result.Message = "Correo disponible";
                }
                else
                {

                    result.Success = false;
                    result.Message = "Email ya registrado";

                    var userMail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                    if (user == null)
                    {
                        result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                        result.Success = false;
                    }
                    else
                    {
                        result.data = userMail;
                        result.Success = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetEmailAsync"];
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
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
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
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetActiveUsersAsync"];
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
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.RoleID == roleID).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetByRoleByIDAsync"];
            }
            return result;
        }

        public async Task<OperationResult> SearchByNameAsync(string firstName, string lastName)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.FirstName == firstName && x.LastName == lastName).FirstOrDefaultAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["SearchByNameAsync"];
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
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetUsersRegisteredInRangeAsync"];
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
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.Address == address).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetAddressAsync"];
            }
            return result;
        }


        public async Task<OperationResult> GetPasswordAsync(string password)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Password == password);

                if (user != null)
                {

                    result.data = user;
                    result.Success = true;
                    result.Message = "Contraseña válida";
                }
                else
                {

                    result.Success = false;
                    result.Message = "Contraseña no encontrada";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetPasswordAsync"];
            }
            return result;
        }
        public override async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                var consult = await _context.Users.ToListAsync();
                result.data = consult;
                result.Success = true; // Add this since it was missing
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.GetAllEntitiesErrorMessage["GetAllEntitiesError"];
                _logger.LogError(_repErrorMapper.GetAllEntitiesErrorMessage["GetAllEntitiesError"] + ex.Message.ToString());
            }
            return result;
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Users, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Set<Users>().Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.GetAllEntitiesErrorMessage["GetAllEntitiesError"];
            }
            return result;
        }

        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.data = user;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.GetEntityByIdErrorMessage["GetEntityByIdError"];
            }
            return result;
        }

    

public override async Task<OperationResult> SaveEntityAsync(Users user)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (user == null)
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    return result;
                }

             

                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + _repErrorMapper.SaveEntityErrorMessage["SaveEntityError"];
                result.Success = false;
                _logger.LogError(ex, "Error while saving User.");
            }

            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Users user)
        {
            OperationResult result = new OperationResult();
            try
            {
                var existingUser = await _context.Users.FindAsync(user.UserId);
                if (existingUser == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    // Update properties
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;
                    existingUser.RoleID = user.RoleID;
                    existingUser.Address = user.Address;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.IsActive = user.IsActive;
                    existingUser.UpdatedAt = DateTime.UtcNow;

                    _context.Users.Update(existingUser);
                    await _context.SaveChangesAsync();

                    result.data = existingUser; // Return the updated entity
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["UpdateEntityAsync"];
                result.Success = false;
            }
            return result;
        }

    }
}