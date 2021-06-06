using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class TextRepresentation
    {
        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
