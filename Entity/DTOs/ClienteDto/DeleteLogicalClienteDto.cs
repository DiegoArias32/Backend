using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.ClienteDto
{
    public class DeleteLogicalClienteDto : BaseDto
    {
        public DeleteLogicalClienteDto()
        {
            Status = false;
            IsActive = false;
        }
    }
}