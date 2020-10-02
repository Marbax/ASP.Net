using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers.WebAPI
{
    public class FruitsController : ApiController
    {
        static List<string> fruits = new List<string>
        {
            "Apple", "Banana", "Orange", "Cocos"
        };

        public IEnumerable<string> GetAll()
        {
            return fruits;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id < fruits.Count)
                return Request.CreateResponse(HttpStatusCode.OK, fruits[id]);
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
        }

        public HttpResponseMessage Post([FromBody]string fruit)
        {
            fruits.Add(fruit);

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, fruit);
            // Location заголовок стоит создавать, если новый элемент был создан
           
            msg.Headers.Location = new Uri(Request.RequestUri + "/" + (fruits.Count - 1));

            return msg;
        }
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            if (id > fruits.Count)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                fruits[id] = value;
                return Request.CreateResponse(HttpStatusCode.OK, fruits[id]);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            if (id > fruits.Count)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                string deleted = fruits[id];
                fruits.RemoveAt(id);
                return Request.CreateResponse(HttpStatusCode.OK, deleted);
            }
        }
    }
}
