using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HayamiAPI;
using HayamiAPI.Models;

namespace HayamiAPI.Controllers
{
    public class ProductDtsController : ApiController
    {
        private Context db = new Context();

        // GET: api/ProductDts
        public IQueryable<ProductDt> GetProductDts()
        {
            return db.ProductDts;
        }

        // GET: api/ProductDts/5
        [ResponseType(typeof(ProductDt))]
        public IHttpActionResult GetProductDt(int id)
        {
            ProductDt productDt = db.ProductDts.Find(id);
            if (productDt == null)
            {
                return NotFound();
            }

            return Ok(productDt);
        }

        // PUT: api/ProductDts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductDt(int id, ProductDt productDt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productDt.ProductDtID)
            {
                return BadRequest();
            }

            db.Entry(productDt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDtExists(id))
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

        // POST: api/ProductDts
        [ResponseType(typeof(ProductDt))]
        public IHttpActionResult PostProductDt(ProductDt productDt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductDts.Add(productDt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productDt.ProductDtID }, productDt);
        }

        // DELETE: api/ProductDts/5
        [ResponseType(typeof(ProductDt))]
        public IHttpActionResult DeleteProductDt(int id)
        {
            ProductDt productDt = db.ProductDts.Find(id);
            if (productDt == null)
            {
                return NotFound();
            }

            db.ProductDts.Remove(productDt);
            db.SaveChanges();

            return Ok(productDt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductDtExists(int id)
        {
            return db.ProductDts.Count(e => e.ProductDtID == id) > 0;
        }
    }
}