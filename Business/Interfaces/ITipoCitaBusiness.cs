using Entity.DTOs.TipoCitaDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITipoCitaBusiness : IBaseBusiness<TipoCitaDto>
    {
        Task<bool> UpdateTipoCitaAsync(UpdateTipoCitaDto entity);
        Task<bool> DeleteLogicalTipoCitaAsync(DeleteLogicalTipoCitaDto entity);
        Task<IEnumerable<TipoCitaDto>> GetTiposCitaActivosAsync();
        Task<TipoCitaDto> GetByNombreAsync(string nombre);
    }
}