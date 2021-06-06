using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Music.Brainz.Common.Domain.MusicBrainz.Enum;
using Music.Brainz.Common.Settings;

namespace Music.Brainz.Infrastructure.Services.MusicBrainz
{
    public abstract class MusicBrainzService : HttpServiceBase
    {
        private readonly MusicBrainzApiSettings _musicBrainzApiSettings;
        protected readonly ILogger<MusicBrainzService> _logger;
        protected readonly IApiResponseHandler _apiResponseHandler;

        protected MusicBrainzService(HttpClient httpClient, IOptions<AppSettings> settings,
            IApiResponseHandler apiResponseHandler,
            ILogger<MusicBrainzService> logger) : base(httpClient, logger)
        {
            _apiResponseHandler = apiResponseHandler ?? throw new ArgumentNullException(nameof(apiResponseHandler));
            _musicBrainzApiSettings = settings?.Value?.MusicBrainzApiSettings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Format and return Search Route
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        protected string GetSearchUrl(EntityEnum entity, string query, int limit, int offset)
        {
            query = WebUtility.UrlEncode(query);
            var searchRoute = string.Format(_musicBrainzApiSettings.SearchRoute, entity.ToString().ToLower(), query, limit, offset);
            return $"{_musicBrainzApiSettings.BaseUrl}/{searchRoute}";
        }

        /// <summary>
        /// Format and return Lookup Route
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        protected string GetLookupUrl(EntityEnum entity, string id, IncEnum[] includes)
        {
            var includesSubquery = this.GetIncludesSubquery(includes);
            var lookupRoute = string.Format(_musicBrainzApiSettings.LookupRoute , entity.ToString().ToLower(), id, includesSubquery);
            return $"{_musicBrainzApiSettings.BaseUrl}/{lookupRoute}";
        }

        /// <summary>
        /// Return IncEnum array joined by '+' sign
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        protected string GetIncludesSubquery(IncEnum[] includes)
        {
            return includes?.Length > 0 ? string.Join("+", includes.Select(s => s.ToString().ToLower()).ToArray()) : string.Empty;
        }
    }
}
