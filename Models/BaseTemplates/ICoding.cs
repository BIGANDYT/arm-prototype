namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    public interface ICoding
    {
        HtmlString JavascriptCodeTop { get; set; }

        HtmlString JavascriptCodeBottom { get; set; }

        HtmlString CssCode { get; set; }
    }
}