using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs.CitaDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Implement
{
    public class CitaBusiness : BaseBusiness<CitaDto>, ICitaBusiness
    {
        private readonly ICitaData _citaData;
        private readonly IClienteData _clienteData;
        private readonly ISedeData _sedeData;
        private readonly ITipoCitaData _tipoCitaData;
        private readonly IHoraDisponibleData _horaDisponibleData; // ✅ NUEVA DEPENDENCIA

        public CitaBusiness(
            ICitaData citaData,
            IClienteData clienteData,
            ISedeData sedeData,
            ITipoCitaData tipoCitaData,
            IHoraDisponibleData horaDisponibleData) : base(citaData) // ✅ NUEVO PARÁMETRO
        {
            _citaData = citaData;
            _clienteData = clienteData;
            _sedeData = sedeData;
            _tipoCitaData = tipoCitaData;
            _horaDisponibleData = horaDisponibleData; // ✅ NUEVA ASIGNACIÓN
        }

        public async Task<bool> UpdateCitaAsync(UpdateCitaDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de actualización no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                await ValidateUpdateCitaAsync(entity);

                return await _citaData.UpdateCitaAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar cita: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteLogicalCitaAsync(DeleteLogicalCitaDto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad de eliminación lógica no puede ser nula");

                if (entity.Id <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(entity.Id));

                return await _citaData.DeleteLogicalCitaAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar eliminación lógica de la cita: {ex.Message}", ex);
            }
        }

        public async Task<CitaDto> GetByNumeroCitaAsync(string numeroCita)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCita))
                    throw new ArgumentException("El número de cita no puede estar vacío", nameof(numeroCita));

                return await _citaData.GetByNumeroCitaAsync(numeroCita);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cita por número: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByClienteIdAsync(int clienteId)
        {
            try
            {
                if (clienteId <= 0)
                    throw new ArgumentException("El ID del cliente debe ser mayor que cero", nameof(clienteId));

                return await _citaData.GetCitasByClienteIdAsync(clienteId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas por cliente: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByClienteNumeroAsync(string numeroCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroCliente))
                    throw new ArgumentException("El número de cliente no puede estar vacío", nameof(numeroCliente));

                return await _citaData.GetCitasByClienteNumeroAsync(numeroCliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas por número de cliente: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByFechaAsync(DateTime fecha)
        {
            try
            {
                return await _citaData.GetCitasByFechaAsync(fecha);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas por fecha: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasBySedeAsync(int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    throw new ArgumentException("El ID de la sede debe ser mayor que cero", nameof(sedeId));

                return await _citaData.GetCitasBySedeAsync(sedeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas por sede: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasByEstadoAsync(string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estado))
                    throw new ArgumentException("El estado no puede estar vacío", nameof(estado));

                return await _citaData.GetCitasByEstadoAsync(estado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas por estado: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasPendientesAsync()
        {
            try
            {
                return await _citaData.GetCitasPendientesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas pendientes: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CitaDto>> GetCitasCompletadasAsync()
        {
            try
            {
                return await _citaData.GetCitasCompletadasAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener citas completadas: {ex.Message}", ex);
            }
        }

        public async Task<CitaDto> AgendarCitaAsync(CitaDto cita)
        {
            try
            {
                if (cita == null)
                    throw new ArgumentNullException(nameof(cita), "La cita no puede ser nula");

                // Validar disponibilidad antes de agendar
                var disponible = await ValidarDisponibilidadAsync(cita.FechaCita, cita.HoraCita, cita.SedeId);
                if (!disponible)
                    throw new InvalidOperationException("La hora seleccionada no está disponible");

                // Generar número de cita único
                cita.NumeroCita = await GenerarNumeroCitaAsync();
                cita.Estado = "Pendiente";

                await ValidateEntityAsync(cita);

                return await _citaData.CreateAsync(cita);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agendar cita: {ex.Message}", ex);
            }
        }

        public async Task<bool> CancelarCitaAsync(int citaId, string observaciones)
        {
            try
            {
                if (citaId <= 0)
                    throw new ArgumentException("El ID de la cita debe ser mayor que cero", nameof(citaId));

                var cita = await _citaData.GetByIdAsync(citaId);
                if (cita == null)
                    throw new InvalidOperationException($"No se encontró la cita con ID {citaId}");

                if (cita.Estado == "Completada")
                    throw new InvalidOperationException("No se puede cancelar una cita que ya fue completada");

                if (cita.Estado == "Cancelada")
                    throw new InvalidOperationException("La cita ya está cancelada");

                var updateDto = new UpdateCitaDto
                {
                    Id = citaId,
                    Estado = "Cancelada",
                    Observaciones = observaciones
                };

                return await _citaData.UpdateCitaAsync(updateDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cancelar cita: {ex.Message}", ex);
            }
        }

        public async Task<bool> CompletarCitaAsync(int citaId, string tecnicoAsignado, string observacionesTecnico)
        {
            try
            {
                if (citaId <= 0)
                    throw new ArgumentException("El ID de la cita debe ser mayor que cero", nameof(citaId));

                var cita = await _citaData.GetByIdAsync(citaId);
                if (cita == null)
                    throw new InvalidOperationException($"No se encontró la cita con ID {citaId}");

                if (cita.Estado == "Completada")
                    throw new InvalidOperationException("La cita ya está completada");

                if (cita.Estado == "Cancelada")
                    throw new InvalidOperationException("No se puede completar una cita cancelada");

                var updateDto = new UpdateCitaDto
                {
                    Id = citaId,
                    Estado = "Completada",
                    FechaCompletada = DateTime.UtcNow,
                    TecnicoAsignado = tecnicoAsignado,
                    ObservacionesTecnico = observacionesTecnico
                };

                return await _citaData.UpdateCitaAsync(updateDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al completar cita: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarDisponibilidadAsync(DateTime fecha, TimeSpan hora, int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    throw new ArgumentException("El ID de la sede debe ser mayor que cero", nameof(sedeId));

                // No permitir agendar citas en fechas pasadas
                if (fecha.Date < DateTime.Today)
                    return false;

                // ✅ MEJORADO: Validar que la hora esté configurada en la BD para esta sede
                var horasConfiguradas = await _horaDisponibleData.GetBySedeAsync(sedeId);
                var horasDisponibles = horasConfiguradas
                    .Where(h => h.IsActive && h.Status)
                    .Select(h => TimeSpan.Parse(h.Hora)) // Convertir string a TimeSpan
                    .ToList();

                if (!horasDisponibles.Contains(hora))
                    return false;

                return await _citaData.ValidarDisponibilidadAsync(fecha, hora, sedeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar disponibilidad: {ex.Message}", ex);
            }
        }

        // ✅ MÉTODO COMPLETAMENTE REESCRITO - AHORA USA LA BASE DE DATOS
        public async Task<IEnumerable<TimeSpan>> GetHorasDisponiblesAsync(DateTime fecha, int sedeId)
        {
            try
            {
                if (sedeId <= 0)
                    throw new ArgumentException("El ID de la sede debe ser mayor que cero", nameof(sedeId));

                // ✅ 1. Obtener horas configuradas en la base de datos para esta sede
                var horasConfiguradas = await _horaDisponibleData.GetBySedeAsync(sedeId);
                
                if (!horasConfiguradas.Any())
                {
                    // Si no hay horas configuradas para esta sede, retornar lista vacía
                    return new List<TimeSpan>();
                }

                // ✅ 2. Filtrar solo las horas activas y convertir string a TimeSpan
                var horariosDisponibles = horasConfiguradas
                    .Where(h => h.IsActive && h.Status)
                    .Select(h => TimeSpan.Parse(h.Hora)) // Convertir string a TimeSpan
                    .ToList();

                // ✅ 3. Obtener horas ya ocupadas por citas existentes
                var horasOcupadasEnum = await _citaData.GetHorasOcupadasAsync(fecha, sedeId);
                var horasOcupadas = horasOcupadasEnum.ToHashSet(); // Convertir a HashSet para mejor performance

                // ✅ 4. Filtrar horarios disponibles (configurados - ocupados)
                var horasLibres = horariosDisponibles
                    .Where(h => !horasOcupadas.Contains(h))
                    .OrderBy(h => h)
                    .ToList();

                return horasLibres;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener horas disponibles: {ex.Message}", ex);
            }
        }

        public async Task<string> GenerarNumeroCitaAsync()
        {
            try
            {
                string numeroCita;
                bool existe;

                do
                {
                    var prefijo = "EH";
                    var año = DateTime.Now.Year;
                    var aleatorio = new Random().Next(1000, 9999);

                    numeroCita = $"{prefijo}-{año}-{aleatorio}";
                    existe = await _citaData.ExistsNumeroCitaAsync(numeroCita);

                } while (existe);

                return numeroCita;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar número de cita: {ex.Message}", ex);
            }
        }

        protected override async Task ValidateEntityAsync(CitaDto entity)
        {
            if (entity.ClienteId <= 0)
                throw new ArgumentException("El ID del cliente es requerido");

            if (entity.SedeId <= 0)
                throw new ArgumentException("El ID de la sede es requerido");

            if (entity.TipoCitaId <= 0)
                throw new ArgumentException("El ID del tipo de cita es requerido");

            if (entity.FechaCita == default)
                throw new ArgumentException("La fecha de la cita es requerida");

            if (entity.HoraCita == default)
                throw new ArgumentException("La hora de la cita es requerida");

            // Validar que las entidades relacionadas existan
            var cliente = await _clienteData.GetByIdAsync(entity.ClienteId);
            if (cliente == null)
                throw new InvalidOperationException($"No se encontró el cliente con ID {entity.ClienteId}");

            var sede = await _sedeData.GetByIdAsync(entity.SedeId);
            if (sede == null)
                throw new InvalidOperationException($"No se encontró la sede con ID {entity.SedeId}");

            var tipoCita = await _tipoCitaData.GetByIdAsync(entity.TipoCitaId);
            if (tipoCita == null)
                throw new InvalidOperationException($"No se encontró el tipo de cita con ID {entity.TipoCitaId}");

            // Validar que la fecha no sea en el pasado
            if (entity.FechaCita.Date < DateTime.Today)
                throw new ArgumentException("No se puede agendar citas en fechas pasadas");

            await Task.CompletedTask;
        }

        private async Task ValidateUpdateCitaAsync(UpdateCitaDto entity)
        {
            if (entity.FechaCita.HasValue && entity.FechaCita.Value.Date < DateTime.Today)
                throw new ArgumentException("No se puede agendar citas en fechas pasadas");

            if (entity.ClienteId.HasValue && entity.ClienteId.Value > 0)
            {
                var cliente = await _clienteData.GetByIdAsync(entity.ClienteId.Value);
                if (cliente == null)
                    throw new InvalidOperationException($"No se encontró el cliente con ID {entity.ClienteId}");
            }

            if (entity.SedeId.HasValue && entity.SedeId.Value > 0)
            {
                var sede = await _sedeData.GetByIdAsync(entity.SedeId.Value);
                if (sede == null)
                    throw new InvalidOperationException($"No se encontró la sede con ID {entity.SedeId}");
            }

            if (entity.TipoCitaId.HasValue && entity.TipoCitaId.Value > 0)
            {
                var tipoCita = await _tipoCitaData.GetByIdAsync(entity.TipoCitaId.Value);
                if (tipoCita == null)
                    throw new InvalidOperationException($"No se encontró el tipo de cita con ID {entity.TipoCitaId}");
            }

            await Task.CompletedTask;
        }
    }
}