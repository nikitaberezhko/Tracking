using BusModels;
using Domain;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Repositories.Abstractions;

namespace Infrastructure.Bus.Implementations.Consumers;

public class CreateOrderConsumer(
    IStatusRepository statusRepository,
    ILogger<CreateOrderConsumer> logger) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var status = new Status
        {
            OrderId = context.Message.OrderId,
            CompletionPercent = 0,
            StatusType = StatusEnum.Created
        };
        
        logger.LogInformation("New status created for new order with id: {OrderId}", context.Message.OrderId);

        await statusRepository.CreateAsync(status);
    }
}