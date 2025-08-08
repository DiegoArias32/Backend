using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Cita : BaseEntity
    {
        public string NumeroCita { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Completada, Cancelada
        public string? Observaciones { get; set; }
        public DateTime? FechaCompletada { get; set; }

        // Claves foráneas
        public int ClienteId { get; set; }
        public int SedeId { get; set; }
        public int TipoCitaId { get; set; }

        // Navegación
        public virtual Cliente Cliente { get; set; }
        public virtual Sede Sede { get; set; }
        public virtual TipoCita TipoCita { get; set; }
    }
}