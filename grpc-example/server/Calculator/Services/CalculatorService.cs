using Grpc.Core;

namespace Calculator.Services;

public class CalculatorService(ILogger<CalculatorService> logger): Calculator.CalculatorBase
{
    public override Task<OperationResponse> Add(OperationRequest request, ServerCallContext context)
    {
        var operationResult = request.Number1 + request.Number2;
        logger.LogInformation($"Add: {request.Number1} + {request.Number2} = {operationResult}");
        return Task.FromResult(new OperationResponse { Result = operationResult });
    }

    public override Task<OperationResponse> Subtract(OperationRequest request, ServerCallContext context)
    {
        var operationResult = request.Number1 - request.Number2;
        logger.LogInformation($"Subtract: {request.Number1} - {request.Number2} = {operationResult}");
        return Task.FromResult(new OperationResponse { Result = operationResult });
    }

    public override Task<OperationResponse> Multiply(OperationRequest request, ServerCallContext context)
    {
        var operationResult = request.Number1 * request.Number2;
        logger.LogInformation($"Multiply: {request.Number1} * {request.Number2} = {operationResult}");
        return Task.FromResult(new OperationResponse { Result = operationResult });
    }

    public override Task<OperationResponse> Divide(OperationRequest request, ServerCallContext context)
    {
        if (request.Number2 == 0)
        {
            logger.LogWarning("Division by zero attempted");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Cannot divide by zero."));
        }
        
        var operationResult = request.Number1 / request.Number2;
        logger.LogInformation($"Divide: {request.Number1} / {request.Number2} = {operationResult}");
        return Task.FromResult(new OperationResponse { Result = operationResult });
    }
}