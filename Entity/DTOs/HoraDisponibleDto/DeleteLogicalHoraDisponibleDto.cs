using Entity.DTOs.Base;

namespace Entity.DTOs.HoraDisponibleDto
{
    public class DeleteLogicalHoraDisponibleDto : BaseDto
    {
        public DeleteLogicalHoraDisponibleDto()
        {
            Status = false;
            IsActive = false;
        }
    }
}