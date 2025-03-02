using System.Globalization;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;


namespace NetMed.Persistence.Interfaces
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<OperationResult> GetEmailAsync(string email);

        Task<OperationResult> GetActiveUsersAsync(bool isActive = true);

        Task<OperationResult> GetByRoleByIDAsync(int roleID);

        Task<OperationResult> SearchByNameAsync(string firstName);

        Task<OperationResult> GetUsersRegisteredInRangeAsync(DateTime startDate, DateTime endDate);

        Task<OperationResult> GetPhoneNumberAsync(string phoneNumber);

        Task<OperationResult> GetAddressAsync(string address);
      
    }
}
