using Domain;
using Messaging.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaProducer<OrderCreatedEvent>(builder.Configuration.GetSection("Kafka:OrderCreated"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/orders", async (IMessageProducer<OrderCreatedEvent> producer) =>
{
    await producer.ProduceAsync(new OrderCreatedEvent
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Milk",
        CreatedAt = DateTime.UtcNow,
    });
});

app.Run();