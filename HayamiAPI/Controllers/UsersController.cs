using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HayamiAPI.Models;
using HayamiAPI.Library;
using CryptSharp;
using System.Diagnostics;

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private Context db = new Context();

        // PUT: api/Users/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUser(int id, User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.UserID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
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

        // POST: Register
        [HttpPost, Route("register")]
        public HttpResponseMessage Register(User user)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            User userData = new User()
            {
                UserName = user.UserName.Trim(),
                UserPassword = Crypter.Blowfish.Crypt(user.UserPassword.Trim()),
                UserToken = RandomAccessToken.GenerateToken(20),
                UserEmail = user.UserEmail,
                CreatedAt = DateTime.Today,
                UpdDate = DateTime.Today
            };

            db.Users.Add(userData);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // POST: Login
        [HttpPost, Route("login")]
        public IHttpActionResult Login(User user)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            string username = user.UserName.Trim();
            string password = user.UserPassword.Trim();

            User userData = db.Users.FirstOrDefault(u => u.UserName == username);
            if (userData != null && Crypter.CheckPassword(password, userData.UserPassword))
            {
                return Ok(userData);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}