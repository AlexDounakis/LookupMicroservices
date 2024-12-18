using IpDetailsCache.Models;

namespace IPDetailsCache.Services
{
    public interface ICacheService
    {
        IpDetails? GetIpDetails(string ip);
        void SetIpDetails(IpDetails ipDetails);
    }
}
