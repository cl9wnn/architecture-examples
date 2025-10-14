using Domain;
using Microsoft.Extensions.Logging;

namespace Application;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger) : IMessageHandler<OrderCreatedMessage>
{
    public Task HandleAsync(OrderCreatedMessage message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order №{Id} ({name}) is created at {time}.", message.Id, message.Name, message.CreatedAt);
        
        return Task.CompletedTask;
    }
}