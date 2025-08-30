using CleanArchitecture.Application.Common.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IMemoryCache _cache;

        public CachingBehavior(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Nếu request không implement ICachableRequest => bỏ qua cache
            if (request is not ICachableRequest cachable)
                return await next();

            // Nếu đã có trong cache => trả về luôn
            if (_cache.TryGetValue(cachable.CacheKey, out TResponse response))
                return response;

            // Nếu chưa có => chạy handler và lưu vào cache
            response = await next();
            _cache.Set(
                cachable.CacheKey,
                response,
                cachable.Expiration ?? TimeSpan.FromMinutes(5)
            );

            return response;
        }
    }
}
