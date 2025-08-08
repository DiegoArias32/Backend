using Entity.DTOs.HoraDisponibleDto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IHoraDisponibleData : IBaseData<HoraDisponibleDto>
    {
        Task<bool> DeleteLogicalHoraDisponibleAsync(int id);
        Task<IEnumerable<HoraDisponibleDto>> GetBySedeAsync(int sedeId);
        Task<IEnumerable<HoraDisponibleDto>> GetByTipoCitaAsync(int tipoCitaId);
        Task<bool> UpdateHoraDisponibleAsync(HoraDisponibleDto entity);
    }
}