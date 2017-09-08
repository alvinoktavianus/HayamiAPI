using HayamiAPI.Library;
using HayamiAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
            
            return Request.CreateResponse(HttpStatusCode.OK, db.TransactionHds);
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
                    TotalPrice = trdtls.TotalPrice,
                    Qty = trdtls.Qty,
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
                FgStatus = "O",
                TotalDiscount = transactionHd.TotalDiscount,
                TotalPrice = transactionHd.TotalPrice,
                CreatedAt = DateTime.Now,
                UpdDate = DateTime.Now,
                TransactionDts = transactionDtData
            };

            db.TransactionHds.Add(newTransactionData);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

    }
}
