using AutoMapper;
using Domain;
using Exceptions.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Models.Request;
using Services.Services.Models.Response;

namespace Services.Services.Implementations;

public class StatusService(
    IStatusRepository statusRepository,
    IMapper mapper,
    IValidator<GetStatusModel> getStatusValidator,
    IValidator<GetStatusByOrderIdModel> getStatusByOrderIdValidator,
    IValidator<UpdateStatusModel> updateStatusValidator) : IStatusService
{
    public async Task<StatusModel> GetStatus(GetStatusModel model)
    {
        var validationResult = await getStatusValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation error",
                Message = "Status validation error",
                StatusCode = StatusCodes.Status400BadRequest
            };

        var status = mapper.Map<Status>(model);
        var statusResult = await statusRepository.GetAsync(status.Id);
        
        var result = mapper.Map<StatusModel>(statusResult);
        return result;
    }
    
    public async Task<StatusModel> GetStatusByOrderId(GetStatusByOrderIdModel model)
    {
        var validationResult = await getStatusByOrderIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation error",
                Message = "Status validation error",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var status = mapper.Map<Status>(model);
        var statusResult = await statusRepository.GetByOrderIdAsync(status);
        
        var result = mapper.Map<StatusModel>(statusResult);
        return result;
    }
    
    public async Task<StatusModel> UpdateStatus(UpdateStatusModel model)
    {
        var validationResult = await updateStatusValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation error",
                Message = "Status validation error",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var status = mapper.Map<Status>(model);
        var statusResult = await statusRepository.UpdateAsync(status);
        
        var result = mapper.Map<StatusModel>(statusResult);
        return result;
    }
}