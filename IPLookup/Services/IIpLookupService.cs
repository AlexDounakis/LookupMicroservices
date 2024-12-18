using IPLookup.Models;

namespace IPLookup.Services
{
    public interface IIpLookupService
    {
        Task<IpDetails?> GetIpDetailsAsync(string ipAddress);
    }
}
