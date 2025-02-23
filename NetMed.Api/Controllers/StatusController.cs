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
                try
                {
                    var statusList = await _statusRepository.GetAllAsync();
                    return Ok(statusList);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener todos los estados.");
                    return StatusCode(500, "Ocurrió un error interno al obtener los estados.");
                }
            }

            [HttpGet("GetStatusById")]
            public async Task<IActionResult> GetStatusById(int id)
            {
                try
                {
                    var status = await _statusRepository.GetEntityByIdAsync(id);

                    if (status == null)
                    {
                        _logger.LogWarning($"Estado con ID {id} no encontrado.");
                        return NotFound();
                    }

                    return Ok(status);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al obtener el estado con ID {id}.");
                    return StatusCode(500, "Ocurrió un error interno al obtener el estado.");
                }
            }

            [HttpPost("CreateStatus")]
            public async Task<IActionResult> CreateStatus([FromBody] Status status)
            {
                try
                {
                    if (status == null)
                    {
                        _logger.LogWarning("Intento de crear un estado con datos nulos.");
                        return BadRequest("El estado no puede ser nulo.");
                    }

                    var result = await _statusRepository.SaveEntityAsync(status);

                    if (!result.Success)
                    {
                        _logger.LogWarning($"Error al crear el estado: {result.Mesagge}");
                        return BadRequest(result.Mesagge);
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear el estado.");
                    return StatusCode(500, "Ocurrió un error interno al crear el estado.");
                }
            }

            [HttpPut("UpdateStatus")]
            public async Task<IActionResult> UpdateStatus([FromBody] Status status)
            {
                try
                {
                    if (status == null)
                    {
                        _logger.LogWarning("Intento de actualizar un estado con datos nulos.");
                        return BadRequest("El estado no puede ser nulo.");
                    }

                    var result = await _statusRepository.UpdateEntityAsync(status);

                    if (!result.Success)
                    {
                        _logger.LogWarning($"Error al actualizar el estado: {result.Mesagge}");
                        return BadRequest(result.Mesagge);
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al actualizar el estado.");
                    return StatusCode(500, "Ocurrió un error interno al actualizar el estado.");
                }
            }

            [HttpDelete("DeleteStatus")]
            public async Task<IActionResult> DeleteStatus([FromBody] Status status)
            {
                try
                {
                    if (status == null)
                    {
                        _logger.LogWarning("Intento de eliminar un estado con datos nulos.");
                        return BadRequest("El estado no puede ser nulo.");
                    }

                    var result = await _statusRepository.DeleteEntityAsync(status);

                    if (!result.Success)
                    {
                        _logger.LogWarning($"Error al eliminar el estado: {result.Mesagge}");
                        return BadRequest(result.Mesagge);
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al eliminar el estado.");
                    return StatusCode(500, "Ocurrió un error interno al eliminar el estado.");
                }
            }
        }
    }
