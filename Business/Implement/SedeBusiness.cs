using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs.SedeDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Implement
{
    public class SedeBusiness : BaseBusiness<SedeDto>, ISedeBusiness
    {
        private readonly ISedeData _sedeData;

        public SedeBusiness(ISedeData sedeData) : base(sedeData)
        {
            _sedeData = sedeData;
        }

        public async Task<bool> UpdateSedeAsync(UpdateSedeDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de actualización no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                await ValidateUpdateSedeAsync(entity);

                return await _sedeData.UpdateSedeAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar sede: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteLogicalSedeAsync(DeleteLogicalSedeDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de eliminación lógica no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                return await _sedeData.DeleteLogicalSedeAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar eliminación lógica de la sede: {ex.Message}", ex);
            }
        }

        public async Task<SedeDto> GetByCodigoAsync(string codigo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigo))
                    throw new ArgumentException("El código de sede no puede estar vacío", nameof(codigo));

                return await _sedeData.GetByCodigoAsync(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener sede por código: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<SedeDto>> GetSedesActivasAsync()
        {
            try
            {
                return await _sedeData.GetSedesActivasAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener sedes activas: {ex.Message}", ex);
            }
        }

        public async Task<SedeDto> GetSedePrincipalAsync()
        {
            try
            {
                return await _sedeData.GetSedePrincipalAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener sede principal: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExistsCodigoAsync(string codigo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigo))
                    return false;

                return await _sedeData.ExistsCodigoAsync(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de código de sede: {ex.Message}", ex);
            }
        }

        protected override async Task ValidateEntityAsync(SedeDto entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Nombre))
                throw new ArgumentException("El nombre de la sede es requerido");

            if (string.IsNullOrWhiteSpace(entity.Codigo))
                throw new ArgumentException("El código de la sede es requerido");

            if (string.IsNullOrWhiteSpace(entity.Direccion))
                throw new ArgumentException("La dirección de la sede es requerida");

            if (string.IsNullOrWhiteSpace(entity.Ciudad))
                throw new ArgumentException("La ciudad de la sede es requerida");

            if (string.IsNullOrWhiteSpace(entity.Departamento))
                throw new ArgumentException("El departamento de la sede es requerido");

            // Validar unicidad del código
            if (await _sedeData.ExistsCodigoAsync(entity.Codigo))
                throw new InvalidOperationException($"Ya existe una sede con el código {entity.Codigo}");

            await Task.CompletedTask;
        }

        private async Task ValidateUpdateSedeAsync(UpdateSedeDto entity)
        {
            // Validaciones específicas para actualización si es necesario
            await Task.CompletedTask;
        }
    }
}