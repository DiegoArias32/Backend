using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.SedeDto
{
    public class UpdateSedeDto : BaseDto
    {
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public bool? EsPrincipal { get; set; }
    }
}