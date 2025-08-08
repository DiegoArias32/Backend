using Entity.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.TipoCitaDto
{
    public class DeleteLogicalTipoCitaDto : BaseDto
    {
        public DeleteLogicalTipoCitaDto()
        {
            Status = false;
            IsActive = false;
        }
    }
}