using Entity.DTOs.CitaDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICitaBusiness : IBaseBusiness<CitaDto>
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
        Task<CitaDto> AgendarCitaAsync(CitaDto cita);
        Task<bool> CancelarCitaAsync(int citaId, string observaciones);
        Task<bool> CompletarCitaAsync(int citaId, string tecnicoAsignado, string observacionesTecnico);
        Task<bool> ValidarDisponibilidadAsync(DateTime fecha, TimeSpan hora, int sedeId);
        Task<IEnumerable<TimeSpan>> GetHorasDisponiblesAsync(DateTime fecha, int sedeId);
        Task<string> GenerarNumeroCitaAsync();
    }
}