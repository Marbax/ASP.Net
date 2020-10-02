using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers.WebAPI
{
    public class ProductGridController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        public IEnumerable<Product> GetProducts()
        {
            return repository.GetAll();
        }
        public Product GetProducts(int id)
        {
            return repository.Get(id);
        }
        public dynamic GetProducts(string sidx, string sord, int page, int rows)
        {
            var products = repository.GetAll();
            var pageIndex = Convert.ToInt32(page) - 1;
            var pageSize = rows;
            var totalRecords = products.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            //products = products.Skip(pageIndex * pageSize).Take(pageSize);
            switch (sidx)
            {
                case "Name":
                    products = sord.ToUpper() == "DESC"
                      ? products.OrderByDescending(s => s.Name)
                      : products.OrderBy(s => s.Name);
                    break;
                case "Id":
                    products = sord.ToUpper() == "DESC"
                      ? products.OrderByDescending(s => s.Id)
                      : products.OrderBy(s => s.Id);
                    break;
                case "Category":
                    products = sord.ToUpper() == "DESC"
                      ? products.OrderByDescending(s => s.Category)
                      : products.OrderBy(s => s.Category);
                    break;
                case "Price":
                    products = sord.ToUpper() == "DESC"
                      ? products.OrderByDescending(s => s.Price)
                      : products.OrderBy(s => s.Price);
                    break;
            }
            products = products.Skip(pageIndex * pageSize).Take(pageSize);
            return new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from product in products
                    select new
                    {
                        i = product.Id.ToString(),
                        cell = new string[] {
                         product.Id.ToString(),
                         product.Name,
                         product.Category,
                         product.Price.ToString(CultureInfo.InvariantCulture)
                      }
                    }).ToArray()
            };
        }

        //add new product, HTTP POST request is used:?
        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage PutProduct(int id, Product item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            else
                return Request.CreateResponse<Product>(HttpStatusCode.OK, item);
        }

        public HttpResponseMessage DeleteProduct(int id)
        {
            Product p = repository.Get(id);
            if (p == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                repository.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, p);
                //return new HttpResponseMessage(HttpStatusCode.OK);
            }

        }
    }
}
