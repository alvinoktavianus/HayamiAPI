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
    public class StoragesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Storages
        public IQueryable<Storage> GetStorages()
        {
            return db.Storages;
        }

        // GET: api/Storages/5
        [ResponseType(typeof(Storage))]
        public IHttpActionResult GetStorage(int id)
        {
            Storage storage = db.Storages.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Storages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStorage(int id, Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storage.StorageID)
            {
                return BadRequest();
            }

            db.Entry(storage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExists(id))
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

        // POST: api/Storages
        [ResponseType(typeof(Storage))]
        public IHttpActionResult PostStorage(Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Storages.Add(storage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = storage.StorageID }, storage);
        }

        // DELETE: api/Storages/5
        [ResponseType(typeof(Storage))]
        public IHttpActionResult DeleteStorage(int id)
        {
            Storage storage = db.Storages.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            db.Storages.Remove(storage);
            db.SaveChanges();

            return Ok(storage);
        }

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