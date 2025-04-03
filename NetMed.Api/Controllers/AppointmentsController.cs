using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
      
        public AppointmentsController(IAppointmentsService appointmentsService) 
        {
            _appointmentsService = appointmentsService;
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
        public async Task<IActionResult> Remove(RemoveAppointmentsDto removeAppointmentsDto)
        {
            var appointments = await _appointmentsService.Remove(removeAppointmentsDto);
            return Ok(appointments);
        }
    }
}
