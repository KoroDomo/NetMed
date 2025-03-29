

using AutoMapper;
using NetMed.Application.Dtos.Status;
using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Application.Mapper
{
    public class StatusMapper : Profile
    {
        public StatusMapper()
        {
            CreateMap<StatusModel, StatusDto>()
           .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.StatusID))
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName));

            CreateMap<Status, StatusDto>()
           .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName));

          CreateMap<Status, UpdateStatusDto>()
         .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName));

        }

    }
}
