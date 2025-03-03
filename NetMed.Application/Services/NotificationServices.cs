using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Notification;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Domain.Base;
using NetMed.Persistence.Context;


namespace NetMed.Application.Services
{
    public class NotificationServices : INotificationContract
    {

        private readonly NetmedContext _context;
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationServices> _logger;
        private readonly IConfiguration _configuration;

        public  NotificationServices(NetmedContext context,INotificationRepository notificationRepository,
                                                           ILogger<NotificationServices> logger,
                                                           IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _notificationRepository = notificationRepository;

        }

        public Task<OperationResult> DeleteDto(DeleteNotificationDto dtoDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<OperationResult>> GetAllDto()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetDtoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SaveDto(SaveNotificationDto dtoSave)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateDto(UpdateNotificationDto dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
