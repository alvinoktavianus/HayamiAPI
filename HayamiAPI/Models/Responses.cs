namespace HayamiAPI.Models
{
    public class Responses
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public static Responses CreateResponseMessage(string code, string description)
        {
            return new Responses()
            {
                Code = code,
                Description = description
            };
        }

        public static Responses CreateForbiddenResponseMessage()
        {
            return new Responses()
            {
                Code = "FORBIDDEN",
                Description = "Go, Away!"
            };
        }

        public static Responses CreateNotFoundResponseMessage()
        {
            return new Responses()
            {
                Code = "NOT_FOUND",
                Description = "The data you are looking for are not found in our database"
            };
        }
    }
}