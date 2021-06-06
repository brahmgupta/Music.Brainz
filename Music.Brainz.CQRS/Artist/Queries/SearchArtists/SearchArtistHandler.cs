using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Music.Brainz.Common.Domain.MusicBrainz;
using Music.Brainz.Common.Response;
using Music.Brainz.CQRS.Artist.Models;

namespace Music.Brainz.CQRS.Artist.Queries.SearchArtists
{
    public class SearchArtistHandler : IRequestHandler<SearchArtistQuery, Response>
    {
        private readonly IArtistService _artistService;
        private readonly IErrorResponseFactory _errorResponseFactory;
        private readonly ILogger<SearchArtistHandler> _logger;
        private readonly IMapper _mapper;
        public SearchArtistHandler(IArtistService artistService,
            IErrorResponseFactory errorResponseFactory,
            ILogger<SearchArtistHandler> logger,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _errorResponseFactory = errorResponseFactory ?? throw new ArgumentNullException(nameof(errorResponseFactory));
            _artistService = artistService ?? throw new ArgumentNullException(nameof(artistService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Handle 'SearchArtistQuery' to find Artist
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response> Handle(SearchArtistQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Query))
            {
                throw new ArgumentNullException(nameof(request.Query));
            }

            var searchArtistResult = await _artistService.SearchArtistAsync(request.Query, request.Limit, request.OffSet, cancellationToken);

            // Handle if server error
            if (searchArtistResult.IsFailed)
            {
                var response = _errorResponseFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, searchArtistResult);
                _logger.LogError("Error searching artists");
                return response;
            }

            // Handle if no artist found
            if (!searchArtistResult.Value.Artists.ToList().Any())
            {
                var response = _errorResponseFactory.CreateErrorResponse(HttpStatusCode.NotFound, searchArtistResult);
                _logger.LogError("Artist not found");
                return response;
            }

            // Log for Trace
            _logger.LogTrace("Successfully searched artists.");

            // Map to Artist Model
            var model = _mapper.Map<ArtistListModel>(searchArtistResult.Value);

            // Transform result to ValueResponse
            return new ValueResponse<ArtistListModel>(HttpStatusCode.OK, model);
        }
    }
}
