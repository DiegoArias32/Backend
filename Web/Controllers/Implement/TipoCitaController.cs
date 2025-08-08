using Business.Interfaces;
using Entity.DTOs.TipoCitaDto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implement
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Tipos de Cita")]
    public class TipoCitaController : BaseController<TipoCitaDto>, ITipoCitaController
    {
        private readonly ITipoCitaBusiness _tipoCitaBusiness;

        public TipoCitaController(ITipoCitaBusiness tipoCitaBusiness) : base(tipoCitaBusiness)
        {
            _tipoCitaBusiness = tipoCitaBusiness;
        }

        protected override object GetEntityId(TipoCitaDto entity)
        {
            return entity.Id;
        }

        [HttpPatch("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateTipoCitaAsync([FromBody] UpdateTipoCitaDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest(new { message = "Los datos del tipo de cita no pueden ser nulos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _tipoCitaBusiness.UpdateTipoCitaAsync(entity);
                return Ok(new { success = result, message = "Tipo de cita actualizado exitosamente" });
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

        [HttpGet("activos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TipoCitaDto>>> GetTiposCitaActivosAsync()
        {
            try
            {
                var result = await _tipoCitaBusiness.GetTiposCitaActivosAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("nombre/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TipoCitaDto>> GetByNombreAsync(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return BadRequest(new { message = "El nombre del tipo de cita no puede estar vacío" });

                var result = await _tipoCitaBusiness.GetByNombreAsync(nombre);

                if (result == null)
                    return NotFound(new { message = $"No se encontró el tipo de cita con nombre {nombre}" });

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
}