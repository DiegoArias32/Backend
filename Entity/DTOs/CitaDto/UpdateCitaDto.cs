using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.CitaDto
{
    public class UpdateCitaDto : BaseDto
    {
        public string? NumeroCita { get; set; }
        public DateTime? FechaCita { get; set; }
        public TimeSpan? HoraCita { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaCompletada { get; set; }
        public string? TecnicoAsignado { get; set; }
        public string? ObservacionesTecnico { get; set; }
        public int? ClienteId { get; set; }
        public int? SedeId { get; set; }
        public int? TipoCitaId { get; set; }
    }
}