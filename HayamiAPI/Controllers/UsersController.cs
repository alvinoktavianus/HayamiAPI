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
using CryptSharp;

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private Context db = new Context();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: Register
        [HttpPost, Route("register")]
        public HttpResponseMessage Register(User user)
        {
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
            string email = user.UserEmail.Trim();
            string password = user.UserPassword.Trim();

            User userData = db.Users.FirstOrDefault(u => u.UserEmail == email);
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
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

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