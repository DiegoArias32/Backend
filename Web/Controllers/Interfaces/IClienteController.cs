using Entity.DTOs.ClienteDto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IClienteController : IBaseController<ClienteDto>
    {
        Task<ActionResult<bool>> UpdateClienteAsync(UpdateClienteDto entity);
        Task<ActionResult<ClienteDto>> GetByNumeroClienteAsync(string numeroCliente);
        Task<ActionResult<ClienteDto>> GetByNumeroDocumentoAsync(string numeroDocumento);
        Task<ActionResult<ClienteDto>> ValidarClienteAsync(string numeroCliente);
        Task<ActionResult<bool>> ExistsNumeroClienteAsync(string numeroCliente);
        Task<ActionResult<bool>> ExistsNumeroDocumentoAsync(string numeroDocumento);
        Task<ActionResult<bool>> ExistsEmailAsync(string email);
    }
}