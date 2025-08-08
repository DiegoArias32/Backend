using Entity.DTOs.HoraDisponibleDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IHoraDisponibleBusiness : IBaseBusiness<HoraDisponibleDto>
    {
        Task<IEnumerable<HoraDisponibleDto>> GetBySedeAsync(int sedeId);
        Task<IEnumerable<HoraDisponibleDto>> GetByTipoCitaAsync(int tipoCitaId);
        Task<bool> UpdateHoraDisponibleAsync(HoraDisponibleDto entity);
        Task<bool> DeleteLogicalHoraDisponibleAsync(int id);
    }
}