using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HayamiAPI.Library;
using HayamiAPI.Models;
using System.Diagnostics;

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private Context db = new Context();

        // GET: api/Customers
        public HttpResponseMessage GetCustomers()
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            return Request.CreateResponse(HttpStatusCode.OK, db.Customers);

        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public HttpResponseMessage GetCustomer(int id)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            Customer customer = db.Customers.Find(id);
            if (customer == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());
            return Request.CreateResponse(HttpStatusCode.OK, customer);

        }

        // PUT: api/Customers/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCustomer(int id, Customer customer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != customer.CustomerID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(customer).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CustomerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Customers
        [HttpPost, Route("new")]
        public HttpResponseMessage PostCustomer(Customer customer)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var newCustomer = new Customer()
            {
                CustCode = customer.CustCode,
                CustName = customer.CustName,
                CustAddr = customer.CustAddr,
                CustCity = customer.CustCity,
                CustPosCode = customer.CustPosCode,
                CustPhone = customer.CustPhone,
                CustEmail = customer.CustEmail,
                CustExp = customer.CustExp,
                CustDesc = customer.CustDesc,
                CounterID = customer.CounterID,
            };

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Customers/5
        //[ResponseType(typeof(Customer))]
        //public IHttpActionResult DeleteCustomer(int id)
        //{
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Customers.Remove(customer);
        //    db.SaveChanges();

        //    return Ok(customer);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}