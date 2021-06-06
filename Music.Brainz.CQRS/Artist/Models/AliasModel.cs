namespace Music.Brainz.CQRS.Artist.Models
{
    public class AliasModel
    {
        public string SortName { get; set; }

        public string TypeId { get; set; }

        public string Name { get; set; }

        public string Locale { get; set; }

        public string Type { get; set; }

        public bool? Primary { get; set; }

        public object BeginDate { get; set; }

        public object EndDate { get; set; }
    }
}
