using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;
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
        public async Task<IActionResult> Post([FromBody] AppoinmentsDto appointment)
        {
            var saveAppointmentsDto = new SaveAppointmentsDto();

            var appointments = await _appointmentsService.Save(saveAppointmentsDto);
            return Ok(appointments);
        }
        [HttpPost("UpdateAppointment")]
        public async Task<IActionResult> Put([FromBody] AppoinmentsDto appointment)
        {
            var updateAppointmentsDto = new UpdateAppointmentsDto();
            var appointments = await _appointmentsService.Update(updateAppointmentsDto);
            return Ok(appointments);
        }
    }
}
