using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Music.Brainz.Common.Domain.MusicBrainz;
using Music.Brainz.Common.Response;
using Music.Brainz.CQRS.Artist.Models;

namespace Music.Brainz.CQRS.Artist.Queries.GetArtist
{
    public class LookUpArtistHandler : IRequestHandler<LookUpArtistQuery, Response>
    {
        private readonly IArtistService _artistService;
        private readonly IErrorResponseFactory _errorResponseFactory;
        private readonly ILogger<LookUpArtistHandler> _logger;
        private readonly IMapper _mapper;
        public LookUpArtistHandler(IArtistService artistService,
            IErrorResponseFactory errorResponseFactory,
            ILogger<LookUpArtistHandler> logger,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _errorResponseFactory = errorResponseFactory ?? throw new ArgumentNullException(nameof(errorResponseFactory));
            _artistService = artistService ?? throw new ArgumentNullException(nameof(artistService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Handle 'LookUpArtistQuery' to find artist by Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response> Handle(LookUpArtistQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var artistResult = await _artistService.LookUpArtistAsync(request.Id, request.Includes, cancellationToken);

            // Handle if server error
            if (artistResult.IsFailed)
            {
                var response = _errorResponseFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, artistResult);
                _logger.LogError("Error getting artist");
                return response;
            }

            // Handle if no artist found
            if (artistResult.Value == null)
            {
                var response = _errorResponseFactory.CreateErrorResponse(HttpStatusCode.NotFound, artistResult);
                _logger.LogError("Artist not found");
                return response;
            }

            // Log for Trace
            _logger.LogTrace("Successfully got artist.");

            // Map to Artist Model
            var model = _mapper.Map<ArtistModel>(artistResult.Value);

            // Transform result to ValueResponse
            return new ValueResponse<ArtistModel>(HttpStatusCode.OK, model);
        }
    }
}
