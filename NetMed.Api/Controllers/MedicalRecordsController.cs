using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;

        public MedicalRecordsController(IMedicalRecordsRepository medicalRecordsRepository, ILogger<MedicalRecordsRepository> logger) 
        {
            _medicalRecordsRepository = medicalRecordsRepository;
        }

        [HttpGet("GetMedicalRecords")]
        public async Task<IActionResult> Get()
        {
            var medical = await _medicalRecordsRepository.GetAllAsync();
            return Ok(medical);
        }

        [HttpGet("GetMedicalRecordsById")]
        public async Task<IActionResult> Get(short id)
        {
            var medical = await _medicalRecordsRepository.GetEntityByIdAsync(id);
            return Ok(medical);
        }

        [HttpPost("SaveMedicalRecords")]
        public async Task<IActionResult> Post([FromBody] MedicalRecords record)
        {
            var records = await _medicalRecordsRepository.SaveEntityAsync(record);
            return Ok(records);
        }

        [HttpPost("UpdateMedicalRecords")]
        public async Task<IActionResult> Put([FromBody] MedicalRecords record)
        {
            var records = await _medicalRecordsRepository.SaveEntityAsync(record);
            return Ok(records);
        }

    }
}
