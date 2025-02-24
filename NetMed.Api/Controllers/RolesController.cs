using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    namespace NetMed.Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class RolesController : ControllerBase
        {
            private readonly IRolesRepository _rolesRepository;
            private readonly ILogger<RolesController> _logger;

            public RolesController(IRolesRepository rolesRepository,
                                   ILogger<RolesController> logger)
            {
                _rolesRepository = rolesRepository;
                _logger = logger;
            }

            [HttpGet("GetAllRoles")]
            public async Task<IActionResult> GetAllRoles()
            {

                var rolesList = await _rolesRepository.GetAllAsync();
                return Ok(rolesList);
            }

            [HttpGet("GetRoleById")]
            public async Task<IActionResult> GetRoleById(int rolesID)
            {
                var roles = await _rolesRepository.GetRoleByIdAsync(rolesID);
                return Ok(roles);
            }
            

            [HttpPost("CreateRole")]
            public async Task<IActionResult> Post([FromBody] Roles role)
            {

                var roles = await _rolesRepository.CreateRoleAsync(role);
                return Ok(roles);

            }

            [HttpPut("UpdateRole")]
            public async Task<IActionResult> Put([FromBody] Roles role)
            {


                var roles = await _rolesRepository.UpdateRoleAsync(role);
                return Ok(roles);


            }

            [HttpDelete("DeleteRole")]
            public async Task<IActionResult> DeleteRole([FromBody] int roleId)
            {
                var roles = await _rolesRepository.DeleteRoleAsync(roleId);
                return Ok(roles);
            }
        }
    }
}
        