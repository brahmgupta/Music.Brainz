using System.Collections.Generic;
using Newtonsoft.Json;

namespace Music.Brainz.Common.Domain.MusicBrainz.Models
{
    public class Artist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("type-id")]
        public string TypeId { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("sort-name")]
        public string SortName { get; set; }
        
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("area")]
        public Area Area { get; set; }

        [JsonProperty("life-span")]
        public LifeSpan LifeSpan { get; set; }

        [JsonProperty("gender-id")]
        public string GenderId { get; set; }
        
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("begin-area")]
        public BeginArea BeginArea { get; set; }

        [JsonProperty("aliases")]
        public List<Alias> Aliases { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("releases")]
        public List<Release> Releases { get; set; }
    }
}
