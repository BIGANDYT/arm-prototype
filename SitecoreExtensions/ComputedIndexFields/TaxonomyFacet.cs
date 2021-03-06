﻿namespace SitecoreExtensions.ComputedIndexFields
{
    using System.Linq;

    using Sitecore.ContentSearch;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class TaxonomyFacet : ComputedIndexFieldBase
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            if (!this.IsIndexableItem(indexable))
            {
                return null;
            }

            var item = (Item)(SitecoreIndexableItem)indexable;

            var fieldNameWithoutFacet = FieldName.Replace("facet", string.Empty);
            MultilistField multiListField = item.Fields[fieldNameWithoutFacet];

            if (multiListField == null)
            {
                return null;
            }

            return multiListField.GetItems().Select(x => x.DisplayName).ToList();
        }
    }
}