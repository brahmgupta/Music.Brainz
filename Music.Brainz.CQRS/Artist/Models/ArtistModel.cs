using System.Collections.Generic;

namespace Music.Brainz.CQRS.Artist.Models
{
    public class ArtistModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public List<ReleaseModel> Releases { get; set; }
    }
}
