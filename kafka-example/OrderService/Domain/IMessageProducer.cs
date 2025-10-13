namespace Domain;

public interface IMessageProducer<in TMessage>: IDisposable
{
    Task ProduceAsync(TMessage message, string? key = null, CancellationToken cancellationToken = default);
}