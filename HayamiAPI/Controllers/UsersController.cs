﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HayamiAPI.Models;
using HayamiAPI.Library;
using CryptSharp;
using System.Diagnostics;
using HayamiAPI.CustomRequest;
using System.Data.Entity;

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

        [HttpPut, Route("update")]
        public HttpResponseMessage UpdateProfile(ChangePassword changePassword)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            User oldUser = Authentication.GetCurrentUser(accessToken);
            if (!Crypter.CheckPassword(changePassword.OldPassword, oldUser.UserPassword)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            User newUser = new User()
            {
                UserPassword = Crypter.Blowfish.Crypt(changePassword.NewPassword.Trim())
            };
            db.Entry(newUser).State = EntityState.Modified;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted);
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