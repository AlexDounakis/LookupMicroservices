using Refit;

namespace BatchProcessing.Clients
{
    public interface IIPLookupClient
    {
        [Get("/api/iplookup/{ipAddress}")]
        Task<HttpResponseMessage> GetIpDetailsAsync(string ipAddress);
    }
}
