namespace SitecoreExtensions.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Sitecore;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Links;
    using Sitecore.Resources.Media;
    using Sitecore.Web.UI.WebControls;

    using SitecoreModels.BaseTemplates;
    using SitecoreModels.FieldTypes;

    using Image = SitecoreModels.FieldTypes.Image;

    public static class MappingHelper
    {
        public static T MapBaseItem<T>(ref T obj, Item item) where T : IBaseItem
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.Item = item;
            obj.ID = item.ID.Guid;
            obj.Name = item.Name;
            obj.DisplayName = item.DisplayName;
            obj.FullPath = item.Paths.Path;
            obj.SortOrder = GetInt(item[FieldIDs.Sortorder]);
            obj.TemplateId = item.TemplateID.Guid;
            obj.Updated = item.Statistics.Updated;
            obj.Url = LinkManager.GetItemUrl(item);

            return obj;
        }

        public static T MapBasePage<T>(ref T obj, Item item) where T : IBasePage
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            MapBaseItem(ref obj, item);
            MapBody(ref obj, item);
            MapCoding(ref obj, item);
            MapImage(ref obj, item);
            MapNavigation(ref obj, item);
            MapOpenGraph(ref obj, item);
            MapSearchEngineOptimisation(ref obj, item);
            MapSummary(ref obj, item);
            MapTitles(ref obj, item);

            return obj;
        }

        public static T MapImage<T>(ref T obj, Item item) where T : IImage
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.Thumbnail = MapImageField(item, "Thumbnail");

            return obj;
        }

        public static T MapImageGallery<T>(ref T obj, Item item) where T : IImageGallery
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var imageGallery = new List<Image>();

            var multiListField = (MultilistField)item.Fields["ImageGallery"];
            if (multiListField != null && multiListField.TargetIDs.Any())
            {
                foreach (var targetID in multiListField.TargetIDs)
                {
                    MediaItem targetItem = Context.Database.GetItem(targetID);
                    if (targetItem != null)
                    {
                        var image = new Image
                                        {
                                            MediaItem = targetItem,
                                            Alt = targetItem.Alt,
                                            Height = GetInt(targetItem.InnerItem["Height"]),
                                            Width = GetInt(targetItem.InnerItem["Width"]),
                                            Src = MediaManager.GetMediaUrl(targetItem)
                                        };

                        imageGallery.Add(image);
                    }
                }
            }

            obj.ImageGallery = imageGallery;
            return obj;
        }

        public static T MapBody<T>(ref T obj, Item item) where T : IBody
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.Body = new HtmlString(FieldRenderer.Render(item, "Body"));
            return obj;
        }

        public static T MapDate<T>(ref T obj, Item item) where T : IDate
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.Date = ((DateField)item.Fields["Date"]).DateTime;
            return obj;
        }

        public static T MapCoding<T>(ref T obj, Item item) where T : ICoding
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.JavascriptCodeTop = new HtmlString(FieldRenderer.Render(item, "JavascriptCodeTop"));
            obj.JavascriptCodeBottom = new HtmlString(FieldRenderer.Render(item, "JavascriptCodeBottom"));
            obj.CssCode = new HtmlString(FieldRenderer.Render(item, "CssCode"));

            return obj;
        }

        public static T MapOpenGraph<T>(ref T obj, Item item) where T : IOpenGraph
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.OpenGraphTitle = new HtmlString(FieldRenderer.Render(item, "OpenGraphTitle"));
            obj.OpenGraphDescription = new HtmlString(FieldRenderer.Render(item, "OpenGraphDescription"));
            obj.OpenGraphImageRendered = new HtmlString(FieldRenderer.Render(item, "OpenGraphImage"));
            obj.OpenGraphImage = MapImageField(item, "OpenGraphImage");

            return obj;
        }

        public static T MapSearchEngineOptimisation<T>(ref T obj, Item item) where T : ISearchEngineOptimisation
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.SeoMetaKeywords = new HtmlString(FieldRenderer.Render(item, "SeoMetaKeywords"));
            obj.SeoMetaDescription = new HtmlString(FieldRenderer.Render(item, "SeoMetaDescription"));
            obj.SeoCustomMetaData = new HtmlString(FieldRenderer.Render(item, "SeoCustomMetaData"));
            obj.SeoCanIndex = ((CheckboxField)item.Fields["SeoCanIndex"]).Checked;
            obj.SeoFollowLinks = ((CheckboxField)item.Fields["SeoFollowLinks"]).Checked;

            return obj;
        }

        public static T MapTitles<T>(ref T obj, Item item) where T : ITitles
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.Title = new HtmlString(FieldRenderer.Render(item, "Title"));
            obj.BrowserTitle = new HtmlString(FieldRenderer.Render(item, "BrowserTitle"));
            obj.ShortTitle = new HtmlString(FieldRenderer.Render(item, "ShortTitle"));
            obj.ShowTitle = ((CheckboxField)item.Fields["ShowTitle"]).Checked;

            return obj;
        }

        public static T MapNavigation<T>(ref T obj, Item item) where T : INavigation
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.ShowInNavigation = ((CheckboxField)item.Fields["ShowInNavigation"]).Checked;
            obj.ShowInSearch = ((CheckboxField)item.Fields["ShowInSearch"]).Checked;
            obj.ShowInSitemap = ((CheckboxField)item.Fields["ShowInSitemap"]).Checked;

            return obj;
        }

        public static T MapSummary<T>(ref T obj, Item item) where T : ISummary
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            obj.Summary = new HtmlString(FieldRenderer.Render(item, "Summary"));
            return obj;
        }

        public static int GetInt(string str)
        {
            int x;
            int.TryParse(str, out x);
            return x;
        }

        public static Image MapImageField(Item item, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (fieldName == null)
            {
                throw new ArgumentNullException("fieldName");
            }

            var field = (ImageField)item.Fields[fieldName];
            if (field == null || field.MediaItem == null)
            {
                return null;
            }

            return new Image
            {
                MediaItem = field.MediaItem,
                Src = MediaManager.GetMediaUrl(item),
                Width = GetInt(field.Width),
                Height = GetInt(field.Height),
                Alt = field.Alt,
                CssClass = field.Class
            };
        }

        public static File MapFileField(Item item, string fieldName)
        {
            var field = (FileField)item.Fields[fieldName];
            if (field == null || field.MediaItem == null)
            {
                return null;
            }

            return new File { Src = MediaManager.GetMediaUrl(item), Size = new MediaItem(field.MediaItem).Size };
        }
    }
}