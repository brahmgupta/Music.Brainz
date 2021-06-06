using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentResults;
using Microsoft.Extensions.Logging;
using Music.Brainz.API;
using Music.Brainz.Common.Domain.MusicBrainz;
using Music.Brainz.Common.Domain.MusicBrainz.Models;
using Music.Brainz.Common.Response;
using Music.Brainz.CQRS;
using Music.Brainz.CQRS.Artist.Models;
using Music.Brainz.CQRS.Artist.Queries.SearchArtists;
using NSubstitute;
using Xunit;

namespace Music.Brainz.Tests.CQRS
{
    public class SearchArtistHandlerTest
    {
        private SearchArtistHandler _sut;
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;

        public SearchArtistHandlerTest()
        {
            _fixture = new Fixture();

            var mockMapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task Should_MapArtistsToModel()
        {
            // Arrange
            var artistService = Substitute.For<IArtistService>();

            var mockArtistListResponse = Results.Ok<ArtistList>(_fixture.Create<ArtistList>());

            artistService.SearchArtistAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(mockArtistListResponse);

            _sut = new SearchArtistHandler(artistService,
                Substitute.For<IErrorResponseFactory>(),
                Substitute.For<ILogger<SearchArtistHandler>>(),
                _mapper);

            // Act
            var response = await _sut.Handle(new SearchArtistQuery() { Query = "test" }, CancellationToken.None);
            var valueResponse = (ValueResponse<ArtistListModel>)response;

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.IsType<ValueResponse<ArtistListModel>>(response);
            Assert.Equal(valueResponse.Value.Artists.Count(), mockArtistListResponse.Value.Artists.Count());
        }
    }
}
