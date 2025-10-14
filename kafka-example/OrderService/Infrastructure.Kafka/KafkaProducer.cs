using Confluent.Kafka;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka;

public class KafkaProducer<TMessage> : IMessageProducer<TMessage>
{
    private readonly string _topic;
    private readonly IProducer<string, TMessage> _producer;
    private readonly ILogger<KafkaProducer<TMessage>> _logger;
    
    public KafkaProducer(IOptions<KafkaOptions> options, ILogger<KafkaProducer<TMessage>> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
        };
        
        _producer = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new KafkaJsonSerializer<TMessage>())
            .SetErrorHandler((_, e) => _logger.LogError("Kafka Producer error: {Reason}", e.Reason))
            .Build();
        
        _topic = options.Value.Topic;
    }
    
    public async Task ProduceAsync(TMessage message, string? key = null, CancellationToken cancellationToken = default)
    {
        var kafkaMessage = new Message<string, TMessage>
        {
            Key = key ?? Guid.NewGuid().ToString(),
            Value = message
        };

        try
        {
            await _producer.ProduceAsync(_topic, kafkaMessage, cancellationToken);
        }
        catch (ProduceException<string, TMessage> ex)
        {
            _logger.LogError(ex, "Failed to produce message: {Msg}", ex.Error.Reason);
            throw;
        }
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}