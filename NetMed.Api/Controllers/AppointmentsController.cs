using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IMessageService _messageService;
        private readonly ILoggerSystem _logger;

        public AppointmentsController(IAppointmentsService appointmentsService, ILoggerSystem logger, IMessageService messageService) 
        {
            _appointmentsService = appointmentsService;
            _messageService = messageService;
            _logger = logger;
        }
        [HttpGet("GetAppointment")]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentsService.GetAll();
            return Ok(appointments); 
        }
        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> Get(int id)
        {
            var appointments = await _appointmentsService.GetById(id);
            return Ok(appointments);
        }
        [HttpPost("SaveAppointement")]
        public async Task<IActionResult> Post(SaveAppointmentsDto saveAppointmentsDto)
        {
            var appointments = await _appointmentsService.Save(saveAppointmentsDto);
            return Ok(appointments);
        }
        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> Put(UpdateAppointmentsDto updateAppointmentsDto)
        {
            var appointments = await _appointmentsService.Update(updateAppointmentsDto);
            return Ok(appointments);
        }
        [HttpDelete("RemoveAppointment")]
        public async Task<IActionResult> Remove(int id)
        {
            var appointments = await _appointmentsService.Remove(id);
            return Ok(appointments);
        }
    }
}
