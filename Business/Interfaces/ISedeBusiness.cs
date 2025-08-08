using Entity.DTOs.SedeDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISedeBusiness : IBaseBusiness<SedeDto>
    {
        Task<bool> UpdateSedeAsync(UpdateSedeDto entity);
        Task<bool> DeleteLogicalSedeAsync(DeleteLogicalSedeDto entity);
        Task<SedeDto> GetByCodigoAsync(string codigo);
        Task<IEnumerable<SedeDto>> GetSedesActivasAsync();
        Task<SedeDto> GetSedePrincipalAsync();
        Task<bool> ExistsCodigoAsync(string codigo);
    }
}