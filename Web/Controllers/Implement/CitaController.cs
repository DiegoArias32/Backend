using Business.Interfaces;
using Entity.DTOs.CitaDto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implement
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Citas")]
    public class CitaController : BaseController<CitaDto>, ICitaController
    {
        private readonly ICitaBusiness _citaBusiness;

        public CitaController(ICitaBusiness citaBusiness) : base(citaBusiness)
        {
            _citaBusiness = citaBusiness;
        }

        protected override object GetEntityId(CitaDto entity)
        {
            return entity.Id;
        }

        [HttpPatch("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateCitaAsync([FromBody] UpdateCitaDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest(new { message = "Los datos de la cita no pueden ser nulos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _citaBusiness.UpdateCitaAsync(entity);
                return Ok(new { success = result, message = "Cita actualizada exitosamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("numero/{numeroCita}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CitaDto>> GetByNumeroCitaAsync(string numeroCita)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCita))
                    return BadRequest(new { message = "El número de cita no puede estar vacío" });

                var result = await _citaBusiness.GetByNumeroCitaAsync(numeroCita);

                if (result == null)
                    return NotFound(new { message = $"No se encontró la cita con número {numeroCita}" });

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("cliente/{numeroCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitasByClienteNumeroAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    return BadRequest(new { message = "El número de cliente no puede estar vacío" });

                var result = await _citaBusiness.GetCitasByClienteNumeroAsync(numeroCliente);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("fecha/{fecha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitasByFechaAsync(DateTime fecha)
        {
            try
            {
                var result = await _citaBusiness.GetCitasByFechaAsync(fecha);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("sede/{sedeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitasBySedeAsync(int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    return BadRequest(new { message = "El ID de la sede debe ser mayor que cero" });

                var result = await _citaBusiness.GetCitasBySedeAsync(sedeId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("estado/{estado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitasByEstadoAsync(string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estado))
                    return BadRequest(new { message = "El estado no puede estar vacío" });

                var result = await _citaBusiness.GetCitasByEstadoAsync(estado);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("pendientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitasPendientesAsync()
        {
            try
            {
                var result = await _citaBusiness.GetCitasPendientesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("completadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitasCompletadasAsync()
        {
            try
            {
                var result = await _citaBusiness.GetCitasCompletadasAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpPost("agendar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CitaDto>> AgendarCitaAsync([FromBody] CitaDto cita)
        {
            try
            {
                if (cita == null)
                    return BadRequest(new { message = "Los datos de la cita no pueden ser nulos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _citaBusiness.AgendarCitaAsync(cita);
                return Created($"/api/cita/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpPatch("cancelar/{citaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CancelarCitaAsync(int citaId, [FromBody] string observaciones)
        {
            try
            {
                if (citaId <= 0)
                    return BadRequest(new { message = "El ID de la cita debe ser mayor que cero" });

                var result = await _citaBusiness.CancelarCitaAsync(citaId, observaciones);
                return Ok(new { success = result, message = "Cita cancelada exitosamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // MÉTODO PRINCIPAL - Con endpoint HTTP
        [HttpPatch("completar/{citaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CompletarCitaAsync(int citaId, [FromBody] CompletarCitaRequest request)
        {
            try
            {
                if (citaId <= 0)
                    return BadRequest(new { message = "El ID de la cita debe ser mayor que cero" });

                if (request == null)
                    return BadRequest(new { message = "Los datos para completar la cita son requeridos" });

                var result = await _citaBusiness.CompletarCitaAsync(citaId, request.TecnicoAsignado, request.ObservacionesTecnico);
                return Ok(new { success = result, message = "Cita completada exitosamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // MÉTODO REQUERIDO POR LA INTERFAZ ICitaController (sin decoradores HTTP)
        [NonAction]
        public async Task<ActionResult<bool>> CompletarCitaAsync(int id, string tecnicoAsignado, string observacionesTecnico)
        {
            try
            {
                var result = await _citaBusiness.CompletarCitaAsync(id, tecnicoAsignado, observacionesTecnico);
                return Ok(result); // Devuelve directamente el bool
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, false);
            }
        }

        [HttpGet("disponibilidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ValidarDisponibilidadAsync([FromQuery] DateTime fecha, [FromQuery] TimeSpan hora, [FromQuery] int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    return BadRequest(new { message = "El ID de la sede debe ser mayor que cero" });

                var result = await _citaBusiness.ValidarDisponibilidadAsync(fecha, hora, sedeId);
                return Ok(new { disponible = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("horas-disponibles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TimeSpan>>> GetHorasDisponiblesAsync([FromQuery] DateTime fecha, [FromQuery] int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    return BadRequest(new { message = "El ID de la sede debe ser mayor que cero" });

                var result = await _citaBusiness.GetHorasDisponiblesAsync(fecha, sedeId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }

    // Clase auxiliar para el request de completar cita
    public class CompletarCitaRequest
    {
        public string TecnicoAsignado { get; set; } = string.Empty;
        public string ObservacionesTecnico { get; set; } = string.Empty;
    }
}