using Entity.DTOs.TipoCitaDto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface ITipoCitaController : IBaseController<TipoCitaDto>
    {
        Task<ActionResult<bool>> UpdateTipoCitaAsync(UpdateTipoCitaDto entity);
        Task<ActionResult<IEnumerable<TipoCitaDto>>> GetTiposCitaActivosAsync();
        Task<ActionResult<TipoCitaDto>> GetByNombreAsync(string nombre);
    }
}