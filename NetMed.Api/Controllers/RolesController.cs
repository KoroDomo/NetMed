using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;
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
                try
                {
                    var roles = await _rolesRepository.GetAllAsync();
                    return Ok(roles);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener todos los roles.");
                    return StatusCode(500, "Ocurrió un error interno al obtener los roles.");
                }
            }

            [HttpGet("GetRoleById")]
            public async Task<IActionResult> GetRoleById(int id)
            {
                try
                {
                    var role = await _rolesRepository.GetEntityByIdAsync(id);

                    if (role == null)
                    {
                        _logger.LogWarning($"Rol con ID {id} no encontrado.");
                        return NotFound();
                    }

                    return Ok(role);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al obtener el rol con ID {id}.");
                    return StatusCode(500, "Ocurrió un error interno al obtener el rol.");
                }
            }

            [HttpPost("CreateRole")]
            public async Task<IActionResult> CreateRole([FromBody] Roles role)
            {
                try
                {
                    if (role == null)
                    {
                        _logger.LogWarning("Intento de crear un rol con datos nulos.");
                        return BadRequest("El rol no puede ser nulo.");
                    }

                    var result = await _rolesRepository.SaveEntityAsync(role);

                    if (!result.Success)
                    {
                        _logger.LogWarning($"Error al crear el rol: {result.Mesagge}");
                        return BadRequest(result.Mesagge);
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear el rol.");
                    return StatusCode(500, "Ocurrió un error interno al crear el rol.");
                }
            }

            [HttpPut("UpdateRole")]
            public async Task<IActionResult> UpdateRole([FromBody] Roles role)
            {
                try
                {
                    if (role == null)
                    {
                        _logger.LogWarning("Intento de actualizar un rol con datos nulos.");
                        return BadRequest("El rol no puede ser nulo.");
                    }

                    var result = await _rolesRepository.UpdateEntityAsync(role);

                    if (!result.Success)
                    {
                        _logger.LogWarning($"Error al actualizar el rol: {result.Mesagge}");
                        return BadRequest(result.Mesagge);
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al actualizar el rol.");
                    return StatusCode(500, "Ocurrió un error interno al actualizar el rol.");
                }
            }

            [HttpDelete("DeleteRole")]
            public async Task<IActionResult> DeleteRole([FromBody] Roles role)
            {
                try
                {
                    if (role == null)
                    {
                        _logger.LogWarning("Intento de eliminar un rol con datos nulos.");
                        return BadRequest("El rol no puede ser nulo.");
                    }

                    var result = await _rolesRepository.DeleteEntityAsync(role);

                    if (!result.Success)
                    {
                        _logger.LogWarning($"Error al eliminar el rol: {result.Mesagge}");
                        return BadRequest(result.Mesagge);
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al eliminar el rol.");
                    return StatusCode(500, "Ocurrió un error interno al eliminar el rol.");
                }
            }
        }
    }
}
        