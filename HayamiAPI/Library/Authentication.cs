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

        public static ResponseMessage CreateResponseMessage(string code, string description)
        {
            return new ResponseMessage()
            {
                Code = code,
                Description = description
            };
        }

        public static ResponseMessage CreateForbiddenResponseMessage()
        {
            return new ResponseMessage()
            {
                Code = "FORBIDDEN",
                Description = "Go, Away!"
            };
        }

        public static ResponseMessage CreateNotFoundResponseMessage()
        {
            return new ResponseMessage()
            {
                Code = "NOT_FOUND",
                Description = "The data you are looking for are not found in our database"
            };
        }
    }
}