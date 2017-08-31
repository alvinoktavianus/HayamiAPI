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
    [RoutePrefix("api/products")]
    public class ProductHdsController : ApiController
    {
        private Context db = new Context();

        // GET: api/ProductHds
        public HttpResponseMessage GetProductHds()
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            return Request.CreateResponse(HttpStatusCode.OK, db.ProductHds);
        }

        // GET: api/ProductHds/5
        [ResponseType(typeof(ProductHd))]
        public HttpResponseMessage GetProductHd(int id)
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            ProductHd productHd = db.ProductHds.Find(id);
            if (productHd == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());
            return Request.CreateResponse(HttpStatusCode.OK, productHd);
        }

        // PUT: api/ProductHds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductHd(int id, ProductHd productHd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productHd.ProductHdID)
            {
                return BadRequest();
            }

            db.Entry(productHd).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductHdExists(id))
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

        // POST: api/ProductHds
        [HttpPost, Route("new")]
        public HttpResponseMessage PostProductHd(ProductHd productHd)
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var newCounter = new ProductHd()
            {
                ProductCode = productHd.ProductCode,
                ProductName = productHd.ProductName,
                ProductDesc = productHd.ProductDesc,
                TypeID = productHd.TypeID,
                ModelID = productHd.ModelID,
                CreatedAt = DateTime.Today,
                UpdDate = DateTime.Today
            };

            db.ProductHds.Add(productHd);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/ProductHds/5
        [ResponseType(typeof(ProductHd))]
        public IHttpActionResult DeleteProductHd(int id)
        {
            ProductHd productHd = db.ProductHds.Find(id);
            if (productHd == null)
            {
                return NotFound();
            }

            db.ProductHds.Remove(productHd);
            db.SaveChanges();

            return Ok(productHd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductHdExists(int id)
        {
            return db.ProductHds.Count(e => e.ProductHdID == id) > 0;
        }
    }
}