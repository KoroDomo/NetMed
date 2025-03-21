using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
<<<<<<< HEAD
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;

=======
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.Extensions.Configuration;
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4

namespace NetMed.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<AddUsersDto>, IUsersRepository
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
<<<<<<< HEAD
                    result.Success = true;
                    result.Message = "Correo disponible";
                }
                else
                {
                    result.data = user;
                    result.Success = false;
                    result.Message = "Email ya registrado";
=======
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4
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
<<<<<<< HEAD
=======
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
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4
            }
            return result;
        }


<<<<<<< HEAD

=======
            try
            {
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetPhoneNumberAsync"];
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
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4

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
        public override async Task<OperationResult> GetAllAsync(Expression<Func<Users,bool>> filter)
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
<<<<<<< HEAD
=======
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["GetPasswordAsync"];
            }
            return result;
        }
        public override async Task<OperationResult> GetAllAsync(Expression<Func<AddUsersDto, bool>> filter)
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
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4
                result.Message = ex.Message + _repErrorMapper.GetEntityByIdErrorMessage["GetEntityByIdError"];
            }
            return result;
        }


<<<<<<< HEAD
        public override async Task<bool> ExistsAsync(Expression<Func<Users, bool>>filter)
=======
        public override async Task<bool> ExistsAsync(Expression<Func<AddUsersDto, bool>> filter)
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4
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
                result.Message = ex.Message + _repErrorMapper.GetAllEntitiesErrorMessage["GetAllEntitiesError"];
                _logger.LogError(_repErrorMapper.GetAllEntitiesErrorMessage["GetAllEntitiesError"] + ex.Message.ToString());
            }
          
            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(AddUsersDto users)
        {
            OperationResult result = new OperationResult();
            try
            {

                if (users == null)
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    return result;
                }
                users.UserId = 0;
                _context.Users.Add(users);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["SaveEntityAsync"];
                result.Success = false;
                _logger.LogError(ex, _repErrorMapper.ErrorUsersRepositoryMessages["SaveEntityAsync"]);
            }

            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(AddUsersDto entity)
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
                result.Message = ex.Message + _repErrorMapper.ErrorUsersRepositoryMessages["UpdateEntityAsync"];
                result.Success = false;
            }
            return result;
        }
    }
}