using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.CitaDto
{
    public class CitaDto : BaseDto
    {
        public string? NumeroCita { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaCompletada { get; set; }

        // Claves foráneas
        public int ClienteId { get; set; }
        public int SedeId { get; set; }
        public int TipoCitaId { get; set; }

        // Propiedades calculadas para mostrar información relacionada
        public string ClienteNombre { get; set; }
        public string ClienteNumero { get; set; }
        public string ClienteDocumento { get; set; }
        public string SedeNombre { get; set; }
        public string SedeDireccion { get; set; }
        public string TipoCitaNombre { get; set; }
        public string TipoCitaIcono { get; set; }
    }
}