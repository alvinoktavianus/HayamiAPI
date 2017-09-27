using HayamiAPI.Library;
using HayamiAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Diagnostics;

namespace HayamiAPI.Controllers
{
    [RoutePrefix("api/returns")]
    public class ReturnsController : ApiController
    {
        private Context db = new Context();

        // GET: api/returns
        public HttpResponseMessage GetTransactionReturns()
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var transReturn = db.TransactionReturHds
                .Include(trh => trh.TransactionReturDts)
                .OrderByDescending(trh => trh.CreatedAt)
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, transReturn);
        }

        [ResponseType(typeof(TransactionReturHd))]
        public HttpResponseMessage GetSingleTransactionReturn(int id)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            TransactionReturHd transactionReturHd = db.TransactionReturHds.Find(id);
            if (transactionReturHd == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());

            transactionReturHd.TransactionReturDts = db.TransactionReturDts.Where(s => s.TransReturHdID == transactionReturHd.TransReturHdID).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, transactionReturHd);
        }

        [HttpPost, Route("new")]
        public HttpResponseMessage CreateNewReturn(TransactionReturHd transactionReturHd)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            List<TransactionReturDt> transReturnDt = new List<TransactionReturDt>();

            foreach (var trReturDt in transactionReturHd.TransactionReturDts)
            {
                TransactionReturDt transactionReturDt = new TransactionReturDt
                {
                    ProductHdID = trReturDt.ProductHdID,
                    ProductSize = trReturDt.ProductSize,
                    ReturQty = trReturDt.ReturQty,
                    ReturType = trReturDt.ReturType,
                    CreatedAt = DateTime.Now,
                    UpdDate = DateTime.Now
                };
                transReturnDt.Add(transactionReturDt);
            }

            var newReturn = new TransactionReturHd
            {
                TransReturNo = Generator.GenerateReturnNumber(),
                ReturStatus = "O",
                ReturDesc = transactionReturHd.ReturDesc,
                CreatedAt = DateTime.Now,
                UpdDate = DateTime.Now,
                ActionDate = DateTime.Now,
                TransactionReturDts = transReturnDt
            };

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (transactionReturHd.CounterID != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                newReturn.CounterID = transactionReturHd.CounterID;
            }

            db.TransactionReturHds.Add(newReturn);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
