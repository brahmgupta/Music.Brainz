using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class LifeSpan
    {
        [JsonProperty("ended")]
        public bool? Ended { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("begin")]
        public string Begin { get; set; }
    }
}
