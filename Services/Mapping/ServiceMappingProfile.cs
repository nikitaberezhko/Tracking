using AutoMapper;
using Domain;
using Services.Services.Models.Request;
using Services.Services.Models.Response;

namespace Services.Mapping;

public class ServiceMappingProfile : Profile
{
    public ServiceMappingProfile()
    {
        // Request models -> Domain
        CreateMap<GetStatusModel, Status>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.Ignore())
            .ForMember(d => d.CompletionPercent, map => map.Ignore())
            .ForMember(d => d.StatusType, map => map.Ignore());
        
        CreateMap<GetStatusByOrderIdModel, Status>()
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.CompletionPercent, map => map.Ignore())
            .ForMember(d => d.StatusType, map => map.Ignore());
        
        CreateMap<UpdateStatusModel, Status>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.CompletionPercent, map => map.MapFrom(c => c.CompletionPercent))
            .ForMember(d => d.StatusType, map => map.MapFrom(c => c.StatusType))
            .ForMember(d => d.OrderId, map => map.Ignore());
        
        // Domain -> Response models
        CreateMap<Status, StatusModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.CompletionPercent, map => map.MapFrom(c => c.CompletionPercent))
            .ForMember(d => d.StatusType, map => map.MapFrom(c => c.StatusType));
    }
}