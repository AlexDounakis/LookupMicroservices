using IPDetailsCache.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IpDetailsCache
{
    public static class CacheConfigExtensions
    {
        public static void AddCacheConfig(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
        }

    }
}
