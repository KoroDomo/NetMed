using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Notification;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationContract _notificationContract;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationContract notificationContract,
                                     ILogger<NotificationController> logger)
        {
           _notificationContract = notificationContract;
            _logger = logger;
        }

        [HttpGet("GetAllNotifications")]
        public async Task<IActionResult> Get()
        {
            var notificationsList = await _notificationContract.GetAllDto();
            return Ok(notificationsList);
        }
        
        [HttpPost("CreatedNotifications")]
        public async Task<IActionResult> Post([FromBody] SaveNotificationDto notification)
        {
            var notifications = await _notificationContract.SaveDto(notification);
            return Ok(notifications);

            
        }

       
        [HttpPut("UpdateNotifications")]
        public async Task<IActionResult> Put([FromBody] UpdateNotificationDto notification)
        {
            var notifications = await _notificationContract.UpdateDto(notification);
            return Ok(notifications);
        }

        [HttpDelete("DeleteNotifications")]
        public async Task<IActionResult> Delete([FromBody] int notificationiD)
        {
            var notifications = await _notificationContract.DeleteDto(notificationiD);
            return Ok(notifications);
        }

    }
}
    
