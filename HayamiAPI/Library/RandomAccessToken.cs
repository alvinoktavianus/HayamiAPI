using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HayamiAPI.Library
{
    public class RandomAccessToken
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string GenerateToken(int Size)
        {
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, Size).Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }
    }
}