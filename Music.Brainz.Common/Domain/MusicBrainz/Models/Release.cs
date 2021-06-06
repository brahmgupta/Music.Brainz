using System.Collections.Generic;
using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class Release
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("packaging")]
        public string Packaging { get; set; }

        [JsonProperty("text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        [JsonProperty("release-events")]
        public List<ReleaseEvent> ReleaseEvents { get; set; }

        [JsonProperty("status-id")]
        public string StatusId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("packaging-id")]
        public string PackagingId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
