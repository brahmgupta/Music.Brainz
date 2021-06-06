using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Music.Brainz.Common.Domain.MusicBrainz;
using Music.Brainz.Common.Domain.MusicBrainz.Enum;
using Music.Brainz.Common.Domain.MusicBrainz.Models;
using Music.Brainz.Common.Settings;

namespace Music.Brainz.Infrastructure.Services.MusicBrainz
{
    public class ArtistService : MusicBrainzService, IArtistService
    {
        public ArtistService(HttpClient httpClient, IOptions<AppSettings> settings,
            IApiResponseHandler apiResponseHandler,
            ILogger<ArtistService> logger) : base(httpClient, settings, apiResponseHandler, logger)
        {
        }

        /// <summary>
        /// Search artist
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offSet"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ArtistList>> SearchArtistAsync(string query, int limit = 10, int offSet = 0, CancellationToken cancellationToken = default)
        {
            var url = GetSearchUrl(EntityEnum.Artist, query, limit, offSet);

            var response = await GetAsync(url, cancellationToken);
            var result = await _apiResponseHandler.HandleResponse<ArtistList>(response);

            _logger.LogTrace("Handled Search Artist API Call");

            return result;
        }

        /// <summary>
        /// Get Artist by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Artist>> LookUpArtistAsync(string id, IncEnum[] includes, CancellationToken cancellationToken = default)
        {
            var url = GetLookupUrl(EntityEnum.Artist, id, includes);

            var response = await GetAsync(url, cancellationToken);
            var result = await _apiResponseHandler.HandleResponse<Artist>(response);

            _logger.LogTrace("Handled Lookup Artist API Call");

            return result;
        }
    }
}
