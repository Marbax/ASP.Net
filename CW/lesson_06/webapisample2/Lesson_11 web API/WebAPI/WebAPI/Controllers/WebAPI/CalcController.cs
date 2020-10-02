using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers.WebAPI
{
    public class CalcController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Add(int a, int b)
        {
            return Request.CreateResponse(HttpStatusCode.OK, a + b);
        }
        [HttpGet]
        public HttpResponseMessage Sub(int a, int b)
        {
            return Request.CreateResponse(HttpStatusCode.OK, a - b);
        }
    }
}
