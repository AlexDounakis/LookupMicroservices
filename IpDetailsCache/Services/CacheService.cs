using IpDetailsCache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IPDetailsCache.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IpDetails? GetIpDetails(string ip)
        {
            return _cache.TryGetValue(ip, out IpDetails? ipDetails) ? ipDetails : null;
        }

        public void SetIpDetails(IpDetails ipDetails)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(ipDetails.Ip, ipDetails, cacheEntryOptions);
        }
    }
}
