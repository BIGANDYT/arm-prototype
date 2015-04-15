namespace SitecoreExtensions.ComputedIndexFields
{
    using System;
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.ContentSearch.Diagnostics;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public abstract class ComputedIndexFieldBase : IComputedIndexField
    {
        public static DeviceItem DefaultDeviceItem { get; set; }

        public static ID DefaultDeviceItemID = ID.Parse("{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}");

        static ComputedIndexFieldBase()
        {
            // Ensure masterDB exists
            var contextItem = Factory.GetDatabaseNames().Contains("master")
                ? Database.GetDatabase("master").GetItem(DefaultDeviceItemID)
                : Database.GetDatabase("web").GetItem(DefaultDeviceItemID);

            if (contextItem != null)
            {
                DefaultDeviceItem = new DeviceItem(contextItem);
            }
        }

        protected bool IsIndexableItem(IIndexable indexable, IsIndexableItemOptions options = null)
        {
            Assert.ArgumentNotNull(indexable, "indexable");

            var scIndexable = indexable as SitecoreIndexableItem;
            if (scIndexable == null)
            {
                CrawlingLog.Log.Warn(this + " : unsupported IIndexable type : " + indexable.GetType());
                return false;
            }

            var item = (Item)scIndexable;
            if (item == null)
            {
                CrawlingLog.Log.Warn(this + " : unsupported SitecoreIndexableItem type : " + scIndexable.GetType());
                return false;
            }

            options = options ?? new IsIndexableItemOptions();

            // optimization to reduce indexing time by skipping this logic for items in the Core database
            if (options.IgnoreCore && String.Compare(item.Database.Name, "core", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return false;
            }

            // have we restricted Items to a particular template?
            if (options.IncludeTemplateIDs != null && !options.IncludeTemplateIDs.Contains(item.TemplateID))
            {
                return false;
            }

            // have we restricted Items to a particular BASE template?
            if (options.IncludeTemplateIDsBase != null)
            {
                var baseTemplateIDs = item.Template.BaseTemplates.Select(x => x.ID).ToList();

                // Check if any of the options.IncludeTemplateIDsBase are found in baseTemplates
                if (!options.IncludeTemplateIDsBase.Any(baseTemplateIDs.Contains))
                {
                    return false;
                }
            }

            // have we excluded Item with a particular template?
            if (options.ExcludeTemplateIDs != null && options.ExcludeTemplateIDs.Contains(item.TemplateID))
            {
                return false;
            }

            // Must the item be accessible from the website?
            if (options.MustHaveUrl)
            {
                if (!item.Paths.IsMediaItem)
                {
                    if (!item.Paths.IsContentItem)
                    {
                        return false;
                    }

                    //if (item.Visualization.GetLayout(DefaultDeviceItem) == null)
                    //{
                    //    return false;
                    //}
                }
            }

            // Must an item sit have a particular ancestor?
            if (options.IncludeRootIDs != null)
            {
                var longID = item.Paths.LongID;
                var found = options.IncludeRootIDs.Any(id => longID.Contains(id.ToString()));

                if (!found)
                {
                    return false;
                }
            }

            return true;
        }

        public abstract object ComputeFieldValue(IIndexable indexable);

        public string FieldName { get; set; }

        public string ReturnType { get; set; }
    }
}