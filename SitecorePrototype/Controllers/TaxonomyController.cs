namespace SitecorePrototype.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Sitecore.Common;
    using Sitecore.Data;
    using SitecoreRepository;

    [AllowAnonymous]
    public class TaxonomyController : ApiController
    {
        private readonly TaxonomyRepository repo;

        public TaxonomyController()
        {
            repo = new TaxonomyRepository();
        }

        public IHttpActionResult GetCategories()
        {
            var categories = repo.GetCategories();
            return this.Ok(categories);
        }

        public IHttpActionResult GetOptions(Guid parentID)
        {
            var categories = repo.GetOptions(parentID.ToID());
            return this.Ok(categories);
        }

        [HttpPost]
        public IHttpActionResult TagItem(Guid itemID)
        {
            var tagIDs = HttpContext.Current.Request.Form["tagIDs"];

            var item = repo.TagItem(itemID.ToID(), tagIDs.Split('|').Select(ID.Parse));
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}