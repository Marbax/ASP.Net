using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers.WebAPI
{
    public class ProductController : ApiController
    {
        IProductRepository ProductRep;
        public ProductController()
        {
            ProductRep = new ProductRepository();
        }
        public IEnumerable<Product> GetAll()
        {
            return ProductRep.GetAll();
        }

        public HttpResponseMessage Get(int id)
        {
            var product = ProductRep.Get(id);
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "NotFound");
            else
                return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        public HttpResponseMessage Post(Product product)
        {
            Product p= ProductRep.Add(product);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, p);
            string url = Url.Link("DefaultApi", new { id = p.Id });

            // Location заголовок стоит создавать, если новый элемент был создан
            msg.Headers.Location = new Uri(url);

            return msg;
        }

    }
}
