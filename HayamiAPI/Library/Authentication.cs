using HayamiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HayamiAPI.Library
{
    public class Authentication
    {
        public const string TOKEN_KEYWORD = "TOKEN";

        public static bool IsAuthenticated(string accessToken)
        {
            using (var context = new Context())
            {
                User user = context.Users.FirstOrDefault(u => u.UserToken == accessToken);
                if (user != null) return false;
                return true;
            }
        }
    }
}