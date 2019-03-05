using DevxOdata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevxOdata.Controllers.WebApii
{
    public class CustomerController : ApiController
    {
  
        public IHttpActionResult GetAll()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return Ok(new
            {
                success = true,
                data = db.Customers.OrderBy(x=>x.Name).ToList()
            });
        }
    }
}
