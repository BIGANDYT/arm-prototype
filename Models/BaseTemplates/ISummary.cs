namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    public interface ISummary
    {
        HtmlString Summary { get; set; }
    }
}