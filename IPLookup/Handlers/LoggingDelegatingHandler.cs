﻿using System.Net.Http.Headers;

namespace IPLookup.Handlers
{
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingDelegatingHandler> _logger;

        public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var req = request;
            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} -   Request]";

            _logger.LogInformation($"{msg}========Start==========");
            _logger.LogInformation($"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
            _logger.LogInformation($"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");

            foreach (var header in req.Headers)
                _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (req.Content != null)
            {
                foreach (var header in req.Content.Headers)
                    _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (req.Content is StringContent || IsTextBasedContentType(req.Headers) || IsTextBasedContentType(req.Content.Headers))
                {
                    var result = await req.Content.ReadAsStringAsync();

                    _logger.LogInformation($"{msg} Content:");
                    _logger.LogInformation($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
                }
            }

            var start = DateTime.Now;
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var end = DateTime.Now;

            _logger.LogInformation($"{msg} Duration: {end - start}");
            _logger.LogInformation($"{msg}==========End==========");

            msg = $"[{id} - Response]";
            _logger.LogInformation($"{msg}=========Start=========");

            var resp = response;

            _logger.LogInformation($"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");

            foreach (var header in resp.Headers)
                _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (resp.Content != null)
            {
                foreach (var header in resp.Content.Headers)
                    _logger.LogInformation($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (resp.Content is StringContent || IsTextBasedContentType(resp.Headers) || IsTextBasedContentType(resp.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await resp.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    _logger.LogInformation($"{msg} Content:");
                    _logger.LogInformation($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
                    _logger.LogInformation($"{msg} Duration: {end - start}");
                }
            }

            _logger.LogInformation($"{msg}==========End==========");

            return response;

        }

        readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string> values;
            if (!headers.TryGetValues("Content-Type", out values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
}
