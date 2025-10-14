using Confluent.Kafka;
using Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka;

public class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly string _topic;
    private readonly IConsumer<string, TMessage> _consumer;
    private readonly IMessageHandler<TMessage> _messageHandler;
    private readonly ILogger<KafkaConsumer<TMessage>> _logger;

    public KafkaConsumer(IOptions<KafkaOptions> options, IMessageHandler<TMessage> messageHandler,
        ILogger<KafkaConsumer<TMessage>> logger)
    {
        _messageHandler = messageHandler;
        _logger = logger;
        var config = new ConsumerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            GroupId = options.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
        };

        _topic = options.Value.Topic;

        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }
    private async Task? ConsumeAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                await _messageHandler.HandleAsync(result.Message.Value, stoppingToken);
            }
        }
        catch (ConsumeException ex)
        {
            _logger.LogError("Kafka consume error: {ex}", ex.Message);
        }
    }
}