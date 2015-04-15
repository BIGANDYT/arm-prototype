namespace SitecoreRepository
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore;
    using Sitecore.Data;

    public class TaxonomyRepository
    {
        public Dictionary<ID, string> GetCategories()
        {
            var dic = new Dictionary<ID, string>();
            var categoryTemplateID = new ID("{B43C2493-20B6-403D-893B-1441260D38B0}");
            var optionTemplateID = new ID("{1B6F06DD-D34B-4F76-8FF5-1B4120EB1612}");

            var query = string.Format("/sitecore/content/Global/Taxonomy//*[@templateid='{0}']", categoryTemplateID);

            foreach (var categoryItem in Context.Database.SelectItems(query))
            {
                if (categoryItem.Children.Any(x => x.TemplateID == optionTemplateID))
                {
                    dic[categoryItem.ID] = categoryItem.DisplayName;
                }
            }

            return dic;
        }

        public Dictionary<ID, string> GetOptions(ID parentID)
        {
            var parentItem = Context.Database.GetItem(parentID);
            if (parentItem == null)
            {
                return null;
            }

            return parentItem.Children.ToDictionary(x => x.ID, x => x.DisplayName);
        } 
    }
}