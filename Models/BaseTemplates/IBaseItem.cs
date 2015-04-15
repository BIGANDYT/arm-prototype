namespace SitecoreModels.BaseTemplates
{
    using System;

    using Newtonsoft.Json;

    using Sitecore.Data.Items;

    public interface IBaseItem
    {
        Item Item { get; set; }

        Guid ID { get; set; }

        string Url { get; set; }

        string FullPath { get; set; }

        string Name { get; set; }

        string DisplayName { get; set; }

        Guid TemplateId { get; set; }

        int SortOrder { get; set; }

        DateTime Updated { get; set; }
    }

    public class BaseItem : IBaseItem
    {
        [JsonIgnore]
        public virtual Item Item { get; set; }

        public virtual Guid ID { get; set; }

        public virtual string Url { get; set; }

        public virtual string FullPath { get; set; }

        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual Guid TemplateId { get; set; }

        public virtual int SortOrder { get; set; }

        public virtual DateTime Updated { get; set; }
    }
}