namespace SitecorePrototype.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Description;
    using SitecoreModels.Products;
    using SitecoreRepository;

    [AllowAnonymous]
    public class ProductController : ApiController
    {
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetAll()
        {
            var repo = new ProductRepository();

            try
            {
                var products = repo.GetAll();
                return this.Ok(products);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}