using BusModels;
using Domain;
using MassTransit;
using Services.Repositories.Abstractions;

namespace Infrastructure.Bus.Implementations.Consumers;

public class CreateOrderConsumer(
    IStatusRepository statusRepository) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var status = new Status
        {
            OrderId = context.Message.OrderId,
            CompletionPercent = 0,
            StatusType = StatusEnum.Created
        };

        await statusRepository.CreateAsync(status);
    }
}