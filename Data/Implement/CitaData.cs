using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.DTOs.CitaDto;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implement
{
    public class CitaData : BaseData<Cita, CitaDto>, ICitaData
    {
        private readonly ApplicationDbContext _dbContext;

        protected override DbSet<Cita> DbSet => _dbContext.Citas;

        public CitaData(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _dbContext = context;
        }

        public override async Task<IEnumerable<CitaDto>> GetAllAsync()
        {
            var entities = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.FechaCita)
                .ThenBy(c => c.HoraCita)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(entities);
        }

        public override async Task<CitaDto> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.Id == id && c.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<CitaDto>(entity);
        }

        public async Task<bool> UpdateCitaAsync(UpdateCitaDto entity)
        {
            var existingEntity = await _dbContext.Citas.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            _mapper.Map(entity, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLogicalCitaAsync(DeleteLogicalCitaDto entity)
        {
            var existingEntity = await _dbContext.Citas.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            existingEntity.IsActive = false;
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CitaDto> GetByNumeroCitaAsync(string numeroCita)
        {
            var entity = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.NumeroCita == numeroCita && c.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<CitaDto>(entity);
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByClienteIdAsync(int clienteId)
        {
            var entities = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.ClienteId == clienteId && c.IsActive)
                .OrderByDescending(c => c.FechaCita)
                .ThenBy(c => c.HoraCita)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(entities);
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByClienteNumeroAsync(string numeroCliente)
        {
            var entities = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.Cliente.NumeroCliente == numeroCliente && c.IsActive)
                .OrderByDescending(c => c.FechaCita)
                .ThenBy(c => c.HoraCita)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(entities);
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByFechaAsync(DateTime fecha)
        {
            var entities = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.FechaCita.Date == fecha.Date && c.IsActive)
                .OrderBy(c => c.HoraCita)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(entities);
        }

        public async Task<IEnumerable<CitaDto>> GetCitasBySedeAsync(int sedeId)
        {
            var entities = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.SedeId == sedeId && c.IsActive)
                .OrderByDescending(c => c.FechaCita)
                .ThenBy(c => c.HoraCita)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(entities);
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByEstadoAsync(string estado)
        {
            var entities = await _dbContext.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Sede)
                .Include(c => c.TipoCita)
                .Where(c => c.Estado == estado && c.IsActive)
                .OrderByDescending(c => c.FechaCita)
                .ThenBy(c => c.HoraCita)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(entities);
        }

        public async Task<IEnumerable<CitaDto>> GetCitasPendientesAsync()
        {
            return await GetCitasByEstadoAsync("Pendiente");
        }

        public async Task<IEnumerable<CitaDto>> GetCitasCompletadasAsync()
        {
            return await GetCitasByEstadoAsync("Completada");
        }

        public async Task<bool> ExistsNumeroCitaAsync(string numeroCita)
        {
            return await _dbContext.Citas
                .AnyAsync(c => c.NumeroCita == numeroCita && c.IsActive);
        }

        public async Task<bool> ValidarDisponibilidadAsync(DateTime fecha, TimeSpan hora, int sedeId)
        {
            return !await _dbContext.Citas
                .AnyAsync(c => c.FechaCita.Date == fecha.Date &&
                              c.HoraCita == hora &&
                              c.SedeId == sedeId &&
                              c.IsActive &&
                              c.Estado != "Cancelada");
        }

        public async Task<IEnumerable<TimeSpan>> GetHorasOcupadasAsync(DateTime fecha, int sedeId)
        {
            return await _dbContext.Citas
                .Where(c => c.FechaCita.Date == fecha.Date &&
                           c.SedeId == sedeId &&
                           c.IsActive &&
                           c.Estado != "Cancelada")
                .Select(c => c.HoraCita)
                .ToListAsync();
        }
    }
}