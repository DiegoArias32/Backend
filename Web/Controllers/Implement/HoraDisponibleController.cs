using Business.Interfaces;
using Entity.DTOs.HoraDisponibleDto;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Implement;
using Web.Controllers.Interfaces;

namespace Web.Controllers
{
    public class HoraDisponibleController : BaseController<HoraDisponibleDto>, IHoraDisponibleController
    {
        private readonly IHoraDisponibleBusiness _horaDisponibleBusiness;

        public HoraDisponibleController(IHoraDisponibleBusiness business)
            : base(business)
        {
            _horaDisponibleBusiness = business;
        }

        // Métodos específicos adicionales
        [HttpGet("sede/{sedeId:int}")]
        public async Task<ActionResult<IEnumerable<HoraDisponibleDto>>> GetBySede(int sedeId)
        {
            try
            {
                var result = await _horaDisponibleBusiness.GetBySedeAsync(sedeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpGet("tipocita/{tipoCitaId:int}")]
        public async Task<ActionResult<IEnumerable<HoraDisponibleDto>>> GetByTipoCita(int tipoCitaId)
        {
            try
            {
                var result = await _horaDisponibleBusiness.GetByTipoCitaAsync(tipoCitaId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // Sobrescribir métodos si necesitas lógica específica
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateSpecific(int id, [FromBody] HoraDisponibleDto dto)
        {
            try
            {
                if (dto == null || dto.Id != id)
                    return BadRequest(new { message = "Datos inválidos" });

                var updated = await _horaDisponibleBusiness.UpdateHoraDisponibleAsync(dto);
                if (!updated)
                    return NotFound(new { message = "Registro no encontrado" });

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpDelete("logical/{id:int}")]
        public async Task<ActionResult<bool>> DeleteLogicalSpecific(int id)
        {
            try
            {
                var deleted = await _horaDisponibleBusiness.DeleteLogicalHoraDisponibleAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Registro no encontrado" });

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // Implementación requerida por BaseController
        protected override object GetEntityId(HoraDisponibleDto entity)
        {
            return entity?.Id;
        }
    }
}