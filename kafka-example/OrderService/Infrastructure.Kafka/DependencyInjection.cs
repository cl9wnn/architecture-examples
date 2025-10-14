using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Kafka;

public static class DependencyInjection
{
    public static void AddKafkaProducer<TMessage>(this IServiceCollection services,
        IConfigurationSection configurationSection)
    {
        services.Configure<KafkaOptions>(configurationSection);
        services.AddSingleton<IMessageProducer<TMessage>, KafkaProducer<TMessage>>();
    }
}