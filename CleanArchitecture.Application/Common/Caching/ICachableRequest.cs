namespace CleanArchitecture.Application.Common.Caching
{
    public interface ICachableRequest
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
