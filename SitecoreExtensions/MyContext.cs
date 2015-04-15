namespace SitecoreExtensions
{
    using System;

    using Sitecore;
    using Sitecore.Caching;
    using Sitecore.ContentSearch;

    using SitecoreExtensions.Helpers;

    using SitecoreModels.BaseTemplates;

    public class MyContext
    {
        private static readonly ItemsContext _items;

        public static ItemsContext Items
        {
            get
            {
                return _items;
            }
        }

        static MyContext()
        {
            _items = new ItemsContext();
        }

        public static IBasePage BasePage
        {
            get
            {
                const string Key = "BasePage";
                if (Items[Key] == null)
                {
                    if (Context.Item == null)
                    {
                        throw new Exception("Sitecore.Context.Item cannot be null");
                    }

                    var basePage = new BasePage();
                    Items[Key] = MappingHelper.MapBasePage(ref basePage, Context.Item);
                }

                return (IBasePage)Items[Key];
            }
        }

        public static ISearchIndex SearchIndex
        {
            get
            {
                const string Key = "ContentSearchMasterIndex";
                if (Items[Key] == null)
                {
                    if (Context.Database == null)
                    {
                        throw new Exception("Sitecore.Context.Database cannot be null");
                    }

                    Items[Key] = Context.Database.Name == "web"
                                     ? ContentSearchManager.GetIndex("sitecore_web_index")
                                     : ContentSearchManager.GetIndex("sitecore_master_index");
                }

                return (ISearchIndex)Items[Key];
            }
        }

        //public static ContextSearchResultItem CurrentSearchResultItem
        //{
        //    get
        //    {
        //        if (Sitecore.Context.Item == null) return null;

        //        using (var s = SearchIndex.CreateSearchContext())
        //        {
        //            return s.GetQueryable<ContextSearchResultItem>()
        //                    .FirstOrDefault(x => x.ItemId == Sitecore.Context.Item.ID && (!IsMaster || x.IsLatestVersion));
        //        }
        //    }
        //}

        public static bool IsMaster
        {
            get { return Context.Database != null && Context.Database.Name == "master"; }
        }
    }
}