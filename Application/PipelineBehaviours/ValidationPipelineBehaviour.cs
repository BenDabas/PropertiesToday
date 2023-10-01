using Application.Exceptions;
using Application.PipelineBehaviours.Contracts;
using FluentValidation;
using MediatR;

namespace Application.PipelineBehaviours
{
    // This class represents a pipeline behavior in MediatR, specifically used for validation of requests using FluentValidation.
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidatable
    {
        // Holds the validators that will be used to validate the incoming requests.
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // This method is responsible for handling the incoming request and validating it.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Check if there are any validators provided.
            if (_validators.Any())
            {
                // Create a validation context with the incoming request (wrapped the request).
                var context = new ValidationContext<TRequest>(request);

                List<string> errors = new();

                // Run all the validators asynchronously and wait for the results.
                var validationResults = await Task
                    .WhenAll(_validators
                        .Select(x => x.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if(failures.Count != 0)
                {
                    foreach(var failure in failures)
                    {
                        errors.Add(failure.ErrorMessage);
                    }

                    throw new CustomValidationException(errors, "One or more validation failure(s) occured.");
                }
            }

            // If validation passes, or there are no validators, continue to the next behavior or handler in the pipeline.
            return await next();
        }
    }
}
