namespace SitecoreModels.BaseTemplates
{
    using System.Web;

    public interface IBody
    {
        HtmlString Body { get; set; }
    }
}