using AutoMapper;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Infraestructure.Mapper
{
    public class NetworkTypeServiceMapper : Profile
    {
        public NetworkTypeServiceMapper()
        {
            
            CreateMap<NetworkTypeModel, NetworkTypeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();


            CreateMap<NetworkType, NetworkTypeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();

            
            CreateMap<NetworkType, UpdateNetworkTypeDto>()
                .ForMember(dest => dest.NetworkTypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();
        }
    }
}

