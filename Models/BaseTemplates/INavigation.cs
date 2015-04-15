namespace SitecoreModels.BaseTemplates
{
    public interface INavigation
    {
        bool ShowInNavigation { get; set; }

        bool ShowInSitemap { get; set; }

        bool ShowInSearch { get; set; }
    }
}