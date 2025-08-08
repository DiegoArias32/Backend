using Business.Interfaces;
using Entity.DTOs.SedeDto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implement
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Sedes")]
    public class SedeController : BaseController<SedeDto>, ISedeController
    {
        private readonly ISedeBusiness _sedeBusiness;

        public SedeController(ISedeBusiness sedeBusiness) : base(sedeBusiness)
        {
            _sedeBusiness = sedeBusiness;
        }

        protected override object GetEntityId(SedeDto entity)
        {
            return entity.Id;
        }

        [HttpPatch("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateSedeAsync([FromBody] UpdateSedeDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest(new { message = "Los datos de la sede no pueden ser nulos" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _sedeBusiness.UpdateSedeAsync(entity);
                return Ok(new { success = result, message = "Sede actualizada exitosamente" });
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

        [HttpGet("codigo/{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SedeDto>> GetByCodigoAsync(string codigo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigo))
                    return BadRequest(new { message = "El código de sede no puede estar vacío" });

                var result = await _sedeBusiness.GetByCodigoAsync(codigo);

                if (result == null)
                    return NotFound(new { message = $"No se encontró la sede con código {codigo}" });

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

        [HttpGet("activas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SedeDto>>> GetSedesActivasAsync()
        {
            try
            {
                var result = await _sedeBusiness.GetSedesActivasAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("principal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SedeDto>> GetSedePrincipalAsync()
        {
            try
            {
                var result = await _sedeBusiness.GetSedePrincipalAsync();

                if (result == null)
                    return NotFound(new { message = "No se encontró una sede principal configurada" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}