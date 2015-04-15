namespace SitecoreModels.BaseTemplates
{
    using SitecoreModels.FieldTypes;

    public interface IImage
    {
        Image Thumbnail { get; set; }
    }
}