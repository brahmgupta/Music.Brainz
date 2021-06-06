using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Music.Brainz.Infrastructure.Services
{
    public abstract class HttpServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private const string UserAgent = "202106-brahmgupta";

        protected HttpServiceBase(HttpClient client, ILogger logger)
        {
            _httpClient = client;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                var httpResponse = await _httpClient.GetAsync(new Uri(uri), cancellationToken);

                return httpResponse;
            }
            catch(Exception exception)
            {
                _logger.LogError("An error has occured during API call. '{RequestUri}' with error message ", uri, exception.InnerException?.Message);
                throw;
            }
        }
    }
}
