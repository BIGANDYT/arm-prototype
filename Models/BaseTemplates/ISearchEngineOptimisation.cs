namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    public interface ISearchEngineOptimisation
    {
        HtmlString SeoMetaKeywords { get; set; }

        HtmlString SeoMetaDescription { get; set; }

        bool SeoCanIndex { get; set; }

        bool SeoFollowLinks { get; set; }

        HtmlString SeoCustomMetaData { get; set; }
    }
}