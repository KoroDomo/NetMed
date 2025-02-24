using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;

    namespace NetMed.Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class StatusController : ControllerBase
        {
            private readonly IStatusRepository _statusRepository;
            private readonly ILogger<StatusController> _logger;

            public StatusController(IStatusRepository statusRepository,
                                    ILogger<StatusController> logger)
            {
                _statusRepository = statusRepository;
                _logger = logger;
            }

            [HttpGet("GetAllStatus")]
            public async Task<IActionResult> GetAllStatus()
            {
                    var statusList = await _statusRepository.GetAllAsync();
                    return Ok(statusList);
           
            }

        [HttpGet("GetStatusById")]
        public async Task<IActionResult> GetStatusById(int id)
        {

            var status = await _statusRepository.GetStatusByIdAsync(id);
            return Ok(status);

        }

            [HttpPost("CreateStatus")]
            public async Task<IActionResult> CreateStatus([FromBody] Status status)
            {
              
                    var statu = await _statusRepository.CreateStatusAsync(status);
                    return Ok(statu);
               
            }

            [HttpPut("UpdateStatus")]
            public async Task<IActionResult> UpdateStatus([FromBody] Status status)
            {

            var result = await _statusRepository.UpdateStatusAsync(status);
                    return Ok(result);
                
                
            }

            [HttpDelete("DeleteStatus")]
            public async Task<IActionResult> DeleteStatus([FromBody] int  statusId)
            {
               
                    var result = await _statusRepository.DeleteStatusAsync(statusId);
                    return Ok(result);
                
            }
        }
    }
