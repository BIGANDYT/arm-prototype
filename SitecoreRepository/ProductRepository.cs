namespace SitecoreRepository
{
    using System.Collections.Generic;
    using Sitecore;

    using SitecoreExtensions.Helpers;

    using SitecoreModels.Products;

    public class ProductRepository
    {
        public IEnumerable<Product> GetAll()
        {
            foreach (var item in Context.Database.SelectItems("/sitecore/content/Home/Products/*"))
            {
                var productItem = new Product();
                MappingHelper.MapBasePage(ref productItem, item);

                yield return productItem;
            }
        } 
    }
}