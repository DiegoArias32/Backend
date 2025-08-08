using Entity.DTOs.CitaDto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface ICitaController : IBaseController<CitaDto>
    {
        Task<ActionResult<bool>> UpdateCitaAsync(UpdateCitaDto entity);
        Task<ActionResult<CitaDto>> GetByNumeroCitaAsync(string numeroCita);
        Task<ActionResult<IEnumerable<CitaDto>>> GetCitasByClienteNumeroAsync(string numeroCliente);
        Task<ActionResult<IEnumerable<CitaDto>>> GetCitasByFechaAsync(DateTime fecha);
        Task<ActionResult<IEnumerable<CitaDto>>> GetCitasBySedeAsync(int sedeId);
        Task<ActionResult<IEnumerable<CitaDto>>> GetCitasByEstadoAsync(string estado);
        Task<ActionResult<IEnumerable<CitaDto>>> GetCitasPendientesAsync();
        Task<ActionResult<IEnumerable<CitaDto>>> GetCitasCompletadasAsync();
        Task<ActionResult<CitaDto>> AgendarCitaAsync(CitaDto cita);
        Task<ActionResult<bool>> CancelarCitaAsync(int citaId, string observaciones);
        Task<ActionResult<bool>> CompletarCitaAsync(int citaId, string tecnicoAsignado, string observacionesTecnico);
        Task<ActionResult<bool>> ValidarDisponibilidadAsync(DateTime fecha, TimeSpan hora, int sedeId);
        Task<ActionResult<IEnumerable<TimeSpan>>> GetHorasDisponiblesAsync(DateTime fecha, int sedeId);
    }
}