namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    public interface ITitles
    {
        HtmlString Title { get; set; }

        HtmlString BrowserTitle { get; set; }

        HtmlString ShortTitle { get; set; }

        bool ShowTitle { get; set; }
    }
}