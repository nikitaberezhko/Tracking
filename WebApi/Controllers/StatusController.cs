using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Abstractions;
using Services.Services.Models.Request;
using WebApi.Models;
using WebApi.Models.Status.Request;
using WebApi.Models.Status.Response;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/statuses/")]
[ApiVersion(1.0)]
public class StatusController(
    IStatusService statusService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CommonResponse<GetStatusResponse>>> GetStatus(
        GetStatusRequest request)
    {
        var status = await statusService.GetStatus(mapper.Map<GetStatusModel>(request));
        
        var response = new CommonResponse<GetStatusResponse>
        {
            Data = new GetStatusResponse
            {
                OrderId = status.OrderId,
                CompletionPercent = status.CompletionPercent,
                StatusType = status.StatusType
            }
        };
        
        return response;
    }
    
    [HttpGet("orders")]
    public async Task<ActionResult<CommonResponse<GetStatusByOrderIdResponse>>> GetStatusById(
        GetStatusByOrderIdRequest request)
    {
        var status = await statusService.GetStatusByOrderId(
            mapper.Map<GetStatusByOrderIdModel>(request));
        
        var response = new CommonResponse<GetStatusByOrderIdResponse>
        {
            Data = new GetStatusByOrderIdResponse
            {
                Id = status.Id,
                CompletionPercent = status.CompletionPercent,
                StatusType = status.StatusType
            }
        };
        
        return response;
    }

    [HttpPatch]
    public async Task<ActionResult<CommonResponse<UpdateStatusResponse>>> UpdateStatus(
        UpdateStatusRequest request)
    {
        var status = await statusService.UpdateStatus(
            mapper.Map<UpdateStatusModel>(request));
        
        var response = new CommonResponse<UpdateStatusResponse>
        {
            Data = new UpdateStatusResponse
            {
                Id = status.Id,
                CompletionPercent = status.CompletionPercent,
                StatusType = status.StatusType
            }
        };
        
        return response;
    }
}