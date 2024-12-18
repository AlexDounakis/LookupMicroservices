using Microsoft.AspNetCore.Mvc;
using Refit;

namespace IPLookup.Clients
{
    public interface IIPStackClient
    {
        [Get("/{ipAddress}")]
        Task<HttpResponseMessage> GetIpDetailsAsync(string ipAddress, [FromQuery] string access_key);
    }
}
