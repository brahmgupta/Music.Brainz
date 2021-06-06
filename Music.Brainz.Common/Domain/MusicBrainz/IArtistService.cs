using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Music.Brainz.Common.Domain.MusicBrainz.Enum;
using Music.Brainz.Common.Domain.MusicBrainz.Models;

namespace Music.Brainz.Common.Domain.MusicBrainz
{
    public interface IArtistService
    {
        Task<Result<ArtistList>> SearchArtistAsync(string query, int limit = 10, int offSet = 0, CancellationToken cancellationToken = default);
        Task<Result<Artist>> LookUpArtistAsync(string id, IncEnum[] includes, CancellationToken cancellationToken = default);
    }
}
