using Domain;
using Messaging.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaProducer<Order>(builder.Configuration.GetSection("Kafka:Order"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/orders", async (IMessageProducer<Order> producer) =>
{
    await producer.ProduceAsync(new Order
    {
        Name = "Milk"
    });
});

app.Run();