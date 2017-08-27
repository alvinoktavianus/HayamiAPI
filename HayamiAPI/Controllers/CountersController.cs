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

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/counters")]
    public class CountersController : ApiController
    {
        private Context db = new Context();

        // GET: api/Counters
        public HttpResponseMessage GetCounters()
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            return Request.CreateResponse(HttpStatusCode.OK, db.Counters);

        }

        // GET: api/Counters/5
        [ResponseType(typeof(Counter))]
        public HttpResponseMessage GetCounter(int id)
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            Counter counter = db.Counters.Find(id);
            if (counter == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());
            return Request.CreateResponse(HttpStatusCode.OK, counter);

        }

        // PUT: api/Counters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCounter(int id, Counter counter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != counter.CounterID)
            {
                return BadRequest();
            }

            db.Entry(counter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Counters
        [HttpPost, Route("new")]
        public HttpResponseMessage PostCounter(Counter counter)
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var newCounter = new Counter()
            {
                CounterName = counter.CounterName,
                CounterAddr = counter.CounterAddr,
                CounterCity = counter.CounterCity,
                CounterPosCode = counter.CounterPosCode,
                CounterPhone = counter.CounterPhone,
                CounterEmail = counter.CounterEmail,
                CreatedAt = DateTime.Today,
                UpdDate = DateTime.Today
            };
            
            db.Counters.Add(newCounter);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);

        }

        // DELETE: api/Counters/5
        //[ResponseType(typeof(Counter))]
        //public IHttpActionResult DeleteCounter(int id)
        //{
        //    Counter counter = db.Counters.Find(id);
        //    if (counter == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Counters.Remove(counter);
        //    db.SaveChanges();

        //    return Ok(counter);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CounterExists(int id)
        {
            return db.Counters.Count(e => e.CounterID == id) > 0;
        }
    }
}