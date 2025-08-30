using CleanArchitecture.Application.Common.Caching;
using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries
{
    public record GetAllProductsQuery(string Id) : IRequest<IEnumerable<ProductDto>>, ICachableRequest
    {
        public string CacheKey => "GetAllProducts";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(15);
    }
}
