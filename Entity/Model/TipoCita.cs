using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class TipoCita : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int TiempoEstimadoMinutos { get; set; } = 120; // 2 horas por defecto
        public bool RequiereDocumentacion { get; set; } = true;

        // Navegación
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}