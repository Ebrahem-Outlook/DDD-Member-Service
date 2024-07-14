using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Core.Behaviors;

internal sealed class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("String Request ... {@RequestName} : {@DateTimeUtc}", typeof(TRequest), DateTime.UtcNow);

        TResponse response = await next();

        if (response.IsFaulier)
        {
            _logger.LogError("String Request ... {@RequestName} : {@Error} : {@DateTimeUtc}", typeof(TRequest), Error, DateTime.UtcNow);
        }

        _logger.LogInformation("End Request ... {@RequestName} : {@DateTimeUtc}", typeof(TRequest), DateTime.UtcNow);

        return response;
    }
}
