using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class ArtistList
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }
    }
}