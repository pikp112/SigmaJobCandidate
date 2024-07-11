using Microsoft.Extensions.Caching.Memory;

namespace SigmaCandidate.Infrastructure.Services
{
    public class MemoryCacheService(IMemoryCache cache) : ICacheService
    {
        private readonly IMemoryCache _cache = cache;

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
        {
            if (!_cache.TryGetValue(key, out T result))
            {
                result = await factory();
                _cache.Set(key, result, expiration);
            }
            return result;
        }
    }
}