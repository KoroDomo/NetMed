using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Persistence.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRespository _appointmentsRespository;
        private readonly IMessageService _messageService;
        private readonly ILoggerSystem _logger;

        public AppointmentsController(IAppointmentsRespository appointmentsRespository, ILoggerSystem logger, IMessageService messageService) 
        {
            _appointmentsRespository = appointmentsRespository;
            _messageService = messageService;
            _logger = logger;
        }
        [HttpGet("GetAppointment")]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentsRespository.GetAllAsync();
            return Ok(appointments); 
        }
        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> Get(int id)
        {
            var appointments = await _appointmentsRespository.GetEntityByIdAsync(id);
            return Ok(appointments);
        }
        [HttpPost("SaveAppointement")]
        public async Task<IActionResult> Post([FromBody] Appointments appointment)
        {
            var appointments = await _appointmentsRespository.SaveEntityAsync(appointment);
            return Ok(appointments);
        }
        [HttpPost("UpdateAppointment")]
        public async Task<IActionResult> Put([FromBody] Appointments appointment)
        {
            var appointments = await _appointmentsRespository.UpdateEntityAsync(appointment);
            return Ok(appointments);
        }
    }
}
