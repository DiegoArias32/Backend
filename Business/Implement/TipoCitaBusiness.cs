using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs.TipoCitaDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Implement
{
    public class TipoCitaBusiness : BaseBusiness<TipoCitaDto>, ITipoCitaBusiness
    {
        private readonly ITipoCitaData _tipoCitaData;

        public TipoCitaBusiness(ITipoCitaData tipoCitaData) : base(tipoCitaData)
        {
            _tipoCitaData = tipoCitaData;
        }

        public async Task<bool> UpdateTipoCitaAsync(UpdateTipoCitaDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de actualización no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                await ValidateUpdateTipoCitaAsync(entity);

                return await _tipoCitaData.UpdateTipoCitaAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar tipo de cita: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteLogicalTipoCitaAsync(DeleteLogicalTipoCitaDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de eliminación lógica no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                return await _tipoCitaData.DeleteLogicalTipoCitaAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar eliminación lógica del tipo de cita: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<TipoCitaDto>> GetTiposCitaActivosAsync()
        {
            try
            {
                return await _tipoCitaData.GetTiposCitaActivosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tipos de cita activos: {ex.Message}", ex);
            }
        }

        public async Task<TipoCitaDto> GetByNombreAsync(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new ArgumentException("El nombre del tipo de cita no puede estar vacío", nameof(nombre));

                return await _tipoCitaData.GetByNombreAsync(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tipo de cita por nombre: {ex.Message}", ex);
            }
        }

        protected override async Task ValidateEntityAsync(TipoCitaDto entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Nombre))
                throw new ArgumentException("El nombre del tipo de cita es requerido");

            if (entity.TiempoEstimadoMinutos <= 0)
                throw new ArgumentException("El tiempo estimado debe ser mayor que cero");

            if (entity.TiempoEstimadoMinutos > 480) // Máximo 8 horas
                throw new ArgumentException("El tiempo estimado no puede ser mayor a 480 minutos (8 horas)");

            await Task.CompletedTask;
        }

        private async Task ValidateUpdateTipoCitaAsync(UpdateTipoCitaDto entity)
        {
            if (entity.TiempoEstimadoMinutos.HasValue)
            {
                if (entity.TiempoEstimadoMinutos <= 0)
                    throw new ArgumentException("El tiempo estimado debe ser mayor que cero");

                if (entity.TiempoEstimadoMinutos > 480) // Máximo 8 horas
                    throw new ArgumentException("El tiempo estimado no puede ser mayor a 480 minutos (8 horas)");
            }

            await Task.CompletedTask;
        }
    }
}