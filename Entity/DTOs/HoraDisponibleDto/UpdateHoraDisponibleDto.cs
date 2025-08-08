using Entity.DTOs.Base;
using System;

namespace Entity.DTOs.HoraDisponibleDto
{
    public class UpdateHoraDisponibleDto : BaseDto
    {
        public string? Hora { get; set; }
        public int? SedeId { get; set; }
        public int? TipoCitaId { get; set; }
    }
}