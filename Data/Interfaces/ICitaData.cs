using Entity.DTOs.CitaDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICitaData : IBaseData<CitaDto>
    {
        Task<bool> UpdateCitaAsync(UpdateCitaDto entity);
        Task<bool> DeleteLogicalCitaAsync(DeleteLogicalCitaDto entity);
        Task<CitaDto> GetByNumeroCitaAsync(string numeroCita);
        Task<IEnumerable<CitaDto>> GetCitasByClienteIdAsync(int clienteId);
        Task<IEnumerable<CitaDto>> GetCitasByClienteNumeroAsync(string numeroCliente);
        Task<IEnumerable<CitaDto>> GetCitasByFechaAsync(DateTime fecha);
        Task<IEnumerable<CitaDto>> GetCitasBySedeAsync(int sedeId);
        Task<IEnumerable<CitaDto>> GetCitasByEstadoAsync(string estado);
        Task<IEnumerable<CitaDto>> GetCitasPendientesAsync();
        Task<IEnumerable<CitaDto>> GetCitasCompletadasAsync();
        Task<bool> ExistsNumeroCitaAsync(string numeroCita);
        Task<bool> ValidarDisponibilidadAsync(DateTime fecha, TimeSpan hora, int sedeId);
        Task<IEnumerable<TimeSpan>> GetHorasOcupadasAsync(DateTime fecha, int sedeId);
    }
}