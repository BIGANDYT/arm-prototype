namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    using SitecoreModels.FieldTypes;

    public interface IBasePage : IBaseItem, ISummary, IBody, IImage, ITitles, ICoding, INavigation, IOpenGraph, ISearchEngineOptimisation
    {
    }

    public class BasePage : BaseItem, IBasePage
    {
        public virtual HtmlString Summary { get; set; }

        public virtual HtmlString Body { get; set; }

        public virtual Image Thumbnail { get; set; }

        public virtual HtmlString Title { get; set; }

        public virtual HtmlString BrowserTitle { get; set; }

        public virtual HtmlString ShortTitle { get; set; }

        public virtual bool ShowInNavigation { get; set; }

        public virtual bool ShowInSitemap { get; set; }

        public virtual bool ShowInSearch { get; set; }

        public virtual bool ShowChildren { get; set; }

        public virtual bool ShowTitle { get; set; }

        public virtual HtmlString SeoMetaKeywords { get; set; }

        public virtual HtmlString SeoMetaDescription { get; set; }

        public virtual bool SeoCanIndex { get; set; }

        public virtual bool SeoFollowLinks { get; set; }

        public virtual HtmlString SeoCustomMetaData { get; set; }

        public virtual HtmlString OpenGraphTitle { get; set; }

        public virtual HtmlString OpenGraphDescription { get; set; }

        public virtual HtmlString OpenGraphImageRendered { get; set; }

        public virtual Image OpenGraphImage { get; set; }

        public virtual HtmlString JavascriptCodeTop { get; set; }

        public virtual HtmlString JavascriptCodeBottom { get; set; }

        public virtual HtmlString CssCode { get; set; }
    }
}