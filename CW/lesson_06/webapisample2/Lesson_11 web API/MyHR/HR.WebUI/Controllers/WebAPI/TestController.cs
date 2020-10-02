using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.WebUI.Controllers.WebAPI
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "Test Web API";
        }
    }
}
