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
    [RoutePrefix("api/transactions")]
    public class TransactionsController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        public HttpResponseMessage GetAllTransactions()
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            var transactions = db.TransactionHds
                .Include(t => t.TransactionDts)
                .OrderByDescending(tr => tr.TransDate)
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, transactions);
        }

        [ResponseType(typeof(TransactionHd))]
        public HttpResponseMessage GetSingleTransaction(int id)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            TransactionHd transactionHd = db.TransactionHds.Find(id);
            if (transactionHd == null) return Request.CreateResponse(HttpStatusCode.NotFound, Responses.CreateNotFoundResponseMessage());

            transactionHd.TransactionDts = db.TransactionDts.Where(s => s.TransHdID == transactionHd.TransHdID).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, transactionHd);
        }

        [HttpPost, Route("new")]
        public HttpResponseMessage CreateNewTransactions(TransactionHd transactionHd)
        {
            db.Database.Log = (message) => Debug.WriteLine(message);

            var token = Request.Headers;
            if (!token.Contains(Authentication.TOKEN_KEYWORD)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());
            string accessToken = Request.Headers.GetValues(Authentication.TOKEN_KEYWORD).FirstOrDefault();
            if (Authentication.IsAuthenticated(accessToken)) return Request.CreateResponse(HttpStatusCode.Forbidden, Responses.CreateForbiddenResponseMessage());

            List<TransactionDt> transactionDtData = new List<TransactionDt>();

            foreach (var trdtls in transactionHd.TransactionDts)
            {
                TransactionDt transactionDt = new TransactionDt()
                {
                    ProductHdID = trdtls.ProductHdID,
                    ProductSize = trdtls.ProductSize,
                    TotalPrice = trdtls.TotalPrice,
                    Qty = trdtls.Qty,
                    QtyOri = trdtls.QtyOri,
                    ReceiveQty = trdtls.ReceiveQty,
                    AddDiscountType = trdtls.AddDiscountType,
                    AddDiscountValue = trdtls.AddDiscountValue,
                    AddDiscountDesc = trdtls.AddDiscountDesc,
                    DiscountID = trdtls.DiscountID,
                    CreatedAt = DateTime.Now,
                    UpdDate = DateTime.Now,
                    ActionDate = DateTime.Now
                };
                transactionDtData.Add(transactionDt);
            }

            var newTransactionData = new TransactionHd()
            {
                TransNo = Generator.GenerateInvoiceNumber(),
                TransDate = DateTime.Now,
                CounterID = transactionHd.CounterID,
                CustomerID = transactionHd.CustomerID,
                FgStatus = "O",
                TotalDiscount = transactionHd.TotalDiscount,
                TotalPrice = transactionHd.TotalPrice,
                CreatedAt = DateTime.Now,
                UpdDate = DateTime.Now,
                TransactionDts = transactionDtData
            };

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (transactionHd.CounterID != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                newTransactionData.CounterID = transactionHd.CounterID;
            }
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            else if (transactionHd.CustomerID != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                newTransactionData.CustomerID = transactionHd.CustomerID;
            }

            db.TransactionHds.Add(newTransactionData);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPut]
        public HttpResponseMessage UpdateTransactionById(int id)
        {

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}
