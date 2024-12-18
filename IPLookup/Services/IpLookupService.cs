using IPLookup.Clients;
using IPLookup.Configurations;
using IPLookup.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace IPLookup.Services
{
    public class IpLookupService : IIpLookupService
    {
        private readonly IIPStackClient _ipStackApiClient;
        private readonly ILogger<IpLookupService> _logger;
        private readonly string _accessKey;
        public IpLookupService(ILogger<IpLookupService> logger,
            IIPStackClient ipStackClient,
            IOptions<ApiSettings> options)
        {
            _logger = logger;
            _ipStackApiClient = ipStackClient;
            ArgumentNullException.ThrowIfNull(_accessKey = options.Value.IPStackApiKey);
        }

        public async Task<IpDetails?> GetIpDetailsAsync(string ipAddress)
        {
            var response = await _ipStackApiClient.GetIpDetailsAsync(ipAddress, _accessKey);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("IPStack client response was unsuccessful with Status Code : {StatusCode}", response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IpDetails>(content);

        }
    }
}
