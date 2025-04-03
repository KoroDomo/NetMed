using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Roles;
using NetMed.Domain.Entities;

namespace NetMed.Api.Controllers
{
    namespace NetMed.Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class RolesController : ControllerBase
        {
            private readonly IRolesContract _rolesContract;
            private readonly ILogger<RolesController> _logger;

            public RolesController(IRolesContract rolesContract,
                                   ILogger<RolesController> logger)
            {
                _rolesContract = rolesContract;
                _logger = logger;
            }

            [HttpGet("GetAllRoles")]
            public async Task<IActionResult> GetAllRoles()
            {

                var rolesList = await _rolesContract.GetAllDto();
                return Ok(rolesList);
            }


            [HttpGet("GetRolesById")]
            public async Task<IActionResult> Get( int roles)
            {
                var rolesList = await _rolesContract.GetDtoById(roles);
                return Ok(rolesList);
            }


            [HttpPost("CreateRole")]
            public async Task<IActionResult> Post([FromBody] SaveRolesDto role)
            {

                var roles = await _rolesContract.SaveDto(role);
                return Ok(roles);

            }

            [HttpPut("UpdateRole")]
            public async Task<IActionResult> Put([FromBody] UpdateRolesDto role)
            {
                var roles = await _rolesContract.UpdateDto(role);
                return Ok(roles);
            }

            [HttpDelete("DeleteRole")]
            public async Task<IActionResult> DeleteRole([FromBody] Roles role)
            {
                var roles = await _rolesContract.DeleteDto(role.Id);
                return Ok(roles);
            }
        }
    }
}
        