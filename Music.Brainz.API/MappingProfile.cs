using AutoMapper;
using Music.Brainz.Common.Domain.MusicBrainz.Models;
using Music.Brainz.CQRS.Artist.Models;

namespace Music.Brainz.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Alias, AliasModel>();
            CreateMap<Area, AreaModel>();
            CreateMap<Artist, ArtistModel>();
            CreateMap<ArtistList, ArtistListModel>();
            CreateMap<BeginArea, BeginAreaModel>();
            CreateMap<LifeSpan, LifeSpanModel>();
            CreateMap<Release, ReleaseModel>();
            CreateMap<ReleaseEvent, ReleaseEventModel>();
            CreateMap<TextRepresentation, TextRepresentationModel>();
        }
    }
}
