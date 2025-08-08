using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.CitaDto
{
    public class DeleteLogicalCitaDto : BaseDto
    {
        public DeleteLogicalCitaDto()
        {
            Status = false;
            IsActive = false;
        }
    }
}