using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Status;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;

    namespace NetMed.Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class StatusController : ControllerBase
        {
            private readonly IStatusContract _statusContract;
            private readonly ILogger<StatusController> _logger;

            public StatusController(IStatusContract statusContract,
                                    ILogger<StatusController> logger)
            {
                _statusContract = statusContract;
                _logger = logger;
            }

            [HttpGet("GetAllStatus")]
            public async Task<IActionResult> GetAllStatus()
            {
                    var statusList = await _statusContract.GetAllDto();
                    return Ok(statusList);
           
            }

            [HttpPost("CreateStatus")]
            public async Task<IActionResult> CreateStatus([FromBody] SaveStatusDto status)
            {
              
                    var statu = await _statusContract.SaveDto(status);
                    return Ok(statu);
               
            }

            [HttpPut("UpdateStatus")]
            public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto status)
            {

            var result = await _statusContract.UpdateDto(status);
                    return Ok(result);
                
                
            }

            [HttpDelete("DeleteStatus")]
            public async Task<IActionResult> DeleteStatus([FromBody] int  statusId)
            {
               
                    var result = await _statusContract.DeleteDto(statusId);
                    return Ok(result);
                
            }
        }
    }
