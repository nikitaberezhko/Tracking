using Domain;
using Exceptions.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class StatusRepository(DbContext dbContext) : IStatusRepository
{
    public async Task<Guid> CreateAsync(Status entity)
    {
        try
        {
            entity.Id = Guid.NewGuid();
            entity.CompletionPercent = default;
            entity.StatusType = StatusEnum.Created;
            
            await dbContext.Set<Status>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        
            return entity.Id;
        }
        catch
        {
            throw new DomainException
            {
                Title = "Create status failed",
                Message = "Status with this order id already exists",
                StatusCode = StatusCodes.Status409Conflict
            };
        }
    }
    
    public async Task<Status> GetAsync(Guid id)
    {
        var result = await dbContext.Set<Status>().FirstOrDefaultAsync(x => x.Id == id);
        if (result != null)
            return result;
        
        throw new DomainException
        {
            Title = "Status not found",
            Message = "Status with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<Status> GetByOrderIdAsync(Status entity)
    {
        var result = await dbContext.Set<Status>().FirstOrDefaultAsync(x => x.OrderId == entity.OrderId);
        if (result != null)
            return result;

        throw new DomainException
        {
            Title = "Status not found",
            Message = "Status with this order id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<Status> UpdateAsync(Status entity)
    {
        var status = await dbContext.Set<Status>().FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (status != null)
        {
            entity.OrderId = status.OrderId;
            dbContext.Entry(status).CurrentValues.SetValues(entity);
            
            await dbContext.SaveChangesAsync();
            return status;
        }
        
        throw new DomainException
        {
            Title = "Status not found",
            Message = "Status with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}