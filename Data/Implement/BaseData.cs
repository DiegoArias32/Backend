using AutoMapper;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implement
{
    public abstract class BaseData<TEntity, TDto> : IBaseData<TDto>
            where TEntity : class
            where TDto : class
    {
        protected readonly DbContext _context;
        protected readonly IMapper _mapper;
        protected abstract DbSet<TEntity> DbSet { get; }

        protected BaseData(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await DbSet
                .Where(e => EF.Property<bool>(e, "IsActive") == true)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await DbSet
                .Where(e => EF.Property<int>(e, "Id") == id && EF.Property<bool>(e, "IsActive") == true)
                .FirstOrDefaultAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            // Establecer CreatedAt si la entidad tiene esta propiedad
            var createdAtProperty = typeof(TEntity).GetProperty("CreatedAt");
            if (createdAtProperty != null)
            {
                createdAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            // Establecer IsActive si la entidad tiene esta propiedad
            var isActiveProperty = typeof(TEntity).GetProperty("IsActive");
            if (isActiveProperty != null)
            {
                isActiveProperty.SetValue(entity, true);
            }

            DbSet.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            // Establecer UpdatedAt si la entidad tiene esta propiedad
            var updatedAtProperty = typeof(TEntity).GetProperty("UpdatedAt");
            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            DbSet.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return false;

            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteLogicalAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return false;

            var isActiveProperty = typeof(TEntity).GetProperty("IsActive");
            if (isActiveProperty != null)
            {
                isActiveProperty.SetValue(entity, false);
            }

            var updatedAtProperty = typeof(TEntity).GetProperty("UpdatedAt");
            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.UtcNow);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllIncludingInactiveAsync()
        {
            var entities = await DbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

    }
}