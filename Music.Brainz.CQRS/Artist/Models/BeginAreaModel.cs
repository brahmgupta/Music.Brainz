
namespace Music.Brainz.CQRS.Artist.Models
{
    public class BeginAreaModel
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string TypeId { get; set; }

        public string Name { get; set; }

        public string SortName { get; set; }

        public LifeSpanModel LifeSpan { get; set; }
    }
}
