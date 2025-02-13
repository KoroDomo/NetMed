
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Repositories.Interfaces
{
   public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<Users> GetEmailAsync(string email);

        Task<List<Users>> GetActiveUsersAsync(bool isActive = true);

        Task<Users> GetByRoleAsync(int roleID);

        Task<Users> SearchByNameAsync(string firstName);

        Task<Users> GetUsersRegisteredInRangeAsync(DateTime startDate, DateTime endDate);

        Task UpdatePasswordAsync(int userId, string newPasswordHash);
        Task<bool> ValidateCredentialsAsync(string email, string passwordHash);
        Task DeactivateUserAsync(int userId);
        Task AssignRoleAsync(int userId, int roleId);


    }   
}
