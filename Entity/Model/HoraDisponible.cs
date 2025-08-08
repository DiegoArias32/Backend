using Entity.Base;
using System;

namespace Entity.Model
{
    public class HoraDisponible : BaseEntity
    {
        public string Hora { get; set; } // Ejemplo: 08:30, 09:00, etc.
        public int SedeId { get; set; }
        public int? TipoCitaId { get; set; } // Opcional: si quieres que ciertas horas sean solo para ciertos tipos de cita

        // Navegación
        public virtual Sede Sede { get; set; }
        public virtual TipoCita TipoCita { get; set; }
    }
}
