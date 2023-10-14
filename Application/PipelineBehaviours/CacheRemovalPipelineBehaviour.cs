using Application.Models;
using Application.PipelineBehaviours.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Application.PipelineBehaviours
{
    public class CacheRemovalPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoval
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettingsModel _cacheSettings;
        public CacheRemovalPipelineBehaviour(IDistributedCache cache, IOptions<CacheSettingsModel> cacheSettings)
        {
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            foreach( var key in request.CacheKeys )
            {
                string cacheKey = $"{_cacheSettings.ApplicationName}:{key}";

                var cacheResponse = await _cache.GetAsync(cacheKey, cancellationToken);

                if (cacheResponse != null)
                    await _cache.RemoveAsync(cacheKey, cancellationToken);

            }

            return response;
        }
    }
}
