using Calculator;
using Grpc.Core;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var channel = GrpcChannel.ForAddress("http://calculator:8080");

var client = new Calculator.Calculator.CalculatorClient(channel);

app.MapGet("/add", async (double x, double y) =>
{
    var request = new OperationRequest
    {
        Number1 = x,
        Number2 = y
    };
    
    var response = await client.AddAsync(request);
    return Results.Ok(response.Result);
});

app.MapGet("/subtract", async (double x, double y) =>
{
    var request = new OperationRequest
    {
        Number1 = x,
        Number2 = y
    };
    
    var response = await client.SubtractAsync(request);
    return Results.Ok(response.Result);
});

app.MapGet("/multiply", async (double x, double y) =>
{
    var request = new OperationRequest
    {
        Number1 = x,
        Number2 = y
    };
    
    var response = await client.MultiplyAsync(request);
    return Results.Ok(response.Result);
});

app.MapGet("/divide", async (double x, double y) =>
{
    try 
    {
        var request = new OperationRequest
        {
            Number1 = x,
            Number2 = y
        };
    
        var response = await client.DivideAsync(request);
        return Results.Ok(response.Result);
    }
    catch (RpcException e) 
    {
        return Results.Problem(e.Status.Detail);
    }
});
app.Run();