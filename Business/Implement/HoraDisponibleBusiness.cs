using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs.HoraDisponibleDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Implement
{
    public class HoraDisponibleBusiness : BaseBusiness<HoraDisponibleDto>, IHoraDisponibleBusiness
    {
        private readonly IHoraDisponibleData _horaDisponibleData;

        public HoraDisponibleBusiness(IHoraDisponibleData horaDisponibleData)
            : base(horaDisponibleData)
        {
            _horaDisponibleData = horaDisponibleData;
        }

        public async Task<IEnumerable<HoraDisponibleDto>> GetBySedeAsync(int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    throw new ArgumentException("El ID de la sede debe ser mayor que cero", nameof(sedeId));

                return await _horaDisponibleData.GetBySedeAsync(sedeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener horas disponibles por sede: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<HoraDisponibleDto>> GetByTipoCitaAsync(int tipoCitaId)
        {
            try
            {
                if (tipoCitaId <= 0)
                    throw new ArgumentException("El ID del tipo de cita debe ser mayor que cero", nameof(tipoCitaId));

                return await _horaDisponibleData.GetByTipoCitaAsync(tipoCitaId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener horas disponibles por tipo de cita: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateHoraDisponibleAsync(HoraDisponibleDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                await ValidateEntityAsync(entity);

                return await _horaDisponibleData.UpdateHoraDisponibleAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar hora disponible: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteLogicalHoraDisponibleAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

                return await _horaDisponibleData.DeleteLogicalHoraDisponibleAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar eliminación lógica de la hora disponible: {ex.Message}", ex);
            }
        }

        protected override async Task ValidateEntityAsync(HoraDisponibleDto entity)
        {
            if (entity.SedeId <= 0)
                throw new ArgumentException("El ID de la sede es requerido");

            if (entity.Hora == default)
                throw new ArgumentException("La hora es requerida");

            // Si tienes validaciones adicionales, agrégalas aquí

            await Task.CompletedTask;
        }
    }
}