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

        [HttpGet("GetNotifications")]
        public async Task<IActionResult> Get()
        {
            var notifications = await _notificationRepository.GetAllAsync();
            return Ok(notifications);
        }

     
        [HttpGet("GetNotificationById")]
        public async Task<IActionResult> Get(int id)
        {
            var notification = await _notificationRepository.GetEntityByIdAsync(id);

            if (notification == null)
            {
                _logger.LogWarning($"Notificacion con ID {id} no encontrado.");
                return NotFound();
            }

            return Ok(notification);
        }

        
        [HttpPost("SaveNotifications")]
        public async Task<IActionResult> Post([FromBody] Notification notification)
        {
            var notifications = await _notificationRepository.SaveEntityAsync(notification);
            return Ok(notifications);

            
        }

       
        [HttpPut("UpdateNotifications")]
        public async Task<IActionResult> Put([FromBody] Notification notification)
        {
            var notifications = await _notificationRepository.UpdateEntityAsync(notification);
            return Ok(notifications);
        }

     
        [HttpDelete("(DeleteNotifications)")]
        public async Task<IActionResult> Delete([FromBody] Notification notification)
        {
            var notifications = await _notificationRepository.DeleteEntityAsync(notification);
            return Ok(notifications);
        }
    }
}
    
