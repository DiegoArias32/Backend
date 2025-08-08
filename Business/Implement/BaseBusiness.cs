using Business.Interfaces;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Implement
{
    public abstract class BaseBusiness<T> : IBaseBusiness<T> where T : class
    {
        protected readonly IBaseData<T> _baseData;

        protected BaseBusiness(IBaseData<T> baseData)
        {
            _baseData = baseData;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _baseData.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los registros: {ex.Message}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllIncludingInactiveAsync()
        {
            try
            {
                return await _baseData.GetAllIncludingInactiveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los registros (incluidos inactivos): {ex.Message}", ex);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

                return await _baseData.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el registro con ID {id}: {ex.Message}", ex);
            }
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula");

                await ValidateEntityAsync(entity);

                return await _baseData.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el registro: {ex.Message}", ex);
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula");

                await ValidateEntityAsync(entity);

                return await _baseData.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el registro: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

                var entity = await _baseData.GetByIdAsync(id);
                if (entity == null)
                    throw new InvalidOperationException($"No se encontró el registro con ID {id}");

                return await _baseData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el registro con ID {id}: {ex.Message}", ex);
            }
        }

        public virtual async Task<bool> DeleteLogicalAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

                var entity = await _baseData.GetByIdAsync(id);
                if (entity == null)
                    throw new InvalidOperationException($"No se encontró el registro con ID {id}");

                return await _baseData.DeleteLogicalAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar eliminación lógica del registro con ID {id}: {ex.Message}", ex);
            }
        }

        protected virtual async Task ValidateEntityAsync(T entity)
        {
            await Task.CompletedTask;
        }
    }
}