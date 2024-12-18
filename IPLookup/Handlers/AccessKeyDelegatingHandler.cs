using IPLookup.Configurations;
using Microsoft.Extensions.Options;

namespace IPLookup.Handlers
{
    public class AccessKeyDelegatingHandler : DelegatingHandler
    {
        private readonly string _accessKey;

        public AccessKeyDelegatingHandler(IOptions<ApiSettings> options)
        {
            ArgumentNullException.ThrowIfNull(_accessKey = options.Value.IPStackApiKey);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("access_key", "=" + _accessKey);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
