using Entity.DTOs.ClienteDto;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IClienteBusiness : IBaseBusiness<ClienteDto>
    {
        Task<bool> UpdateClienteAsync(UpdateClienteDto entity);
        Task<bool> DeleteLogicalClienteAsync(DeleteLogicalClienteDto entity);
        Task<ClienteDto> GetByNumeroClienteAsync(string numeroCliente);
        Task<ClienteDto> GetByNumeroDocumentoAsync(string numeroDocumento);
        Task<bool> ExistsNumeroClienteAsync(string numeroCliente);
        Task<bool> ExistsNumeroDocumentoAsync(string numeroDocumento);
        Task<bool> ExistsEmailAsync(string email);
        Task<ClienteDto> ValidarClienteAsync(string numeroCliente);
    }
}