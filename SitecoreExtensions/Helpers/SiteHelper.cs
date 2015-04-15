namespace SitecoreExtensions.Helpers
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Sitecore.Sites;
    using Sitecore.Web;

    public static class SiteHelper
    {
        public static SiteContext GetContextSite()
        {
            if (Context.PageMode.IsPageEditor || Context.PageMode.IsPreview)
            {
                // item ID for page editor and front-end preview mode
                var id = WebUtil.GetQueryString("sc_itemid");

                Item item = null;

                // if a query string ID was found, get the item for page editor and front-end preview mode
                if (!string.IsNullOrEmpty(id))
                {
                    item = Context.Database.GetItem(id);
                }

                item = item ?? Context.Item;

                // loop through all configured sites
                foreach (var site in Factory.GetSiteInfoList())
                {
                    // get this site's home page item
                    var homePage = Context.Database.GetItem(site.RootPath + site.StartItem);

                    // if the item lives within this site, this is our context site
                    if (homePage != null && homePage.Axes.IsAncestorOf(item))
                    {
                        return Factory.GetSite(site.Name);
                    }
                }

                // fallback and assume context site
                return Context.Site;
            }

            // standard context site resolution via hostname, virtual/physical path, and port number
            return Context.Site;
        }
    }
}