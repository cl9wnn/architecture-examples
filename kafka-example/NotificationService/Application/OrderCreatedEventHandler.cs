using Domain;
using Microsoft.Extensions.Logging;

namespace Application;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : IMessageHandler<OrderCreatedEvent>
{
    public Task HandleAsync(OrderCreatedEvent message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order №{Id} ({name}) is created at {time}.", message.Id, message.Name, message.CreatedAt);
        // Notify user about the creation order by email, sms, push-messages...
        
        return Task.CompletedTask;
    }
}