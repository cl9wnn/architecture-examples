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
            AutoOffsetReset = Enum.Parse<AutoOffsetReset>(options.Value.AutoOffsetReset, true),
            EnableAutoCommit = options.Value.EnableAutoCommit,
        };

        _topic = options.Value.Topic;

        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => ConsumeAsync(stoppingToken);

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        _consumer.Dispose();
        return base.StopAsync(cancellationToken);
    }
    
    private async Task ConsumeAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);
                    if (result?.Message == null)
                        continue;
                    
                    await _messageHandler.HandleAsync(result.Message.Value, stoppingToken);
                    _consumer.Commit(result);
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Kafka consume error: {Reason}", ex.Error.Reason);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing message");
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Kafka consumer stopping...");
        }
    }
}