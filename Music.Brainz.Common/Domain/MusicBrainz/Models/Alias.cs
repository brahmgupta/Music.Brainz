using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class Alias
    {
        [JsonProperty("sort-name")]
        public string SortName { get; set; }

        [JsonProperty("type-id")]
        public string TypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("primary")]
        public bool? Primary { get; set; }

        [JsonProperty("begin-date")]
        public object BeginDate { get; set; }

        [JsonProperty("end-date")]
        public object EndDate { get; set; }
    }
}
