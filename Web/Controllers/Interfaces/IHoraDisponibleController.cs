using Entity.DTOs.HoraDisponibleDto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IHoraDisponibleController : IBaseController<HoraDisponibleDto>
    {
        Task<ActionResult<IEnumerable<HoraDisponibleDto>>> GetBySede(int sedeId);
        Task<ActionResult<IEnumerable<HoraDisponibleDto>>> GetByTipoCita(int tipoCitaId);
        Task<ActionResult<bool>> UpdateSpecific(int id, HoraDisponibleDto dto);
        Task<ActionResult<bool>> DeleteLogicalSpecific(int id);
    }
}