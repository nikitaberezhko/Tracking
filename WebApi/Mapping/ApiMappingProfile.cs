using AutoMapper;
using Services.Services.Models.Request;
using Services.Services.Models.Response;
using WebApi.Models.Status.Request;
using WebApi.Models.Status.Response;

namespace WebApi.Mapping;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // Requests -> Request models
        CreateMap<GetStatusRequest, GetStatusModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));

        CreateMap<GetStatusByOrderIdRequest, GetStatusByOrderIdModel>()
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId));

        CreateMap<UpdateStatusRequest, UpdateStatusModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.CompletionPercent, map => map.MapFrom(c => c.CompletionPercent))
            .ForMember(d => d.StatusType, map => map.MapFrom(c => c.StatusType));
        
        // Response models -> Responses 
        CreateMap<StatusModel, GetStatusResponse>()
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.CompletionPercent, map => map.MapFrom(c => c.CompletionPercent))
            .ForMember(d => d.StatusType, map => map.MapFrom(c => c.StatusType));

        CreateMap<StatusModel, GetStatusByOrderIdResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.CompletionPercent, map => map.MapFrom(c => c.CompletionPercent))
            .ForMember(d => d.StatusType, map => map.MapFrom(c => c.StatusType));
        
        CreateMap<StatusModel, UpdateStatusResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.CompletionPercent, map => map.MapFrom(c => c.CompletionPercent))
            .ForMember(d => d.StatusType, map => map.MapFrom(c => c.StatusType));
    }
}