using AutoMapper;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Application.Mapper
{
    public class InsuranceProviderMapper : Profile
    {
        public InsuranceProviderMapper()
        {
            // Mapeo de ModeloEntidad -> DTO
            CreateMap<InsuranceProviderModel, InsuranceProviderDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ContactNumber))
                .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.CoverageDetails, opt => opt.MapFrom(src => src.CoverageDetails))
                .ForMember(dest => dest.IsPreferred, opt => opt.MapFrom(src => src.IsPreferred))
                .ForMember(dest => dest.NetworkTypeID, opt => opt.MapFrom(src => src.NetworkTypeID))
                .ForMember(dest => dest.AcceptedRegions, opt => opt.MapFrom(src => src.AcceptedRegions))
                .ForMember(dest => dest.MaxCoverageAmount, opt => opt.MapFrom(src => src.MaxCoverageAmount))
                .ReverseMap();

            CreateMap<InsuranceProviders, InsuranceProviderDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.CoverageDetails, opt => opt.MapFrom(src => src.CoverageDetails))
                .ForMember(dest => dest.IsPreferred, opt => opt.MapFrom(src => src.IsPreferred))
                .ForMember(dest => dest.NetworkTypeID, opt => opt.MapFrom(src => src.NetworkTypeID))
                .ForMember(dest => dest.AcceptedRegions, opt => opt.MapFrom(src => src.AcceptedRegions))
                .ForMember(dest => dest.MaxCoverageAmount, opt => opt.MapFrom(src => src.MaxCoverageAmount))
                .ReverseMap();

            CreateMap<InsuranceProviders, UpdateInsuranceProviderDto>()
                .ForMember(dest => dest.InsuranceProviderID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.ChangeDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.CoverageDetails, opt => opt.MapFrom(src => src.CoverageDetails))
                .ForMember(dest => dest.IsPreferred, opt => opt.MapFrom(src => src.IsPreferred))
                .ForMember(dest => dest.NetworkTypeID, opt => opt.MapFrom(src => src.NetworkTypeID))
                .ForMember(dest => dest.AcceptedRegions, opt => opt.MapFrom(src => src.AcceptedRegions))
                .ForMember(dest => dest.MaxCoverageAmount, opt => opt.MapFrom(src => src.MaxCoverageAmount))
                .ReverseMap();
        }
    }
}

