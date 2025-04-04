using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Patients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsServices _patientsServices;
        private readonly ILogger<PatientsController> _logger;
        public PatientsController(IPatientsServices patientsServices,
            ILogger<PatientsController> logger)
        {
            _patientsServices = patientsServices;
            _logger = logger;
        }
        // GET: api/<PatientsController>
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
           var patients = await _patientsServices.GetAllData();
            if (patients.Success == true)
            {
                return Ok(patients.data);
            }
            return Ok();
        }

        // GET api/<PatientsController>/5
        [HttpGet("GetPatientsById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _patientsServices.GetById(id);
            return Ok(patient);
        }


        // POST api/<PatientsController>
        [HttpPost("SavePatients")]
        public async Task<IActionResult> Post([FromBody] AddPatientDto patientsDto)
        {
            try
            {
                var pacient = await _patientsServices.Add(patientsDto);
                if (pacient.Success)
                {
                    return Ok(pacient);
                }
                else
                {
                    return BadRequest(pacient.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the patient.");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/<PatientsController>/5
        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> Put([FromBody] UpdatePatientDto patientsDto)

        {
            try
            {
                var pacients = await _patientsServices.Update(patientsDto);
                if (pacients.Success != true)
                {
                    return BadRequest(pacients.Message);
                }

                return Ok(pacients.data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the patient.");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> DeleteAsync(DeletePatientDto patientsDto)
        {
            var doc = await _patientsServices.Delete(patientsDto);
            return Ok(doc);
        }
    }
}
