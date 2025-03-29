

using AutoMapper;
using NetMed.Application.Dtos.Notification;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using System.Net;

namespace NetMed.Application.Mapper
{
    public class NotificationMapper : Profile
    {

        public NotificationMapper()
        {
            CreateMap<NotificationsModel, NotificationDto>()
           .ForMember(not => not.NotificationId, opt => opt.MapFrom(src => src.NotificationID))
           .ForMember(not => not.UserID, opt => opt.MapFrom(src => src.UserID))
           .ForMember(not => not.Message, opt => opt.MapFrom(src => src.Message))
           .ForMember(not => not.SentAt, opt => opt.MapFrom(src => src.SentAt))
           .ReverseMap();


           CreateMap<Notification, NotificationDto>()
          .ForMember(not => not.NotificationId, opt => opt.MapFrom(src => src.Id))
          .ForMember(not => not.UserID, opt => opt.MapFrom(src => src.UserID))
          .ForMember(not => not.Message, opt => opt.MapFrom(src => src.Message))
          .ForMember(not => not.SentAt, opt => opt.MapFrom(src => src.SentAt))
          .ReverseMap();

          CreateMap<Notification, UpdateNotificationDto>()
         .ForMember(not => not.NotificationId, opt => opt.MapFrom(src => src.Id))
         .ForMember(not => not.UserID, opt => opt.MapFrom(src => src.UserID))
         .ForMember(not => not.Message, opt => opt.MapFrom(src => src.Message))
         .ForMember(not => not.SentAt, opt => opt.MapFrom(src => src.SentAt))
         .ReverseMap();

        }

           
    }
}
