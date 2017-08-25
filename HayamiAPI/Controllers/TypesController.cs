﻿using System;
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
    [RoutePrefix("api/types")]
    public class TypesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Types
        public IQueryable<Models.Type> GetTypes()
        {
            return db.Types;
        }

        // GET: api/Types/5
        [ResponseType(typeof(Models.Type))]
        public IHttpActionResult GetType(int id)
        {
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        // PUT: api/Types/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutType(int id, Models.Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != type.TypeID)
            {
                return BadRequest();
            }

            db.Entry(type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // POST: api/Types
        [HttpPost, Route("new")]
        public HttpResponseMessage PostType(Models.Type type)
        {
            var newType = new Models.Type()
            {
                TypeName = type.TypeName,
                TypePrice = type.TypePrice,
                CreatedAt = DateTime.Today,
                UpdDate = DateTime.Today
            };

            db.Types.Add(newType);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Types/5
        //[ResponseType(typeof(Models.Type))]
        //public IHttpActionResult DeleteType(int id)
        //{
        //    Models.Type type = db.Types.Find(id);
        //    if (type == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Types.Remove(type);
        //    db.SaveChanges();

        //    return Ok(type);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeExists(int id)
        {
            return db.Types.Count(e => e.TypeID == id) > 0;
        }
    }
}