
using AutoMapper;
using NetMed.Application.Dtos.Roles;
using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Application.Mapper
{
    public class RolesMapper : Profile
    {
        public RolesMapper() 
        {
         CreateMap<RolesModel, RolesDto>()
            .ForMember(r => r.RolesId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(r => r.RoleName, opt => opt.MapFrom(src => src.RoleName))
            .ReverseMap();

            CreateMap<Roles, UpdateRolesDto>()
          .ForMember(r => r.RolesId, opt => opt.MapFrom(src => src.Id))
          .ForMember(r => r.RoleName, opt => opt.MapFrom(src => src.RoleName))
          .ReverseMap();


            CreateMap<RolesModel, UpdateRolesDto>()
           .ForMember(r => r.RolesId, opt => opt.MapFrom(src => src.RoleId))
           .ForMember(r => r.RoleName, opt => opt.MapFrom(src => src.RoleName))
           .ReverseMap();



        }
    }
}
