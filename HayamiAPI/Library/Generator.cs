using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HayamiAPI.Library
{
    public class Generator
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string GenerateInvoiceNumber()
        {
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = Enumerable.Range(0, 5).Select(x => input[random.Next(0, input.Length)]);
            return "INVC/" + DateTime.Now.ToString("yyyyMMdd") + "/" + new string(chars.ToArray());
        }
    }
}