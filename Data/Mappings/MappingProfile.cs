using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===== CLIENTE MAPPINGS =====
            CreateMap<Entity.Model.Cliente, Entity.DTOs.ClienteDto.ClienteDto>();

            CreateMap<Entity.DTOs.ClienteDto.ClienteDto, Entity.Model.Cliente>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignorar en creación
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Se maneja automáticamente
                .ForMember(dest => dest.Citas, opt => opt.Ignore()); // Ignorar navegación

            // Mapping para updates - solo mapear valores no nulos
            CreateMap<Entity.DTOs.ClienteDto.UpdateClienteDto, Entity.Model.Cliente>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping para eliminación lógica
            CreateMap<Entity.DTOs.ClienteDto.DeleteLogicalClienteDto, Entity.Model.Cliente>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ===== SEDE MAPPINGS =====
            CreateMap<Entity.Model.Sede, Entity.DTOs.SedeDto.SedeDto>();

            CreateMap<Entity.DTOs.SedeDto.SedeDto, Entity.Model.Sede>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore());

            // Mapping para updates - solo mapear valores no nulos
            CreateMap<Entity.DTOs.SedeDto.UpdateSedeDto, Entity.Model.Sede>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping para eliminación lógica
            CreateMap<Entity.DTOs.SedeDto.DeleteLogicalSedeDto, Entity.Model.Sede>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ===== TIPO CITA MAPPINGS =====
            CreateMap<Entity.Model.TipoCita, Entity.DTOs.TipoCitaDto.TipoCitaDto>();

            CreateMap<Entity.DTOs.TipoCitaDto.TipoCitaDto, Entity.Model.TipoCita>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore());

            // Mapping para updates - solo mapear valores no nulos
            CreateMap<Entity.DTOs.TipoCitaDto.UpdateTipoCitaDto, Entity.Model.TipoCita>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping para eliminación lógica
            CreateMap<Entity.DTOs.TipoCitaDto.DeleteLogicalTipoCitaDto, Entity.Model.TipoCita>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ===== CITA MAPPINGS =====
            // Cita to CitaDto (con propiedades calculadas)
            CreateMap<Entity.Model.Cita, Entity.DTOs.CitaDto.CitaDto>()
                .ForMember(dest => dest.ClienteNombre,
                    opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.NombreCompleto : string.Empty))
                .ForMember(dest => dest.ClienteNumero,
                    opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.NumeroCliente : string.Empty))
                .ForMember(dest => dest.ClienteDocumento,
                    opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.NumeroDocumento : string.Empty))
                .ForMember(dest => dest.SedeNombre,
                    opt => opt.MapFrom(src => src.Sede != null ? src.Sede.Nombre : string.Empty))
                .ForMember(dest => dest.SedeDireccion,
                    opt => opt.MapFrom(src => src.Sede != null ? src.Sede.Direccion : string.Empty))
                .ForMember(dest => dest.TipoCitaNombre,
                    opt => opt.MapFrom(src => src.TipoCita != null ? src.TipoCita.Nombre : string.Empty))
                .ForMember(dest => dest.TipoCitaIcono,
                    opt => opt.MapFrom(src => src.TipoCita != null ? src.TipoCita.Icono : string.Empty));

            // CitaDto to Cita (ignorando navegación)
            CreateMap<Entity.DTOs.CitaDto.CitaDto, Entity.Model.Cita>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Sede, opt => opt.Ignore())
                .ForMember(dest => dest.TipoCita, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // UpdateCitaDto to Cita - solo mapear valores no nulos
            CreateMap<Entity.DTOs.CitaDto.UpdateCitaDto, Entity.Model.Cita>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Sede, opt => opt.Ignore())
                .ForMember(dest => dest.TipoCita, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping para eliminación lógica
            CreateMap<Entity.DTOs.CitaDto.DeleteLogicalCitaDto, Entity.Model.Cita>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Sede, opt => opt.Ignore())
                .ForMember(dest => dest.TipoCita, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));



            // ===== HORA DISPONIBLE MAPPINGS =====
            CreateMap<Entity.Model.HoraDisponible, Entity.DTOs.HoraDisponibleDto.HoraDisponibleDto>()
                .ForMember(dest => dest.SedeNombre,
                    opt => opt.MapFrom(src => src.Sede != null ? src.Sede.Nombre : string.Empty))
                .ForMember(dest => dest.TipoCitaNombre,
                    opt => opt.MapFrom(src => src.TipoCita != null ? src.TipoCita.Nombre : string.Empty));

            CreateMap<Entity.DTOs.HoraDisponibleDto.HoraDisponibleDto, Entity.Model.HoraDisponible>()
                .ForMember(dest => dest.Sede, opt => opt.Ignore())
                .ForMember(dest => dest.TipoCita, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Mapping para updates - solo mapear valores no nulos
                 CreateMap<Entity.DTOs.HoraDisponibleDto.UpdateHoraDisponibleDto, Entity.Model.HoraDisponible>()
                .ForMember(dest => dest.Sede, opt => opt.Ignore())
                .ForMember(dest => dest.TipoCita, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping para eliminación lógica
                CreateMap<Entity.DTOs.HoraDisponibleDto.DeleteLogicalHoraDisponibleDto, Entity.Model.HoraDisponible>()
               .ForMember(dest => dest.Sede, opt => opt.Ignore())
               .ForMember(dest => dest.TipoCita, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
               .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}