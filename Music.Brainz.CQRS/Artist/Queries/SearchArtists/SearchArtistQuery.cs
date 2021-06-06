using MediatR;
using Music.Brainz.Common.Response;

namespace Music.Brainz.CQRS.Artist.Queries.SearchArtists
{
    public class SearchArtistQuery : IRequest<Response>
    {
        public string Query { get; set; }
        public int Limit { get; set; } = 10;
        public int OffSet { get; set; } = 0;
    }
}
