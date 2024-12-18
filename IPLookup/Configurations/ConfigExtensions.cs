using IPLookup.Clients;
using IPLookup.Configurations;
using IPLookup.Exceptions;
using Polly;
using Polly.Fallback;
using Polly.Retry;
using Refit;

public static class ConfigExtensions
{
    public static void AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<LoggingDelegatingHandler>();
        services.AddTransient<AccessKeyDelegatingHandler>();

        var policyWrap = Policy.WrapAsync(CreateFallbackPolicy(), CreateRetryPolicy());

        services.AddConfiguredRefitClient<IIPStackClient>(new Uri(configuration["IPStack:BaseURL"]!), policyWrap);
    }

    private static IHttpClientBuilder AddConfiguredRefitClient<T>(this IServiceCollection services, Uri baseApiUri, IAsyncPolicy<HttpResponseMessage> policyWrap)
        where T : class =>
        services
            .AddRefitClient<T>()
            .ConfigureHttpClient(c => c.BaseAddress = baseApiUri)
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddHttpMessageHandler<AccessKeyDelegatingHandler>()
            .AddPolicyHandler(policyWrap);

    private static AsyncRetryPolicy<HttpResponseMessage> CreateRetryPolicy() =>
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .RetryAsync(
                retryCount: 2,
                onRetry: (outcome, retryCount, _) =>
                {
                    Console.WriteLine($"Retry {retryCount}: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                });

    private static AsyncFallbackPolicy<HttpResponseMessage> CreateFallbackPolicy() =>
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .FallbackAsync(
                fallbackAction: (_, _) => throw new IPServiceNotAvailableException(
                    "IP Stack service not available after retries.", null),
                onFallbackAsync: (_, _) =>
                {
                    Console.WriteLine("Retries exhausted. Triggering fallback...");
                    return Task.CompletedTask;
                });
}


