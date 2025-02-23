using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRespository _appointmentsRespository;

        public AppointmentsController(IAppointmentsRespository appointmentsRespository, ILogger <AppointmentsRepository> logger) 
        {
            _appointmentsRespository = appointmentsRespository;
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
