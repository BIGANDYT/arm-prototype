namespace SitecoreModels.FieldTypes
{
    using Newtonsoft.Json;

    using Sitecore.Data.Items;

    public class Image
    {
        [JsonIgnore]
        public MediaItem MediaItem { get; set; }

        public string Src { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Alt { get; set; }

        public string CssClass { get; set; }
    }
}