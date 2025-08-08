using Entity.DTOs.SedeDto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface ISedeController : IBaseController<SedeDto>
    {
        Task<ActionResult<bool>> UpdateSedeAsync(UpdateSedeDto entity);
        Task<ActionResult<SedeDto>> GetByCodigoAsync(string codigo);
        Task<ActionResult<IEnumerable<SedeDto>>> GetSedesActivasAsync();
        Task<ActionResult<SedeDto>> GetSedePrincipalAsync();
    }
}