using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.DTOs.TipoCitaDto;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implement
{
    public class TipoCitaData : BaseData<TipoCita, TipoCitaDto>, ITipoCitaData
    {
        private readonly ApplicationDbContext _dbContext;

        protected override DbSet<TipoCita> DbSet => _dbContext.TiposCita;

        public TipoCitaData(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _dbContext = context;
        }

        public async Task<bool> UpdateTipoCitaAsync(UpdateTipoCitaDto entity)
        {
            var existingEntity = await _dbContext.TiposCita.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            _mapper.Map(entity, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLogicalTipoCitaAsync(DeleteLogicalTipoCitaDto entity)
        {
            var existingEntity = await _dbContext.TiposCita.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            existingEntity.IsActive = false;
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TipoCitaDto>> GetTiposCitaActivosAsync()
        {
            var entities = await _dbContext.TiposCita
                .Where(tc => tc.IsActive)
                .OrderBy(tc => tc.Nombre)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TipoCitaDto>>(entities);
        }

        public async Task<TipoCitaDto> GetByNombreAsync(string nombre)
        {
            var entity = await _dbContext.TiposCita
                .Where(tc => tc.Nombre == nombre && tc.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<TipoCitaDto>(entity);
        }
    }
}