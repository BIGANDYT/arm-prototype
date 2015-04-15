namespace SitecoreRepository
{
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.SecurityModel;

    public class TaxonomyRepository
    {
        private readonly ID categoryTemplateID = new ID("{B43C2493-20B6-403D-893B-1441260D38B0}");
        private readonly ID optionTemplateID = new ID("{1B6F06DD-D34B-4F76-8FF5-1B4120EB1612}");

        public Dictionary<ID, string> GetCategories()
        {
            var dic = new Dictionary<ID, string>();

            var query = string.Format("/sitecore/content/Global/Taxonomy//*[@@templateid='{0}']", this.categoryTemplateID);

            foreach (var categoryItem in Context.Database.SelectItems(query))
            {
                if (categoryItem.Children.Any(x => x.TemplateID == this.optionTemplateID))
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

        public Item TagItem(ID itemID, IEnumerable<ID> optionIDs)
        {
            var item = Context.Database.GetItem(itemID);
            if (item == null)
            {
                return null;
            }

            var developerTypes = new List<ID>();
            var developResourceTypes = new List<ID>();
            var productTechServiceNames = new List<ID>();
            var productResourceTypes = new List<ID>();
            
            var query = string.Format("/sitecore/content/Global/Taxonomy//*[@@templateid='{0}']", this.optionTemplateID);
            var parents = Context.Database.SelectItems(query).ToDictionary(x => x.ID, x => x.ParentID);

            foreach (var optionID in optionIDs)
            {
                ID parentID;
                if (!parents.TryGetValue(optionID, out parentID))
                {
                    continue;
                }

                switch (parentID.ToString().ToUpper())
                {
                    case "{51ACDBCA-3076-4BF0-8F55-E13121E2DF31}":
                        developerTypes.Add(optionID);
                        break;
                    case "{E02F91A3-770E-4449-8704-A2A4A474FACD}":
                        developResourceTypes.Add(optionID);
                        break;
                    case "{5E347DC8-38E5-43E0-96D8-BEC1AC27593E}":
                        productTechServiceNames.Add(optionID);
                        break;
                    case "{375FC297-7FE2-4AD5-8C11-60200E83DCB2}":
                        productResourceTypes.Add(optionID);
                        break;
                }
            }

            using (new SecurityDisabler())
            {
                item.Editing.BeginEdit();
                item["DeveloperTypes"] = string.Join("|", developerTypes);
                item["DeveloperResourceTypes"] = string.Join("|", developResourceTypes);
                item["ProductTechServiceNames"] = string.Join("|", productTechServiceNames);
                item["ProductResourceTypes"] = string.Join("|", productResourceTypes);
                item.Editing.EndEdit();
            }

            return item;
        }
    }
}