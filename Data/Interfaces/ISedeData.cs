using Entity.DTOs.SedeDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISedeData : IBaseData<SedeDto>
    {
        Task<bool> UpdateSedeAsync(UpdateSedeDto entity);
        Task<bool> DeleteLogicalSedeAsync(DeleteLogicalSedeDto entity);
        Task<SedeDto> GetByCodigoAsync(string codigo);
        Task<IEnumerable<SedeDto>> GetSedesActivasAsync();
        Task<SedeDto> GetSedePrincipalAsync();
        Task<bool> ExistsCodigoAsync(string codigo);
    }
}