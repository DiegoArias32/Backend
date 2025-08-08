using Entity.DTOs.TipoCitaDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITipoCitaData : IBaseData<TipoCitaDto>
    {
        Task<bool> UpdateTipoCitaAsync(UpdateTipoCitaDto entity);
        Task<bool> DeleteLogicalTipoCitaAsync(DeleteLogicalTipoCitaDto entity);
        Task<IEnumerable<TipoCitaDto>> GetTiposCitaActivosAsync();
        Task<TipoCitaDto> GetByNombreAsync(string nombre);
    }
}