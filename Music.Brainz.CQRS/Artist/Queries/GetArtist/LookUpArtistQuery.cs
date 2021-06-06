using MediatR;
using Music.Brainz.Common.Domain.MusicBrainz.Enum;
using Music.Brainz.Common.Response;

namespace Music.Brainz.CQRS.Artist.Queries.GetArtist
{
    public class LookUpArtistQuery : IRequest<Response>
    {
        public string Id { get; set; }

        public IncEnum[] Includes { get; set; }
    }
}
