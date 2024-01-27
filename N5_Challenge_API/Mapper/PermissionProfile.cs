using AutoMapper;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Entitys;

namespace N5_Challenge_API.Mapper
{
    public class PermisssionProfile : Profile
    {
        public PermisssionProfile()
        {
            CreateMap<PermissionDto, permision>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.IdEmployee,
                    opt => opt.MapFrom(src => src.IdEmployee)
                )
                .ForMember(
                    dest => dest.IdPermissionType,
                    opt => opt.MapFrom(src => src.IdPermissionType)
                )
                .ForMember(
                    dest => dest.Enabled,
                    opt => opt.MapFrom(src => src.Enabled)
                ).ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => src.CreatedDate)
                );

            CreateMap<permision, PermissionDto>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.IdEmployee,
                   opt => opt.MapFrom(src => src.IdEmployee)
               )
               .ForMember(
                   dest => dest.IdPermissionType,
                   opt => opt.MapFrom(src => src.IdPermissionType)
               )
               .ForMember(
                   dest => dest.Enabled,
                   opt => opt.MapFrom(src => src.Enabled)
               ).ForMember(
                   dest => dest.CreatedDate,
                   opt => opt.MapFrom(src => src.CreatedDate)
               )
               .ForMember(
                   dest =>  dest.PermissionType,
                   opt => opt.MapFrom(src => src.IdPermissionTypeNavigation)
               );
        }
    }
}
