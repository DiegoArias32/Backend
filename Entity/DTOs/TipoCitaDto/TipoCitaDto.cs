using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.TipoCitaDto
{
    public class TipoCitaDto : BaseDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int TiempoEstimadoMinutos { get; set; }
        public bool RequiereDocumentacion { get; set; }
    }
}