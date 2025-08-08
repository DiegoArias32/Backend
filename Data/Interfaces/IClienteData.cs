using Entity.DTOs.ClienteDto;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IClienteData : IBaseData<ClienteDto>
    {
        Task<bool> UpdateClienteAsync(UpdateClienteDto entity);
        Task<bool> DeleteLogicalClienteAsync(DeleteLogicalClienteDto entity);
        Task<ClienteDto> GetByNumeroClienteAsync(string numeroCliente);
        Task<ClienteDto> GetByNumeroDocumentoAsync(string numeroDocumento);
        Task<bool> ExistsNumeroClienteAsync(string numeroCliente);
        Task<bool> ExistsNumeroDocumentoAsync(string numeroDocumento);
        Task<bool> ExistsEmailAsync(string email);
    }
}