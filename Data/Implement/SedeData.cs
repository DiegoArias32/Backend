using AutoMapper;
using Data.Interfaces;
using Entity.Contexts;
using Entity.DTOs.SedeDto;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implement
{
    public class SedeData : BaseData<Sede, SedeDto>, ISedeData
    {
        private readonly ApplicationDbContext _dbContext;

        protected override DbSet<Sede> DbSet => _dbContext.Sedes;

        public SedeData(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _dbContext = context;
        }

        public async Task<bool> UpdateSedeAsync(UpdateSedeDto entity)
        {
            var existingEntity = await _dbContext.Sedes.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            _mapper.Map(entity, existingEntity);
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLogicalSedeAsync(DeleteLogicalSedeDto entity)
        {
            var existingEntity = await _dbContext.Sedes.FindAsync(entity.Id);
            if (existingEntity == null) return false;

            existingEntity.IsActive = false;
            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<SedeDto> GetByCodigoAsync(string codigo)
        {
            var entity = await _dbContext.Sedes
                .Where(s => s.Codigo == codigo && s.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<SedeDto>(entity);
        }

        public async Task<IEnumerable<SedeDto>> GetSedesActivasAsync()
        {
            var entities = await _dbContext.Sedes
                .Where(s => s.IsActive)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SedeDto>>(entities);
        }

        public async Task<SedeDto> GetSedePrincipalAsync()
        {
            var entity = await _dbContext.Sedes
                .Where(s => s.EsPrincipal && s.IsActive)
                .FirstOrDefaultAsync();
            return _mapper.Map<SedeDto>(entity);
        }

        public async Task<bool> ExistsCodigoAsync(string codigo)
        {
            return await _dbContext.Sedes
                .AnyAsync(s => s.Codigo == codigo && s.IsActive);
        }
    }
}