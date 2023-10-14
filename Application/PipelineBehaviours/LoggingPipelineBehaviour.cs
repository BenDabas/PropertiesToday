using Application.PipelineBehaviours.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.PipelineBehaviours
{
    public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ILogable
    {
        private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

        public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Currently handling request: {typeof(TRequest).Name}");
            
            var response = await next();

            _logger.LogInformation($"Currently handling response: {typeof(TResponse).Name}");

            return response;
        }
    }
}
