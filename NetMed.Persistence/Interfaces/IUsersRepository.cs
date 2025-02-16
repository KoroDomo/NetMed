using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<OperationResult> GetEmailAsync(string email);

        Task<OperationResult> GetActiveUsersAsync(bool isActive = true);

        Task<OperationResult> GetByRoleAsync(int roleID);

        Task<OperationResult> SearchByNameAsync(string firstName);

        Task<OperationResult> GetUsersRegisteredInRangeAsync(DateTime startDate, DateTime endDate);

        Task UpdatePasswordAsync(int userId, string newPasswordHash);
        Task<bool> ValidateCredentialsAsync(string email, string passwordHash);
        Task DeactivateUserAsync(int userId);
        Task AssignRoleAsync(int userId, int roleId);


    }
}
