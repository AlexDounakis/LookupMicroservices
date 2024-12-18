using AutoMapper;
using BatchProcessing.Clients;
using BatchProcessing.Models;
using IPDetailsCache.Services;
using System.Text.Json;

namespace BatchProcessing.Services
{
    public class JobProcessor : IJobProcessor
    {
        private readonly ICacheService _cacheService;
        private readonly IIPLookupClient _ipLookupClient;
        private readonly IMapper _mapper;
        private readonly ILogger<JobProcessor> _logger;

        public JobProcessor(
            ICacheService cacheService,
            IIPLookupClient ipLookupClient,
            IMapper mapper,
            ILogger<JobProcessor> logger)
        {
            _cacheService = cacheService;
            _ipLookupClient = ipLookupClient;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task GetData(List<string> chunk)
        {
            var tasks = chunk.Select(ip => _ipLookupClient.GetIpDetailsAsync(ip));
            var responses = await Task.WhenAll(tasks);

            foreach (var response in responses)
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ipDetails = JsonSerializer.Deserialize<IpDetails>(content);
                    _cacheService.SetIpDetails(MapToCacheModel(ipDetails!));
                }
                else
                {
                    _logger.LogError("JobProcessor - Fetching data for '{response.RequestMessage}' has not succeeded with status code : {response.StatusCode}", response.RequestMessage, response.StatusCode);
                }
            }
        }

        private IpDetailsCache.Models.IpDetails MapToCacheModel(IpDetails details) =>
            _mapper.Map<IpDetailsCache.Models.IpDetails>(details);

    }
}
