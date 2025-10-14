using Application;
using Domain;
using Messaging.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMessageHandler<OrderCreatedMessage>, OrderCreatedMessageHandler>();
builder.Services.AddKafkaConsumer<OrderCreatedMessage>(builder.Configuration.GetSection("Kafka:OrderCreated"));

var app = builder.Build();

app.Run();