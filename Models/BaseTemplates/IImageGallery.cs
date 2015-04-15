namespace SitecoreModels.BaseTemplates
{
    using System.Collections.Generic;

    using SitecoreModels.FieldTypes;

    public interface IImageGallery
    {
        IEnumerable<Image> ImageGallery { get; set; }
    }
}