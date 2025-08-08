using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.DTOs.HoraDisponibleDto;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implement
{
    public class HoraDisponibleData : BaseData<HoraDisponible, HoraDisponibleDto>, IHoraDisponibleData
    {
        private readonly ApplicationDbContext _dbContext;

        protected override DbSet<HoraDisponible> DbSet => _dbContext.HorasDisponibles;

        public HoraDisponibleData(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _dbContext = context;
        }

        public override async Task<IEnumerable<HoraDisponibleDto>> GetAllAsync()
        {
            var entities = await _dbContext.HorasDisponibles
                .Include(h => h.Sede)
                .Include(h => h.TipoCita)
                .Where(h => h.IsActive)
                .OrderBy(h => h.Hora)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HoraDisponibleDto>>(entities);
        }

        public override async Task<HoraDisponibleDto> GetByIdAsync(int id)
        {
            var entity = await _dbContext.HorasDisponibles
                .Include(h => h.Sede)
                .Include(h => h.TipoCita)
                .Where(h => h.Id == id && h.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<HoraDisponibleDto>(entity);
        }

        public async Task<IEnumerable<HoraDisponibleDto>> GetBySedeAsync(int sedeId)
        {
            var entities = await _dbContext.HorasDisponibles
                .Include(h => h.Sede)
                .Include(h => h.TipoCita)
                .Where(h => h.SedeId == sedeId && h.IsActive)
                .OrderBy(h => h.Hora)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HoraDisponibleDto>>(entities);
        }

        public async Task<IEnumerable<HoraDisponibleDto>> GetByTipoCitaAsync(int tipoCitaId)
        {
            var entities = await _dbContext.HorasDisponibles
                .Include(h => h.Sede)
                .Include(h => h.TipoCita)
                .Where(h => h.TipoCitaId == tipoCitaId && h.IsActive)
                .OrderBy(h => h.Hora)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HoraDisponibleDto>>(entities);
        }

        public async Task<bool> UpdateHoraDisponibleAsync(HoraDisponibleDto entity)
        {
            var existingEntity = await _dbContext.HorasDisponibles.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            _mapper.Map(entity, existingEntity);
            existingEntity.UpdatedAt = System.DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLogicalHoraDisponibleAsync(int id)
        {
            var existingEntity = await _dbContext.HorasDisponibles.FindAsync(id);
            if (existingEntity == null) return false;

            existingEntity.IsActive = false;
            existingEntity.UpdatedAt = System.DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}