using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Music.Brainz.Common.Settings;
using Music.Brainz.Infrastructure;
using Music.Brainz.Infrastructure.Services.MusicBrainz;
using NSubstitute;
using RichardSzalay.MockHttp;
using Xunit;
using AutoFixture;
using Music.Brainz.Common.Domain.MusicBrainz.Models;
using Newtonsoft.Json;

namespace Music.Brainz.Tests.Infrastructure
{
    public class ArtistServiceTest
    {
        private ArtistService _sut;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly Fixture _fixture;

        public ArtistServiceTest()
        {
            _fixture = new Fixture();
            _appSettings = Options.Create(new AppSettings()
            {
                MusicBrainzApiSettings = new MusicBrainzApiSettings()
                {
                    BaseUrl = "http://MusicBrainzUrl",
                    LookupRoute = "lookuproute",
                    SearchRoute = "searchroute"
                }
            });
        }

        [Fact]
        public async Task SearchArtist_ShouldBeSuccess()
        {
            // Arrange
            var mockResponse = _fixture.Create<ArtistList>();

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{_appSettings.Value.MusicBrainzApiSettings.BaseUrl}/{_appSettings.Value.MusicBrainzApiSettings.SearchRoute }")
                .Respond("application/json", JsonConvert.SerializeObject(mockResponse));

            var client = mockHttp.ToHttpClient();

            _sut = new ArtistService(
                client,
                _appSettings,
                new ApiResponseHandler(Substitute.For<ILogger<ApiResponseHandler>>()),
                Substitute.For<ILogger<ArtistService>>());

            // Act
            var response = await _sut.SearchArtistAsync("artistName");
            var artistList = response.Value;

            // Assert
            Assert.True(response.IsSuccess);
            Assert.True(artistList.Artists.Any());
            Assert.Equal(artistList.Artists.First()?.Name, mockResponse.Artists.First().Name);
        }

        [Fact]
        public async Task SearchArtist_ShouldFail()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{_appSettings.Value.MusicBrainzApiSettings.BaseUrl}/{_appSettings.Value.MusicBrainzApiSettings.SearchRoute }")
                .Respond(req => new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var client = mockHttp.ToHttpClient();

            _sut = new ArtistService(
                client,
                _appSettings,
                new ApiResponseHandler(Substitute.For<ILogger<ApiResponseHandler>>()),
                Substitute.For<ILogger<ArtistService>>());

            // Act
            var response = await _sut.SearchArtistAsync("artistName");

            // Assert
            Assert.True(response.IsFailed);
        }
    }
}
