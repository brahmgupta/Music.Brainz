using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class ReleaseEvent
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("area")]
        public Area Area { get; set; }
    }
}
