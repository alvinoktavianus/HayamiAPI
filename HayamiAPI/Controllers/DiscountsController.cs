using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    [RoutePrefix("api/discounts")]
    public class DiscountsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Discounts
        public HttpResponseMessage GetDiscounts()
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            return Request.CreateResponse(HttpStatusCode.OK, db.Discounts);
        }

        // GET: api/Discounts/5
        [ResponseType(typeof(Discount))]
        public HttpResponseMessage GetDiscount(int id)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            Discount discount = db.Discounts.Find(id);
            if (discount == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());
            return Request.CreateResponse(HttpStatusCode.OK, discount);
            
        }

        // PUT: api/Discounts/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutDiscount(int id, Discount discount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != discount.DiscountID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(discount).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DiscountExists(id))
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

        // POST: api/Discounts
        [HttpPost, Route("new")]
        public HttpResponseMessage PostDiscount(Discount discount)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var newDiscount = new Discount()
            {
                DiscDivide = discount.DiscDivide
            };

            db.Discounts.Add(discount);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Discounts/5
        //[ResponseType(typeof(Discount))]
        //public IHttpActionResult DeleteDiscount(int id)
        //{
        //    Discount discount = db.Discounts.Find(id);
        //    if (discount == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Discounts.Remove(discount);
        //    db.SaveChanges();

        //    return Ok(discount);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscountExists(int id)
        {
            return db.Discounts.Count(e => e.DiscountID == id) > 0;
        }
    }
}