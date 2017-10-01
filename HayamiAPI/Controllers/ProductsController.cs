using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HayamiAPI.Library;
using HayamiAPI.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private Context db = new Context();

        // GET: api/ProductHds
        public HttpResponseMessage GetProducts()
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var products = db.ProductHds
                .Include(ph => ph.ProductDts)
                .OrderByDescending(ph => ph.CreatedAt)
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        // GET: api/ProductHds/5
        [ResponseType(typeof(ProductHd))]
        public HttpResponseMessage GetProduct(int id)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            ProductHd productHd = db.ProductHds.Find(id);
            if (productHd == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());

            productHd.ProductDts = db.ProductDts.Where(s => s.ProductHdID == productHd.ProductHdID).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, productHd);
        }

        // PUT: api/ProductHds/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutProduct(int id, ProductHd productHd)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != productHd.ProductHdID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(productHd).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
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

        // POST: api/ProductHds
        [HttpPost, Route("new")]
        public HttpResponseMessage PostProduct(ProductHd productHd)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            List<ProductDt> productDetailsData = new List<ProductDt>();

            foreach (var pdts in productHd.ProductDts)
            {
                ProductDt productDt = new ProductDt
                {
                    ProductQty = pdts.ProductQty,
                    ProductSize = pdts.ProductSize,
                    CreatedAt = DateTime.Now,
                    UpdDate = DateTime.Now,
                    StorageID = pdts.StorageID
                };
                productDetailsData.Add(productDt);
            }

            var newProductHdData = new ProductHd()
            {
                ProductCode = productHd.ProductCode,
                ProductName = productHd.ProductName,
                ProductDesc = productHd.ProductDesc,
                TypeID = productHd.TypeID,
                ModelID = productHd.ModelID,
                ProductDts = productDetailsData,
                ImagePath1 = productHd.ImagePath1,
                ImagePath2 = productHd.ImagePath2,
                ImagePath3 = productHd.ImagePath3,
                ImagePath4 = productHd.ImagePath4,
                ImagePath5 = productHd.ImagePath5,
                CreatedAt = DateTime.Now,
                UpdDate = DateTime.Now
            };

            db.ProductHds.Add(newProductHdData);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/ProductHds/5
        //[ResponseType(typeof(ProductHd))]
        //public IHttpActionResult DeleteProduct(int id)
        //{
        //    ProductHd productHd = db.ProductHds.Find(id);
        //    if (productHd == null)
        //    {
        //        return NotFound();
        //    }

        //    db.ProductHds.Remove(productHd);
        //    db.SaveChanges();

        //    return Ok(productHd);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.ProductHds.Count(e => e.ProductHdID == id) > 0;
        }
    }
}