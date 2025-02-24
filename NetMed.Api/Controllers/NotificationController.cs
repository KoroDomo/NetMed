using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationRepository notificationRepository,
                                     ILogger<NotificationController> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        [HttpGet("GetAllNotifications")]
        public async Task<IActionResult> Get()
        {
            var notificationsList = await _notificationRepository.GetAllAsync();
            return Ok(notificationsList);
        }

             
        [HttpGet("GetNotificationByIdUsers")]
        public async Task<IActionResult> Get(int userID)
        {
            var notification = await _notificationRepository.GetNotificationsByUserIdAsync(userID);
            return Ok(notification);
        }

        
        [HttpPost("CreatedNotifications")]
        public async Task<IActionResult> Post([FromBody] Notification notification)
        {
            var notifications = await _notificationRepository.CreateNotificationAsync(notification);
            return Ok(notifications);

            
        }

       
        [HttpPut("UpdateNotifications")]
        public async Task<IActionResult> Put([FromBody] Notification notification)
        {
            var notifications = await _notificationRepository.UpdateNotificationAsync(notification);
            return Ok(notifications);
        }

        [HttpDelete("DeleteNotifications")]
        public async Task<IActionResult> Delete([FromBody] int notificationId)
        {
            var notifications = await _notificationRepository.DeleteNotificationAsync(notificationId);
            return Ok(notifications);
        }

    }
}
    
