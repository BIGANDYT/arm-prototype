namespace SitecoreExtensions.ComputedIndexFields
{
    using System.Collections.Generic;

    using Sitecore.Data;

    public class IsIndexableItemOptions
    {
        public bool IgnoreCore { get; set; }

        public bool MustHaveUrl { get; set; }

        public List<ID> IncludeTemplateIDs { get; set; }

        public List<ID> IncludeTemplateIDsBase { get; set; }

        public List<ID> ExcludeTemplateIDs { get; set; }

        public List<ID> IncludeRootIDs { get; set; }

        public IsIndexableItemOptions()
        {
            this.IgnoreCore = true;
            this.MustHaveUrl = true;
        }
    }
}