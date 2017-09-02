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
    [RoutePrefix("api/storages")]
    public class StoragesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Storages
        public HttpResponseMessage GetStorages()
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            return Request.CreateResponse(HttpStatusCode.OK, db.Storages);

        }

        // GET: api/Storages/5
        [ResponseType(typeof(Storage))]
        public HttpResponseMessage GetStorage(int id)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            Storage storage = db.Storages.Find(id);
            if (storage == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());
            return Request.CreateResponse(HttpStatusCode.OK, storage);
        }

        // PUT: api/Storages/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutStorage(int id, Storage storage)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != storage.StorageID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(storage).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StorageExists(id))
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

        // POST: api/Storages
        [HttpPost, Route("new")]
        public HttpResponseMessage PostStorage(Storage storage)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var newStorage = new Storage()
            {
                StorageName = storage.StorageName,
                StorageCapacity = storage.StorageCapacity,
                StorageStock = storage.StorageStock,
                StoragePrior = storage.StoragePrior,
                CreatedAt = DateTime.Today,
                UpdDate = DateTime.Today
            };
            
            db.Storages.Add(newStorage);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Storages/5
        //[ResponseType(typeof(Storage))]
        //public IHttpActionResult DeleteStorage(int id)
        //{
        //    Storage storage = db.Storages.Find(id);
        //    if (storage == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Storages.Remove(storage);
        //    db.SaveChanges();

        //    return Ok(storage);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StorageExists(int id)
        {
            return db.Storages.Count(e => e.StorageID == id) > 0;
        }
    }
}