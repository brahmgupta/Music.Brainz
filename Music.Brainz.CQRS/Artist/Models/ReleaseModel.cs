using System.Collections.Generic;

namespace Music.Brainz.CQRS.Artist.Models
{
    public class ReleaseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}
