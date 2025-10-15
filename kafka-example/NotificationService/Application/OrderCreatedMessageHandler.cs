using Domain;
using Microsoft.Extensions.Logging;

namespace Application;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger) : IMessageHandler<OrderCreatedMessage>
{
    public Task HandleAsync(OrderCreatedMessage message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order №{Id} ({name}) is created at {time}.", message.Id, message.Name, message.CreatedAt);
        // Notify user about the creation order by email, sms, push-messages...
        
        return Task.CompletedTask;
    }
}