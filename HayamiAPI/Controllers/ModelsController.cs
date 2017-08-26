using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HayamiAPI.Models;
using HayamiAPI.Library;

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/models")]
    public class ModelsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Models
        public HttpResponseMessage GetModels()
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            return Request.CreateResponse(HttpStatusCode.OK, db.Models);
        }

        // GET: api/Models/5
        [ResponseType(typeof(Model))]
        public HttpResponseMessage GetModel(int id)
        {
            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return  Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            Model model = db.Models.Find(id);
            if (model == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());
            return Request.CreateResponse(HttpStatusCode.NotFound, model);
        }

        // PUT: api/Models/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModel(int id, Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.ModelID)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

        // POST: api/Models/new
        [HttpPost, Route("new")]
        public HttpResponseMessage New(Model model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            Model newModel = new Model()
            {
                ModelName = model.ModelName,
                CreatedAt = DateTime.Today,
                UpdDate = DateTime.Today
            };
            db.Models.Add(newModel);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE: api/Models/5
        //[ResponseType(typeof(Model))]
        //public IHttpActionResult DeleteModel(int id)
        //{
        //    Model model = db.Models.Find(id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Models.Remove(model);
        //    db.SaveChanges();

        //    return Ok(model);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelExists(int id)
        {
            return db.Models.Count(e => e.ModelID == id) > 0;
        }
    }
}