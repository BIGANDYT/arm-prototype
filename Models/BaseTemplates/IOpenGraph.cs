namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    using SitecoreModels.FieldTypes;

    public interface IOpenGraph
    {
        HtmlString OpenGraphTitle { get; set; }

        HtmlString OpenGraphDescription { get; set; }

        HtmlString OpenGraphImageRendered { get; set; }

        Image OpenGraphImage { get; set; }
    }
}