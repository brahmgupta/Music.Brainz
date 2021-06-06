using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Brainz.CQRS.Artist.Models
{
    public class ArtistListModel
    {
        public int Count { get; set; }

        public int Offset { get; set; }

        public List<ArtistModel> Artists { get; set; }
    }
}
