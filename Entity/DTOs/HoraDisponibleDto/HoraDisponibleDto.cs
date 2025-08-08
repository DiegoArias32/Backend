using Entity.DTOs.Base;
using System;

namespace Entity.DTOs.HoraDisponibleDto
{
    public class HoraDisponibleDto : BaseDto
    {
        public string Hora { get; set; }
        public int SedeId { get; set; }
        public int? TipoCitaId { get; set; }
        public string? SedeNombre { get; set; }
        public string? TipoCitaNombre { get; set; }
    }
}