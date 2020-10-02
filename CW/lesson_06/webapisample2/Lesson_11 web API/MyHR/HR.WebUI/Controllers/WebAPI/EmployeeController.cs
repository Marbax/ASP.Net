using HR.DAL.BizLayer;
using HR.DAL.DbLayer;
using HR.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.WebUI.Controllers.WebAPI
{
    public class EmployeeController : ApiController
    {
        //HrContext context = new HrContext();
        IGenericRepository<BizEmployee> BizEmployeeRep = new BizEmployeeRepository();
        public IEnumerable<BizEmployee> GetAll()
        {
            return BizEmployeeRep.GetAll();
        }
        public HttpResponseMessage Get(int id)
        {
            var emp = BizEmployeeRep.Get(id);
            if (emp == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");

            return Request.CreateResponse(HttpStatusCode.OK, emp);
        }

        public HttpResponseMessage Post([FromBody]BizEmployee emp)
        {
            BizEmployeeRep.AddOrUpdate(emp);
            var msg = Request.CreateResponse(HttpStatusCode.Created, emp);
            string url = Url.Link("DefaultApi", new { id = emp.EmployeeId });
            msg.Headers.Location = new Uri(url);
            return msg;
        }

    }
}
